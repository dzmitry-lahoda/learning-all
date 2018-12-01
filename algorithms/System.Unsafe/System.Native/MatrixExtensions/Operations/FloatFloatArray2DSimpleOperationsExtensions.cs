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
    public static class FloatFloatArray2DSimpleOperationsExtensions
    {

        /// <summary>
        /// Returns the indeces of the absolute maximum element.
        /// </summary>
        /// <param name="matrix"></param>
        /// <returns>The array with 2 indices of absolute maximum element.</returns>
        public static int[] MaxPosition(this float[,] matrix)
        {
            Contract.Requires<ArgumentException>(matrix.GetLength(0) > 0);
            Contract.Requires<ArgumentException>(matrix.GetLength(1) > 0);
            Contract.Ensures(Contract.Result<int[]>()[0] >= 0);
            Contract.Ensures(Contract.Result<int[]>()[0] < matrix.GetLength(0));
            Contract.Ensures(Contract.Result<int[]>()[1] >= 0);
            Contract.Ensures(Contract.Result<int[]>()[1] < matrix.GetLength(1));
            
            var max = matrix[0, 0];
            int[] position = Do.Find2UsingBase(0, matrix.GetLength(0), 0, matrix.GetLength(1), max, 
                (r, c, @base) => matrix[r, c] > @base,(r, c) => matrix[r, c]);
            return position;
        }

        /// <summary>
        /// Adds a scalar to each element in the matrix and places the results into new matrix.
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="scalar">The scalar to add.</param>
        /// <returns>The matrix which stores results of the addition.</returns>
        public static float[,] Add(this float[,] matrix, float scalar)
        {
            Contract.Ensures(Contract.Result<float[,]>().GetLength(0) == matrix.GetLength(0));
            Contract.Ensures(Contract.Result<float[,]>().GetLength(1) == matrix.GetLength(1));
            var rowCount = matrix.GetLength(0);
            var columnCount = matrix.GetLength(1);
            var newMatrix = new float[rowCount, columnCount];
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
        public static float[,] Substract(this float[,] matrix, float scalar)
        {
            return matrix.Add(-scalar);
        }

        /// <summary>
        /// Negates each element of the matrix.
        /// </summary>
        /// <returns>The new matrix which stores result of negation.</returns>
        public static float[,] Negate(this float[,] matrix)
        {
            Contract.Ensures(Contract.Result<float[,]>().GetLength(0) == matrix.GetLength(0));
            Contract.Ensures(Contract.Result<float[,]>().GetLength(1) == matrix.GetLength(1));
            var rowCount = matrix.GetLength(0);
            var columnCount = matrix.GetLength(1);
            var newMatrix = new float[rowCount, columnCount];
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
        public static float[,] PointwiseMultiply(this float[,] matrix, float scalar)
        {
            Contract.Ensures(Contract.Result<float[,]>().GetLength(0) == matrix.GetLength(0));
            Contract.Ensures(Contract.Result<float[,]>().GetLength(1) == matrix.GetLength(1));
            var rowCount = matrix.GetLength(0);
            var columnCount = matrix.GetLength(1);
            var newMatrix = new float[rowCount, columnCount];
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
        public static float[,] DivideEachElement(this float[,] matrix, float scalar)
        {
            Contract.Ensures(Contract.Result<float[,]>().GetLength(0) == matrix.GetLength(0));
            Contract.Ensures(Contract.Result<float[,]>().GetLength(1) == matrix.GetLength(1));
            var rowCount = matrix.GetLength(0);
            var columnCount = matrix.GetLength(1);
            var newMatrix = new float[rowCount, columnCount];
            Do.For2(0, rowCount,0, columnCount, (r,c) => newMatrix[r, c] = matrix[r, c] / scalar);
            return newMatrix;
        }

        /// <summary>
        /// Finds sum of all elemtens in each matrix column.
        /// </summary>
        /// <param name="matrix"></param>
        /// <returns>The vector which contains sums.</returns>
        public static float[] SumPerColumn(this float[,] matrix)
        {
            Contract.Ensures(Contract.Result<float[]>().Length == matrix.GetLength(1));
            var rowCount = matrix.GetLength(0);
            var columnCount = matrix.GetLength(1);
            var newVector = new float[columnCount];
            Do.For2(0, rowCount,0, columnCount, (r,c) => newVector[c] += matrix[r, c]);
            return newVector;
        }

        /// <summary>
        /// Finds sum of all elemtens in each matrix row.
        /// </summary>
        /// <param name="matrix"></param>
        /// <returns>The vector which contains sums.</returns>
        public static float[] SumPerRow(this float[,] matrix)
        {
            Contract.Ensures(Contract.Result<float[]>().Length == matrix.GetLength(1));
            var rowCount = matrix.GetLength(0);
            var columnCount = matrix.GetLength(1);
            var newVector = new float[rowCount];
            Do.For2(0, rowCount,0, columnCount,(r, c) => newVector[r] += matrix[r, c]);
            return newVector;
        }



    }
}
