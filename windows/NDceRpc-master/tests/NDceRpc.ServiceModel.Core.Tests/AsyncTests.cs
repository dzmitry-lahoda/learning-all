using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;

namespace NDceRpc.ServiceModel.Core.Tests
{
    [TestFixture]
    [Ignore]
    public class AsyncTests
    {


        [Test]
        public void CallbackAsyncCallback_wait_done()
        {

            var address = @"net.pipe://127.0.0.1/" + this.GetType().Name + "_" + MethodBase.GetCurrentMethod().Name;
            var binding = new NetNamedPipeBinding();


            var srv = new AsyncService(null);
            var callback = new AsyncServiceCallback();

            using (var host = new ServiceHost(srv, new Uri(address)))
            {
                host.AddServiceEndpoint(typeof(IAsyncService), binding, address);
                host.Open();


                using (var factory = new DuplexChannelFactory<IAsyncService>(new InstanceContext(callback), binding))
                {
                    var client = factory.CreateChannel(new EndpointAddress(address));
                    client.DoSyncCall();

                }
            }
        }

        [Test]
        public void CallAsync_wait_done()
        {
            var address = @"net.pipe://127.0.0.1/" + this.GetType().Name + "_" + MethodBase.GetCurrentMethod().Name;
            var binding = new NetNamedPipeBinding();

            var done = new ManualResetEvent(false);
            var srv = new AsyncService(done);
            var callback = new AsyncServiceCallback();

            using (var host = new ServiceHost(srv, new Uri(address)))
            {
                host.AddServiceEndpoint(typeof(IAsyncService), binding, address);
                host.Open();

                ThreadPool.QueueUserWorkItem(_ =>
                {
                    using (var factory = new DuplexChannelFactory<IAsyncService>(new InstanceContext(callback), binding))
                    {
                        var client = factory.CreateChannel(new EndpointAddress(address));
                        AsyncCallback act = (x) =>
                        {
                            Assert.AreEqual(x.AsyncState, 1);
                        };
                        var result = client.BeginServiceAsyncMethod(act, 1);
                        result.AsyncWaitHandle.WaitOne();
                        Assert.AreEqual(result.AsyncState, 1);
                        client.EndServiceAsyncMethod(result);

                    }
                });

                done.WaitOne();
            }
        }


        [Test]
        public void CallTask_wait_done()
        {
            var address = @"net.pipe://127.0.0.1/" + this.GetType().Name + "_" + MethodBase.GetCurrentMethod().Name;
            var binding = new NetNamedPipeBinding();


            var srv = new AsyncTaskService();


            using (var host = new ServiceHost(srv, new Uri(address)))
            {
                host.AddServiceEndpoint(typeof(IAsyncTaskService), binding, address);
                host.Open();

                var done = Task.Factory.StartNew(() =>
                {
                    using (var factory = new ChannelFactory<IAsyncTaskService>(binding))
                    {
                        var client = factory.CreateChannel(new EndpointAddress(address));
                        var result = client.GetMessages("123");
                        result.Wait();
                        Assert.AreEqual(result.Result, "321");
                    }
                });
                done.Wait();
            }
        }

        [Test]
        [ExpectedException(typeof(CommunicationObjectFaultedException))]
        public void CallAsync_noServer_done()
        {

            var address = @"net.pipe://127.0.0.1/" + this.GetType().Name + "_" + MethodBase.GetCurrentMethod().Name;
            var binding = new NetNamedPipeBinding();

            var callback = new AsyncServiceCallback();


            using (var factory = new DuplexChannelFactory<IAsyncService>(new InstanceContext(callback), binding))
            {
                var client = factory.CreateChannel(new EndpointAddress(address));
                AsyncCallback act = x => Assert.AreEqual(x.AsyncState, 1);
                IAsyncResult result = client.BeginServiceAsyncMethod(act, 1);
                result.AsyncWaitHandle.WaitOne();
                Assert.AreEqual(result.AsyncState, 1);
            }
        }

    }
}
