// NativeClient.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include <windows.h>
#include <stdio.h>
#include <assert.h>
#include <string>
#include "Stopwatch.h"
#include <objects.h>
#include <Messages.pb.h>
#include <windows.h>
#include <windowsx.h>
#include <Rpc.h>
#include "..\Example1Explicit\Example1Explicit.h"


using namespace std;
using namespace reqresp;
VOID RegisterDispatcherWindow();
typedef void (*req_builder)(unsigned int size,unsigned char** dataP,unsigned int* sizeP);

typedef void (*resp_builder)(unsigned int size,unsigned char* resp_data);

typedef void (*makeRequest)(unsigned int req_size,req_builder b, resp_builder br,void** result,int* resultSize);
void makeSharedMemoryRequest(unsigned int req_size,req_builder b,resp_builder br,void** result,int* resultSize);
void makePipeRequest(unsigned int req_size,req_builder b,resp_builder br,void** result,int* resultSize);
void makeRpc(unsigned int req_size,req_builder b,resp_builder br,void** result,int* resultSize);
void makeWindowsMessageRequest(unsigned int req_size,req_builder b,resp_builder br,void** result,int* resultSize);

char* name = "cool data";
char* id = "very very cool id";
char* type = "very very cool type";

const int kb = 1024;
int req_size100 = 100*kb;
int req_size50 = 50*kb;
int req_size10 = 10*kb;
int req_size1 = 1*kb;
int req_size0_1 = 0.1*kb;

std::map<int,custom_request> req;

void init_reqs(int req_size,int ids,int types){
	custom_request sc;
	sc.m_name = std::string(name);
	for (int i = 0;i<ids;i++){
		sc.m_ids.insert(sc.m_ids.end(),id);
	}
	for (int i = 0;i<types;i++){	
		sc.m_types.insert(sc.m_types.end(),type);
	}

	req.insert(make_pair(req_size,sc));
}



handle_t rpcTransport = NULL;

void init_rpc(){
	RPC_STATUS status;
	unsigned char* szStringBinding = NULL;

	// Creates a string binding handle.
	// This function is nothing more than a printf.
	// Connection is not done here.

	status = RpcStringBindingComposeA(
		NULL, // UUID to bind to.
		reinterpret_cast<unsigned char*>("ncalrpc"),                                                      
		NULL,                                                     
		reinterpret_cast<unsigned char*>("FastDataServer"), 
		
		//reinterpret_cast<unsigned char*>("ncacn_np"),                                                      
		//NULL,                                                     
		//reinterpret_cast<unsigned char*>("\\pipe\\FastDataServer"),         

		//NOTE: TCP is slower then Named Pipes
		//reinterpret_cast<unsigned char*>("ncacn_ip_tcp"), // Use TCP/IP protocol.
		//   reinterpret_cast<unsigned char*>("localhost"), // TCP/IP network address to use.
		//   reinterpret_cast<unsigned char*>("4747"), // TCP/IP port to use.

		NULL, // Protocol dependent network options to use.
		&szStringBinding); // String binding output.

	assert (status == S_OK);
	// Validates the format of the string binding handle and converts
	// it to a binding handle.
	// Connection is not done here either.
	status = RpcBindingFromStringBindingA(
		szStringBinding, // The string binding to validate.
		&rpcTransport); // Put the result in the explicit
	// binding handle.
	assert (status == S_OK);
	assert (rpcTransport != NULL);
}

void req_obj(unsigned int size,unsigned char** rdataP,unsigned int* rmessageSizeP)
{	
	auto r = req[size];
	unsigned int rmessageSize = r.getSize();
	unsigned char* rdata = (unsigned char*)malloc(rmessageSize);
	r.toArray(rdata,rmessageSize);
	*rdataP = rdata;
	*rmessageSizeP = rmessageSize;
}

