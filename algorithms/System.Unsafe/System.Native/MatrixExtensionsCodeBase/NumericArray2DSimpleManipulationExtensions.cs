using System.Diagnostics.Contracts;
using MatrixExtensions.Generic;
using MatrixExtensionsGenericCodeBase;


namespace MatrixExtensions.Manipulation
{
    /// <summary>
    /// Provides extension methods for 2D numeric arrays manupulation.
    /// </summary>
    [Pure]
    public static class NumericArray2DSimpleManipulationExtensions
    {

        /// <summary>
        /// Returns the matrix's elements as an vector with the data laid out column-wise.
        /// </summary>
        /// <example><pre>
        /// 1, 2, 3
        /// 4, 5, 6  will be returned as  1, 4, 7, 2, 5, 8, 3, 6, 9
        /// 7, 8, 9
        /// </pre></example>
        /// <returns>The new vector containing the matrix's elements.</returns>
        public static NumericBroad[] ToColumnWiseArray(this NumericBroad[,] matrix)
        {
            Contract.Ensures(Contract.Result<NumericBroad[]>().Length == matrix.ColumnCount() * matrix.RowCount());
            var rowCount = matrix.RowCount();
            var columnCount = matrix.ColumnCount();
            var newVector = new NumericBroad[rowCount * columnCount];
            Do.For(0, columnCount, c =>
            {
                var index = c * rowCount;
                Do.For(0, rowCount, r =>
                {
                    newVector[index + r] = matrix[r, c];
                }
                );
            });

            return newVector;
        }
        /// <summary>
        /// Returns the matrix's elements as an vector with the data laid row-wise.
        /// </summary>
        /// <example><pre>
        /// 1, 2, 3
        /// 4, 5, 6  will be returned as  1, 2, 3, 4, 5, 6, 7, 8, 9
        /// 7, 8, 9
        /// </pre></example>
        /// <returns>The new vector containing the matrix's elements.</returns>
        public static NumericBroad[] ToRowWiseArray(this NumericBroad[,] matrix)
        {
            Contract.Ensures(Contract.Result<NumericBroad[]>().Length == matrix.ColumnCount() * matrix.RowCount());
            var rowCount = matrix.RowCount();
            var columnCount = matrix.ColumnCount();
            var newVector = new NumericBroad[rowCount * columnCount];
            Do.For(0, rowCount, r =>
            {
                var index = r * columnCount;
                Do.For(0, columnCount, c =>
                {
                    newVector[index + c] = matrix[r, c];
                });
            });
            return newVector;
        }
    }
}