using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using NDceRpc.Microsoft.Interop;
using NDceRpc.Native;
using NUnit.Framework;

namespace NDceRpc.Microsoft.HostOfNativeSample
{
    [TestFixture]
    public class HostingNativeTests
    {

#if X64
        private const string Implementer = "DceRpcIdl.x64.dll";
        private const string Consumer = "DceRpcIdlClient.x64.dll";
#else
        private const string Implementer = "DceRpcIdl.dll";
        private const string Consumer = "DceRpcIdlClient.dll";
#endif

        [DllImport(Implementer, CallingConvention = CallingConvention.StdCall)]
        static extern IntPtr GetDummyServer();
        [DllImport(Consumer, CallingConvention = CallingConvention.StdCall)]
        static extern IntPtr GetDummyClient();
        [DllImport(Consumer, CallingConvention = CallingConvention.StdCall)]
        static extern void CallDummyServer(IntPtr bindingHandle);


        [DllImport(Implementer, CallingConvention = CallingConvention.StdCall)]
        static extern IntPtr GetExplicitWithCallbacksServer();
        [DllImport(Consumer, CallingConvention = CallingConvention.StdCall)]
        static extern void CallExplicitWithCallbacksServer(IntPtr bindingHandle);

        [DllImport(Consumer, CallingConvention = CallingConvention.StdCall)]
        static extern void CallErrorHandlingServer(IntPtr bindingHandle);
        [DllImport(Consumer, CallingConvention = CallingConvention.StdCall)]
        static extern IntPtr GetErrorHandlingClient();
        [DllImport(Implementer, CallingConvention = CallingConvention.StdCall)]
        static extern IntPtr GetErrorHandlingServer();

        [DllImport(Consumer, CallingConvention = CallingConvention.StdCall)]
        static extern void CallErrorThrowingServer(IntPtr bindingHandle);

        

        [Test]
        public void TestErrorsNoServerListening()
        {
        
        
            var endPointName = Implementer + DateTime.Now.Ticks;

            var endpointBindingInfo = new EndpointBindingInfo(RpcProtseq.ncalrpc, null, endPointName);


            var client = new NativeClient(endpointBindingInfo);
            CallErrorHandlingServer(client.Binding);
        }


        [Test]
        public void TestErrors()
        {
            var server = GetErrorHandlingServer();

            var serverInterface = (RPC_SERVER_INTERFACE)Marshal.PtrToStructure(server, typeof(RPC_SERVER_INTERFACE));
            var endPointName = Implementer + DateTime.Now.Ticks + serverInterface.InterfaceId.SyntaxGUID.ToString("N");

            var endpointBindingInfo = new EndpointBindingInfo(RpcProtseq.ncalrpc, null, endPointName);

            NativeHost.StartServer(endpointBindingInfo, server);

            RPC_STATUS status;

            var client = new NativeClient(endpointBindingInfo);
            CallErrorHandlingServer(client.Binding);
        }

        [Test]
        public void TestCppExceptions()
        {
            var server = GetErrorHandlingServer();

            var serverInterface = (RPC_SERVER_INTERFACE)Marshal.PtrToStructure(server, typeof(RPC_SERVER_INTERFACE));
            var endPointName = Implementer + DateTime.Now.Ticks + serverInterface.InterfaceId.SyntaxGUID.ToString("N");

            var endpointBindingInfo = new EndpointBindingInfo(RpcProtseq.ncalrpc, null, endPointName);

            NativeHost.StartServer(endpointBindingInfo, server);

            RPC_STATUS status;

            var client = new NativeClient(endpointBindingInfo);
            CallErrorThrowingServer(client.Binding);
        }

        [Test]
        public void TestCallback()
        {
            var server = GetExplicitWithCallbacksServer();
            
            var endPointName =Implementer + DateTime.Now.Ticks;

            var serverInterface = (RPC_SERVER_INTERFACE)Marshal.PtrToStructure(server, typeof(RPC_SERVER_INTERFACE));

            var endpointBindingInfo = new EndpointBindingInfo(RpcProtseq.ncalrpc, null, endPointName);

            NativeHost.StartServer(endpointBindingInfo, server);



            RPC_STATUS status;


            var client = new NativeClient(endpointBindingInfo);
            CallExplicitWithCallbacksServer(client.Binding);
        }

        [Test]
        public void TestDummy()
        {
            var server = GetDummyServer();

            var serverInterface = (RPC_SERVER_INTERFACE)Marshal.PtrToStructure(server, typeof(RPC_SERVER_INTERFACE));
            var endPointName = Implementer + DateTime.Now.Ticks + serverInterface.InterfaceId.SyntaxGUID.ToString("N");
            var endpointBindingInfo = new EndpointBindingInfo(RpcProtseq.ncalrpc, null,endPointName);

            NativeHost.StartServer(endpointBindingInfo, server);

            var dummyClient = GetDummyClient();
            var clientInterface = (RPC_CLIENT_INTERFACE)Marshal.PtrToStructure(dummyClient, typeof(RPC_CLIENT_INTERFACE));

            RPC_STATUS status;


            var client = new NativeClient(endpointBindingInfo);
            CallDummyServer(client.Binding);
        }


        [Test]
        public void TestDummyInterceptor()
        {
            var server = GetDummyServer();

            var serverInterface = (RPC_SERVER_INTERFACE)Marshal.PtrToStructure(server, typeof(RPC_SERVER_INTERFACE));
            var realFunction = serverInterface.DispatchTableValue.Value.FirstDispatchFunction;
            RPC_DISPATCH_FUNCTION interceptor = (ref IntPtr ptr) =>
                {
                    realFunction(ref ptr);
                };
            serverInterface.DispatchTable = Marshal.GetFunctionPointerForDelegate(interceptor);
            //TODO: needs to constuct calls by hand because generated c file has static const to do interception
            //TODO: try to find RPC logic for this
        }

        
    }
}
