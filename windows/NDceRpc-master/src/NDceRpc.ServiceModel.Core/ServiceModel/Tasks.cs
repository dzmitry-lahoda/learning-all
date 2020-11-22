using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDceRpc.ServiceModel
{
    internal class Tasks
    {
        //TODO: use cusom factory to prevent conflicts
        //TODO: what RPC uses? wrap and use!!!!
        private static TaskFactory _factory = Task.Factory;
        public static TaskFactory Factory
        {
            get { return _factory; }
            set { _factory = value; }
        }
    }
}
