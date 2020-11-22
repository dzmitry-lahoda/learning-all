using System;

namespace NDceRpc.ServiceModel
{
    public class RpcCallbackChannelFactory
    {
        private Binding _binding;
        private Type _type;
        private readonly Guid _session;
        private readonly bool _callback;
        private ClientRuntime _client;
        private ServiceEndpoint _endpoint;

        public RpcCallbackChannelFactory(Binding binding, Type typeOfService,Guid session, bool callback = false)
        {

            _session = session;
            _callback = callback;
            _endpoint = new ServiceEndpoint(binding, typeOfService, null, Guid.Empty);
        }

        public TService CreateChannel<TService>(EndpointAddress createEndpoint)
        {
            if (_client == null)
                _client = new ClientRuntime(createEndpoint.Uri, _endpoint, false, null, _session);
            return (TService)_client.Channell;
        }

        public void Dispose()
        {
            _client.Dispose();
        }
    }
}