#define DceRpcIdl_API __declspec(dllexport) 

DceRpcIdl_API  void* __stdcall GetDummyServer();
DceRpcIdl_API void* __stdcall GetExplicitWithCallbacksServer();