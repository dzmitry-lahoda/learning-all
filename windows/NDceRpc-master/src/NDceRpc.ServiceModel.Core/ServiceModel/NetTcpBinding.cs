using NDceRpc.Microsoft.Interop;
using NDceRpc.Serialization;

namespace NDceRpc.ServiceModel
{
    public class NetTcpBinding : Binding
    {
        private RPC_C_AUTHN _authentication = RPC_C_AUTHN.RPC_C_AUTHN_WINNT;
        private BinaryObjectSerializer _serializer = new ProtobufObjectSerializer();

        internal override RpcProtseq ProtocolTransport
        {
            get { return RpcProtseq.ncacn_ip_tcp; }
        }

        public override RPC_C_AUTHN Authentication
        {
            get { return _authentication; }
            set { _authentication = value; }
        }

        public override BinaryObjectSerializer Serializer
        {
            get { return _serializer; }
            set { _serializer = value; }
        }
    }
}