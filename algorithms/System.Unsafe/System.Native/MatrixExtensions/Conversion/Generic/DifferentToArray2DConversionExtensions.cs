using System;
using System.Net;
using System.Diagnostics.Contracts;
using System.Collections.Generic;

using MatrixExtensions.Generic;
using MatrixExtensionsGenericCodeBase;

namespace MatrixExtensions.Conversion.Generic
{
    /// <summary>
    /// Provides generic extension methods for converting <see cref="List{List{T}}"/> to 2D arrays.
    /// </summary>
    [Pure]
    public static class DifferentToArray2DConversionExtensions
    {

        /// <summary>
        /// Converts list of lists to matrix.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="listOfUniformLenghtLists">List containf lists with equal lenght.</param>
        /// <returns>The matrix which stores the result of the conversion.</returns>
        public static T[,] ToArray2D<T>(this List<List<T>> listOfUniformLenghtLists)
        {
            Contract.Requires<ArgumentException>(listOfUniformLenghtLists.Count>0);
            Contract.Requires<ArgumentException>(listOfUniformLenghtLists[0] !=null);
            Contract.Ensures(Contract.Result<T[,]>().RowCount() == listOfUniformLenghtLists.Count);
            Contract.Ensures(Contract.Result<T[,]>().ColumnCount() == listOfUniformLenghtLists[0].Count);

            var rowCount = listOfUniformLenghtLists.Count;
            var columnCount = listOfUniformLenghtLists[0].Count;
            var matrix = new T[rowCount,columnCount];
            Do.For(0, rowCount, r =>
                                      {
                                          var row = listOfUniformLenghtLists[r];
                                          Do.For(0, columnCount, c =>
                                                                       {
                                                                           matrix[r,c] = row[c];
                                                                       });
                                      });
            return matrix;
        }
    
    }
}
