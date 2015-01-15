using System;
using MathNet.Numerics.LinearAlgebra.Double;
using MathNet.Numerics.LinearAlgebra.Generic;

namespace Blocks.Extensions
{
    public static class MatrixExtensions
    {
        public static Vector<double> ToRowWiseVector(this Matrix<double> matrix)
        {
            if (matrix is DenseMatrix)
            {
                var array = matrix.ToRowWiseArray();
                var vector = new DenseVector(array);
                return vector;
            }
            throw new NotImplementedException();
            
        }

        public static Matrix<double> Shift(this Matrix<double> matrix, int shiftColumns)
        {
            var addMatrix = new DenseMatrix(matrix.RowCount, shiftColumns, Double.NaN);
            var newMatrix = addMatrix.Append(matrix);
            newMatrix = newMatrix.SubMatrix(0, matrix.RowCount, 0, matrix.ColumnCount);
            return newMatrix;
        }

        public static Matrix<double> RemoveRow(this Matrix<double> matrix, int index)
        {
            var newMatrix = matrix.Clone();
            var columns = newMatrix.ColumnCount;
            var rows = newMatrix.RowCount;
            Matrix<double> beforeRow = newMatrix.SubMatrix(0, index - 1, 0, columns);
            Matrix<double> afterRow = newMatrix.SubMatrix(index + 1, rows - index, 0, columns);
            newMatrix = beforeRow.Stack(afterRow);
            return newMatrix;

        }

        public static bool IsQuadratic(this Matrix<double> matrix)
        {
            var isQuadratic = matrix.ColumnCount == matrix.RowCount;
            return isQuadratic;
        }

        public static bool IsDiagonalSymmetric(this Matrix<double> matrix)
        {
            return true;
        }

        /// <summary>
        /// Compares two matrices for near equality by checking that corresponding matrix entries are near equal.
        /// </summary>
        public static bool NearEquals(this Matrix<double> matrix, Matrix<double> other)
        {
            // Reject equality when the argument is null or has a different shape.
            if (other == null)
            {
                return false;
            }
            if (matrix.ColumnCount != other.ColumnCount || matrix.RowCount != other.RowCount)
            {
                return false;
            }

            // Accept if the argument is the same object as this.
            if (ReferenceEquals(matrix, other))
            {
                return true;
            }
            // If all else fails, perform element wise comparison.
            for (var i = 0; i < matrix.RowCount; i++)
            {
                for (var j = 0; j < matrix.ColumnCount; j++)
                {

                    if (!DefaultComparing.NearEqual(matrix[i, j], other[i, j]))
                    {
                        return false;
                    }
                }

            }

                return true;
            }

        }
    
}