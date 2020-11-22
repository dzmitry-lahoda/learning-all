using System.Collections.Generic;
using System.Reflection;

namespace NDceRpc.ServiceModel
{
    //TODO: try to use other structure and measure performance
    internal class DispatchTable 
    {
        /// <summary>
        /// Maps runtime CLR reflection MetadataToken to operation which should put on the wire. 
        /// </summary>
        public  Dictionary<int, OperationDispatchBase> TokenToOperation = new Dictionary<int, OperationDispatchBase>();

        /// <summary>
        /// Maps identifier from the wire to operation.
        /// </summary>
        public Dictionary<int, OperationDispatchBase> IdToOperation = new Dictionary<int, OperationDispatchBase>();

    }
}