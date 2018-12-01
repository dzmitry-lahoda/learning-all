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
    public static class FloatFloatArraySimpleOperationsExtensions
    {

        /// <summary>
        /// Returns the index of the absolute maximum element.
        /// </summary>
        /// <param name="vector"></param>
        /// <returns>The index of absolute maximum element.</returns>
        public static int MaxPosition(this float[] vector)
        {
            Contract.Requires<ArgumentException>(vector.Length > 0, "vector.Length");
            Contract.Ensures(Contract.Result<int>() >= 0);
            Contract.Ensures(Contract.Result<int>() < vector.Length);
              var max = vector[0];
              var position = Do.FindUsingBase(0, vector.Length, max, 
                  (i, @base) => vector[i] > @base, (i) =>vector[i]);

            return position;
        }

        /// <summary>
        /// Adds a scalar to each element of the vector.
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="scalar">The scalar to add.</param>
        /// <returns>The vector which stores the result of the addition.</returns>
        public static float[] Add(this float[] vector, float scalar)
        {
            Contract.Ensures(Contract.Result<float[]>().Length == vector.Length);
            var length = vector.Length;
            var newVector = new float[length];
            Do.For(0, length, i =>newVector[i] = vector[i] + scalar);
            return newVector;
        }

        /// <summary>
        /// Subtracts a scalar from each element of the vector.
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="scalar">The scalar to subtract.</param>
        /// <returns>The vector which stores the result of the subtraction.</returns>
        public static float[] Substract(this float[] vector, float scalar)
        {
            return vector.Add(-scalar);
        }

        /// <summary>
        /// Negates each element of the vector.
        /// </summary>
        /// <returns>A vector containing the negated values.</returns>
        public static float[] Negate(this float[] vector)
        {
            Contract.Ensures(Contract.Result<float[]>().Length == vector.Length);
            var length = vector.Length;
            var newVector = new float[length];
            Do.For(0, length, i =>newVector[i] = -vector[i]);
            return newVector;
        }

        /// <summary>
        /// Multiplies a vector with a scalar and returns the result.
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="scalar">The scalar to multiply the vector by.</param>
        /// <returns>The result of the multiplication.</returns>
        public static float[] PointwiseMultiply(this float[] vector, float scalar)
        {
            Contract.Ensures(Contract.Result<float[]>().Length == vector.Length);
            var length = vector.Length;
            var newVector = new float[length];
            Do.For(0, length, i => newVector[i] = vector[i] * scalar);
            return newVector;
        }

        /// <summary>
        /// Divides each element of current vector by a scalar and returns the result.
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="scalar">The scalar to divide the vector by.</param>
        /// <returns>The vecotr with result of the division.</returns>
        public static float[] DivideEachElement(this float[] vector, float scalar)
        {
            Contract.Requires<ArgumentException>(scalar != 0);
            Contract.Ensures(Contract.Result<float[]>().Length == vector.Length);
            var length = vector.Length;
            var newVector = new float[length];
            Do.For(0, length, i => newVector[i] = vector[i] / scalar);
            return newVector;
        }

    }
}