void req_msg(unsigned int size,unsigned char** rdataP,unsigned int* rmessageSizeP)
{	
	auto cr = req[size];
	request r;
	r.set_name(cr.m_name);

	for (auto it=cr.m_types.begin(); it!=cr.m_types.end(); it++)
	{
		std::string s = *it;
		r.add_types(s);
	}
	for (auto it= cr.m_ids.begin(); it!=cr.m_ids.end();it++){
		std::string s = *it;
		r.add_ids(s);
	}
	unsigned int rmessageSize = r.ByteSize();
	unsigned char* rdata = (unsigned char*)malloc(rmessageSize);
	r.SerializeToArray(rdata,rmessageSize);
	*rdataP = rdata;
	*rmessageSizeP = rmessageSize;
}

void resp_bytes(unsigned int size,unsigned char* resp_data){
	//do nothing
}

void resp_msg(unsigned int size,unsigned char* resp_data){
	response r;
	r.ParseFromArray(resp_data,size);
	//NOTE: we have to translate it to custom object in order to have more real mesurment
	//NOTE: and to satisfie public API
	custom_response cr;
	for (auto it = r.data().begin();it!=r.data().end();it++){
		cr.m_data.insert(cr.m_data.end(),*it);
	}
}

void resp_obj(unsigned int size,unsigned char* resp_data){
	custom_response r;
	int readS = 0;
	r.fromArray(resp_data,&readS);

}

void req_bytes(unsigned int size,unsigned char** rdataP,unsigned int* rmessageSizeP)
{	
	unsigned int rmessageSize = size;
	unsigned char* req_data = (unsigned char*)malloc(rmessageSize);
	char* req_msg = (char*)req_data;
	memcpy_s(req_msg,50,  "Hello from client!!!",50);
	*rdataP = req_data;
	*rmessageSizeP = rmessageSize;
}

// inits shared memory buffer
void init_mem(int size);
void init_mem_open();

bool oneway = false;
bool reuse = false;

const wchar_t* pipe_name = TEXT("\\\\.\\pipe\\FastDataServer");


