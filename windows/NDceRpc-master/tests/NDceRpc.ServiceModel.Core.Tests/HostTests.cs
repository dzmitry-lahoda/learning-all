using System;
using System.Reflection;
using NUnit.Framework;

namespace NDceRpc.ServiceModel.Core.Tests
{
    [TestFixture]
    public class HostTests
    {
        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Open_Open_error()
        {
            var address = @"net.pipe://127.0.0.1/" + this.GetType().Name + MethodBase.GetCurrentMethod().Name;
            var serv = new Service(null);
            using (var host = new NDceRpc.ServiceModel.ServiceHost(serv, new Uri[] { new Uri(address) }))
            {
                var b = new NDceRpc.ServiceModel.NetNamedPipeBinding();
                host.AddServiceEndpoint(typeof(IService), b, address);
                host.Open();
                host.Open();
            }
        }

        [Test]

        public void Open_2Endpoints_callsBoth()
        {
            var baseAddress = @"net.pipe://127.0.0.1/" + this.GetType().Name + MethodBase.GetCurrentMethod().Name;
            var serv = new Service(null);
            using (var host = new ServiceHost(serv, new Uri[] { new Uri(baseAddress) }))
            {
                var binding = new NetNamedPipeBinding();
                host.AddServiceEndpoint(typeof(IService), binding, baseAddress + "/1");
                host.AddServiceEndpoint(typeof(IService), binding, baseAddress + "/2");
                host.Open();
                using (var channelFatory = new ChannelFactory<IService>(binding))
                {
                    var c1 = channelFatory.CreateChannel(new EndpointAddress(baseAddress + "/1"));
                    var c2 = channelFatory.CreateChannel(new EndpointAddress(baseAddress + "/2"));
                    c1.DoWithParamsAndResult("", Guid.Empty);
                    c2.DoWithParamsAndResult("", Guid.Empty);
                }

            }
        }

        
    }
}
