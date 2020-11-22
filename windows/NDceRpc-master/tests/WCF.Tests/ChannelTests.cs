using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Threading;
using NUnit.Framework;
using NUnit.Framework.Constraints;


namespace WCF.Tests
{
    [TestFixture]
    public class ChannelTests
    {


        [Test]
        [ExpectedException(typeof(System.ObjectDisposedException))]
        public void DisposedChannelFactory_call()
        {
            var address = @"net.pipe://127.0.0.1/"+Guid.NewGuid().ToString("N");
            var serv = new SimpleService();
            var b = new NetNamedPipeBinding();

            using (var host = new ServiceHost(serv, new Uri[] { new Uri(address), }))
            {
                host.AddServiceEndpoint(typeof(ISimpleService), b, address);
                host.Open();
                var f = new ChannelFactory<ISimpleService>(b);
                var c = f.CreateChannel(new EndpointAddress(address));
                using (f){}
                c.Do();
            }
        }


        [Test]
        public void LongNamePipe()
        {
            var address = @"net.pipe://127.0.0.1/1/test.test/testtestLongNameLongNameLongNameLongNameLongNameLongNameLongNameLongNameLongNamefd0286a60b9b4db18659-b715e5db5b3bd0286a6-0b9b-4db1-8659-b715e5db5b3b";
            var serv = new Service(null);
            var b = new NetNamedPipeBinding();

            using (var host = new ServiceHost(serv, new Uri[] { new Uri(address), }))
            {
                host.AddServiceEndpoint(typeof(IService), b, address);
                host.Open();
                using (var f = new ChannelFactory<IService>(b))
                {
                    var c = f.CreateChannel(new EndpointAddress(address));

                    var result = c.DoWithParamsAndResult(":)", Guid.NewGuid());
                    Assert.AreEqual(2, result.d1);
                }
            }
        }


        [Test]
        public void PipeICommuicationObject()
        {
            var address = @"net.pipe://127.0.0.1/test" + MethodBase.GetCurrentMethod().Name;
            var serv = new Service(null);
            var host = new ServiceHost(serv, new Uri(address));
            var b = new NetNamedPipeBinding();
            host.AddServiceEndpoint(typeof(IService), b, address);
            host.Open();
            var f = new ChannelFactory<IService>(b);
            var c = f.CreateChannel(new EndpointAddress(address));
            c.DoWithParamsAndResult(null, Guid.Empty);
            var obj = c as ICommunicationObject;
            var state = obj.State;
            Assert.AreEqual(CommunicationState.Opened, state);
            host.Close();
        }

        [Test]
        public void PipeChannel_notOpenedServer_created()
        {
            var address = @"net.pipe://127.0.0.1/test" + MethodBase.GetCurrentMethod().Name;
            var b = new NetNamedPipeBinding();
            var f = new ChannelFactory<IService>(b);
            var c = f.CreateChannel(new EndpointAddress(address));
            var obj = c as ICommunicationObject;
            var state = obj.State;
            Assert.AreEqual(CommunicationState.Created, state);
        }

        [Test]
        public void PipeChannel_notOpenedServer_fail()
        {
            var address = @"net.pipe://127.0.0.1/test" + MethodBase.GetCurrentMethod().Name;
            var b = new NetNamedPipeBinding();
            var f = new ChannelFactory<IService>(b);
            var c = f.CreateChannel(new EndpointAddress(address));
            Exception comminicationEx = null;
            try
            {
                c.DoWithParamsAndResult(null, Guid.Empty);
            }
            catch (Exception ex)
            {
                comminicationEx = ex;
            }
            var obj = c as ICommunicationObject;
            var state = obj.State;
            Assert.AreEqual(CommunicationState.Faulted, state);
            Assert.That(comminicationEx, new ExceptionTypeConstraint(typeof(EndpointNotFoundException)));
        }

        [Test]
        public void PipeChannel_openClose_fail()
        {
            var address = @"net.pipe://127.0.0.1/test" + MethodBase.GetCurrentMethod().Name;
            var b = new NetNamedPipeBinding();
            var serv = new Service(null);
            var host = new ServiceHost(serv, new Uri(address));
            host.AddServiceEndpoint(typeof(IService), b, address);
            host.Open();
            var f = new ChannelFactory<IService>(b);
            var c = f.CreateChannel(new EndpointAddress(address));
            c.DoWithParamsAndResult(null, Guid.Empty);
            host.Close();
            Exception comminicationEx = null;
            try
            {
                c.DoWithParamsAndResult(null, Guid.Empty);
            }
            catch (Exception ex)
            {
                comminicationEx = ex;
            }
            var obj = c as ICommunicationObject;
            var state = obj.State;
            Assert.AreEqual(CommunicationState.Faulted, state);
            Assert.That(comminicationEx, new ExceptionTypeConstraint(typeof(CommunicationException)));
        }

        [Test]
        public void IContextChannel_operationTimeoutSetGet_Ok()
        {
            var address = @"net.pipe://127.0.0.1/" + this.GetType().Name + "_" + MethodBase.GetCurrentMethod().Name;
            var binding = new NetNamedPipeBinding();
            using (var server = new System.ServiceModel.ServiceHost(new SimpleService(), new Uri(address)))
            {

                server.AddServiceEndpoint(typeof(ISimpleService), binding, address);
                server.Open();
                Thread.Sleep(100);
                using (var channelFactory = new System.ServiceModel.ChannelFactory<ISimpleService>(binding))
                {
                    var client = channelFactory.CreateChannel(new EndpointAddress(address));
                    var contextChannel = client as IContextChannel;
                    var newTimeout = TimeSpan.FromSeconds(123);
                    contextChannel.OperationTimeout = newTimeout;
                    var timeout = contextChannel.OperationTimeout;
                    Assert.AreEqual(newTimeout, timeout);

                }
            }
        }
    }


}
