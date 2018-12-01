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
    public static class IntIntArray2DManipulationExtensions
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
        public static int[,] Append(this int[,] matrix, int[] right)
        {
            Contract.Requires<ArgumentException>(matrix.GetLength(0) == right.Length);
            Contract.Ensures(Contract.Result<int[,]>().GetLength(1) == matrix.GetLength(1) + 1);
            var rowCount = right.Length;
            var columnCount = matrix.GetLength(1) + 1;
            var result = new int[rowCount, columnCount];
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
        public static int[,] Append(this int[,] matrix, int[,] right)
        {
            Contract.Requires<ArgumentException>(matrix.GetLength(0) == right.GetLength(0));
            Contract.Ensures(Contract.Result<int[,]>().GetLength(1) == matrix.GetLength(1) + right.GetLength(1));
            var rowCount = matrix.GetLength(0);
            var columnsInitial = matrix.GetLength(1);
            var columnsOfConcat = right.GetLength(1);
            var columnCount = columnsInitial + columnsOfConcat;
            var result = new int[rowCount, columnCount];
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
        public static int[,] Stack(this int[,] matrix, int[] bottom)
        {
            Contract.Requires<ArgumentException>(matrix.GetLength(1) == bottom.Length);
            Contract.Ensures(Contract.Result<int[,]>().GetLength(0) == matrix.GetLength(0) + 1);
            var columnCount = bottom.Length;
            var rowCount = matrix.GetLength(0) + 1;
            var result = new int[rowCount, columnCount];
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
        public static int[,] Stack(this int[,] matrix, int[,] bottom)
        {
            Contract.Requires<ArgumentException>(matrix.GetLength(1) == bottom.GetLength(1));
            Contract.Ensures(Contract.Result<int[,]>().GetLength(0) == matrix.GetLength(0) + bottom.GetLength(0));

            var columnCount = matrix.GetLength(1);
            var rowsInitial = matrix.GetLength(0);
            var rowsOfConcat = bottom.GetLength(0);
            var rowCount = rowsInitial + rowsOfConcat;
            var result = new int[rowCount, columnCount];
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