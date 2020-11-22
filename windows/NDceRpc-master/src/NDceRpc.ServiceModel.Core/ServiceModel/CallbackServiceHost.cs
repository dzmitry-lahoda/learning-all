using System;

namespace NDceRpc.ServiceModel
{

    internal sealed class CallbackServiceHost : ServiceHostBase
    {
        private readonly InstanceContext _serviceCtx;


        private NDceRpc.ServiceModel.CallbackBehaviorAttribute _behaviour;


        //public CallbackServiceHost(Type service, Uri baseAddress,)
        //    : this(Activator.CreateInstance(service), baseAddress.ToString())
        //{
        //    //TODO: make it not singleton
        //}

        public CallbackServiceHost(InstanceContext serviceCtx, Uri baseAddress, NDceRpc.ServiceModel.CallbackBehaviorAttribute behaviour)
            : this(serviceCtx, baseAddress.ToString(), behaviour)
        {
        }

        public CallbackServiceHost(InstanceContext serviceCtx, string baseAddress, CallbackBehaviorAttribute behaviour)
        {
            _serviceCtx = serviceCtx;
            _behaviour = behaviour;
            if (serviceCtx == null) throw new ArgumentNullException("serviceCtx");
            _baseAddress = new Uri(baseAddress,UriKind.Absolute);

    
   
            base._concurrency = _behaviour.ConcurrencyMode;
            _service = serviceCtx._contextObject;
        }
  
        public ServiceEndpoint AddServiceEndpoint(Type contractType, Guid uuid, Binding binding, string address)
        {
            var uri = new Uri(address, UriKind.RelativeOrAbsolute);
            if (!uri.IsAbsoluteUri)
            {
                address = _baseAddress + address;
            }

            var endpoint = base.CreateEndpoint(contractType, binding, address, uuid);
            _endpointDispatchers.Add( new RpcEndpointDispatcher(_service,endpoint, false, _serviceCtx.SynchronizationContext));

            return endpoint;
        }

        public void Close()
        {
            this.Dispose();
        }
    }
}