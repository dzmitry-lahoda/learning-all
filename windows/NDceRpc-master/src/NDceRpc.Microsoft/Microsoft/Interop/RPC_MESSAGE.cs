using System;
using System.Runtime.InteropServices;

namespace NDceRpc.Microsoft.Interop
{
    [StructLayout(LayoutKind.Sequential)]
    public struct RPC_MESSAGE
    {
        IntPtr Handle;
        uint DataRepresentation;
        IntPtr Buffer;
        ushort BufferLength;
        ushort ProcNum;
        IntPtr TransferSyntax;
        IntPtr RpcInterfaceInformation;
        IntPtr ReservedForRuntime;
        IntPtr ManagerEpv;
        IntPtr ImportContext;
        uint RpcFlags;
    }
}