//TODO: dispose resource on client disconnect
int _tmain(int argc, _TCHAR* argv[])
{
	//TODO: use some lib for command line
	req_builder builder = req_msg;
	resp_builder br = resp_msg;
	makeRequest call = makeSharedMemoryRequest;
	for (int i = 0; i < argc; i++){
		std::wstring  s = argv[i];
		if (s == TEXT("-m")) 
		{
			std::wstring  s = argv[i+1];
			if (s == TEXT("pipes"))
			{
				call = makePipeRequest;
			}
			else if (s == TEXT("messaging"))
			{
				call = makeWindowsMessageRequest;
			}
			else if(s == TEXT("rpc")){
				call = makeRpc;
			}
			else{
				call = makeSharedMemoryRequest;
			}

		}
		if (s == TEXT("-d")) 
		{
			std::wstring  s = argv[i+1];
			builder = s == TEXT("bytes") ? req_bytes : (s == TEXT("object") ? req_obj : req_msg);
			br = s == TEXT("bytes") ? resp_bytes : (s == TEXT("object") ? resp_obj : resp_msg);
		}
		if (s == TEXT("-r")) 
		{
			reuse = true;
		}
		if (s == TEXT("-o")) 
		{
			oneway = true;
		}
	}

	if (reuse) {
		if (call == makeSharedMemoryRequest){
			init_mem(3000*kb);
			init_mem_open();
		}
		if (call== makeWindowsMessageRequest) 
			RegisterDispatcherWindow();
		if (call == makePipeRequest)
			while ( !WaitNamedPipe(pipe_name, 1)); 
		if (call == makeRpc)
			init_rpc(); 
	}



	if (builder != req_bytes)
	{
		if (!oneway){
			init_reqs(req_size100,5000,10);
			init_reqs(req_size50,2500,10);
			init_reqs(req_size10,500,10);
			init_reqs(req_size1,50,10);
			init_reqs(req_size0_1,5,10);
		}
		else{
			init_reqs(req_size100,50000,10);
			init_reqs(req_size50,25000,10);
			init_reqs(req_size10,5000,10);
			init_reqs(req_size1,500,10);
			init_reqs(req_size0_1,50,10);
		}
	}
	if (oneway){
		req_size100 = req_size100*10;;
		req_size50 = req_size50*10;
		req_size10 = req_size10*10;
		req_size1 = req_size1*10;
		req_size0_1 = req_size0_1*10;
	}
	Stopwatch sw;
	sw.set_mode(REAL_TIME);
	sw.lang = StopwatchReportLang::MARKDOWN;

	//TODO: clear response after each request
	void* response = NULL;
	int resp_size = -1;
	unsigned char* req = NULL;
	cout << "Client started"<< endl;
	unsigned int real_size = 0;


	char* msr100 = "Client process requests ~100kb and gets ~1000kb response of server process";
	char* msr50 = "Client process requests ~50kb and gets ~500kb response of server process";
	char* msr10 = "Client process requests ~10kb and gets ~100kb response  of server process";
	char* msr1 = "Client process requests ~1kb and gets ~10kb response  of server process";
	char* msr0_1 = "Client process requests ~0.1kb and gets ~1kb response  of server process";
	if (oneway)
	{
		msr100 = "Client process requests ~1000kb";
		msr50 = "Client process requests ~500kb";
		msr10 = "Client process requests ~100kb";
		msr1 = "Client process requests ~10kb";
		msr0_1 = "Client process requests ~1kb";
	}


	builder(req_size100,&req,&real_size);
	cout << "Request size :" << real_size/kb << " kb" << endl;
	for (int i=0;i<5;i++){

		sw.start(msr100);
		call(req_size100,builder,br,&response,&resp_size);
		sw.stop(msr100);
	}	
	cout << "Response size was :" << resp_size/kb << " kb" <<endl;


	builder(req_size50,&req,&real_size);
	cout << "Request size :" << real_size/kb << " kb" << endl;
	for (int i=0;i<10;i++){

		sw.start(msr50);
		call(req_size50,builder,br,&response,&resp_size);
		sw.stop(msr50);
	}	
	cout << "Response size was :" << resp_size/kb << " kb" <<endl;

	builder(req_size10,&req,&real_size);
	cout << "Message size :" << real_size/kb << " kb" << endl;
	for (int i=0;i<50;i++){

		sw.start(msr10);
		call(req_size10,builder,br,&response,&resp_size);
		sw.stop(msr10);
	}
	cout << "Response size was :" << resp_size/kb << " kb" <<endl;

	builder(req_size1,&req,&real_size);
	cout << "Message size :" << real_size << " bytes" << endl;
	for (int i=0;i<500;i++){
		//Sleep(1);
		//BUG: sometimes hangs here when pipes used....
		sw.start(msr1);
		call(req_size1,builder,br,&response,&resp_size);
		sw.stop(msr1);
	}
	cout << "Response size was :" << resp_size/kb << " kb" <<endl;


	builder(req_size0_1,&req,&real_size);
	cout << "Message size :" << real_size << " bytes" << endl;
	for (int i=0;i<1000;i++){//BUG: server of shared memory crashes if make number of 10000
		//Sleep(1);
		//BUG: sometimes hangs here when pipes used....
		sw.start(msr0_1);
		call(req_size0_1,builder,br,&response,&resp_size);
		sw.stop(msr0_1);
	}
	cout << "Response size was :" << resp_size << " bytes" <<endl;


	builder(req_size100,&req,&real_size);
	cout << "Request size :" << real_size/kb << " kb" << endl;
	for (int i=0;i<5;i++){

		sw.start(msr100);
		call(req_size100,builder,br,&response,&resp_size);
		sw.stop(msr100);
	}	
	cout << "Response size was :" << resp_size/kb << " kb" <<endl;


	builder(req_size50,&req,&real_size);
	cout << "Request size :" << real_size/kb << " kb" << endl;
	for (int i=0;i<10;i++){

		sw.start(msr50);
		call(req_size50,builder,br,&response,&resp_size);
		sw.stop(msr50);
	}	
	cout << "Response size was :" << resp_size/kb << " kb" <<endl;

	builder(req_size10,&req,&real_size);
	cout << "Message size :" << real_size/kb << " kb" << endl;
	for (int i=0;i<50;i++){

		sw.start(msr10);
		call(req_size10,builder,br,&response,&resp_size);
		sw.stop(msr10);
	}
	cout << "Response size was :" << resp_size/kb << " kb" <<endl;

	builder(req_size1,&req,&real_size);
	cout << "Message size :" << real_size << " bytes" << endl;
	for (int i=0;i<500;i++){
		//Sleep(1);
		//BUG: sometimes hangs here when pipes used....
		sw.start(msr1);
		call(req_size1,builder,br,&response,&resp_size);
		sw.stop(msr1);
	}
	cout << "Response size was :" << resp_size/kb << " kb" <<endl;


	builder(req_size0_1,&req,&real_size);
	cout << "Message size :" << real_size << " bytes" << endl;
	for (int i=0;i<1000;i++){//BUG: server of shared memory crashes if make number of 10000
		//Sleep(1);
		//BUG: sometimes hangs here when pipes used....
		sw.start(msr0_1);
		call(req_size0_1,builder,br,&response,&resp_size);
		sw.stop(msr0_1);
	}
	cout << "Response size was :" << resp_size << " bytes" <<endl;


	//TODO: make assertion of response

	sw.report(msr100);
	sw.report(msr50);
	sw.report(msr10);
	sw.report(msr1);
	sw.report(msr0_1);

	char wait(' ');
	cin >> &wait;	
	return 0;
}

