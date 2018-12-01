using System;

namespace MatrixExtensionsTestProject
{
    /// <summary>
    /// A node with an index and a value. Used for test purposes only.
    /// </summary>
    [Serializable]
    public class Node : IEquatable<Node>
    {
        internal int _index;
        internal double _value;

        /// <summary>
        /// Default Constructor.
        /// </summary>
        public Node()
        {
        }
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="index">The index of the value.</param>
        /// <param name="value">The value to store.</param>
        public Node(int index, double value)
        {
            _index = index;
            _value = value;
        }

        /// <summary>
        /// Index of this Node.
        /// </summary>
        public int Index
        {
            get
            {
                return _index;
            }
            set
            {
                _index = value;
            }
        }
        /// <summary>
        /// Value at Index.
        /// </summary>
        public double Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
            }
        }

        public override string ToString()
        {
            return string.Format("Index: {0}, Value: {1}", _index, _value);
        }

        public bool Equals(Node other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other._index == _index && other._value == _value;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (Node)) return false;
            return Equals((Node) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (_index*397) ^ _value.GetHashCode();
            }
        }

    }
}