using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using NUnit.Framework;

namespace NDceRpc.ServiceModel.Core.Tests
{
    [TestFixture]
    public class TestConcurrency
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
        public void TestOneWayConcurencyOnNamedPipe()
        {
            var address = @"net.pipe://127.0.0.1/testpipename" + MethodBase.GetCurrentMethod().Name;
            var srv = new Service();
            using (var server = new ServiceHost(srv, new Uri(address)))
            {
                server.AddServiceEndpoint(typeof(IService), new NetNamedPipeBinding { }, address);
                server.Open();
                using (var channelFactory = new ChannelFactory<IService>(new NetNamedPipeBinding { }))
                {

                    var client = channelFactory.CreateChannel(new EndpointAddress(address));

                    for (int i = 0; i < 20; i++)
                        client.OneWayExecute();
                    while (srv.Count != 20)
                    {
                        Thread.Sleep(10);
                    }
                }
            }
        }

        [Test]
        public void TestOneWayCallbackConcurencyOnNamedPipe()
        {
           
            var address = @"net.pipe://127.0.0.1/testpipename" + MethodBase.GetCurrentMethod().Name;
            var srv = new CallbackService();
            var callback = new CallbackServiceCallback();
            using (var server = new ServiceHost(srv, new Uri(address)))
            {
                server.AddServiceEndpoint(typeof(ICallbackService), new NetNamedPipeBinding { }, address);
                server.Open();

                using (var channelFactory = new DuplexChannelFactory<ICallbackService>(new InstanceContext(callback), new NetNamedPipeBinding { }))
                {

                    var client = channelFactory.CreateChannel(new EndpointAddress(address));

                    for (int i = 0; i < 20; i++)
                        client.OneWayExecute();
                    while (callback.Count != 20)
                    {
                        Thread.Sleep(10);
                    }

                }
            }
        }
    }
}
