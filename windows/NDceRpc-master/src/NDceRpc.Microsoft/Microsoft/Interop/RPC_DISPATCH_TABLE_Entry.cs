using System;
using System.Runtime.InteropServices;

namespace NDceRpc.Microsoft.Interop
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct RPC_DISPATCH_TABLE_Entry
    {
        public IntPtr DispatchMethod;
        public IntPtr Zero;
    }
}