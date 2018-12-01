using System.Diagnostics.Contracts;
using System.Threading;
using MatrixExtensions.Generic;
using MatrixExtensionsGenericCodeBase;

namespace MatrixExtensions.Conversion
{
    /// <summary>
    /// Provides extension methods for numeric arrays conversion.
    /// </summary>
    [Pure]
    public static class DoubleIntConversionExtensions
    {
        /// <summary>
        /// Converts vector of one numerical type to other.
        /// </summary>
        /// <param name="vector"></param>
        /// <returns>The vector containing result of conversion.</returns>
        public static double[] ToDouble(this int[] vector)
        {
            var length = vector.Length;
            var newVector = new double[length];
            if (vector.Length == 0)
            {
                return newVector;
            }
            Do.For(0, length, i => newVector[i] = vector[i]);
            return newVector;
        }

        /// <summary>
        /// Converts matrix of one numerical type to other.
        /// </summary>
        /// <param name="matrix"></param>
        /// <returns>The matrix containing result of conversion.</returns>
        public static double[,] ToDouble(this int[,] matrix)
        {
            var rowCount = matrix.GetLength(0);
            var columnCount = matrix.GetLength(1);
            var newMatrix = new double[rowCount, columnCount];
            if (matrix.Length == 0)
            {
                return newMatrix;
            }
            Do.For2(0, rowCount, 0, columnCount, (r, c) => newMatrix[r, c] = matrix[r, c]);
            return newMatrix;
        }

    }
}