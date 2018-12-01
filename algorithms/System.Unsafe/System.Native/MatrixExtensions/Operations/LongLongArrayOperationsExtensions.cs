using System;
using System.Diagnostics.Contracts;
using System.Threading;
using MatrixExtensions.Generic;
using MatrixExtensionsGenericCodeBase;

namespace MatrixExtensions.Operations
{
    /// <summary>
    /// Provides extension methods for 1D numeric arrays.
    /// </summary>
    [Pure]
    public  static class LongLongArrayOperationsExtensions
    {

        /// <summary>
        /// Adds another vector to this vector.
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="other">The vector to add to this one.</param>
        /// <returns>The vector which stores the result of the addition.</returns>
        public static long[] Add(this long[] vector, long[] other)
        {
            Contract.Requires<ArgumentNullException>(other != null);
            Contract.Requires<ArgumentException>(vector.Length == other.Length);
            Contract.Ensures(Contract.Result<long[]>().Length == vector.Length);
            var length = vector.Length ;
            var newVector = new long[length];
            Do.For(0, length, i =>newVector[i] = vector[i] + other[i]);
            return newVector;
        }


        /// <summary>
        /// Subtracts another vector from this vector.
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="other">The vector to subtract from this one.</param>
        /// <returns>The vector which stores the result of the subtraction.</returns>
        public static long[] Substract(this long[] vector, long[] other)
        {
            Contract.Requires<ArgumentNullException>(other != null);
            Contract.Requires<ArgumentException>(vector.Length == other.Length);
            Contract.Ensures(Contract.Result<long[]>().Length == vector.Length);
            var length = vector.Length;
            var newVector = new long[length];
            Do.For(0, length, i =>newVector[i] = vector[i] - other[i]);
            return newVector;
        }



        /// <summary>
        /// Pointwise multiplies this vector with another vector and stores the result into the result vector.
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="other">The vector to pointwise multiply with this one.</param>
        /// <returns>The vector which stores the result of the  pointwise multiplication.</returns>
        public static long[] PointwiseMultiply(this long[] vector, long[] other)
        {
            Contract.Requires<ArgumentNullException>(other != null);
            Contract.Requires<ArgumentException>(vector.Length == other.Length);
            Contract.Ensures(Contract.Result<long[]>().Length == vector.Length);
            var length = vector.Length;
            var newVector = new long[length];
            Do.For(0, length, i => newVector[i] = vector[i] * other[i]);
            return newVector;
        }

      

    }
}