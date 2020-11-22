using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NDceRpc.ServiceModel.Dispatcher;

namespace NDceRpc.ServiceModel
{
    public class ChannelDispatcher
    {
        private IList<IErrorHandler> _errorHandlers = new List<IErrorHandler>();

        public IList<IErrorHandler> ErrorHandlers
        {
            get { return _errorHandlers; }
        }
    }
}
