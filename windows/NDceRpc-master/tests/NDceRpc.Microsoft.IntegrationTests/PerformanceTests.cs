

using System;
using System.Diagnostics;
using System.Reflection;
using System.Threading.Tasks;
using NDceRpc.ExplicitBytes;
using NDceRpc.Microsoft.Interop;
using NUnit.Framework;

namespace NDceRpc.Test
{
    [TestFixture]
    public class TestPerformance
    {

        [Test]
        public void TestConcurentCreationOfServersAndClients2()
        {
            var callbackWasCalled = false;
            var serverId = Guid.NewGuid();
            var serverPipe = "\\pipe\\testserver" + MethodBase.GetCurrentMethod().Name;
            var callbackPipe = "\\pipe\\testcallback" + MethodBase.GetCurrentMethod().Name;
            var callbackId = Guid.NewGuid();
            var taskServer = new Task(() =>
                {
                    var server = new ExplicitBytesServer(serverId);
                    server.AddProtocol(RpcProtseq.ncacn_np, serverPipe, byte.MaxValue);
                    server.AddAuthentication(RPC_C_AUTHN.RPC_C_AUTHN_NONE);
                    server.StartListening();

                    server.OnExecute += (x, y) =>
                    {
                        var taskCallback = new Task(() =>
                        {
                            ExplicitBytesClient callbackClient = new ExplicitBytesClient(callbackId, new EndpointBindingInfo(RpcProtseq.ncacn_np, null,
                                                                           callbackPipe));
                            callbackClient.AuthenticateAs(null, ExplicitBytesClient.Self,
                                                          RPC_C_AUTHN_LEVEL.RPC_C_AUTHN_LEVEL_NONE,
                                                          RPC_C_AUTHN.RPC_C_AUTHN_NONE);
                            callbackClient.Execute(new byte[0]);
                        });
                        taskCallback.Start();
                        taskCallback.Wait();
                        return y;
                    };
                });
            taskServer.Start();
            taskServer.Wait();

            var taskClient = new Task(() =>
                {
                    var client = new ExplicitBytesClient(serverId, new EndpointBindingInfo(RpcProtseq.ncacn_np, null, serverPipe));
                    client.AuthenticateAs(null, ExplicitBytesClient.Self, RPC_C_AUTHN_LEVEL.RPC_C_AUTHN_LEVEL_NONE,
                                          RPC_C_AUTHN.RPC_C_AUTHN_NONE);
                    var callbackServer = new ExplicitBytesServer(callbackId);
                    callbackServer.AddProtocol(RpcProtseq.ncacn_np, callbackPipe, byte.MaxValue);
                    callbackServer.AddAuthentication(RPC_C_AUTHN.RPC_C_AUTHN_NONE);
                    callbackServer.OnExecute += (x, y) =>
                        {
                            callbackWasCalled = true;
                            return y;
                        };
                    client.Execute(new byte[0]);
                });
            taskClient.Start();
            taskClient.Wait();
            Assert.IsTrue(callbackWasCalled);


        }

        [Test]
        public void TestPerformanceWithLargePayloads()
        {
            Guid iid = Guid.NewGuid();
            using (ExplicitBytesServer server = new ExplicitBytesServer(iid))
            {
                server.AddProtocol(RpcProtseq.ncalrpc, "lrpctest", 5);
                server.AddAuthentication(RPC_C_AUTHN.RPC_C_AUTHN_WINNT);
                server.StartListening();
                server.OnExecute +=
                    delegate(IRpcCallInfo client, byte[] arg)
                    { return arg; };

                using (ExplicitBytesClient client = new ExplicitBytesClient(iid, new EndpointBindingInfo(RpcProtseq.ncalrpc, null, "lrpctest")))
                {
                    client.AuthenticateAs(null, ExplicitBytesClient.Self, RPC_C_AUTHN_LEVEL.RPC_C_AUTHN_LEVEL_PKT_PRIVACY, RPC_C_AUTHN.RPC_C_AUTHN_WINNT);
                    client.Execute(new byte[0]);

                    byte[] bytes = new byte[1 * 1024 * 1024]; //1mb in/out
                    new Random().NextBytes(bytes);

                    Stopwatch timer = new Stopwatch();
                    timer.Start();

                    for (int i = 0; i < 50; i++)
                        client.Execute(bytes);

                    timer.Stop();
                    Trace.WriteLine(timer.ElapsedMilliseconds.ToString(), "TestPerformanceWithLargePayloads");
                }
            }
        }

