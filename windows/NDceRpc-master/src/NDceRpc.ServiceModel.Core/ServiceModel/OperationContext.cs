using System;

namespace NDceRpc.ServiceModel
{
    //TODO: rename to OperationContext
    public class OperationContext
    {
        [ThreadStatic]
        private static OperationContext _current ;

        private string _sessionId = string.Empty;
        private RpcCallbackChannelFactory _getter;
        private string _address;

        public static OperationContext Current
        {
            get { return _current; }
            set { _current = value; }
        }

        public string SessionId
        {
            get { return _sessionId; }
            set { _sessionId = value; }
        }

        public T GetCallbackChannel<T>()
        {
            return _getter.CreateChannel<T>(new EndpointAddress(_address));
        }

        internal void SetGetter(RpcCallbackChannelFactory rpcChannelFactory, string address)
        {
            _getter = rpcChannelFactory;
            _address = address;
        }
    }
}