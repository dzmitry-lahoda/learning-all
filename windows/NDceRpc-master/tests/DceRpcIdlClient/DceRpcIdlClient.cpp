// DceRpcIdlClient.cpp : Defines the exported functions for the DLL application.
//

#include "stdafx.h"
#include <stdio.h>
#include <assert.h>
#include <string.h>
#include <wchar.h>
#include <tchar.h>
#include <WinBase.h>
#include <stdlib.h>

#include "DceRpcIdlClient.h"

#include "..\DceRpcIdl\Dummy_h.h"
#include "..\DceRpcIdl\ErrorHandling_h.h"
#include "..\DceRpcIdl\ExplicitWithCallbacks_h.h"

DCERPCIDLCLIENT_API void STDAPICALLTYPE CallDummyServer(void* bindingHandle)
{
	Do(bindingHandle);
}

/* [callback] */ void CallClient( 
	/* [string][in] */ wchar_t *szOutput){
		wprintf(szOutput);
		printf("\n");
}

DWORD WINAPI StartListening(PVOID pvParam) {
   
	RPC_ASYNC_STATE Async;
	RPC_STATUS status;

	// Initialize the handle.
	status = RpcAsyncInitializeHandle(&Async, sizeof(RPC_ASYNC_STATE));
	if (status)
	{
		assert (status == RPC_S_OK);
	}

	Async.UserInfo = NULL;
	Async.NotificationType = RpcNotificationTypeEvent;

	Async.u.hEvent = CreateEvent(NULL, FALSE, FALSE, NULL);
	if (Async.u.hEvent == 0)
	{
		// Code to handle the error goes here.
	}

	
		
	AsyncAttach(
		//&Async,
		pvParam,_TEXT("Start waiting callback"));
   return S_OK;
}

HANDLE g_requestTread;

DCERPCIDLCLIENT_API  void STDAPICALLTYPE CallExplicitWithCallbacksServer(void* bindingHandle)
{
	 DWORD dwThreadID = -1;
	g_requestTread = CreateThread(NULL, 0, StartListening,bindingHandle, 0,&dwThreadID);
	Sleep(500);
	Request(bindingHandle,_TEXT("Hello from client!!!"));
	Sleep(500);
	Request(bindingHandle,_TEXT("Hello from client!!!"));
	Sleep(500);
	Request(bindingHandle,_TEXT("Hello from client!!!"));
	Sleep(500);
	Request(bindingHandle,_TEXT("Hello from client!!!"));
}

DCERPCIDLCLIENT_API  void* STDAPICALLTYPE GetDummyClient()
{
	return Dummy_v0_1_c_ifspec;
}

void* __RPC_USER midl_user_allocate(size_t size)
{
	return malloc(size);
}

DCERPCIDLCLIENT_API void __stdcall CallErrorHandlingServer(void* binding){
	error_status_t fault = RPC_S_OK;
    auto comm = DoReturnErrors(binding,&fault);
}

DCERPCIDLCLIENT_API void __stdcall CallErrorThrowingServer(void* binding){

	 DoThrowCppException(binding);
}

DCERPCIDLCLIENT_API void* __stdcall GetErrorHandlingClient(){
	return ErrorHandling_v0_1_c_ifspec;
}


void __RPC_USER midl_user_free(void* p)
{
	free(p);
}