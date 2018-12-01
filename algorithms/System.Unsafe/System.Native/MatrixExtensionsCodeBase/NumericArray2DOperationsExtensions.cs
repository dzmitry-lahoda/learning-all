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
    public static class NumericArray2DOperationsExtensions
    {

        /// <summary>
        /// Adds another matrix to this matrix.
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="other">The matrix to add to this matrix.</param>
        /// <returns>The matrix which stores the result of the addition.</returns>
        public static NumericBroad[,] Add(this NumericBroad[,] matrix, NumericNarrow[,] other)
        {
            Contract.Requires<ArgumentNullException>(other != null);
            Contract.Requires<ArgumentException>(matrix.RowCount() == other.RowCount());
            Contract.Requires<ArgumentException>(matrix.ColumnCount() == other.ColumnCount());
            Contract.Ensures(Contract.Result<NumericBroad[,]>().ColumnCount() == matrix.ColumnCount());
            Contract.Ensures(Contract.Result<NumericBroad[,]>().RowCount()  == matrix.RowCount());
            var rowCount = matrix.RowCount();
            var columnCount = matrix.ColumnCount();
            var newMatrix = new NumericBroad[rowCount,columnCount];
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
        public static NumericBroad[,] Substract(this NumericBroad[,] matrix, NumericNarrow[,] other)
        {
            Contract.Requires<ArgumentNullException>(other != null);
            Contract.Requires<ArgumentException>(matrix.RowCount() == other.RowCount());
            Contract.Requires<ArgumentException>(matrix.ColumnCount() == other.ColumnCount());
            Contract.Ensures(Contract.Result<NumericBroad[,]>().ColumnCount() == matrix.ColumnCount());
            Contract.Ensures(Contract.Result<NumericBroad[,]>().RowCount() == matrix.RowCount());
            var rowCount = matrix.RowCount();
            var columnCount = matrix.ColumnCount();
            var newMatrix = new NumericBroad[rowCount, columnCount];
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
        public static NumericBroad[,] PointwiseMultiply(this NumericBroad[,] matrix, NumericNarrow[,] other)
        {
            Contract.Requires<ArgumentNullException>(other != null);
            Contract.Requires<ArgumentException>(matrix.RowCount() == other.RowCount());
            Contract.Requires<ArgumentException>(matrix.ColumnCount() == other.ColumnCount());
            Contract.Ensures(Contract.Result<NumericBroad[,]>().ColumnCount() == matrix.ColumnCount());
            Contract.Ensures(Contract.Result<NumericBroad[,]>().RowCount() == matrix.RowCount());
            var rowCount = matrix.RowCount();
            var columnCount = matrix.ColumnCount();
            var newMatrix = new NumericBroad[rowCount, columnCount];
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
        public static NumericBroad[,] Multiply(this NumericBroad[,] matrix, NumericNarrow[,] other)
        {
            Contract.Requires<ArgumentNullException>(other != null);
            Contract.Requires<ArgumentException>(matrix.ColumnCount() == other.RowCount());
            Contract.Ensures(Contract.Result<NumericBroad[,]>().RowCount() == matrix.RowCount());
            Contract.Ensures(Contract.Result<NumericBroad[,]>().ColumnCount() == other.ColumnCount());
            var rowCount = matrix.RowCount();
            var columnCount = other.ColumnCount();
            var sumCount = matrix.ColumnCount();
            var newMatrix = new NumericBroad[rowCount, columnCount];
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
        public static NumericBroad[] Multiply(this NumericBroad[,] matrix, NumericNarrow[] other)
        {
            Contract.Requires<ArgumentNullException>(other != null);
            Contract.Requires<ArgumentException>(matrix.ColumnCount() == other.Length);
            Contract.Ensures(Contract.Result<NumericBroad[]>().Length == matrix.RowCount());
            var rowCount = matrix.RowCount();
            var sumCount = matrix.ColumnCount();
            var newVector = new NumericBroad[rowCount];
            Do.For2(0, rowCount, 0, sumCount,
                (r, i) =>newVector[r] += matrix[r, i] * other[i]);
            return newVector;
        }
    }
}