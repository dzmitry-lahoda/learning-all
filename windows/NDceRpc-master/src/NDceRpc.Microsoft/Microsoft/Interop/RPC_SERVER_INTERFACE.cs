using System;
using System.Runtime.InteropServices;

namespace NDceRpc.Microsoft.Interop
{

    [StructLayout(LayoutKind.Sequential)]
    public struct RPC_SERVER_INTERFACE
    {
        public uint Length;
        public RPC_SYNTAX_IDENTIFIER InterfaceId;
        public RPC_SYNTAX_IDENTIFIER TransferSyntax;
        public IntPtr /*PRPC_DISPATCH_TABLE*/ DispatchTable;
        public uint RpcProtseqEndpointCount;
        public IntPtr /*PRPC_PROTSEQ_ENDPOINT*/ RpcProtseqEndpoint;
        public IntPtr DefaultManagerEpv;
        public IntPtr InterpreterInfo;
        public uint Flags;

        public RPC_DISPATCH_TABLE? DispatchTableValue
        {
            get
            {
                if (DispatchTable == IntPtr.Zero)
                {
                    return null;
                }
                var value = (RPC_DISPATCH_TABLE)Marshal.PtrToStructure(DispatchTable, typeof(RPC_DISPATCH_TABLE));
                return value;
            }
        }
    }
}