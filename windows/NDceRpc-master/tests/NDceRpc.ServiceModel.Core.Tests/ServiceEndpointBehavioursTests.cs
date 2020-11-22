using System;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.ServiceModel.Channels;
using NUnit.Framework;

namespace NDceRpc.ServiceModel.Core.Tests
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
            var host = new NDceRpc.ServiceModel.ServiceHost(serv, new Uri[] { new Uri(address), });
            var b = new NDceRpc.ServiceModel.NetNamedPipeBinding();
            var serverEndpoint = host.AddServiceEndpoint(typeof(IService), b, address);
            serverEndpoint.Behaviors.Add(hook);
            Assert.AreEqual(0, hook.Counter);
            host.Open();
            Assert.AreEqual(1, hook.Counter);
            var f = new NDceRpc.ServiceModel.ChannelFactory<IService>(b);
            f.Endpoint.Behaviors.Add(hook);
            Assert.AreEqual(1, hook.Counter);
            var c = f.CreateChannel(new NDceRpc.ServiceModel.EndpointAddress(address));
            Assert.AreEqual(2, hook.Counter);
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

    public class ExceptionsEndpointBehaviour : NDceRpc.ServiceModel.Description.IEndpointBehavior
    {
        public void Validate(ServiceEndpoint endpoint) { }
        public void AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters) { }

        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
        {
            endpointDispatcher.ChannelDispatcher.ErrorHandlers.Add(new ExceptionErrorHanlder());
        }

        public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
            clientRuntime.MessageInspectors.Add(new ExceptionMessageInspector());
        }
    }

    public class ExceptionErrorHanlder : NDceRpc.ServiceModel.Dispatcher.IErrorHandler
    {
        public void ProvideFault(Exception error, NDceRpc.ServiceModel.Channels.MessageVersion version, ref NDceRpc.ServiceModel.Channels.Message fault)
        {
            if (!HandleError(error))
            {
                return;
            }
            var serializer = new NetDataContractSerializer();
            var stream = new MemoryStream();
            serializer.Serialize(stream, error);
            stream.Position = 0;
            fault.Fault.Detail = stream.ToArray();
        }

        public bool HandleError(Exception error)
        {
            // uses string instead of equality to handle with pure WCF faults
            return !(error.GetType().FullName.Contains("ServiceModel.FaultException"));
        }
    }

    public class ExceptionMessageInspector : NDceRpc.ServiceModel.Dispatcher.IClientMessageInspector
    {

        public void AfterReceiveReply(ref NDceRpc.ServiceModel.Channels.Message reply, object correlationState)
        {
            var stream = new MemoryStream(reply.Fault.Detail);
            var serializer = new NetDataContractSerializer();
            var exeption = (Exception)serializer.Deserialize(stream);
            throw exeption;
        }
    }

    public class InvokesCounterBehaviour : NDceRpc.ServiceModel.Description.IEndpointBehavior
    {
        public int Counter { get; set; }


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
