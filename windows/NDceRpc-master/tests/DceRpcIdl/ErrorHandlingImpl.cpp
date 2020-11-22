#include "ErrorHandlingImpl.h"
#include "ErrorHandling_h.h"

DceRpcIdl_API void* __stdcall GetErrorHandlingServer()
{
	return ErrorHandling_v0_1_s_ifspec;
}

/* [comm_status] */ error_status_t DoReturnErrors( 
    /* [in] */ handle_t hBinding,
    /* [fault_status][out] */ error_status_t *fault_s){
		*fault_s = RPC_S_INVALID_ARG;
		return RPC_E_SERVERCALL_REJECTED;
}

void DoThrowCppException( 
    /* [in] */ handle_t hBinding)
{
	throw  EXCEPTION_ARRAY_BOUNDS_EXCEEDED;
}
