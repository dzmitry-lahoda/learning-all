#include "Dummy_h.h";
#include <RpcAsync.h>
#include "Exports.h"
#include "ExplicitWithCallbacks_h.h"
#include <string.h>
#include <wchar.h>
#include <tchar.h>
#include <WinBase.h>
#include <stdio.h>


//TODO: wrap collections of events/bindings/async handles/threads to provide easy callbacks
HANDLE g_workerThread;
HANDLE g_callbackEvent;

//simulates web  requests
DWORD WINAPI WebRequest(PVOID pvParam) {
	while(true){
	  Sleep(1000);
	  SetEvent(g_callbackEvent);
	}
   return(0);
}


/* [async] */ void  AsyncAttach( 
    /* [in] */ //PRPC_ASYNC_STATE AsyncAttach_AsyncHandle,
    /* [in] */ handle_t hBinding,
    /* [string][in] */ wchar_t *szOutput){
		auto 
 wprintf(szOutput);
		
	 DWORD dwThreadID = -1;
		g_callbackEvent = CreateEvent(NULL,false,false,NULL);
		g_workerThread = CreateThread(NULL, 0, WebRequest, NULL, 0,&dwThreadID);
		while (true){
			WaitForSingleObject(g_callbackEvent, INFINITE);
		    wchar_t* msg = L"Hello from server!";
		    CallClient(msg);
		}
		Sleep(INFINITE);
}

void Request( 
    /* [in] */ handle_t hBinding,
    /* [string][in] */ wchar_t *szOutput){
wprintf(szOutput);
printf("\n");
}

DceRpcIdl_API void* __stdcall GetDummyServer(){
	return Dummy_v0_1_s_ifspec;
}


DceRpcIdl_API void* __stdcall GetExplicitWithCallbacksServer(){
	return ExplicitWithCallbacks_v0_1_s_ifspec;
}

void* __RPC_USER midl_user_allocate(size_t size)
{
   return malloc(size);
}


void __RPC_USER midl_user_free(void* p)
{
   free(p);
}



void Do(handle_t IDL_handle){

}