using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;
using System.ServiceModel;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;

namespace NDceRpc.ServiceModel.Test
{
    [TestFixture]
    public class MultiuserConcurrencyTests
    {

        [ServiceContract]
        [Guid("B82F353F-5BDC-45A9-B2E2-D96185D989B9")]
        public interface IService
        {
            [OperationContract(IsOneWay = true)]
            void OneWayExecute();

            [OperationContract(IsOneWay = false)]
            void Execute();
        }

        [Guid("A2A8762E-3FF2-41AB-BB21-F64C26E2ECDC")]
        [ServiceContract(SessionMode = SessionMode.Required, CallbackContract = typeof(ICallbackServiceCallback))]
        public interface ICallbackService
        {
            [OperationContract(IsOneWay = true)]
            void OneWayExecute();

            [OperationContract(IsOneWay = false)]
            void Execute();
        }

        private class CallbackService : ICallbackService
        {
            public void OneWayExecute()
            {
                var callback = OperationContext.Current.GetCallbackChannel<ICallbackServiceCallback>();
                callback.OneWayCall();
            }

            public void Execute()
            {
                var callback = OperationContext.Current.GetCallbackChannel<ICallbackServiceCallback>();
                callback.OneWayCall();
            }
        }

        [Guid("58CF0449-5E82-495C-9443-0B63F7178D9D")]
        public interface ICallbackServiceCallback
        {
            [OperationContract(IsOneWay = true)]
            void OneWayCall();

            [OperationContract(IsOneWay = false)]
            void Call();
        }

        private class CallbackServiceCallback : ICallbackServiceCallback
        {
            public int Count
            {
                get { return count; }
            }

            private int count = 0;
            public void OneWayCall()
            {
                System.Threading.Interlocked.Increment(ref count);
            }

            public void Call()
            {
                System.Threading.Interlocked.Increment(ref count);
            }
        }

        private class Service : IService
        {
            public int Count
            {
                get { return count; }
            }

            private int count = 0;
            public void OneWayExecute()
            {
                System.Threading.Interlocked.Increment(ref count);
            }

            public void Execute()
            {
                System.Threading.Interlocked.Increment(ref count);
            }
        }



        [Test]
        public void TestManyDumplexServersAndClientsCreatingsConcurencyOnNamedPipe()
        {
            var address = @"net.pipe://127.0.0.1/testpipename" + MethodBase.GetCurrentMethod().Name;
            var servers = 5;
            var clients = 5;
            var srv = new CallbackService();
            var callback = new CallbackServiceCallback();
           
            var tasks = new List<Task>();
            for (int i = 0; i < servers; i++)
            {
                var custom = address + i;
                var task = new Task(() =>
                    {
                        using (var server = new ServiceHost(srv, new Uri(address)))
                        {
                            server.AddServiceEndpoint(typeof(ICallbackService), new NetNamedPipeBinding { }, custom);
                            server.Open();
                            var requests = new List<Task>();
                            var channels = new ConcurrentBag<DuplexChannelFactory<ICallbackService>>();
                            for (int j = 0; j < clients; j++)
                            {

                                var childTask = new Task(() =>
                                {
                                    var channelFactory = new DuplexChannelFactory<ICallbackService>(new InstanceContext(callback), new NetNamedPipeBinding());
                                    var client = channelFactory.CreateChannel(new EndpointAddress(custom));
                                    client.Execute();
                                    channels.Add(channelFactory);
                                });
                                requests.Add(childTask);
                            }
                            foreach (var request in requests)
                            {
                                request.Start();
                            }
                            Task.WaitAll(requests.ToArray());

                            foreach (var factory in channels)
                            {
                                factory.Dispose();
                            }
                        }
                    });
                task.Start();
                tasks.Add(task);
            }
            while (callback.Count != servers * clients)
            {
                Thread.Sleep(10);
            }
            Task.WaitAll(tasks.ToArray());
        }

