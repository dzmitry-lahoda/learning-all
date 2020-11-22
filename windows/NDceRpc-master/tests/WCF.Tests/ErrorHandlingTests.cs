using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using System.Text;
using NUnit.Framework;

namespace WCF.Tests
{
    [TestFixture]
    public class ErrorHandlingTests
    {
        [Test]
        public void ServerAncClientExceptions()
        {
            var address = @"net.pipe://127.0.0.1/test" + this.GetType().Name + "_" + MethodBase.GetCurrentMethod().Name;
            var serv = new ExceptionService();
            using (var host = new ServiceHost(serv, new Uri[] {new Uri(address),}))
            {
                var b = new NetNamedPipeBinding();
                host.AddServiceEndpoint(typeof(IExceptionService), b, address);
                host.Open();
                var f = new ChannelFactory<IExceptionService>(b);
                var c = f.CreateChannel(new EndpointAddress(address));

                try
                {
                    c.DoException("message");
                }
                catch (Exception ex)
                {
                    Assert.IsInstanceOf<FaultException>(ex);
                }
                host.Abort();
            }
        }
    }
}
