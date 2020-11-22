using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Xml;
using NUnit.Framework;

namespace WCF.Tests
{
    [TestFixture]
    public class EndpointBehavioursTests
    {
        [Test]
        public void ServerAncClientEndpointBehavior()
        {
            var hook = new InvokesCounterBehaviour();
            var address = @"net.pipe://127.0.0.1/test" + this.GetType().Name + "_" + MethodBase.GetCurrentMethod().Name;
            var serv = new Service(null);
            var host = new ServiceHost(serv, new Uri[] { new Uri(address), });
            var b = new NetNamedPipeBinding();
            var serverEndpoint = host.AddServiceEndpoint(typeof(IService), b, address);
            serverEndpoint.Behaviors.Add(hook);
            Assert.AreEqual(0, hook.Counter);
            host.Open();
            Assert.AreEqual(3, hook.Counter);
            var f = new ChannelFactory<IService>(b);
            f.Endpoint.Behaviors.Add(hook);
            Assert.AreEqual(3, hook.Counter);
            var c = f.CreateChannel(new EndpointAddress(address));
            Assert.AreEqual(6, hook.Counter);
            var result = c.DoWithParamsAndResult("", Guid.NewGuid());

            host.Abort();
        }




        [Test]
        [Description("Propagates server side managed exception to client side and thows as error")]

        public void ServerAncClientExceptionsEndpointBehavior()
        {
            var hook = new ExceptionsEndpointBehaviour();
            var address = @"net.pipe://127.0.0.1/test" + this.GetType().Name + "_" + MethodBase.GetCurrentMethod().Name;
            var serv = new ExceptionService();
            using (var host = new ServiceHost(serv, new Uri[] { new Uri(address), }))
            {
                var b = new NetNamedPipeBinding();
                var serverEndpoint = host.AddServiceEndpoint(typeof(IExceptionService), b, address);
                serverEndpoint.Behaviors.Add(hook);

                host.Open();

                var f = new ChannelFactory<IExceptionService>(b);
                f.Endpoint.Behaviors.Add(hook);

                var c = f.CreateChannel(new EndpointAddress(address));

                try
                {
                    c.DoException("message");
                }
                catch (InvalidOperationException ex)
                {
                    StringAssert.AreEqualIgnoringCase("message", ex.Message);
                }
                host.Abort();
            }
        }
    }

    public class ExceptionsEndpointBehaviour : IEndpointBehavior
    {
        public void Validate(ServiceEndpoint endpoint){}
        public void AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters){}

        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
        {
            endpointDispatcher.ChannelDispatcher.ErrorHandlers.Add(new ExceptionErrorHanlder());
        }

        public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
            clientRuntime.MessageInspectors.Add(new ExceptionMessageInspector());
        }
    }

    public class ExceptionErrorHanlder : IErrorHandler
    {
        public void ProvideFault(Exception error, MessageVersion version, ref Message fault)
        {
            if (error is FaultException)
            {
                return;
            }
            var faultContent = MessageFault.CreateFault(new FaultCode("Sender"), new FaultReason(""), error,
                                              new NetDataContractSerializer());
            fault = Message.CreateMessage(version, faultContent, null);
        }

        public bool HandleError(Exception error)
        {
            return !(error is FaultException);
        }
    }

    public class ExceptionMessageInspector : IClientMessageInspector
    {
        public object BeforeSendRequest(ref Message request, IClientChannel channel)
        {
            return null;
        }

        public void AfterReceiveReply(ref Message reply, object correlationState)
        {
            if (reply.IsFault)
            {

                const string detailElementName = "Detail";

                using (XmlDictionaryReader reader = reply.GetReaderAtBodyContents())
                {
                    while (reader.Read())
                    {
                        if (reader.NodeType == XmlNodeType.Element && reader.LocalName == detailElementName)
                        {
                            break;
                        }
                    }

                    if (reader.NodeType != XmlNodeType.Element || reader.LocalName != detailElementName)
                    {
                        return;
                    }

                    if (!reader.Read())
                    {
                        return;
                    }
                    var serializer = new NetDataContractSerializer();

                    var detail = serializer.ReadObject(reader);


                    var exception = detail as Exception;
                    if (exception != null)
                    {
                        throw exception;
                    }
                }

            }
        }
    }

    public class InvokesCounterBehaviour : IEndpointBehavior
    {
        public int Counter { get; set; }

        public void Validate(ServiceEndpoint endpoint)
        {
            Counter++;
        }

        public void AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
        {
            Counter++;
        }

        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
        {
            Counter++;
        }

        public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
            Counter++;

        }
    }
}
