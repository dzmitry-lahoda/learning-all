using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using StaThreadSyncronizer;

namespace WCF.Tests
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
            var binding = new NetNamedPipeBinding(NetNamedPipeSecurityMode.None) { MaxConnections = 5 };

            using (var server = new ServiceHost(new SyncCallbackService(), new Uri(path)))
            {

                server.AddServiceEndpoint(typeof(ISyncCallbackService), binding, path);



                server.Open();
                using (var syncContext = new StaSynchronizationContext())
                {
                    InstanceContext context = null;
                    DuplexChannelFactory<ISyncCallbackService> channelFactory = null;
                    ISyncCallbackService client = null;
                    syncContext.Send(_ => SynchronizationContext.SetSynchronizationContext(syncContext), null);
                    syncContext.Send(_ => context = new InstanceContext(new NotSyncCallbackServiceCallback()), null);
                    syncContext.Send(_ => channelFactory = new DuplexChannelFactory<ISyncCallbackService>(context, binding), null);
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
            var binding = new NetNamedPipeBinding(NetNamedPipeSecurityMode.None) { MaxConnections = 5 };

            using (var server = new ServiceHost(new SyncCallbackService(), new Uri(path)))
            {

                server.AddServiceEndpoint(typeof(ISyncCallbackService), binding, path);



                server.Open();
                using (var syncContext = new StaSynchronizationContext())
                {
                    InstanceContext context = null;
                    DuplexChannelFactory<ISyncCallbackService> channelFactory = null;
                    ISyncCallbackService client = null;
                    syncContext.Send(_ => SynchronizationContext.SetSynchronizationContext(syncContext), null);
                    syncContext.Send(_ => context = new InstanceContext(new SyncCallbackServiceCallback()), null);
                    syncContext.Send(_ => channelFactory = new DuplexChannelFactory<ISyncCallbackService>(context, binding),null);
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
