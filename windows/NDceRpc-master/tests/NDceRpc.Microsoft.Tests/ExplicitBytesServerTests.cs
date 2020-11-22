

using System;
using NDceRpc.ExplicitBytes;
using NDceRpc.Microsoft.Interop;
using NUnit.Framework;

namespace NDceRpc.Test
{
    [TestFixture]
    public class ExplicitBytesServerTests
    {

        [Test]
        [ExpectedException((typeof(RpcException)))]
        public void TestServerWithDiblicateUuid()
        {
            Guid iid = Guid.NewGuid();

             ExplicitBytesServer server1 = new ExplicitBytesServer(iid);
            try
            {
               

                server1.AddProtocol(RpcProtseq.ncalrpc, "lrpctest1", 5);
                server1.AddAuthentication(RPC_C_AUTHN.RPC_C_AUTHN_WINNT);
                server1.StartListening();

                ExplicitBytesServer server2 = new ExplicitBytesServer(iid);

                server2.AddProtocol(RpcProtseq.ncalrpc, "lrpctest2", 5);
                server2.AddAuthentication(RPC_C_AUTHN.RPC_C_AUTHN_WINNT);
                server2.StartListening();
            }
            finally
            {
               server1.Dispose();
            }



        }

        [Test]
        public void TestUnregisterListener()
        {
            Guid iid = Guid.NewGuid();
            using (ExplicitBytesServer server = new ExplicitBytesServer(iid))
            {
                server.AddProtocol(RpcProtseq.ncalrpc, "lrpctest", 5);
                server.AddAuthentication(RPC_C_AUTHN.RPC_C_AUTHN_WINNT);
                server.StartListening();
                RpcExecuteHandler handler =
                    delegate(IRpcCallInfo client, byte[] arg)
                    { return arg; };
                var endpointBinding = new EndpointBindingInfo(RpcProtseq.ncalrpc, null, "lrpctest");
                using (ExplicitBytesClient client = new ExplicitBytesClient(iid, endpointBinding))
                {
                    client.AuthenticateAs(null, ExplicitBytesClient.Self, RPC_C_AUTHN_LEVEL.RPC_C_AUTHN_LEVEL_PKT_PRIVACY, RPC_C_AUTHN.RPC_C_AUTHN_WINNT);

                    server.OnExecute += handler;
                    client.Execute(new byte[0]);

                    server.OnExecute -= handler;
                    try
                    {
                        client.Execute(new byte[0]);
                        Assert.Fail();
                    }
                    catch (RpcException)
                    { }
                }
            }
        }

        [Test]
        [ExpectedException(typeof(OverflowException))]
        [Ignore]
        public void TestInfiniteRecursiveCalls()
        {
            Guid iid = Guid.NewGuid();
            using (ExplicitBytesServer server = new ExplicitBytesServer(iid))
            {
                server.AddProtocol(RpcProtseq.ncalrpc, "lrpctest", 5);
                server.AddAuthentication(RPC_C_AUTHN.RPC_C_AUTHN_WINNT);
                server.StartListening();
                RpcExecuteHandler handler =
                    delegate(IRpcCallInfo client, byte[] arg)
                    {
                        using (ExplicitBytesClient innerClient = new ExplicitBytesClient(iid,new EndpointBindingInfo(RpcProtseq.ncalrpc, null, "lrpctest")))
                        {
                            innerClient.AuthenticateAs(null, ExplicitBytesClient.Self, RPC_C_AUTHN_LEVEL.RPC_C_AUTHN_LEVEL_PKT_PRIVACY, RPC_C_AUTHN.RPC_C_AUTHN_WINNT);
                            return innerClient.Execute(new byte[0]);
                        }

                    };
                server.OnExecute += handler;
                using (ExplicitBytesClient client = new ExplicitBytesClient(iid, new EndpointBindingInfo(RpcProtseq.ncalrpc, null, "lrpctest")))
                {
                    client.AuthenticateAs(null, ExplicitBytesClient.Self, RPC_C_AUTHN_LEVEL.RPC_C_AUTHN_LEVEL_PKT_PRIVACY, RPC_C_AUTHN.RPC_C_AUTHN_WINNT);
                    client.Execute(new byte[0]);
                }
            }
        }

        [Test]
        public void LongName()
        {
            var address = "\\pipe\\127.0.0.1\\1\\test.test\\testLongNameLongNameLongNameLongNameLongNamed0286a6-0b9b-4db1-8659-b715e5db5b3bd0286a6-0b9b-4db1-8659-b715e5db5b3b";
            Guid iid = Guid.NewGuid();
            using (ExplicitBytesServer server = new ExplicitBytesServer(iid))
            {
                server.AddProtocol(RpcProtseq.ncacn_np, address, 5);
                server.AddAuthentication(RPC_C_AUTHN.RPC_C_AUTHN_WINNT);
                server.StartListening();
                RpcExecuteHandler handler =
                    delegate(IRpcCallInfo client, byte[] arg)
                        {
                            return arg;
                        };
                server.OnExecute += handler;
                using (ExplicitBytesClient client = new ExplicitBytesClient(iid, new EndpointBindingInfo(RpcProtseq.ncacn_np, null, address)))
                {
                    client.AuthenticateAs(null, ExplicitBytesClient.Self, RPC_C_AUTHN_LEVEL.RPC_C_AUTHN_LEVEL_PKT_PRIVACY, RPC_C_AUTHN.RPC_C_AUTHN_WINNT);
                    client.Execute(new byte[0]);
                }
            }
        }

        [Test]
        public void TestOneRecursiveCall()
        {
            Guid iid = Guid.NewGuid();
            bool wasCalled = false;
            using (ExplicitBytesServer server = new ExplicitBytesServer(iid))
            {
                server.AddProtocol(RpcProtseq.ncalrpc, "lrpctest", 5);
                server.AddAuthentication(RPC_C_AUTHN.RPC_C_AUTHN_WINNT);
                server.StartListening();
                RpcExecuteHandler handler =
                    delegate(IRpcCallInfo client, byte[] arg)
                    {
                        if (!wasCalled)
                        {
                            wasCalled = true;
                            using (ExplicitBytesClient innerClient = new ExplicitBytesClient(iid, new EndpointBindingInfo(RpcProtseq.ncalrpc, null, "lrpctest")))
                            {
                                innerClient.AuthenticateAs(null, ExplicitBytesClient.Self, RPC_C_AUTHN_LEVEL.RPC_C_AUTHN_LEVEL_PKT_PRIVACY, RPC_C_AUTHN.RPC_C_AUTHN_WINNT);
                                return innerClient.Execute(new byte[0]);
                            }
                        }
                        return new byte[0];
                    };
                server.OnExecute += handler;
                using (ExplicitBytesClient client = new ExplicitBytesClient(iid, new EndpointBindingInfo(RpcProtseq.ncalrpc, null, "lrpctest")))
                {
                    client.AuthenticateAs(null, ExplicitBytesClient.Self, RPC_C_AUTHN_LEVEL.RPC_C_AUTHN_LEVEL_PKT_PRIVACY, RPC_C_AUTHN.RPC_C_AUTHN_WINNT);
                    client.Execute(new byte[0]);
                }
            }
        }

    }
}
