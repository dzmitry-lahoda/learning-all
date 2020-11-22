using System;

namespace NDceRpc.Microsoft.Interop
{

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="RpcDce.h"/>
    public static  class CONSTANTS
    {
        public const int RPC_C_BINDING_INFINITE_TIMEOUT = 10;
        public const int RPC_C_BINDING_MIN_TIMEOUT = 0;
        public const int RPC_C_BINDING_DEFAULT_TIMEOUT = 5;
        public const int RPC_C_BINDING_MAX_TIMEOUT = 9;
        public const int RPC_X_NO_MEMORY = RPC_S_OUT_OF_MEMORY;
        public const int RPC_S_OUT_OF_MEMORY = ERROR_OUTOFMEMORY;
        public const int ERROR_OUTOFMEMORY = 14;

        /// <summary>
        /// The stub received bad data.
        /// </summary>
        public const int RPC_X_BAD_STUB_DATA = 1783;
                           
        public const int RPC_C_CANCEL_INFINITE_TIMEOUT = -1;

        public const int RPC_C_LISTEN_MAX_CALLS_DEFAULT = 1234;
        public const int RPC_C_PROTSEQ_MAX_REQS_DEFAULT = 10;
#if !NET35
        public static  UIntPtr RPC_C_NO_CREDENTIALS = UIntPtr.Subtract(UIntPtr.Zero,1);
#endif
    }
}
