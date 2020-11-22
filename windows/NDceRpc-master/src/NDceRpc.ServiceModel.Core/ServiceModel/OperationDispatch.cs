using System.Reflection;


namespace NDceRpc.ServiceModel
{
    /// <summary>
    /// Maps method binary identifiers into invocable entities.
    /// </summary>
    internal class OperationDispatch : OperationDispatchBase
    {

        public OperationDispatch(OperationContractAttribute operation, MethodInfo methodInfo, int identifier)
            : base(methodInfo, identifier)
        {
            _operation = operation;
            var allParameters = methodInfo.GetParameters();
           
       

            foreach (var p in allParameters)
            {
                var map = new ParameterDispatch(p);
                _params.Add(map.Identifier, map);
            }

        }

 
    }
}