using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using Blocks;
using dnHTM;
using MathNet.Numerics.LinearAlgebra.Double;

namespace dnHTMTests
{
    public static class TestsHelper
    {
        public static DenseMatrix OneElementTransitionMatrix;

        public static DenseMatrix TwoElementTransitionMatrix;

        public static DenseMatrix ThreeElementTransitionMatrix;

        static TestsHelper()
        {
            Contract.Ensures(FirstOrderMarkovChain.IsValidTransitionMatrix(OneElementTransitionMatrix));
            Contract.Ensures(FirstOrderMarkovChain.IsValidTransitionMatrix(TwoElementTransitionMatrix));
            Contract.Ensures(FirstOrderMarkovChain.IsValidTransitionMatrix(ThreeElementTransitionMatrix));

            OneElementTransitionMatrix = new DenseMatrix(1);
            OneElementTransitionMatrix[0, 0] = 1;

            TwoElementTransitionMatrix = new DenseMatrix(2,2);
            TwoElementTransitionMatrix.SetRow(0, new double[] { 0, 1 });
            TwoElementTransitionMatrix.SetRow(1, new double[] { 1, 0 });

            ThreeElementTransitionMatrix = new DenseMatrix(3,3);
            ThreeElementTransitionMatrix.SetRow(0, new double[] { 0, 0.7, 0.3});
            ThreeElementTransitionMatrix.SetRow(1, new double[] { 0.5, 0 , 0.5});
            ThreeElementTransitionMatrix.SetRow(2, new double[] { 0, 0, 1 });
        }
    }
}
