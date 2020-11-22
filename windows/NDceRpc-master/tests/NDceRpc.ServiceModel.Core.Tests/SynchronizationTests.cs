using System;
using System.Reflection;
using System.Threading;
using NUnit.Framework;
using StaThreadSyncronizer;

namespace NDceRpc.ServiceModel.Core.Tests
{
    [TestFixture]
    public class SynchronizationTests
    {
        private SynchronizationContext _syncContext;

        [SetUp]
        public void SetUp()
        {
            _syncContext = SynchronizationContext.Current;
        }

        [Test]

        public void CallbackToNotSyncContext()
        {
            var path = @"net.pipe://127.0.0.1/" + this.GetType().Name + MethodBase.GetCurrentMethod().Name;
            var binding = new NetNamedPipeBinding() { MaxConnections = 5 };

            using (var server = new ServiceHost(new SyncCallbackService(), new Uri(path)))
            {

                server.AddServiceEndpoint(typeof(ISyncCallbackService), binding, path);



                server.Open();
                using (var syncContext = new StaSynchronizationContext())
                {
                    InstanceContext context = null;
                    NDceRpc.ServiceModel.DuplexChannelFactory<ISyncCallbackService> channelFactory = null;
                    ISyncCallbackService client = null;
                    syncContext.Send(_ => SynchronizationContext.SetSynchronizationContext(syncContext), null);
                    syncContext.Send(_ => context = new InstanceContext(new NotSyncCallbackServiceCallback()), null);
                    syncContext.Send(_ => channelFactory = new NDceRpc.ServiceModel.DuplexChannelFactory<ISyncCallbackService>(context, binding), null);
                    syncContext.Send(_ => client = channelFactory.CreateChannel(new EndpointAddress(path)), null);
                    using (channelFactory)
                    {
                        var callbackThread = client.Call();
                        Assert.AreNotEqual(syncContext.ManagedThreadId, callbackThread);
                    }
                }


            }

        }

        [Test]

        public void CallbackToSyncContext()
        {
            var path = @"net.pipe://127.0.0.1/" + this.GetType().Name + MethodBase.GetCurrentMethod().Name;
            var binding = new NetNamedPipeBinding() { MaxConnections = 5 };

            using (var server = new ServiceHost(new SyncCallbackService(), new Uri(path)))
            {

                server.AddServiceEndpoint(typeof(ISyncCallbackService), binding, path);



                server.Open();
                using (var syncContext = new StaSynchronizationContext())
                {
                    InstanceContext context = null;
                    NDceRpc.ServiceModel.DuplexChannelFactory<ISyncCallbackService> channelFactory = null;
                    ISyncCallbackService client = null;
                    syncContext.Send(_ => SynchronizationContext.SetSynchronizationContext(syncContext), null);
                    syncContext.Send(_ => context = new InstanceContext(new SyncCallbackServiceCallback()), null);
                    syncContext.Send(_ => channelFactory = new NDceRpc.ServiceModel.DuplexChannelFactory<ISyncCallbackService>(context, binding),null);
                    syncContext.Send(_ => client =  channelFactory.CreateChannel(new EndpointAddress(path)),null);
                    using (channelFactory)
                    {
                        var callbackThread = client.Call();
                        Assert.AreEqual(syncContext.ManagedThreadId, callbackThread);
                    }
                }


            }

        }

        [TearDown]
        public void TearDown()
        {
            SynchronizationContext.SetSynchronizationContext(_syncContext);
        }



    }
}
