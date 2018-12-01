using System;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading;

using MatrixExtensions.Generic;
using MatrixExtensionsGenericCodeBase;


namespace MatrixExtensions.Operations
{
    /// <summary>
    ///Provides extension methods for 2D numeric arrays.
    /// </summary>

    [Pure]
    public static class NumericArray2DSimpleOperationsExtensions
    {

        /// <summary>
        /// Returns the indices of the absolute maximum element.
        /// </summary>
        /// <param name="matrix"></param>
        /// <returns>The array with 2 indices of absolute maximum element.</returns>
        public static int[] MaxPosition(this NumericBroad[,] matrix)
        {
            Contract.Requires<ArgumentException>(matrix.RowCount() > 0);
            Contract.Requires<ArgumentException>(matrix.ColumnCount() > 0);
            Contract.Ensures(Contract.Result<int[]>()[0] >= 0);
            Contract.Ensures(Contract.Result<int[]>()[0] < matrix.RowCount());
            Contract.Ensures(Contract.Result<int[]>()[1] >= 0);
            Contract.Ensures(Contract.Result<int[]>()[1] < matrix.ColumnCount());
            
            var max = matrix[0, 0];
            int[] position = Do.Find2UsingBase(0, matrix.RowCount(), 0, matrix.ColumnCount(), max, 
                (r, c, @base) => matrix[r, c] > @base,(r, c) => matrix[r, c]);
            return position;
        }

        /// <summary>
        /// Adds a scalar to each element in the matrix and places the results into new matrix.
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="scalar">The scalar to add.</param>
        /// <returns>The matrix which stores results of the addition.</returns>
        public static NumericBroad[,] Add(this NumericBroad[,] matrix, NumericBroad scalar)
        {
            Contract.Ensures(Contract.Result<NumericBroad[,]>().RowCount() == matrix.RowCount());
            Contract.Ensures(Contract.Result<NumericBroad[,]>().ColumnCount() == matrix.ColumnCount());
            var rowCount = matrix.RowCount();
            var columnCount = matrix.ColumnCount();
            var newMatrix = new NumericBroad[rowCount, columnCount];
            Do.For2(0, rowCount, 0, columnCount,
                (r, c) => newMatrix[r, c] = matrix[r, c] + scalar);
            return newMatrix;
        }

        /// <summary>
        /// Substracts a scalar from each element of the matrix.
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="scalar">The scalar to substruct.</param>
        /// <returns>The matrix which stores the result of the substruction.</returns>
        public static NumericBroad[,] Substract(this NumericBroad[,] matrix, NumericBroad scalar)
        {
            return matrix.Add(-scalar);
        }

        /// <summary>
        /// Negates each element of the matrix.
        /// </summary>
        /// <returns>The new matrix which stores result of negation.</returns>
        public static NumericBroad[,] Negate(this NumericBroad[,] matrix)
        {
            Contract.Ensures(Contract.Result<NumericBroad[,]>().RowCount() == matrix.RowCount());
            Contract.Ensures(Contract.Result<NumericBroad[,]>().ColumnCount() == matrix.ColumnCount());
            var rowCount = matrix.RowCount();
            var columnCount = matrix.ColumnCount();
            var newMatrix = new NumericBroad[rowCount, columnCount];
            Do.For2(0, rowCount,0, columnCount,
                (r,c) => newMatrix[r, c] = -matrix[r, c]
                                                                );
            return newMatrix;
        }

        /// <summary>
        /// Multiplies each element of this matrix with a scalar.
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="scalar">The scalar to multiply with.</param>
        /// <returns>The multiplied matrix.</returns>
        public static NumericBroad[,] PointwiseMultiply(this NumericBroad[,] matrix, NumericBroad scalar)
        {
            Contract.Ensures(Contract.Result<NumericBroad[,]>().RowCount() == matrix.RowCount());
            Contract.Ensures(Contract.Result<NumericBroad[,]>().ColumnCount() == matrix.ColumnCount());
            var rowCount = matrix.RowCount();
            var columnCount = matrix.ColumnCount();
            var newMatrix = new NumericBroad[rowCount, columnCount];
            Do.For2(0, rowCount, 0, columnCount, 
                (r, c) =>newMatrix[r, c] = matrix[r, c] * scalar);
            return newMatrix;
        }

        /// <summary>
        /// Divides each element of current matrix by a scalar and returns the result.
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="scalar">The scalar to divide the matrix by.</param>
        /// <returns>The matrix with result of the division.</returns>
        public static NumericBroad[,] DivideEachElement(this NumericBroad[,] matrix, NumericBroad scalar)
        {
            Contract.Ensures(Contract.Result<NumericBroad[,]>().RowCount() == matrix.RowCount());
            Contract.Ensures(Contract.Result<NumericBroad[,]>().ColumnCount() == matrix.ColumnCount());
            var rowCount = matrix.RowCount();
            var columnCount = matrix.ColumnCount();
            var newMatrix = new NumericBroad[rowCount, columnCount];
            Do.For2(0, rowCount,0, columnCount, (r,c) => newMatrix[r, c] = matrix[r, c] / scalar);
            return newMatrix;
        }

        /// <summary>
        /// Finds sum of all elemtens in each matrix column.
        /// </summary>
        /// <param name="matrix"></param>
        /// <returns>The vector which contains sums.</returns>
        public static NumericBroad[] SumPerColumn(this NumericBroad[,] matrix)
        {
            Contract.Ensures(Contract.Result<NumericBroad[]>().Length == matrix.ColumnCount());
            var rowCount = matrix.RowCount();
            var columnCount = matrix.ColumnCount();
            var newVector = new NumericBroad[columnCount];
            Do.For2(0, rowCount,0, columnCount, (r,c) => newVector[c] += matrix[r, c]);
            return newVector;
        }

        /// <summary>
        /// Finds sum of all elemtens in each matrix row.
        /// </summary>
        /// <param name="matrix"></param>
        /// <returns>The vector which contains sums.</returns>
        public static NumericBroad[] SumPerRow(this NumericBroad[,] matrix)
        {
            Contract.Ensures(Contract.Result<NumericBroad[]>().Length == matrix.ColumnCount());
            var rowCount = matrix.RowCount();
            var columnCount = matrix.ColumnCount();
            var newVector = new NumericBroad[rowCount];
            Do.For2(0, rowCount,0, columnCount,(r, c) => newVector[r] += matrix[r, c]);
            return newVector;
        }



    }
}