HWND hTargetWnd ;
#define IDD_MAINDIALOG                  129
#define IDC_SENDMSG_BUTTON              1001
#define IDC_NUMBER_EDIT                 1002
#define IDC_MESSAGE_EDIT                1003
#define IDC_STATIC                      -1

int current_req_size = 0;
HWND  m_messageDispatcherWindow;
LRESULT CALLBACK ClientWindowProc(HWND hwnd, UINT uMsg, WPARAM wParam, LPARAM lParam);
DWORD WINAPI WinThreadProc(_In_  LPVOID lpParameter)
{
	WNDCLASS wc = { };
	HINSTANCE hInstance = GetModuleHandle(NULL);
	wc.lpfnWndProc   = ClientWindowProc;
	wc.hInstance     = hInstance;
	wc.lpszClassName = TEXT("Client Dispatcher Window Class");

	RegisterClass(&wc);

	m_messageDispatcherWindow = CreateWindowEx(
		0,
		TEXT("Client Dispatcher Window Class"),
		L"Client Dispatcher Window",
		WS_OVERLAPPEDWINDOW,
		CW_USEDEFAULT, CW_USEDEFAULT, CW_USEDEFAULT, CW_USEDEFAULT,
		NULL,
		NULL,
		hInstance,
		NULL);
	MSG msg = { };
	while (GetMessage(&msg, NULL, 0, 0))
	{
		TranslateMessage(&msg);
		DispatchMessage(&msg);
	}
	DestroyWindow(m_messageDispatcherWindow);
	return 0;
}

HANDLE m_thread;

VOID RegisterDispatcherWindow()
{
	// Register the window class.
	DWORD wt = 0;
	m_thread = CreateThread(NULL,NULL,WinThreadProc,NULL,0,&wt);
	hTargetWnd = FindWindow(NULL, L"CppReceiveWM_COPYDATA");

}
void OnCommand(HWND hWnd, int id, HWND hwndCtl, UINT codeNotify);
void OnClose(HWND hWnd);

LRESULT CALLBACK ClientWindowProc(HWND hwnd, UINT message, WPARAM wParam, LPARAM lParam)
{
	switch (message)
	{
		// Handle the WM_COMMAND message in OnCommand
		HANDLE_MSG (hwnd, WM_COMMAND, OnCommand);

		// Handle the WM_CLOSE message in OnClose
		HANDLE_MSG (hwnd, WM_CLOSE, OnClose);

	default:
		return true;
	}
	//return DefWindowProc(hwnd, message, wParam, lParam);
}





