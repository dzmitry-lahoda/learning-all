using System;

namespace NDceRpc.Microsoft.Interop
{
    [System.Runtime.InteropServices.UnmanagedFunctionPointerAttribute(System.Runtime.InteropServices.CallingConvention.StdCall)]
    public delegate void RPC_DISPATCH_FUNCTION(ref IntPtr Message);
}