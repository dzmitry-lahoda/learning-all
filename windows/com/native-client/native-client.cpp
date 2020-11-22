// native-client.cpp : Defines the entry point for the console application.
//

#include <windows.h>
#include <objbase.h>
#include <stdio.h>
#include <stdio.h>
#include <tchar.h>
#include <process.h>
#include <stdlib.h>
#include <utility>

#include "NativeOutOfProcServer.h"

long __stdcall GetMsgProc(int nCode, WPARAM wParam, LPARAM lParam);




QueryInterfacePtr * old;

long STDMETHODCALLTYPE queryinterface1(INativeOutOfProcServer* self, 
                          REFIID riid, void **ppv)
{
   return old(self,riid,ppv);
}


typedef ULONG __stdcall releaseptr(IDispatch *self);

static releaseptr* oldd = NULL;

ULONG __stdcall release(IDispatch *self)
{  
	ULONG c = oldd(self);
	if (c == 1) 
		c = oldd(self);
    return 1;
}

long __stdcall typecount(IDispatch *self,UINT*u)
{   
    *u=1;
    return S_OK;
}

void Intercept(){
		CoInitialize(NULL);

	// interception
    CLSID clsid;
    CLSIDFromProgID(L"shell.application",&clsid);
    IDispatch *obj=NULL;
    CoCreateInstance(clsid,NULL,CLSCTX_INPROC_SERVER,__uuidof(IDispatch),(void**)&obj);
    void* iunknown_vtable= (void*)*((unsigned int*)obj);
    void* idispatch_vtable = (void*)(((unsigned int)iunknown_vtable)+8);
    unsigned int* v1 = (unsigned int*)idispatch_vtable;    
	oldd =  (releaseptr*)*v1;	
	DWORD old;
    VirtualProtect(v1,4,PAGE_EXECUTE_READWRITE,&old);
		//oldd = (releaseptr*)old;
    *v1 = (unsigned int) release;
	obj->Release();
}

long __stdcall WindowProc(HWND hwnd, UINT uMsg, WPARAM wParam, LPARAM lParam);
static HMODULE mod;

unsigned long mainThreadId =0;
static HHOOK cbtHookHandle = NULL;

//int WINAPI wWinMain(HINSTANCE hInstance, HINSTANCE, PWSTR pCmdLine, int nCmdShow)
int _tmain(int argc, _TCHAR* argv[])
{
	
	mainThreadId = GetCurrentThreadId();
    cbtHookHandle  = SetWindowsHookEx( WH_GETMESSAGE , GetMsgProc, NULL, mainThreadId); 

	const wchar_t* windowsClassName = _T("Native Server Main Window")  ;
		WNDCLASS wc = {};
		wc.lpfnWndProc = WindowProc;
		wc.lpszClassName = windowsClassName;
		mod = GetModuleHandle(NULL);
		wc.hInstance = mod;
	
		RegisterClass(&wc);

		// Create the window.

    HWND hwnd = CreateWindowEx(
        0,                              // Optional window styles.
        windowsClassName,                     // Window class
        L"Learn to Program Windows",    // Window text
        WS_OVERLAPPEDWINDOW,            // Window style

        // Size and position
        CW_USEDEFAULT, CW_USEDEFAULT, CW_USEDEFAULT, CW_USEDEFAULT,

        NULL,       // Parent window    
        NULL,       // Menu
        wc.hInstance,  // Instance handle
        NULL        // Additional application data
        );
	DWORD error = GetLastError();

    if (hwnd == NULL)
    {
        return 0;
    }

	ShowWindow(hwnd, SW_NORMAL);
	
    // Run the message loop.

    MSG msg = { };
    while (GetMessage(&msg, NULL, 0, 0))
    {
        TranslateMessage(&msg);
        DispatchMessage(&msg);
    }

    return 0;
	//Intercept();
	//int zp = sizeof(void *) ;
	//INativeOutOfProcServer* obj = (INativeOutOfProcServer*)GlobalAlloc(GMEM_FIXED,sizeof(INativeOutOfProcServer));
	//obj->count = 0;
	//obj->count++;

	//obj->vtbl = &InitNativeOutOfProcSrv();
	//obj->vtbl->SetState(obj,"init");
	//char state[10];
	//obj->vtbl->GetState(obj,state);
	//puts(state);
	//void* vtable = (void*)*((unsigned int*)obj);
	//void* iunknown_vtable= (void*)*((unsigned int*)vtable);
 //   void* queryinterface_vtable = (void*)((unsigned int)iunknown_vtable);

 //   // Get pointer of first emtry in IDispatch vtable (GetTypeInfoCount)
 //   unsigned int* v1 = (unsigned int*)queryinterface_vtable;

 //   // Change memory permissions so address can be overwritten
 //   DWORD old;
 //   VirtualProtect(v1,sizeof(  REFIID) + sizeof(void **) ,PAGE_EXECUTE_READWRITE,&old);

 //   // Override v-table pointer
 //   *v1 = (unsigned int) queryinterface1;

	//// Get vtable and offset in vtable for idispatch
	//
	////void* iunknown_vtable= (void*)*((unsigned int*)obj->vtbl);
 //   // There are three entries in IUnknown, therefore add 8 
 //  // unsigned int*  release_vtable = (unsigned int* )(((unsigned int)iunknown_vtable));
	////	 	 old =(ReleasePtr*)obj->vtbl->Release; 
	//// unsigned int*   release_vtable = (unsigned int*)obj->vtbl->Release;

	//// *release_vtable = (unsigned int)ReleaseNativeOutOfProcSrv1;
	//void* ppv = NULL;
	//obj->vtbl->QueryInterface(obj,IID_AsyncIAdviseSink,&ppv);

	return 0;
}
HWND button1; 
enum { ID_LABEL = 1,ID_BUTTON1,};

