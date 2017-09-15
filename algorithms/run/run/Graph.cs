using System;
using System.Collections.Generic;
using System.Text;

namespace run
{
    public interface INode<out T> where T : struct
    {
        T Value { get; }
    }

    public interface IArc<out T> where T : struct
    {
        INode<T> From { get; }
        INode<T> To { get; }
    }

    public interface IUnknownGraph<out T> where T : struct
    {

    }    

    public struct intnode : INode<int>
    {
        public int Value { get; set; }
    };

    public struct intarc : IArc<int>
    {
        public INode<int> From { get; set; }
        public INode<int> To { get; set; }
    };

    public struct charnode : INode<char>
    {
        public char Value { get; set; }
    };

    public struct chararc : IArc<char>
    {
        public INode<char> From { get; set; }
        public INode<char> To { get; set; }
    };

    public static class GraphExtensions
    {
        public static intarc _(intnode from, intnode to)
        {
            return new intarc { From = from, To = to };
        }

        public static intarc _(int from, int to)
        {
            return new intarc { From = _(from), To = _(to) };
        }

        public static intnode _(int value)
        {
            return new intnode { Value = value };
        }

        public static chararc _(charnode from, charnode to)
        {
            return new chararc { From = from, To = to };
        }

        public static chararc _(char from, char to)
        {
            return new chararc { From = _(from), To = _(to) };
        }

        public static charnode _(char value)
        {
            return new charnode { Value = value };
        }
    }
}
