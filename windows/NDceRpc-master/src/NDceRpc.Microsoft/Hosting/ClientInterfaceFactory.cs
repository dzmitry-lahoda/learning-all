using System;
using System.Runtime.InteropServices;
using NDceRpc.Microsoft.Interop;

namespace NDceRpc.ExplicitBytes
{
    public static class ClientInterfaceFactory
    {




        public static RPC_CLIENT_INTERFACE CreatExplicitBytesClient(Guid iid)
        {
            var client = new RPC_CLIENT_INTERFACE();
            client.Length = (uint)Marshal.SizeOf(typeof(RPC_CLIENT_INTERFACE));
            client.InterfaceId = new RPC_SYNTAX_IDENTIFIER() { SyntaxGUID = iid, SyntaxVersion = ExplicitBytesConstants.INTERFACE_VERSION };
            client.TransferSyntax = new RPC_SYNTAX_IDENTIFIER() { SyntaxGUID = SYNTAX.SYNTAX_IID, SyntaxVersion = SYNTAX.SYNTAX_VERSION };
            client.DispatchTable = IntPtr.Zero;
            client.RpcProtseqEndpointCount = 0u;
            client.RpcProtseqEndpoint = IntPtr.Zero;
            client.Reserved = IntPtr.Zero;
            client.InterpreterInfo = IntPtr.Zero;
            client.Flags = 0u;
            return client;
        }
    }
}