bool message_send = false;
void OnCommand(HWND hWnd, int id, HWND hwndCtl, UINT codeNotify)
{
	if (id == IDC_SENDMSG_BUTTON)
	{
		// Find the target window handle.
		if (!reuse || hTargetWnd == NULL) hTargetWnd = FindWindow(NULL, L"CppReceiveWM_COPYDATA");
		if (hTargetWnd == NULL)
		{
			MessageBox(hWnd, L"Unable to find the \"CppReceiveWM_COPYDATA\" window", 
				L"Error", MB_ICONERROR);
			return;
		}




		COPYDATASTRUCT cds;
		cds.cbData = current_req_size;
		cds.lpData = malloc(current_req_size*10);

		// Send the COPYDATASTRUCT struct through the WM_COPYDATA message to 
		// the receiving window. (The application must use SendMessage, 
		// instead of PostMessage to send WM_COPYDATA because the receiving 
		// application must accept while it is guaranteed to be valid.)
		SendMessage(hTargetWnd, WM_COPYDATA, reinterpret_cast<WPARAM>(hWnd), reinterpret_cast<LPARAM>(&cds));
		//SendMessage(hTargetWnd, WM_COPYDATA, reinterpret_cast<WPARAM>(hWnd), reinterpret_cast<LPARAM>(&cds));
		//SendMessage(hTargetWnd, WM_COPYDATA, reinterpret_cast<WPARAM>(hWnd), reinterpret_cast<LPARAM>(&cds));
		//SendMessage(hTargetWnd, WM_COPYDATA, reinterpret_cast<WPARAM>(hWnd), reinterpret_cast<LPARAM>(&cds));
		//SendMessage(hTargetWnd, WM_COPYDATA, reinterpret_cast<WPARAM>(hWnd), reinterpret_cast<LPARAM>(&cds));
		message_send = true;
		free(cds.lpData);
		DWORD dwError = GetLastError();
		if (dwError != NO_ERROR)
		{
			cout << "SendMessage(WM_COPYDATA)" << dwError << endl;
		}
	}
}


//
//   FUNCTION: OnClose(HWND)
//
//   PURPOSE: Process the WM_CLOSE message
//
void OnClose(HWND hWnd)
{
	EndDialog(hWnd, 0);
}


//
//  FUNCTION: DialogProc(HWND, UINT, WPARAM, LPARAM)
//
//  PURPOSE:  Processes messages for the main dialog.
//
INT_PTR CALLBACK DialogProc(HWND hWnd, UINT message, WPARAM wParam, LPARAM lParam)
{
	switch (message)
	{
		// Handle the WM_COMMAND message in OnCommand
		HANDLE_MSG (hWnd, WM_COMMAND, OnCommand);

		// Handle the WM_CLOSE message in OnClose
		HANDLE_MSG (hWnd, WM_CLOSE, OnClose);

	default:
		return FALSE;
	}
	return 0;
}


void makeWindowsMessageRequest(unsigned int req_size,req_builder b,resp_builder br,void** result,int* resultSize){
	current_req_size =  req_size;
	message_send = false;
	SendMessage(m_messageDispatcherWindow,WM_COMMAND,IDC_SENDMSG_BUTTON,req_size);
	// Find the target window handle.
	//if (!reuse || hTargetWnd == NULL) hTargetWnd = FindWindow(NULL, L"CppReceiveWM_COPYDATA");
	//if (hTargetWnd == NULL)
	//{
	//	cout << "Unable to find the \"CppReceiveWM_COPYDATA\" window Error" <<endl;
	//}

	//COPYDATASTRUCT cds;
	//cds.cbData = current_req_size;
	//cds.lpData = malloc(current_req_size*10);

	//// Send the COPYDATASTRUCT struct through the WM_COPYDATA message to 
	//// the receiving window. (The application must use SendMessage, 
	//// instead of PostMessage to send WM_COPYDATA because the receiving 
	//// application must accept while it is guaranteed to be valid.)

	//SendMessage(hTargetWnd, WM_COPYDATA, reinterpret_cast<WPARAM>(m_messageDispatcherWindow), reinterpret_cast<LPARAM>(&cds));
	////SendMessage(hTargetWnd, WM_COPYDATA, reinterpret_cast<WPARAM>(hWnd), reinterpret_cast<LPARAM>(&cds));
	////SendMessage(hTargetWnd, WM_COPYDATA, reinterpret_cast<WPARAM>(hWnd), reinterpret_cast<LPARAM>(&cds));
	////SendMessage(hTargetWnd, WM_COPYDATA, reinterpret_cast<WPARAM>(hWnd), reinterpret_cast<LPARAM>(&cds));
	////SendMessage(hTargetWnd, WM_COPYDATA, reinterpret_cast<WPARAM>(hWnd), reinterpret_cast<LPARAM>(&cds));
	//message_send = true;
	//free(cds.lpData);
	//DWORD dwError = GetLastError();
	//if (dwError != NO_ERROR)
	//{
	//	cout << "SendMessage(WM_COPYDATA)" << dwError << endl;
	//}
	//PostMessage(m_messageDispatcherWindow,WM_COMMAND,IDC_SENDMSG_BUTTON,req_size);
	//PostThreadMessage(GetThreadId(m_thread),IDC_SENDMSG_BUTTON,0,0);
	//while (!message_send){};//TODO: compare to wait handle
}

