using System;
using System.Collections.Generic;
using NDceRpc.Serialization;
using NDceRpc.ServiceModel.Description;

namespace NDceRpc.ServiceModel
{
    public class ServiceEndpoint
    {
        internal readonly Binding _binding;
        internal readonly Type _contractType;
        internal readonly string _address;
        internal readonly Guid _uuid;
        internal BinaryObjectSerializer _serializer;
        private IList<IEndpointBehavior> _behaviors = new List<IEndpointBehavior>();

        public ServiceEndpoint(Binding binding, Type contractType, string address, Guid uuid)
        {
            _binding = binding;
            _serializer = _binding.Serializer;
            _contractType = contractType;
            _address = address;
            _uuid = uuid;
        }

        public IList<Description.IEndpointBehavior> Behaviors { get { return _behaviors; } }
    }
}