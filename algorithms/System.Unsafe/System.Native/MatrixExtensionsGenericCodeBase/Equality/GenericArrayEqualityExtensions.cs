using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading;

using MatrixExtensions.Generic;
using MatrixExtensionsGenericCodeBase;

namespace MatrixExtensions.Equality
{
    /// <summary>
    /// Provides extension methods for numeric arrays related to equality checking.
    /// </summary>
    [Pure]
    public static class GenericArrayEqualityExtensions
    {
        /// <summary>
        /// Determines whether the specified vector elements are 
        /// pairwise equal to current vector's elements.
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="vector"></param>
        /// <param name="other">The vector to compare.</param>
        /// <returns>True, if all correspondent elements are equal;otherwise, false.</returns>
        public static bool EqualsElements<T1, T2>(this T1[] vector, T2[] other)
        {
            Contract.Requires<ArgumentNullException>(other != null);
            Contract.Requires<ArgumentOutOfRangeException>(vector.Length == other.Length);
            bool result = Do.ForUntilFalse(0, vector.Length, 
                i => vector[i].Equals(other[i]));
            return result;
        }

        /// <summary>
        /// Determines whether the specified vector elements are 
        /// pairwise equal to current vector's elements.
        /// Vector elements considered equal if <see cref="Func{T1,T2,TResult}"/>
        /// invocation returns true. 
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="vector"></param>
        /// <param name="other">The vector to compare.</param>
        /// <param name="equals">The delegate which checks elements for equality.</param>
        /// <returns>True, if all correspondent elements are equal;otherwise, false.</returns>
        public static bool EqualsElements<T1, T2>(this T1[] vector, T2[] other, Func<T1, T2, bool> equals)
        {
            Contract.Requires<ArgumentNullException>(other != null);
            Contract.Requires<ArgumentOutOfRangeException>(vector.Length == other.Length);
            bool result = Do.ForUntilFalse(0, vector.Length,i=> equals.Invoke(vector[i], other[i]));
            return result;
        }


    }
}