// Memory allocation function for RPC.
// The runtime uses these two functions for allocating/deallocating
// enough memory to pass the string to the server.
void* __RPC_USER midl_user_allocate(size_t size)
{
	return malloc(size);
}

// Memory deallocation function for RPC.
void __RPC_USER midl_user_free(void* p)
{
	free(p);
}

void makeRpc(unsigned int req_size,req_builder b,resp_builder br,void** result,int* resultSize){

	unsigned char* request_data = NULL;
	unsigned int rmessageSize = 0;
	b(req_size,&request_data,&rmessageSize);

	unsigned char* response = NULL;
	long responseSize = 0;
	Output(rpcTransport,rmessageSize,request_data,&responseSize,&response);

	br(responseSize,response);
	*result = response;
	*resultSize = responseSize;
	free(response);
}

HANDLE	hPipe = INVALID_HANDLE_VALUE;
void makePipeRequest(unsigned int req_size,req_builder b,resp_builder br,void** result,int* resultSize){
	if (!reuse)
		hPipe = INVALID_HANDLE_VALUE;	
	if (hPipe == INVALID_HANDLE_VALUE)
		while ( !WaitNamedPipe(pipe_name, 1)); 
	while (hPipe == INVALID_HANDLE_VALUE) 
	{ 

		hPipe = CreateFile( 
			pipe_name,   
			GENERIC_READ | 
			GENERIC_WRITE, 
			0,              
			NULL,         
			OPEN_EXISTING,  
			FILE_ATTRIBUTE_NORMAL,              
			NULL); 

		if (hPipe != INVALID_HANDLE_VALUE) 
			break; 
	}

	unsigned char* request_data = NULL;
	unsigned int rmessageSize = 0;
	b(req_size,&request_data,&rmessageSize);

	unsigned char* msg = (unsigned char*)malloc(sizeof(long)+rmessageSize);

	memcpy(msg,&rmessageSize,sizeof(long));
	unsigned char* rmsg = (unsigned char*)(msg+sizeof(long));
	memcpy(rmsg,request_data, rmessageSize);//NOTE: additional copy slows down pipes - can be avoided

	unsigned long cbWritten = 0;
	WriteFile(hPipe,msg,sizeof(long)+rmessageSize,&cbWritten,NULL);

	unsigned char* response = NULL;
	long responseSize = 0;
	if (!oneway){

		unsigned long	cbRead = 0;
		bool size_was_read = ReadFile(hPipe,&responseSize,sizeof(long),&cbRead,NULL);
		response = (unsigned char*) malloc(responseSize);
		//if (!rSizeRead) cout << cbRead << endl;
		//cout << cbRead << endl;
		//if (GetLastError() == ERROR_MORE_DATA) cout << "MORE DATA" << endl;
		bool was_read = ReadFile(hPipe,response,responseSize,&cbRead,NULL);
		*result = response;

		//cout << cbRead << endl;
		//if (!rRead) cout << cbRead << endl;
		//if (GetLastError() == ERROR_MORE_DATA) cout << "MORE DATA" << endl;
		//cout << rRead << endl;
	}
	else{
		unsigned long	cbRead = 0;
		bool got_data = false;
		bool was_read = ReadFile(hPipe,&got_data,sizeof(bool),&cbRead,NULL);
	}
	if (!reuse)
		CloseHandle(hPipe);
	if (!oneway){
		br(responseSize,response);
		*resultSize = responseSize;
	}
	//cout << response << endl;
CLEANUP:
	free(request_data);
}

