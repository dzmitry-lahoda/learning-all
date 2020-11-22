using System;
using System.Runtime.InteropServices;
using NDceRpc.Microsoft.Interop;

namespace NDceRpc.ExplicitBytes
{
    /// <summary>
    /// 
    /// </summary>
    public static class ServerInterfaceFactory
    {



        private static RPC_SERVER_INTERFACE CreateExplicitBytesServer(RpcHandle handle, Ptr<MIDL_SERVER_INFO> pServer, Guid iid)
        {
            var server = new RPC_SERVER_INTERFACE();
            server.Length = (uint)Marshal.SizeOf(typeof(RPC_SERVER_INTERFACE));
            server.InterfaceId = new RPC_SYNTAX_IDENTIFIER() { SyntaxGUID = iid, SyntaxVersion = ExplicitBytesConstants.INTERFACE_VERSION };
            server.TransferSyntax = new RPC_SYNTAX_IDENTIFIER() { SyntaxGUID = SYNTAX.SYNTAX_IID, SyntaxVersion = SYNTAX.SYNTAX_VERSION };

            var fnTable = new RPC_DISPATCH_TABLE();
            fnTable.DispatchTableCount = 1;
            fnTable.DispatchTable =
                handle.Pin(new RPC_DISPATCH_TABLE_Entry() { DispatchMethod = RpcRuntime.ServerEntry.Handle, Zero = IntPtr.Zero });
            fnTable.Reserved = IntPtr.Zero;

            server.DispatchTable = handle.Pin(fnTable);
            server.RpcProtseqEndpointCount = 0u;
            server.RpcProtseqEndpoint = IntPtr.Zero;
            server.DefaultManagerEpv = IntPtr.Zero;
            server.InterpreterInfo = pServer.Handle;
            server.Flags = 0x04000000u;
            return server;
        }

        public static Ptr<RPC_SERVER_INTERFACE> Create(RpcHandle handle, Guid iid, Byte[] formatTypes,
                                                      Byte[] formatProc,
                                                      ExplicitBytesExecute fnExecute)
        {
            Ptr<MIDL_SERVER_INFO> pServer = handle.CreatePtr(new MIDL_SERVER_INFO());

            MIDL_SERVER_INFO temp = new MIDL_SERVER_INFO();
            return Configure(temp,handle, pServer, iid, formatTypes, formatProc, fnExecute);
        }

        private static Ptr<RPC_SERVER_INTERFACE> Configure(MIDL_SERVER_INFO temp, RpcHandle handle, Ptr<MIDL_SERVER_INFO> me, Guid iid,
                                                    Byte[] formatTypes,
                                                    Byte[] formatProc, ExplicitBytesExecute fnExecute)
        {
            Ptr<RPC_SERVER_INTERFACE> svrIface = handle.CreatePtr(CreateExplicitBytesServer(handle, me, iid));
            Ptr<MIDL_STUB_DESC> stub = handle.CreatePtr(new MIDL_STUB_DESC(handle, svrIface.Handle, formatTypes, true));
            temp.pStubDesc = stub.Handle;

            IntPtr ptrFunction = handle.PinFunction(fnExecute);
            temp.DispatchTable = handle.Pin(ptrFunction);

            temp.ProcString = handle.Pin(formatProc);
            temp.FmtStringOffset = handle.Pin(new int[1] { 0 });

            temp.ThunkTable = IntPtr.Zero;
            temp.pTransferSyntax = IntPtr.Zero;
            temp.nCount = IntPtr.Zero;
            temp.pSyntaxInfo = IntPtr.Zero;

            //Copy us back into the pinned address
            Marshal.StructureToPtr( temp, me.Handle, false);
            return svrIface;
        }
    }
}