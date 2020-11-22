using System.Collections.Generic;
using System.Reflection;

namespace NDceRpc.ServiceModel
{


    public class OperationDispatchBase
    {
        protected int _identifier;
        protected Dictionary<int, ParameterDispatch> _params = new Dictionary<int, ParameterDispatch>();
        protected MethodInfo _methodInfo;
        protected OperationContractAttribute _operation;

        protected OperationDispatchBase(MethodInfo methodInfo,int identifier)
        {
            _identifier = identifier;
            _methodInfo = methodInfo;
        }





        public MethodInfo MethodInfo { get { return _methodInfo; } }
        public OperationContractAttribute Operation { get { return _operation; } }
        
        
        public Dictionary<int, ParameterDispatch> Params
        {
            get { return _params; }
        }

        public int Identifier
        {
            get { return _identifier; }

        }

        private bool Equals(OperationDispatchBase other)
        {
            return _identifier == other._identifier;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((OperationDispatchBase)obj);
        }

        public override int GetHashCode()
        {
            return _identifier;
        }
    }
}