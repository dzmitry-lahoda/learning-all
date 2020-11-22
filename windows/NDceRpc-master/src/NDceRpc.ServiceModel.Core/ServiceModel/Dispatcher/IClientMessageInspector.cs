using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NDceRpc.ServiceModel.Dispatcher
{
    public interface IClientMessageInspector
    {
        void AfterReceiveReply(ref NDceRpc.ServiceModel.Channels.Message reply, object correlationState);

    }
}
