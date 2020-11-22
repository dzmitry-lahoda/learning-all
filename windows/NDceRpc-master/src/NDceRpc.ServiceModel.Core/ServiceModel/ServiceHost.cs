using System;
using System.Diagnostics;
using System.Linq;


namespace NDceRpc.ServiceModel
{
    public sealed class ServiceHost :ServiceHostBase
    {


        private NDceRpc.ServiceModel.ServiceBehaviorAttribute _behaviour = new NDceRpc.ServiceModel.ServiceBehaviorAttribute();
   

        public ServiceHost(Type service,params Uri[] baseAddresses)
            : this(Activator.CreateInstance(service), baseAddresses)
        {
            //TODO: make it not singleton
        }

        public ServiceHost(object service,params Uri[] baseAddress)
        {
            if (baseAddress == null)
                throw new ArgumentNullException("baseAddress");
            if (baseAddress.Length > 1)
                throw new NotImplementedException("Can you only one base address for now");
            _baseAddress = new Uri(baseAddress.First().ToString(), UriKind.Absolute);
            Reflect(service);
        }

        private void Reflect(object service)
        {
            var serviceBehaviour = AttributesReader.GetServiceBehavior(service);
            if (serviceBehaviour != null) _behaviour = serviceBehaviour;
            if (service == null) throw new ArgumentNullException("service");
            _service = service;
            _concurrency = _behaviour.ConcurrencyMode;
        }

   

        public ServiceEndpoint AddServiceEndpoint(Type contractType, Binding binding, string address)
        {
            
            var uri = new Uri(address, UriKind.RelativeOrAbsolute);
            if (!uri.IsAbsoluteUri)
            {
                address = _baseAddress + address;
            }
            var uuid = EndpointMapper.CreateUuid(address, contractType);
            bool expectDuplexInitialization = false;
            var service = AttributesReader.GetServiceContract(contractType);
            if (service.CallbackContract != null)
            {
                expectDuplexInitialization = true;
            }
            RpcTrace.TraceEvent(TraceEventType.Start, "Start adding service endpoint for {0} at {1}",contractType,address);
            var endpoint = base.CreateEndpoint(contractType, binding, address, uuid);
            _endpointDispatchers.Add(new RpcEndpointDispatcher(_service, endpoint, expectDuplexInitialization));
            return endpoint;
        }

      
    }
}