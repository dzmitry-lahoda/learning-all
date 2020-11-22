using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;

namespace managed_entities
{

    [Guid("00000002-EAF3-4A7A-A0F2-BCE4C30DA77E")]
    public class api
    {
        public static Binding Binding
        {
            get
            {
                return new NetNamedPipeBinding { MaxReceivedMessageSize = 2 * 1024 * 1024, MaxBufferPoolSize = 2 * 1024 * 1024 };
            }
        }

        public static EndpointAddress Endpoint
        {
            get { return new EndpointAddress(Uri); }
        }

        public static Uri Uri
        {
            get { return new Uri("net.pipe://localhost/mywcftest"); }
        }
    }

    [ServiceContract()]
    public interface IWcfBytes
    {
        [OperationContract(IsOneWay = false)]
        byte[] Execute(byte[] input);
    }
}
