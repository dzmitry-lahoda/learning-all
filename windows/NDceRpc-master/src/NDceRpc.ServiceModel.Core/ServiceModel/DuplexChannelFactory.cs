using System;
using System.Runtime.InteropServices.ComTypes;

namespace NDceRpc.ServiceModel
{
    /// <summary>
    /// Similar to WCF <seealso cref="System.ServiceModel.DuplexChannelFactory{TChannel}"/> and 
    /// COM <seealso cref="IConnectionPoint"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DuplexChannelFactory<T>:IDisposable
    {
        private readonly InstanceContext _context;
        private ClientRuntime _client;
        private ServiceEndpoint _endpoint;

        public DuplexChannelFactory(InstanceContext context, Binding binding)
        {
     
            _context = context;
            _endpoint = new ServiceEndpoint(binding, typeof (T), null, Guid.Empty);
        }

        public T CreateChannel(EndpointAddress createEndpoint)
        {
            if (_client == null)
                _client = new ClientRuntime(createEndpoint.Uri, _endpoint, false, _context);
            return (T)_client.Channell;
        }

        public void Dispose()
        {
            _client.Dispose();
        }

        public ServiceEndpoint Endpoint
        {
            get { return _endpoint; }
            private set { _endpoint = value; }
        }
    }
}