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
    public static class NarrowToBroadConversionExtensions
    {
        /// <summary>
        /// Converts vector of one numerical type to other.
        /// </summary>
        /// <param name="vector"></param>
        /// <returns>The vector containing result of conversion.</returns>
        public static NumericBroad[] ToNumericBroad(this NumericNarrow[] vector)
        {
            var length = vector.Length;
            var newVector = new NumericBroad[length];
            if (vector.IsEmpty())
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
        public static NumericBroad[,] ToNumericBroad(this NumericNarrow[,] matrix)
        {
            var rowCount = matrix.RowCount();
            var columnCount = matrix.ColumnCount();
            var newMatrix = new NumericBroad[rowCount, columnCount];
            if (matrix.IsEmpty())
            {
                return newMatrix;
            }
            Do.For2(0, rowCount, 0, columnCount, (r, c) => newMatrix[r, c] = matrix[r, c]);
            return newMatrix;
        }

    }
}