using System;
using System.Collections.Generic;

namespace NDceRpc.ServiceModel
{
    public abstract class EndpointDispatcher
    {
        protected object _singletonService;
        internal   DispatchTable _operations;
        internal ServiceEndpoint _endpoint;
        protected ChannelDispatcher _channelDispatcher = new ChannelDispatcher();

        internal  DispatchTable Operations
        {
            get { return _operations; }
        }

        public ChannelDispatcher ChannelDispatcher
        {
            get { return _channelDispatcher; }
        }

        protected EndpointDispatcher(object singletonService, ServiceEndpoint endpoint)
        {

            _endpoint = endpoint;
            _singletonService = singletonService;
            _operations = DispatchTableFactory.GetOperations(endpoint._contractType);

        }
    }
}