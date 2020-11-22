using System;
using System.Reflection;
using NUnit.Framework;

namespace NDceRpc.ServiceModel.Core.Tests
{
    [TestFixture]
    public class ErrorHandlingTests
    {
        [Test]
        public void ServerAndClientExceptions()
        {
            var address = @"net.pipe://127.0.0.1/test" + this.GetType().Name + "_" + MethodBase.GetCurrentMethod().Name;
            var serv = new ExceptionService();
            using (var host = new NDceRpc.ServiceModel.ServiceHost(serv, new Uri[] {new Uri(address),}))
            {
                var b = new NDceRpc.ServiceModel.NetNamedPipeBinding();
                host.AddServiceEndpoint(typeof(IExceptionService), b, address);
                host.Open();
                var f = new NDceRpc.ServiceModel.ChannelFactory<IExceptionService>(b);
                var c = f.CreateChannel(new NDceRpc.ServiceModel.EndpointAddress(address));

                try
                {
                    c.DoException("message");
                }
                catch (Exception ex)
                {
                    Assert.IsInstanceOf<NDceRpc.ServiceModel.FaultException>(ex);
                }
                host.Abort();
            }
        }
    }
}
