using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading;

using MatrixExtensions.Generic;
using MatrixExtensionsGenericCodeBase;

namespace MatrixExtensions.Conversion.Generic.Specialized
{
    /// <summary>
    /// Provides generic extension methods for 2D arrays wich accept <see cref="Func{T,TResult}"/>.
    /// </summary>
    [Pure]
    public static class GenericArray2DFuncConversionExtensions
    {

        /// <summary>
        /// Converts matrix's elements to jagged 2D matrix with other elements.
        /// </summary>
        /// <typeparam name="T">Source type.</typeparam>
        /// <typeparam name="TResult">Destination type.</typeparam>
        /// <param name="matrix"></param>
        /// <param name="converter">Conversion function.</param>
        /// <returns>The jagged 2D matrix which stores the result of the conversion.</returns>
        public static TResult[][] ToJagged<T, TResult>(this T[,] matrix, Func<T, TResult> converter)
        {
            Contract.Requires<ArgumentNullException>(converter != null);
            Contract.Ensures(Contract.Result<TResult[][]>().Length == matrix.RowCount());
            Contract.Ensures(Contract.Result<TResult[][]>()[0].Length == matrix.ColumnCount());
            var rowCount = matrix.RowCount();
            var columnCount = matrix.ColumnCount();
            var jaggedMatrix = new TResult[rowCount][];
            Do.For(0, jaggedMatrix.Length, i =>
                                                     {
                                                         jaggedMatrix[i] = new TResult[columnCount];
                                                     });

            Do.For(0, rowCount, r =>
                                      {
                                          Do.For(0, columnCount, c =>
                                                                       {
                                                                           jaggedMatrix[r][c] =
                                                                               converter.Invoke(matrix[r, c]);
                                                                       });
                                      });
            return jaggedMatrix;
        }

        /// <summary>
        /// Converts matrix's elements  to jagged 2D matrix with other elements.
        /// Second and third parameters of convertion function are outer and inner loops' indeces.
        /// </summary>
        /// <typeparam name="T">Source type.</typeparam>
        /// <typeparam name="TResult">Destination type.</typeparam>
        /// <param name="matrix"></param>
        /// <param name="converter">Conversion function. 
        ///  First number - outer loop index, second - inner.
        /// </param>
        /// <returns>The jagged 2D matrix which stores the result of the conversion.</returns>
        public static TResult[][] ToJagged<T, TResult>(this T[,] matrix, Func<T, int, int, TResult> converter)
        {
            Contract.Requires<ArgumentNullException>(converter != null);
            Contract.Ensures(Contract.Result<TResult[][]>().Length == matrix.RowCount());
            Contract.Ensures(Contract.Result<TResult[][]>()[0].Length == matrix.ColumnCount());
            var rowCount = matrix.RowCount();
            var columnCount = matrix.ColumnCount();
            var jaggedMatrix = new TResult[rowCount][];
            Do.For(0, jaggedMatrix.Length, i =>
                                                     {
                                                         jaggedMatrix[i] = new TResult[columnCount];
                                                     });

            Do.For(0, rowCount, r =>
                                      {
                                          Do.For(0, columnCount, c =>
                                                                       {
                                                                           jaggedMatrix[r][c] =
                                                                               converter.Invoke(matrix[r, c], r, c);
                                                                       });
                                      });
            return jaggedMatrix;
        }

        /// <summary>
        /// Converts matrix's elements to other using conversion function.
        /// </summary>
        /// <typeparam name="T">Source type.</typeparam>
        /// <typeparam name="TResult">Destination type.</typeparam>
        /// <param name="matrix"></param>
        /// <param name="converter">Conversion function.</param>
        /// <returns>The matrix which stores the result of the conversion.</returns>
        public static TResult[,] ToOther<T, TResult>(this T[,] matrix, Func<T, TResult> converter)
        {
            Contract.Requires<ArgumentNullException>(converter != null);
            Contract.Ensures(Contract.Result<TResult[,]>().RowCount() == matrix.RowCount());
            Contract.Ensures(Contract.Result<TResult[,]>().ColumnCount() == matrix.ColumnCount());
            var rowCount = matrix.RowCount();
            var columnCount = matrix.ColumnCount();
            var newMatrix = new TResult[rowCount, columnCount];

            Do.For(0, rowCount, r =>
                                      {
                                          Do.For(0, columnCount, c =>
                                                                       {
                                                                           newMatrix[r, c] =
                                                                               converter.Invoke(matrix[r, c]);
                                                                       });
                                      });
            return newMatrix;
        }

        /// <summary>
        /// Converts matrix's elements to other using conversion function.
        /// Second and third parameters of convertion function are outer and inner loops' indeces.
        /// </summary>
        /// <typeparam name="T">Source type.</typeparam>
        /// <typeparam name="TResult">Destination type.</typeparam>
        /// <param name="matrix"></param>
        /// <param name="converter">Conversion rule with usage of element indices. 
        ///  First number- outer loop index, second - inner.
        /// </param>
        /// <returns>The matrix which stores the result of the conversion.</returns>
        public static TResult[,] ToOther<T, TResult>(this T[,] matrix, Func<T, int, int, TResult> converter)
        {
            Contract.Requires<ArgumentNullException>(converter != null);
            Contract.Ensures(Contract.Result<TResult[,]>().RowCount() == matrix.RowCount());
            Contract.Ensures(Contract.Result<TResult[,]>().ColumnCount() == matrix.ColumnCount());
            var rowCount = matrix.RowCount();
            var columnCount = matrix.ColumnCount();
            var newMatrix = new TResult[rowCount, columnCount];

            Do.For2(0, rowCount, 0, columnCount,
                (r, c) => newMatrix[r, c] = converter.Invoke(matrix[r, c], r, c));

            return newMatrix;
        }

    }
}