using System;
using System.Runtime.InteropServices;

namespace NDceRpc.Microsoft.Interop
{
    [StructLayout(LayoutKind.Sequential)]
    public struct RPC_IF_ID
    {
        Guid Uuid;
        ushort VersMajor;
        ushort VersMinor;
    }
}