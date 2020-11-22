using System;

namespace NDceRpc.Microsoft.Interop
{
    [Flags]
    public enum RPC_CALL_ATTRIBUTES_FLAGS : int
    {
        RPC_QUERY_SERVER_PRINCIPAL_NAME = (0x02),
        RPC_QUERY_CLIENT_PRINCIPAL_NAME = (0x04),
        RPC_QUERY_CALL_LOCAL_ADDRESS = (0x08),
        RPC_QUERY_CLIENT_PID = (0x10),
        RPC_QUERY_IS_CLIENT_LOCAL = (0x20),
        RPC_QUERY_NO_AUTH_REQUIRED = (0x40),
    }
}