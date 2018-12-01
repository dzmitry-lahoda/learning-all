using System;

namespace MatrixExtensions.Generic
{
    /// <summary>
    /// Used for template files. Means to have narrower valuer range then <see cref="NumericBroad"/> 
    /// and can be implicitly casted to it.
    /// </summary>
    public struct NumericNarrow
    {


        public static NumericNarrow operator +(NumericNarrow a, NumericNarrow b)
        {
            throw new NotImplementedException("Never will be implemented");
        }

        public static Numeric operator -(NumericNarrow a, NumericNarrow b)
        {
            throw new NotImplementedException("Never will be implemented");
        }

        public static Numeric operator *(NumericNarrow a, NumericNarrow b)
        {
            throw new NotImplementedException("Never will be implemented");
        }

        public static Numeric operator /(NumericNarrow a, NumericNarrow b)
        {
            throw new NotImplementedException("Never will be implemented");
        }

        public static bool operator >(NumericNarrow a, NumericNarrow b)
        {
            throw new NotImplementedException("Never will be implemented");
        }

        public static bool operator <(NumericNarrow a, NumericNarrow b)
        {
            throw new NotImplementedException("Never will be implemented");
        }

        public static bool operator ==(NumericNarrow a, NumericNarrow b)
        {
            throw new NotImplementedException("Never will be implemented");
        }

        public static bool operator !=(NumericNarrow a, NumericNarrow b)
        {
            throw new NotImplementedException("Never will be implemented");
        }

        public static implicit operator int(NumericNarrow d)
        {
            throw new NotImplementedException("Never will be implemented");
        }

        public static implicit operator NumericNarrow(int d)
        {
            throw new NotImplementedException("Never will be implemented");
        }

        public static explicit operator NumericNarrow(NumericBroad d)
        {
            throw new NotImplementedException("Never will be implemented");
        }

        public override bool Equals(object obj)
        {
            throw new NotImplementedException();
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            throw new NotImplementedException();
        }
    }
}
