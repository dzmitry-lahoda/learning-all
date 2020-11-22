using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using System.Text;
using System.Threading;
using NUnit.Framework;

namespace WCF.IntegrationTests
{
    [TestFixture]
    public class TimeoutTests
    {
        [ServiceContract]
        [System.Runtime.InteropServices.Guid("11B688EC-5F06-4AE4-AA0A-2895BB125FE7")]
        public interface IService
        {
            [OperationContract(IsOneWay = false)]
            TimeSpan Execute(TimeSpan sleep);
        }

        [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
        private class Service : IService
        {


            public TimeSpan Execute(TimeSpan sleep)
            {
                Thread.Sleep(sleep);

                return sleep;
            }
        }



        [Test]
        public void TestTimeoutSet()
        {
            var uri = "net.pipe://127.0.0.1/testpipename" + MethodBase.GetCurrentMethod().Name;
            var binding = new System.ServiceModel.NetNamedPipeBinding() { MaxConnections = 5 };
            var timeout = 700;
            binding.ReceiveTimeout = TimeSpan.FromMilliseconds(timeout);
            var hang = TimeSpan.FromMilliseconds(timeout * 2);
            using (var server = new System.ServiceModel.ServiceHost(new Service(), new Uri(uri)))
            {
                server.AddServiceEndpoint(typeof(IService), binding, uri);
                server.Open();
                var channelFactory = new System.ServiceModel.ChannelFactory<IService>(binding);

                var client = channelFactory.CreateChannel(new EndpointAddress(uri));
                var result = client.Execute(TimeSpan.FromMilliseconds(0));
                Assert.AreEqual(TimeSpan.FromMilliseconds(0), result);
                CommunicationException timeoutHappenedException = null;
                try
                {
                    result = client.Execute(hang);
                }
                catch (CommunicationException ex)
                {
                    timeoutHappenedException = ex;

                }
                Assert.NotNull(timeoutHappenedException);
                Assert.AreEqual(typeof(System.IO.IOException), timeoutHappenedException.InnerException.GetType());
                var channel = client as IContextChannel;
                Assert.AreEqual(CommunicationState.Faulted, channel.State);
                try
                {
                    result = client.Execute(TimeSpan.FromMilliseconds(0));
                }
                catch (CommunicationObjectFaultedException afterTimeoutExeption)
                {

                }
                client = channelFactory.CreateChannel(new EndpointAddress(uri));
                result = client.Execute(TimeSpan.FromMilliseconds(0));
            }
        }
    }
}
