using System;
using System.Collections.Generic;
using System.Text;

namespace EPAM.Trainings
{
    public interface IIndexedMessage
    {
        String Message
        {
            get;
        }

        int Index
        {
            get;
        }
    }

    [Serializable]
    public class HashIndexedMessage : IIndexedMessage, IComparable<HashIndexedMessage>,IFormattable
    {
        public HashIndexedMessage(String message, String info)
        {
            this.message = message;
            this.index = message.GetHashCode();
            this.info = info;
        }
        private String message = String.Empty;
        private int index = 0;
        private String info = String.Empty;

        public String Message
        {
            get
            {
                return message;
            }
        }

        public int Index
        {
            get
            {
                return index;
            }
        }

        public String Info
        {
            get
            {
                return info;
            }
        }

        int IComparable<HashIndexedMessage>.CompareTo(HashIndexedMessage other)
        {
            if (other != null)
            {
                if (other.Index > this.Index)
                {
                    return -1;
                }
                if (other.Index < this.Index)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            return 0;
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            return String.Format(formatProvider,
                "This HashIndexedMessage has {0} and {1} message",
                this.Index,this.Message);
        }

    }

    public class DescendingComparer<T> : IComparer<T> where T : IIndexedMessage
    {
        int IComparer<T>.Compare(T x, T y)
        {
            if (y != null && x != null)
            {
                if (x.Index > y.Index)
                {
                    return -1;
                }
                if (x.Index < y.Index)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            return 0;
        }
    }
}