void* rsizeMap;
void* rsizeMapView;
void* rdataMap;
void* rdataView;

void *dataMap;
void* sizeMap;
void init_mem_open(){
	sizeMap = OpenFileMapping(
		FILE_MAP_READ,         
		FALSE,                  
		TEXT("SizeOfData")           
		);
	dataMap = OpenFileMapping(
		FILE_MAP_READ,         
		FALSE,                  
		TEXT("ServerMessageData")           
		);

}

void init_mem(int rmessageSize){
	// mem for size of request data
	rsizeMap = CreateFileMapping(
		INVALID_HANDLE_VALUE,   
		NULL,                  
		PAGE_READWRITE,        
		0,                      
		sizeof(long),               
		TEXT("SizeOfDataRequest")
		);

	// mem for req data
	rdataMap = CreateFileMapping(
		INVALID_HANDLE_VALUE,   
		NULL,                  
		PAGE_READWRITE,        
		0,                      
		rmessageSize,               
		TEXT("ClientMessageData")
		);


}

void makeSharedMemoryRequest(unsigned int req_size,req_builder b,resp_builder br,void** result,int* resultSize){

	unsigned char* rdata = NULL;
	unsigned int rmessageSize = 0;
	b(req_size,&rdata,&rmessageSize);

	if (!reuse) init_mem(rmessageSize);
	rsizeMapView = MapViewOfFile(
		rsizeMap,
		FILE_MAP_WRITE,          
		0,                     
		0,           
		sizeof(long)
		);
	rdataView = MapViewOfFile(
		rdataMap,
		FILE_MAP_WRITE,          
		0,                     
		0,           
		rmessageSize
		);
	memcpy(rsizeMapView,&rmessageSize,sizeof(long));
	memcpy(rdataView,rdata,rmessageSize);


	auto madeRequest = CreateEvent(NULL,false,false,TEXT("ClientMadeRequestEvent"));
	SetEvent(madeRequest);
	// wait for server to read it
	auto readRequest = OpenEvent(SYNCHRONIZE,false,TEXT("ServerReadRequestEvent"));
	while (readRequest == NULL){
		readRequest = OpenEvent(SYNCHRONIZE,false,TEXT("ServerReadRequestEvent"));
	}
	WaitForSingleObject(readRequest,INFINITE);

	// read responce




	auto madeResponse = OpenEvent(SYNCHRONIZE,false,TEXT("ServerMadeResponse"));
	while (madeResponse == NULL){
		madeResponse = OpenEvent(SYNCHRONIZE,false,TEXT("ServerMadeResponse"));
	}
	WaitForSingleObject(madeResponse,INFINITE);

	if (!reuse || dataMap == NULL || sizeMap == NULL) init_mem_open();

	auto sizeMapView = MapViewOfFile(
		sizeMap,
		FILE_MAP_READ,          
		0,                     
		0,           
		sizeof(long)
		);

	auto packet = *(long*) sizeMapView;
	//cout << *packet<< endl;



	auto dataView = MapViewOfFile(
		dataMap,
		FILE_MAP_READ,          
		0,                     
		0,           
		packet
		);

	byte* data = (byte*)malloc(packet);
	char* msg = (char*)data;
	memcpy_s(msg,packet, dataView ,packet);
	auto readResponse = CreateEvent(NULL,true,false,TEXT("ClientReadResponseEvent"));
	SetEvent(readResponse);
	br(packet,data);
	*result = msg;
	*resultSize = packet;
}

