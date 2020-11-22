using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using NDceRpc.ExplicitBytes;
using NDceRpc.Microsoft.Interop;
using NUnit.Framework;

namespace NDceRpc.Test
{
    [TestFixture]
    public class ConcurrencyTests
    {

        

        [Test]
        public void TestConcurentCreationOfServersAndClients()
        {
            var tasks = new List<Task>();
            int counter = 0;
            var servers = 10;
            int clientsPerServer = 10;
            for (int i = 0; i < servers; i++)
            {
                var task = new Task(() =>
                {
                    Guid iid = Guid.NewGuid();
                    using (ExplicitBytesServer server = new ExplicitBytesServer(iid))
                    {
                        server.AddProtocol(RpcProtseq.ncacn_np, @"\pipe\testpipename" + iid, clientsPerServer);
                        server.AddAuthentication(RPC_C_AUTHN.RPC_C_AUTHN_NONE);
                        server.StartListening();
                        server.OnExecute +=
                            delegate(IRpcCallInfo client, byte[] arg)
                            {
                                System.Threading.Interlocked.Increment(ref counter);
                                return arg;
                            };
                        var clientTasks = new List<Task>();
                        for (int j = 0; j < clientsPerServer; j++)
                        {
                            var clientTask = new Task(() =>
                            {
                                using (ExplicitBytesClient client = new ExplicitBytesClient(iid, new EndpointBindingInfo(RpcProtseq.ncacn_np, null, @"\pipe\testpipename" + iid)))
                                {
                                    client.AuthenticateAs(null, ExplicitBytesClient.Self, RPC_C_AUTHN_LEVEL.RPC_C_AUTHN_LEVEL_NONE, RPC_C_AUTHN.RPC_C_AUTHN_NONE);
                                    Assert.AreEqual(123, client.Execute(new byte[1] { 123 })[0]);
                                }
                            });
                            clientTask.Start();
                            clientTasks.Add(clientTask);
                        }
                        Task.WaitAll(clientTasks.ToArray());
                    }
                });
                task.Start();
                tasks.Add(task);
            }
            Task.WaitAll(tasks.ToArray());
            Assert.AreEqual(servers * clientsPerServer, counter);
        }

        private static Random _rand = new Random((int)DateTime.Now.Ticks);

        [Test]
        public void TestRandomizedConcurentCreationOfServersAndClients()
        {
            var tasks = new List<Task>();
            int counter = 0;
            var servers = 10;
            int clientsPerServer = 10;
            for (int i = 0; i < servers; i++)
            {
                var task = new Task(() =>
                {
                    Thread.Sleep(_rand.Next(0,50));
                    Guid iid = Guid.NewGuid();
                    using (ExplicitBytesServer server = new ExplicitBytesServer(iid))
                    {
                        server.AddProtocol(RpcProtseq.ncacn_np, @"\pipe\testpipename" + iid, clientsPerServer);
                        server.AddAuthentication(RPC_C_AUTHN.RPC_C_AUTHN_NONE);
                        server.StartListening();
                        server.OnExecute +=
                            delegate(IRpcCallInfo client, byte[] arg)
                            {
                                System.Threading.Interlocked.Increment(ref counter);
                                return arg;
                            };
                        var clientTasks = new List<Task>();
                        for (int j = 0; j < clientsPerServer; j++)
                        {
                            var clientTask = new Task(() =>
                            {
                                Thread.Sleep(_rand.Next(0, 50));
                                using (ExplicitBytesClient client = new ExplicitBytesClient(iid, new EndpointBindingInfo(RpcProtseq.ncacn_np, null, @"\pipe\testpipename" + iid)))
                                {
                                    client.AuthenticateAs(null, ExplicitBytesClient.Self, RPC_C_AUTHN_LEVEL.RPC_C_AUTHN_LEVEL_NONE, RPC_C_AUTHN.RPC_C_AUTHN_NONE);
                                    Assert.AreEqual(123, client.Execute(new byte[1] { 123 })[0]);
                                }
                            });
                            clientTask.Start();
                            clientTasks.Add(clientTask);
                        }
                        Task.WaitAll(clientTasks.ToArray());
                    }
                });
                task.Start();
                tasks.Add(task);
            }
            Task.WaitAll(tasks.ToArray());
            Assert.AreEqual(servers * clientsPerServer, counter);
        }
    }
}
