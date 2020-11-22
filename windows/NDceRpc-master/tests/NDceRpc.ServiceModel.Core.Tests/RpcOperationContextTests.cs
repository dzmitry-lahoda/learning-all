using System;
using System.Reflection;
using System.Runtime.InteropServices;
using NUnit.Framework;

namespace NDceRpc.ServiceModel.Core.Tests
{
    [TestFixture]
    public class RpcOperationContextTests
    {
        [ServiceContract(SessionMode = SessionMode.Required)]
        [Guid("E97C42A4-4AAB-4639-8F5E-972B0C3CD214")]
        public interface ISessionService
        {
            [OperationContract(IsOneWay = false)]
            string Call();
        }



        [Test]
        public void CallServiceReturningSession2Times_sessionAreEqual()
        {
            var address = @"net.pipe://127.0.0.1/1/test.test/test" + MethodBase.GetCurrentMethod().Name;

            var serv = new SessionService();
            var host = new ServiceHost(serv, new Uri(address));
            var b = new NetNamedPipeBinding();
            host.AddServiceEndpoint(typeof(ISessionService), b, address);
            var f1 = new ChannelFactory<ISessionService>(b);

            var client = f1.CreateChannel(new EndpointAddress(address));
            host.Open();

            var session11 = client.Call();

            var session12 = client.Call();

            host.Dispose();

            Assert.AreEqual(session11, session12);

        }

        [Test]
        public void CallServiceReturningSession2TimesFor2Channels_sessionAreDifferentForDifferentChannels()
        {
            var address = @"net.pipe://127.0.0.1/1/test.test/test" + MethodBase.GetCurrentMethod().Name;

            var serv = new SessionService();
            var host = new ServiceHost(serv, new Uri(address));
            var b = new NetNamedPipeBinding();
            host.AddServiceEndpoint(typeof(ISessionService), b, address);
            var f1 = new ChannelFactory<ISessionService>(b);
            var f2 = new ChannelFactory<ISessionService>(b);
            var client1 = f1.CreateChannel(new EndpointAddress(address));
            var client2 = f2.CreateChannel(new EndpointAddress(address));
            host.Open();

            var session11 = client1.Call();
            var session21 = client2.Call();
            var session22 = client2.Call();
            var session12 = client1.Call();

            f1.Dispose();
            f2.Dispose();
            host.Dispose();
            Assert.AreEqual(session11, session12);
            Assert.AreEqual(session21, session22);
            Assert.AreNotEqual(session11, session21);
        }



        public class SessionService : ISessionService
        {
            public string Call()
            {
                return OperationContext.Current.SessionId;
            }
        }
    }
}