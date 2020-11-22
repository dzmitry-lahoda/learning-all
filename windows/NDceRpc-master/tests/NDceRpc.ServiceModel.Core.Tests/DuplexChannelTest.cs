using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Threading;
using NUnit.Framework;

namespace NDceRpc.ServiceModel.Core.Tests
{
    [TestFixture]
    public class DuplexChannelTest
    {
        [ServiceContract(SessionMode = SessionMode.Required, CallbackContract = typeof(ICallbackServiceCallback))]
        [Guid("AE3F52E9-379E-420A-ADA7-106BD04EA14A")]
        public interface ICallbackService
        {
            [OperationContract(IsOneWay = false)]
            void Call();
        }

        private class CallbackService : ICallbackService
        {
            private CallbackData _data;

            public CallbackService(CallbackData data)
            {
                _data = data;
            }

            public void Call()
            {
                var callback = OperationContext.Current.GetCallbackChannel<ICallbackServiceCallback>();
                callback.OnCallback(_data);
                callback.OnOneWayCallback();
            }
        }

        [Guid("BF92DC07-DE28-4D05-B116-5831311040BA")]
        public interface ICallbackServiceCallback
        {
            [OperationContract(IsOneWay = false, IsInitiating = false, IsTerminating = false)]
            void OnCallback(CallbackData message);

            [OperationContract(IsOneWay = true, IsInitiating = false, IsTerminating = false)]
            void OnOneWayCallback();
        }

        private class CallbackServiceCallback : ICallbackServiceCallback
        {
            public ManualResetEvent Wait { get; set; }
            public CallbackData Called { get; set; }

            public CallbackServiceCallback()
            {
                Wait = new ManualResetEvent(false);
            }

            public void OnCallback(CallbackData message)
            {
                Called = message;
            }



            public void OnOneWayCallback()
            {
                Wait.Set();
            }
        }

        [DataContract]
        public class CallbackData
        {
            [DataMember(Order = 1)]
            public string Data { get; set; }
        }



        [Test]
        public void CallServiceReturningSession2Times_sessionAreEqual()
        {

            var address = @"net.pipe://127.0.0.1/1/test.test/test" + MethodBase.GetCurrentMethod().Name;
            var binding = new NetNamedPipeBinding();

            var data = new CallbackData { Data = "1" };
            var srv = new CallbackService(data);
            var callback = new CallbackServiceCallback();

            using (var host = new ServiceHost(srv, new Uri(address)))
            {
                host.AddServiceEndpoint(typeof(ICallbackService), binding, address);
                host.Open();


                using (var factory = new DuplexChannelFactory<ICallbackService>(new InstanceContext(callback), binding))
                {
                    var client = factory.CreateChannel(new EndpointAddress(address));
                    client.Call();
                }
          
                callback.Wait.WaitOne();

                Assert.AreEqual(data.Data, callback.Called.Data);
            }
        }

        [Test]
        public void CallLocalService()
        {

            var address = @"ipc:///1/test.test/test" + MethodBase.GetCurrentMethod().Name;
            var binding = new LocalBinding();

            var data = new CallbackData { Data = "1" };
            var srv = new CallbackService(data);
            var callback = new CallbackServiceCallback();

            using (var host = new ServiceHost(srv, new Uri(address)))
            {
                host.AddServiceEndpoint(typeof(ICallbackService), binding, address);
                host.Open();


                using (var factory = new DuplexChannelFactory<ICallbackService>(new InstanceContext(callback), binding))
                {
                    var client = factory.CreateChannel(new EndpointAddress(address));
                    client.Call();
                }

                callback.Wait.WaitOne();

                Assert.AreEqual(data.Data, callback.Called.Data);
            }
        }

    }


}
