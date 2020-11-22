using System;
using NDceRpc.ServiceModel.Channels;

namespace NDceRpc.ServiceModel.Dispatcher
{
    
    public interface IErrorHandler
    {
        
        void ProvideFault(Exception error, MessageVersion version, ref Message fault);

      
        bool HandleError(Exception error);
    }
}