        [Test]
        public void PerformanceOnLocalRpcWithSmallPayloads()
        {
            var protoseq = RpcProtseq.ncalrpc;
            var endpointName = MethodBase.GetCurrentMethod().Name;

            TestPerformanceInternal(protoseq, endpointName, 77);
        }

        [Test]
        public void PerformanceOnNamedPipeWithSmallPayloads()
        {
            var protoseq = RpcProtseq.ncacn_np;
            var endpointName = @"\pipe\" + MethodBase.GetCurrentMethod().Name;
            TestPerformanceInternal(protoseq, endpointName, 77);
        }

        [Test]
        public void TestPerformanceOnLocalRpc()
        {
            var protoseq = RpcProtseq.ncalrpc;
            var endpointName = MethodBase.GetCurrentMethod().Name;

            TestPerformanceInternal(protoseq, endpointName,512);
        }

        [Test]
        public void TestPerformanceOnNamedPipe()
        {
            var protoseq = RpcProtseq.ncacn_np;
            var endpointName = @"\pipe\" + MethodBase.GetCurrentMethod().Name;
            TestPerformanceInternal(protoseq, endpointName, 512);
        }

        private static void TestPerformanceInternal(RpcProtseq protoseq, string endpointName,int payloadSize)
        {
            Guid iid = Guid.NewGuid();
            using (ExplicitBytesServer server = new ExplicitBytesServer(iid))
            {
                server.AddProtocol(protoseq, endpointName, 5);
                server.AddAuthentication(RPC_C_AUTHN.RPC_C_AUTHN_WINNT);
                server.StartListening();
                server.OnExecute +=
                    delegate(IRpcCallInfo client, byte[] arg) { return arg; };

                using (
                    ExplicitBytesClient client = new ExplicitBytesClient(iid,
                        new EndpointBindingInfo(protoseq, null, endpointName)))
                {
                    client.AuthenticateAs(null, ExplicitBytesClient.Self, RPC_C_AUTHN_LEVEL.RPC_C_AUTHN_LEVEL_PKT_PRIVACY,
                        RPC_C_AUTHN.RPC_C_AUTHN_WINNT);
                    client.Execute(new byte[0]);

                    byte[] bytes = new byte[payloadSize];
                    new Random().NextBytes(bytes);

                    Stopwatch timer = new Stopwatch();
                    timer.Start();

                    for (int i = 0; i < 5000; i++)
                        client.Execute(bytes);

                    timer.Stop();
                    Trace.WriteLine(timer.ElapsedMilliseconds.ToString(), endpointName);
                }
            }
        }

    

        [Test]
        public void TestPerformanceOnTcpip()
        {
            Guid iid = Guid.NewGuid();
            using (ExplicitBytesServer server = new ExplicitBytesServer(iid))
            {
                server.AddProtocol(RpcProtseq.ncacn_ip_tcp, @"18081", 5);
                server.AddAuthentication(RPC_C_AUTHN.RPC_C_AUTHN_WINNT);
                server.StartListening();
                server.OnExecute +=
                    delegate(IRpcCallInfo client, byte[] arg)
                    { return arg; };

                using (ExplicitBytesClient client = new ExplicitBytesClient(iid, new EndpointBindingInfo(RpcProtseq.ncacn_ip_tcp, null, @"18081")))
                {
                    client.AuthenticateAs(null, ExplicitBytesClient.Self, RPC_C_AUTHN_LEVEL.RPC_C_AUTHN_LEVEL_PKT_PRIVACY, RPC_C_AUTHN.RPC_C_AUTHN_WINNT);
                    client.Execute(new byte[0]);

                    byte[] bytes = new byte[512];
                    new Random().NextBytes(bytes);

                    Stopwatch timer = new Stopwatch();
                    timer.Start();

                    for (int i = 0; i < 5000; i++)
                        client.Execute(bytes);

                    timer.Stop();
                    Trace.WriteLine(timer.ElapsedMilliseconds.ToString(), "TestPerformanceOnTcpip");
                }
            }
        }

 
    }
}
