using System;

namespace MatrixExtensions.Generic
{
    /// <summary>
    /// Used for template files. 
    /// Means to have broader value range than <see cref="NumericNarrow"/>.
    /// </summary>
    public struct NumericBroad
    {


        public static NumericBroad operator +(NumericBroad a, NumericBroad b)
        {
            throw new NotImplementedException("Never will be implemented");
        }

        public static NumericBroad operator -(NumericBroad a, NumericBroad b)
        {
            throw new NotImplementedException("Never will be implemented");
        }

        public static NumericBroad operator *(NumericBroad a, NumericBroad b)
        {
            throw new NotImplementedException("Never will be implemented");
        }

        public static NumericBroad operator /(NumericBroad a, NumericBroad b)
        {
            throw new NotImplementedException("Never will be implemented");
        }

        public static bool operator >(NumericBroad a, NumericBroad b)
        {
            throw new NotImplementedException("Never will be implemented");
        }

        public static bool operator <(NumericBroad a, NumericBroad b)
        {
            throw new NotImplementedException("Never will be implemented");
        }

        public static bool operator ==(NumericBroad a, NumericBroad b)
        {
            throw new NotImplementedException("Never will be implemented");
        }

        public static bool operator !=(NumericBroad a, NumericBroad b)
        {
            throw new NotImplementedException("Never will be implemented");
        }

        public static implicit operator int(NumericBroad d)
        {
            throw new NotImplementedException("Never will be implemented");
        }

        public static implicit operator NumericBroad(int d)
        {
            throw new NotImplementedException("Never will be implemented");
        }

        public static implicit operator NumericBroad(NumericNarrow d)
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
