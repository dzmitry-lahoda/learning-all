using MathNet.Numerics.LinearAlgebra.Double;
using MathNet.Numerics.LinearAlgebra.Generic;

namespace MPF.Blocks
{
    public static class VectorExtensions
    {
        public static Matrix<double> ToSingleRowMatrix(this  Vector vector)
        {
            var matrix = vector.CreateMatrix(1, vector.Count);
            matrix.SetRow(0, vector);
            return matrix;
        }
    }
}