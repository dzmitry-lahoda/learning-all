using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading;

using MatrixExtensionsGenericCodeBase;

namespace MatrixExtensions.Generic
{
    /// <summary>
    /// Provides generic extension methods for 2D arrays.
    /// </summary>
    [Pure]
    public static class GenericArray2DExtensions
    {

        /// <summary>
        /// Retruns true if matrix is empty.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="matrix"></param>
        /// <returns></returns>
        public static bool IsEmpty<T>(this T[,] matrix)
        {
            return matrix.Length == 0;
        }

        /// <summary>
        /// Gets number of columns in 2D array.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="matrix"></param>
        /// <returns></returns>
        public static int RowCount<T>(this T[,] matrix)
        {
            return matrix.GetLength(0);
        }

        /// <summary>
        /// Gets number of rows in 2D array.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="matrix"></param>
        /// <returns></returns>
        public static int ColumnCount<T>(this T[,] matrix)
        {
            return matrix.GetLength(1);
        }

        /// <summary>
        /// Copies a row into a new vector.
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="rowIndex0">The row to copy.</param>
        /// <returns>A vector containing the copied elements.</returns>
        public static T[] GetRow<T>(this T[,] matrix, int rowIndex0)
        {
            Contract.Requires<ArgumentOutOfRangeException>(rowIndex0 >= 0);
            Contract.Requires<ArgumentOutOfRangeException>(rowIndex0 < matrix.RowCount());
            Contract.Ensures(Contract.Result<T[]>().Length == matrix.ColumnCount());
            var columnCount = matrix.ColumnCount();
            var vector = new T[columnCount];
            Do.For(0, columnCount, c => vector[c] = matrix[rowIndex0, c]);
            return vector;
        }

        /// <summary>
        /// Copies a column  of matrix into a new vector.
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="columnIndex0">The column to copy.</param>
        /// <returns>A vector containing the copied elements.</returns>
        public static T[] GetColumn<T>(this T[,] matrix, int columnIndex0)
        {
            Contract.Requires<ArgumentOutOfRangeException>(columnIndex0 >= 0);
            Contract.Requires<ArgumentOutOfRangeException>(columnIndex0 < matrix.ColumnCount());
            Contract.Ensures(Contract.Result<T[]>().Length == matrix.RowCount());
            var rowCount = matrix.RowCount();
            var vector = new T[rowCount];
            Do.For(0, rowCount, r =>vector[r] = matrix[r, columnIndex0]);
            return vector;
        }

        /// <summary>
        /// Copies last column of matrix into a new vector.
        /// </summary>
        /// <param name="matrix"></param>
        /// <returns>A vector containing copied elements.</returns>
        public static T[] GetLastColumn<T>(this T[,] matrix)
        {
            var vector = matrix.GetColumn(matrix.ColumnCount() - 1);
            return vector;
        }

        /// <summary>
        /// Creates a matrix that contains the values from the requested sub-matrix.
        /// </summary>
        /// <param name="rowIndex0">The row to start copying from.</param>
        /// <param name="rowLength">The number of rows to copy. Must be positive.</param>
        /// <param name="columnIndex0">The column to start copying from.</param>
        /// <param name="columnLength">The number of columns to copy. Must be positive.</param>
        public static T[,] GetSubMatrix<T>(this T[,] matrix, int rowIndex0, int rowLength, int columnIndex0, int columnLength)
        {

            #region Contracts
            Contract.Requires<ArgumentOutOfRangeException>(rowIndex0 >= 0);
            Contract.Requires<ArgumentOutOfRangeException>(columnIndex0 >= 0);
            Contract.Requires<ArgumentOutOfRangeException>(rowIndex0 < matrix.RowCount());
            Contract.Requires<ArgumentOutOfRangeException>(columnIndex0 < matrix.ColumnCount());
            Contract.Requires<ArgumentOutOfRangeException>(rowLength >= 0);
            Contract.Requires<ArgumentOutOfRangeException>(columnLength >= 0);
            Contract.Requires<ArgumentOutOfRangeException>(rowIndex0 + rowLength <= matrix.RowCount());
            Contract.Requires<ArgumentOutOfRangeException>(columnIndex0 + columnLength <= matrix.ColumnCount());
            #endregion

            var subMatrix = new T[rowLength, columnLength];

            Do.For2(
                rowIndex0, rowIndex0 + rowLength,columnIndex0, columnIndex0 + columnLength, 
                (r,c) => subMatrix[r - rowIndex0, c - columnIndex0] = matrix[r, c]);

            return subMatrix;
        }

        /// <summary>
        /// Returns an <see cref="IEnumerable{T}"/> that enumerates over the matrix rows.
        /// </summary>
        /// <returns>An <see cref="IEnumerable{T}"/> that enumerates over the matrix rows</returns>
        public static IEnumerable<KeyValuePair<int, T[]>> GetRowEnumeration<T>(this T[,] matrix)
        {
            for (var i = 0; i < matrix.RowCount(); i++)
            {
                yield return new KeyValuePair<int, T[]>(i, matrix.GetRow(i));
            }
        }

        /// <summary>
        /// Returns an <see cref="IEnumerable{T}"/> that enumerates over the matrix columns.
        /// </summary>
        /// <returns>An <see cref="IEnumerable{T}"/> that enumerates over the matrix columns</returns>
        public static IEnumerable<KeyValuePair<int, T[]>> GetColumnEnumeration<T>(this T[,] matrix)
        {
            for (var i = 0; i < matrix.ColumnCount(); i++)
            {
                yield return new KeyValuePair<int, T[]>(i, matrix.GetColumn(i));
            }
        }

        /// <summary>
        /// Returns list of lists with elements of <paramref name="matrix"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="matrix">Matrix to translate into list of lists.</param>
        /// <returns>New list of list with elements from matrix.</returns>
        public static List<List<T>> ToListOfLists<T>(this T[,] matrix)
        {
            var listOfLists = new List<List<T>>();
            for (var i = 0; i < matrix.RowCount(); i++)
            {
                listOfLists.Add(new List<T>());
                for (var j = 0; j < matrix.ColumnCount(); j++)
                {
                    listOfLists[i].Add(matrix[i, j]);
                }
            }
            return listOfLists;
        }

   }
}