const unsigned int hookMessage = WM_USER+16633;
const unsigned int hookMessageDiffw = 132;
const unsigned int hookMessageDiffl = 788;

void SendDispatcherMessage (void* pvoid)
{
	PostThreadMessage(mainThreadId,hookMessage,hookMessageDiffw,hookMessageDiffl);

}

long __stdcall GetMsgProc(int nCode, WPARAM wParam, LPARAM lParam){
	if (nCode == -1) CallNextHookEx(cbtHookHandle, nCode, wParam, lParam);

	PMSG d = (PMSG)lParam;

	if (d->message == hookMessage && d->wParam == hookMessageDiffw && d->lParam == hookMessageDiffl){
		        PostQuitMessage(0);
		puts(":)");
	}

	return CallNextHookEx(cbtHookHandle, nCode, wParam, lParam);
}


long __stdcall WindowProc(HWND hwnd, UINT uMsg, WPARAM wParam, LPARAM lParam){
	    switch (uMsg)
    {
   case hookMessage:
			{
			   return 0;
			}
    case WM_CREATE:
		{
		button1 =  CreateWindow(_T("Button"),_T("1"),BS_PUSHBUTTON | WS_CHILD | WS_VISIBLE ,35,170,35,35,hwnd,(HMENU)ID_BUTTON1,mod,0);
		HRESULT err = GetLastError();
		return 0;
		}
    case WM_DESTROY:
        PostQuitMessage(0);
        return 0;
	//case WM_KEYDOWN:
	 
    case WM_PAINT:
        {
            PAINTSTRUCT ps;

            HDC hdc = BeginPaint(hwnd, &ps);
			HBRUSH brush = (HBRUSH)GetStockObject(BLACK_BRUSH);
            FillRect(hdc, &ps.rcPaint, brush);//(HBRUSH) (COLOR_WINDOW+1));

            EndPaint(hwnd, &ps);
        }
        return 0;
	case WM_COMMAND:
		switch(wParam){
		     case ID_BUTTON1:
				 _beginthread (SendDispatcherMessage, 0, NULL) ;
				// MessageBox(NULL,_T("Hello"),NULL,0);
		}


    }
    return DefWindowProc(hwnd, uMsg, wParam, lParam);
}