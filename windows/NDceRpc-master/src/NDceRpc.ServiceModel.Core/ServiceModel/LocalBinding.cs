using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NDceRpc.Microsoft.Interop;
using NDceRpc.Serialization;

namespace NDceRpc.ServiceModel
{

    public enum LocalSecurityMode
    {
        None
    }

    /// <summary>
    /// Uses http://en.wikipedia.org/wiki/Local_Procedure_Call on Windows
    /// </summary>
    public class LocalBinding : Binding
    {

        public LocalBinding()
        {
            
        }

        public LocalBinding(LocalSecurityMode securityMode)
        {
            //TODO: store security in filed and convert into underlying RPC later
        }

        private RPC_C_AUTHN _authentication = RPC_C_AUTHN.RPC_C_AUTHN_NONE;
        private BinaryObjectSerializer _serializer = new ProtobufObjectSerializer();

        internal override RpcProtseq ProtocolTransport
        {
            get { return RpcProtseq.ncalrpc; }
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
