using System;
using System.Runtime.InteropServices;

namespace NDceRpc.Microsoft.Interop
{
    [StructLayout(LayoutKind.Sequential)]
    public struct MIDL_SERVER_INFO
    {
        public IntPtr /* PMIDL_STUB_DESC */ pStubDesc;
        public IntPtr /* SERVER_ROUTINE* */ DispatchTable;
        public IntPtr /* PFORMAT_STRING */ ProcString;
        public IntPtr /* unsigned short* */ FmtStringOffset;
        public IntPtr /* STUB_THUNK * */ ThunkTable;
        public IntPtr /* PRPC_SYNTAX_IDENTIFIER */ pTransferSyntax;
        public IntPtr /* ULONG_PTR */ nCount;
        public IntPtr /* PMIDL_SYNTAX_INFO */ pSyntaxInfo;
    }
}