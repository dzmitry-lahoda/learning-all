using System;
using System.Collections.Generic;
using System.Text;

namespace EPAM.Trainings.Zadanie2_6
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
    public class HashIndexedMessage : IIndexedMessage, IComparable<HashIndexedMessage>
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
