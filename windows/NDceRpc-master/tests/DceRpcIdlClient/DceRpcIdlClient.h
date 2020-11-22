// The following ifdef block is the standard way of creating macros which make exporting 
// from a DLL simpler. All files within this DLL are compiled with the DCERPCIDLCLIENT_EXPORTS
// symbol defined on the command line. This symbol should not be defined on any project
// that uses this DLL. This way any other project whose source files include this file see 
// DCERPCIDLCLIENT_API functions as being imported from a DLL, whereas this DLL sees symbols
// defined with this macro as being exported.
#ifdef DCERPCIDLCLIENT_EXPORTS
#define DCERPCIDLCLIENT_API __declspec(dllexport)
#else
#define DCERPCIDLCLIENT_API __declspec(dllimport)
#endif

DCERPCIDLCLIENT_API  void* STDAPICALLTYPE GetDummyClient();
DCERPCIDLCLIENT_API void STDAPICALLTYPE CallDummyServer(void* bindingHandle);

DCERPCIDLCLIENT_API void  STDAPICALLTYPE CallExplicitWithCallbacksServer(void* bindingHandle);

DCERPCIDLCLIENT_API void __stdcall CallErrorHandlingServer(void* binding);
DCERPCIDLCLIENT_API void* __stdcall GetErrorHandlingClient();
DCERPCIDLCLIENT_API void __stdcall CallErrorThrowingServer(void* binding);
