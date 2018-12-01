using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading;


namespace MatrixExtensions.Generic
{
    /// <summary>
    /// Provides generic extension methods for 1D arrays.
    /// </summary>
    [Pure]
    public static class GenericArrayExtensions
    {
        /// <summary>
        /// Returns true if vector contains no elements.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="vector"></param>
        /// <returns>True, if number of elements equals 0; otherwise, false.</returns>
        public static bool IsEmpty<T>(this T[] vector)
        {
            return vector.Length == 0;
        }
   
    }
}