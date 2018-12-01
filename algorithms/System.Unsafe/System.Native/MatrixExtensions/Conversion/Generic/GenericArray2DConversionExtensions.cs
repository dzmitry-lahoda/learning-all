using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading;

using MatrixExtensionsGenericCodeBase;

namespace MatrixExtensions.Conversion.Generic
{
    /// <summary>
    /// Provides generic extension methods for 2D arrays conversion.
    /// </summary>
    [Pure]
    public static class GenericArray2DConversionExtensions
    {
                
        /// <summary>
        /// Converts 2D array to jagged 2D matrix.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="matrix"></param>
        /// <returns></returns>
        public static T[][] ToJagged<T>(this T[,] matrix)
        {
            var rowCount = matrix.GetLength(0);
            var columnCount = matrix.GetLength(1);
            var jagged = new T[rowCount][];
            Do.For(0, rowCount, r => jagged[r] = new T[columnCount]);
            Do.For2(0, rowCount, 0, columnCount, (r,c) => jagged[r][c] = matrix[r, c]);
            return jagged;
        }
    }
}