using System;
using System.Reflection;

namespace NDceRpc.ServiceModel
{
    /// <summary>
    /// Maps parameters binary indentifiers into <see cref="Type"/>s.
    /// </summary>
    public class ParameterDispatch
    {
        private int _identifier;

        public ParameterDispatch(ParameterInfo parameterInfo)
        {
            _identifier = parameterInfo.Position;
            Info = parameterInfo;
        }

        public ParameterInfo Info { get; set; }

        public int Identifier
        {
            get { return _identifier; }

        }

        protected bool Equals(ParameterDispatch other)
        {
            return _identifier == other._identifier;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((ParameterDispatch) obj);
        }

        public override int GetHashCode()
        {
            return _identifier;
        }
    }
}