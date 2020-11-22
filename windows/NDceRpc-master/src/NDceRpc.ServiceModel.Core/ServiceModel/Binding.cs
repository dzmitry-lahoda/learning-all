using System;

using NDceRpc.Microsoft.Interop;
using NDceRpc.Serialization;

namespace NDceRpc.ServiceModel
{
    public abstract class Binding : IDefaultCommunicationTimeouts
    {
        private int _maxConnections = byte.MaxValue;
        private TimeSpan _closeTimeout =  RpcServiceDefaults.CloseTimeout;

        internal abstract RpcProtseq ProtocolTransport { get; }

        public int MaxConnections
        {
            get { return _maxConnections; }
            set { _maxConnections = value; }
        }

        public abstract RPC_C_AUTHN Authentication { get; set; }

        public abstract BinaryObjectSerializer Serializer { get; set; }
        public TimeSpan CloseTimeout
        {
            get { return _closeTimeout; }
            set { _closeTimeout = value; }
        }

        public TimeSpan OpenTimeout { get; private set; }
        public TimeSpan ReceiveTimeout { get; set; }
        public TimeSpan SendTimeout { get; private set; }


    }
}