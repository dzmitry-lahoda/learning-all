using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NDceRpc.ServiceModel.Channels
{

    public sealed class MessageVersion
    {
        public static MessageVersion _wcfProtoRpc1 = new MessageVersion();

        public static MessageVersion WcfProtoRpc1
        {
            get { return _wcfProtoRpc1; }
        }
    }
}
