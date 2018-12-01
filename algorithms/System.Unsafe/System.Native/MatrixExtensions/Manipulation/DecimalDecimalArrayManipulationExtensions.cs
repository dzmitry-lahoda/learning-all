using System;
using System.Diagnostics.Contracts;
using System.Threading;

using MatrixExtensions.Generic;
using MatrixExtensionsGenericCodeBase;

namespace MatrixExtensions.Manipulation
{
    /// <summary>
    /// Provides extension methods for concatenation of 1D numeric arrays.
    /// </summary>
    [Pure]
    public static class DecimalDecimalArrayManipulationExtensions
    {

        /// <summary>
        /// Appends to right the other to current.
        /// Vector considered to be a row.
        /// </summary>
        /// <example><pre>
        /// 1, 2
        /// append       we get 1, 2, 7, 8, 9
        /// 7, 8, 9
        /// </pre></example>
        /// <param name="vector"></param>
        /// <param name="right"></param>
        /// <returns>The new longer vector.</returns>
        public static decimal[] Append(this decimal[] vector, decimal[] right)
        {
            Contract.Requires<ArgumentNullException>(vector != null);
            Contract.Ensures(Contract.Result<decimal[]>().Length == vector.Length + right.Length);
            var appendLength = right.Length;
            if (appendLength == 0)
            {
                return (decimal[])vector.Clone();
            }
            var length = vector.Length;
            var newLength = length + appendLength;
            var newVector = new decimal[newLength];
            Do.Invoke(
                () => Do.For(0, length, i => newVector[i] = vector[i]),
                () => Do.For(length, newLength, i => newVector[i] = right[i - length])
                );
            return newVector;
        }

        /// <summary>
        /// Stacks to bottom the new vector to current.
        /// Vector considered to be a row.
        /// </summary>
        /// <example><pre>
        /// 1, 2  3
        /// stack         we get      1, 2, 3
        /// 7, 8, 9                   7, 8, 9
        /// </pre></example>
        /// <param name="vector"></param>
        /// <param name="bottom"></param>
        /// <returns>The new matrix with 2 rows.</returns>
        public static decimal[,] Stack(this decimal[] vector, decimal[] bottom)
        {
            Contract.Requires<ArgumentNullException>(bottom != null, "bottom");
            Contract.Requires<ArgumentException>(vector.Length == bottom.Length, "bottom.Length");
            Contract.Ensures(Contract.Result<decimal[,]>().GetLength(1) == vector.Length);
            Contract.Ensures(Contract.Result<decimal[,]>().GetLength(0) == 2);
            var length = vector.Length;
            var newMatrix = new decimal[2, length];
            if (length == 0)
            {
                return newMatrix;
            }
            Do.Invoke(
                () => Do.For(0, length, c => newMatrix[0, c] = vector[c]),
                () => Do.For(0, length, c => newMatrix[1, c] = bottom[c])
                );
            return newMatrix;
        }

        /// <summary>
        /// Appends to rigth the matrix to current vector.
        /// Vector considered to be a column.
        /// </summary>
        /// <example><pre>
        /// 1
        /// 2
        /// append          we get 1, 3, 4, 5
        /// 3, 4, 5                2, 6, 7, 8
        /// 6, 7, 8
        /// </pre></example>
        /// <param name="vector"></param>
        /// <param name="right"></param>
        /// <returns>The new matrix.</returns>
        public static decimal[,] Append(this decimal[] vector, decimal[,] right)
        {
            Contract.Requires<ArgumentNullException>(right != null, "right");
            Contract.Requires<ArgumentException>(vector.Length == right.GetLength(0), "right.GetLength(0)");
            Contract.Ensures(Contract.Result<decimal[,]>().GetLength(1) == right.GetLength(1) + 1);
            Contract.Ensures(Contract.Result<decimal[,]>().GetLength(0) == right.GetLength(0));
            var rowCount = vector.Length;
            var columnCount = right.GetLength(1) + 1;
            var result = new decimal[rowCount, columnCount];
            Do.Invoke(
            () => Do.For2(0, rowCount, 1, columnCount, (r, c) => result[r, c] = right[r, c - 1]),
            () => Do.For(0, rowCount, r => result[r, 0] = vector[r]));
            return result;
        }

        /// <summary>
        /// Stacks to bottom the matrix to current vector.
        /// Vector considered to be a row.
        /// </summary>
        /// <example><pre>
        /// 1, 2, 3                 1, 2, 3              
        /// append          we get  4, 5, 6
        /// 4, 5, 6                 7, 8, 9
        /// 7, 8, 9             
        /// </pre></example>
        /// <param name="vector"></param>
        /// <param name="bottom"></param>
        /// <returns>The new matrix.</returns>
        public static decimal[,] Stack(this decimal[] vector, decimal[,] bottom)
        {
            Contract.Requires<ArgumentNullException>(bottom != null, "bottom");
            Contract.Requires<ArgumentException>(vector.Length == bottom.GetLength(1), "bottom.GetLength(1)");
            Contract.Ensures(Contract.Result<decimal[,]>().GetLength(1) == bottom.GetLength(1));
            Contract.Ensures(Contract.Result<decimal[,]>().GetLength(0) == bottom.GetLength(0) + 1);
            var columntCount = vector.Length;
            var rowCount = bottom.GetLength(0) + 1;
            var result = new decimal[rowCount, columntCount];
            Do.Invoke(
            () => Do.For2(1, rowCount, 0, columntCount, (r, c) => result[r, c] = bottom[r - 1, c]),
            () => Do.For(0, columntCount, c => result[0, c] = vector[c])
            );
            return result;
        }


    }
}
