using System.Diagnostics.Contracts;
using System.Threading;

using System;
using MatrixExtensions.Generic;
using MatrixExtensionsGenericCodeBase;


namespace MatrixExtensions.Manipulation
{
    /// <summary>
    /// Provides extension methods for concatenation of 2D numeric arrays.
    /// </summary>
    [Pure]
    public static class NumericArray2DManipulationExtensions
    {
        /// <summary>
        /// Concatenates current matrix with the given vector and places the result into the result matrix.
        /// Vector considered to be a column.
        /// </summary>
        /// <example><pre>
        /// 1, 2,
        /// 3, 4,                1, 2, 5
        /// append      we get   3, 4, 6
        /// 5
        /// 6
        /// </pre></example>
        /// <param name="matrix"></param>
        /// <param name="right">The vector to concatenate.</param>
        /// <returns>The combined matrix.</returns>
        public static NumericBroad[,] Append(this NumericBroad[,] matrix, NumericNarrow[] right)
        {
            Contract.Requires<ArgumentException>(matrix.RowCount() == right.Length);
            Contract.Ensures(Contract.Result<NumericBroad[,]>().ColumnCount() == matrix.ColumnCount() + 1);
            var rowCount = right.Length;
            var columnCount = matrix.ColumnCount() + 1;
            var result = new NumericBroad[rowCount, columnCount];
            Do.Invoke(
                () => Do.For2(0, rowCount, 0, columnCount - 1, (r, c) => result[r, c] = matrix[r, c]),
                () => Do.For(0, rowCount, r => result[r, columnCount - 1] = right[r])
            );
            return result;
        }

        /// <summary>
        /// Concatenates current matrix with the given matrix and places the result into the result matrix.
        ///  </summary>
        ///<example><pre>
        /// 1, 2
        /// 3, 4            1, 2, 5, 6
        /// append   we get 3, 4, 7, 8
        /// 5, 6
        /// 7, 8
        /// </pre></example>
        /// <param name="matrix"></param>
        /// <param name="right">The matrix to concatenate.</param>
        /// <returns>The new combined matrix.</returns>
        public static NumericBroad[,] Append(this NumericBroad[,] matrix, NumericNarrow[,] right)
        {
            Contract.Requires<ArgumentException>(matrix.RowCount() == right.RowCount());
            Contract.Ensures(Contract.Result<NumericBroad[,]>().ColumnCount() == matrix.ColumnCount() + right.ColumnCount());
            var rowCount = matrix.RowCount();
            var columnsInitial = matrix.ColumnCount();
            var columnsOfConcat = right.ColumnCount();
            var columnCount = columnsInitial + columnsOfConcat;
            var result = new NumericBroad[rowCount, columnCount];
            Do.Invoke(
                () => Do.For2(0, rowCount, 0, columnsInitial, (r, c) => result[r, c] = matrix[r, c]),
                () => Do.For2(0, rowCount, columnsInitial, columnCount, (r, c) => result[r, c] = right[r, c - columnsInitial])
            );
            return result;
        }

        /// <summary>
        /// Concatenates current matrix with the given vector and places the result into the result matrix.
        /// Vector considered to be a row.
        /// </summary>
        /// <example><pre>
        /// 1, 2, 3
        /// 4, 5, 6           1, 2, 3
        /// stack     we get  4, 5, 6
        /// 7, 8, 9           7, 8, 9
        /// </pre></example>
        /// <param name="matrix"></param>
        /// <param name="bottom">The vector to concatenate.</param>
        /// <returns>The combined matrix.</returns>
        public static NumericBroad[,] Stack(this NumericBroad[,] matrix, NumericNarrow[] bottom)
        {
            Contract.Requires<ArgumentException>(matrix.ColumnCount() == bottom.Length);
            Contract.Ensures(Contract.Result<NumericBroad[,]>().RowCount() == matrix.RowCount() + 1);
            var columnCount = bottom.Length;
            var rowCount = matrix.RowCount() + 1;
            var result = new NumericBroad[rowCount, columnCount];
            Do.Invoke(
            () => Do.For2(0, rowCount - 1,0, columnCount,
                (r, c) =>result[r, c] = matrix[r, c]),
            () => Do.For(0, columnCount, c =>result[rowCount - 1, c] = bottom[c])
            );
            return result;
        }

        /// <summary>
        /// Concatenates current matrix with the given matrix and places the result into the result matrix.
        ///  </summary>
        /// <example><pre>
        /// 1, 2, 3
        /// 4, 5, 6           1, 2, 3
        /// stack     we get  4, 5, 6
        /// 7, 8, 9           7, 8, 9
        /// 2, 5, 2           2, 5, 2
        /// </pre></example>
        /// <param name="matrix"></param>
        /// <param name="bottom">The matrix to concatenate.</param>
        /// <returns>The combined matrix.</returns>
        public static NumericBroad[,] Stack(this NumericBroad[,] matrix, NumericNarrow[,] bottom)
        {
            Contract.Requires<ArgumentException>(matrix.ColumnCount() == bottom.ColumnCount());
            Contract.Ensures(Contract.Result<NumericBroad[,]>().RowCount() == matrix.RowCount() + bottom.RowCount());

            var columnCount = matrix.ColumnCount();
            var rowsInitial = matrix.RowCount();
            var rowsOfConcat = bottom.RowCount();
            var rowCount = rowsInitial + rowsOfConcat;
            var result = new NumericBroad[rowCount, columnCount];
            Do.Invoke(
            () => Do.For2(0, columnCount,0, rowsInitial,
                (c, r) =>result[r, c] = matrix[r, c]),
            () => Do.For2(0, columnCount, rowsInitial, rowCount, 
                (c,r) =>result[r, c] = bottom[r - rowsInitial, c])
            );
            return result;
        }

    }
}