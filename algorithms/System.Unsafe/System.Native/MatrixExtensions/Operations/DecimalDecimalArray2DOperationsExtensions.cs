using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading;

using MatrixExtensions.Generic;
using MatrixExtensionsGenericCodeBase;

namespace MatrixExtensions.Operations
{
    /// <summary>
    /// Provides extension methods for 2D numeric arrays.
    /// </summary>

    [Pure]
    public static class DecimalDecimalArray2DOperationsExtensions
    {

        /// <summary>
        /// Adds another matrix to this matrix.
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="other">The matrix to add to this matrix.</param>
        /// <returns>The matrix which stores the result of the addition.</returns>
        public static decimal[,] Add(this decimal[,] matrix, decimal[,] other)
        {
            Contract.Requires<ArgumentNullException>(other != null);
            Contract.Requires<ArgumentException>(matrix.GetLength(0) == other.GetLength(0));
            Contract.Requires<ArgumentException>(matrix.GetLength(1) == other.GetLength(1));
            Contract.Ensures(Contract.Result<decimal[,]>().GetLength(1) == matrix.GetLength(1));
            Contract.Ensures(Contract.Result<decimal[,]>().GetLength(0)  == matrix.GetLength(0));
            var rowCount = matrix.GetLength(0);
            var columnCount = matrix.GetLength(1);
            var newMatrix = new decimal[rowCount,columnCount];
            Do.For2(0, rowCount,0, columnCount, 
                (i,j) => newMatrix[i, j] = matrix[i, j] + other[i, j]);
            return newMatrix;
        }


        /// <summary>
        /// Subtracts another matrix from this matrix.
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="other">The matrix to subtract.</param>
        /// <returns>The matrix which stores the result of the subtraction.</returns>
        public static decimal[,] Substract(this decimal[,] matrix, decimal[,] other)
        {
            Contract.Requires<ArgumentNullException>(other != null);
            Contract.Requires<ArgumentException>(matrix.GetLength(0) == other.GetLength(0));
            Contract.Requires<ArgumentException>(matrix.GetLength(1) == other.GetLength(1));
            Contract.Ensures(Contract.Result<decimal[,]>().GetLength(1) == matrix.GetLength(1));
            Contract.Ensures(Contract.Result<decimal[,]>().GetLength(0) == matrix.GetLength(0));
            var rowCount = matrix.GetLength(0);
            var columnCount = matrix.GetLength(1);
            var newMatrix = new decimal[rowCount, columnCount];
            Do.For(0, rowCount, i =>
            {
                Do.For(0, columnCount, j => newMatrix[i, j] = matrix[i, j] - other[i, j]);

            });
            return newMatrix;
        }



        /// <summary>
        /// Pointwise multiplies this matrix with another matrix and stores the result into new matrix.
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="other">The matrix to pointwise multiply with this one.</param>
        /// <returns>The matrix which stores the result of the pointwise multiplication.</returns>
        public static decimal[,] PointwiseMultiply(this decimal[,] matrix, decimal[,] other)
        {
            Contract.Requires<ArgumentNullException>(other != null);
            Contract.Requires<ArgumentException>(matrix.GetLength(0) == other.GetLength(0));
            Contract.Requires<ArgumentException>(matrix.GetLength(1) == other.GetLength(1));
            Contract.Ensures(Contract.Result<decimal[,]>().GetLength(1) == matrix.GetLength(1));
            Contract.Ensures(Contract.Result<decimal[,]>().GetLength(0) == matrix.GetLength(0));
            var rowCount = matrix.GetLength(0);
            var columnCount = matrix.GetLength(1);
            var newMatrix = new decimal[rowCount, columnCount];
            Do.For2(0, rowCount, 0, columnCount, (i,j) => newMatrix[i, j] = matrix[i, j] * other[i, j]);
            return newMatrix;
        }

        /// <summary>
        /// Multiplies two matrices.
        /// </summary>
        /// <see href="http://en.wikipedia.org/wiki/Matrix_multiplication"/>
        /// <param name="matrix"></param>
        /// <param name="other">Matrix to multiply.</param>
        /// <returns>The result of multiplication.</returns>
        public static decimal[,] Multiply(this decimal[,] matrix, decimal[,] other)
        {
            Contract.Requires<ArgumentNullException>(other != null);
            Contract.Requires<ArgumentException>(matrix.GetLength(1) == other.GetLength(0));
            Contract.Ensures(Contract.Result<decimal[,]>().GetLength(0) == matrix.GetLength(0));
            Contract.Ensures(Contract.Result<decimal[,]>().GetLength(1) == other.GetLength(1));
            var rowCount = matrix.GetLength(0);
            var columnCount = other.GetLength(1);
            var sumCount = matrix.GetLength(1);
            var newMatrix = new decimal[rowCount, columnCount];
            Do.For2(0, rowCount,0, columnCount,
                (r,c) =>{
                                                                   Do.For(0, sumCount, i =>
                                                                  {
                                                                      newMatrix[r, c] += matrix[r, i] * other[i, c];
                                                                  });
                                                               });
            return newMatrix;
        }

        /// <summary>
        /// Multiplies current matrix with vector.
        /// </summary>
        /// <see href="http://en.wikipedia.org/wiki/Matrix_multiplication"/>
        /// <param name="matrix"></param>
        /// <param name="other">Vector to multiply.</param>
        /// <returns>The new vector which contains result of multiplication.</returns>
        public static decimal[] Multiply(this decimal[,] matrix, decimal[] other)
        {
            Contract.Requires<ArgumentNullException>(other != null);
            Contract.Requires<ArgumentException>(matrix.GetLength(1) == other.Length);
            Contract.Ensures(Contract.Result<decimal[]>().Length == matrix.GetLength(0));
            var rowCount = matrix.GetLength(0);
            var sumCount = matrix.GetLength(1);
            var newVector = new decimal[rowCount];
            Do.For2(0, rowCount, 0, sumCount,
                (r, i) =>newVector[r] += matrix[r, i] * other[i]);
            return newVector;
        }
    }
}