        [Test]
        public void TestManyDumplexClientsCreatingsConcurencyOnNamedPipe()
        {
            var address = @"net.pipe://127.0.0.1/testpipename" + MethodBase.GetCurrentMethod().Name;
            var srv = new CallbackService();
            var callback = new CallbackServiceCallback();
            using (var server = new ServiceHost(srv, new Uri(address)))
            {
                server.AddServiceEndpoint(typeof(ICallbackService), new NetNamedPipeBinding { }, address);
                server.Open();
                var requests = new List<Task>();
                var channels = new ConcurrentBag<DuplexChannelFactory<ICallbackService>>();
                for (int i = 0; i < 20; i++)
                {

                    var task = new Task(() =>
                    {
                        var channelFactory = new DuplexChannelFactory<ICallbackService>(new InstanceContext(callback), new NetNamedPipeBinding());
                            var client = channelFactory.CreateChannel(new EndpointAddress(address));
                            client.Execute();
                        channels.Add(channelFactory);
                    });
                    requests.Add(task);
                }
                foreach (var request in requests)
                {
                    request.Start();
                }
                Task.WaitAll(requests.ToArray());
                while (callback.Count != 20)
                {
                    Thread.Sleep(10);
                }
                foreach (var factory in channels)
                {
                    factory.Dispose();
                }
            }
        }

        [Test]
        public void TestManyClientsConcurencyOnNamedPipe()
        {
            var address = @"net.pipe://127.0.0.1/testpipename" + MethodBase.GetCurrentMethod().Name;
            var srv = new Service();
            using (var server = new ServiceHost(srv, new Uri(address)))
            {
                server.AddServiceEndpoint(typeof(IService), new NetNamedPipeBinding { }, address);
                server.Open();
                var requests = new List<Task>();
                for (int i = 0; i < 20; i++)
                {

                    var task = new Task(
                        () =>
                        {
                            using (var channelFactory = new ChannelFactory<IService>(new NetNamedPipeBinding { }))
                            {
                                var client = channelFactory.CreateChannel(new EndpointAddress(address));
                                client.Execute();
                            }
                            });
                    requests.Add(task);
                }
                foreach (var request in requests)
                {
                    request.Start();
                }
                Task.WaitAll(requests.ToArray());
                Assert.AreEqual(20, srv.Count);
            }
        }

        [Test]
        public void TestManyClientsCreatingsConcurencyOnNamedPipe()
        {
            var address = @"net.pipe://127.0.0.1/testpipename" + MethodBase.GetCurrentMethod().Name;
            var srv = new Service();
            using (var server = new ServiceHost(srv, new Uri(address)))
            {
                server.AddServiceEndpoint(typeof(IService), new NetNamedPipeBinding { }, address);
                server.Open();
                var requests = new List<Task>();
                var channels = new ConcurrentBag<ChannelFactory<IService>>();
                for (int i = 0; i < 20; i++)
                {
                    var task = new Task(() =>
                        {
                            var channelFactory = new ChannelFactory<IService>(new NetNamedPipeBinding {});
                          
                                 var client = channelFactory.CreateChannel(new EndpointAddress(address));
                                 client.Execute();
                                 channels.Add(channelFactory);
                         });
                    requests.Add(task);
                }
                foreach (var request in requests)
                {
                    request.Start();
                }
                Task.WaitAll(requests.ToArray());
                Assert.AreEqual(20, srv.Count);
                foreach (var factory in channels)
                {
                    factory.Dispose();
                }
            }
        }



        [Test]
        public void TestManyClientsCreatingsOneWayConcurencyOnNamedPipe()
        {
            var address = @"net.pipe://127.0.0.1/testpipename" + MethodBase.GetCurrentMethod().Name;
            var srv = new Service();
            using (var server = new ServiceHost(srv, new Uri(address)))
            {
                server.AddServiceEndpoint(typeof(IService), new NetNamedPipeBinding { }, address);
                server.Open();
                var requests = new List<Task>();
                var channels = new ConcurrentBag<ChannelFactory<IService>>();
                for (int i = 0; i < 20; i++)
                {
                    var task = new Task(() =>
                        {
                            var channelFactory = new ChannelFactory<IService>(new NetNamedPipeBinding {});
                            
                                var client = channelFactory.CreateChannel(new EndpointAddress(address));
                                client.OneWayExecute();
                           channels.Add(channelFactory);
                        });
                    requests.Add(task);
                }
                foreach (var request in requests)
                {
                    request.Start();
                }
                Task.WaitAll(requests.ToArray());
                while (srv.Count != 20)
                {
                    Thread.Sleep(10);
                }
                Assert.AreEqual(20, srv.Count);
                foreach (var factory in channels)
                {
                    factory.Dispose();
                }

            }
        }
    }
}
