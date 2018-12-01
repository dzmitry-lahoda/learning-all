using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using Microsoft.VisualStudio.TestTools.UnitTesting;

using MatrixExtensions.Manipulation.Generic;
using MatrixExtensions.Generic;
using MatrixExtensions.Equality;

namespace MatrixExtensionsTestProject
{
     [TestClass]
    public class GenericArray2DManipulationExtensionsTest
    {
        [TestMethod]
        public void TransposeTest()
        {
            //given a matrix
            var matrix = new[,] { { 1, 2 },
                                  { 2, 2 },
                                  { 4, 3d } };
            //we can transpose matrix
            var actual = matrix.Transpose();

            var expected = new[,] {{ 4, 2, 1},
                                    {3d, 2, 2}};
            Assert.IsTrue(expected.EqualsElements(actual));
        }

        [TestMethod]
        public void TransposeOneColumnMatrixTest()
        {
            //given a one column matrix
            var matrix = new[,] { { 1},
                                  { 2 },
                                  { 4 } };
            //we can transpose matrix
            var actual = matrix.Transpose();

            var expected = new[,] {{ 4, 2, 1}};
            Assert.IsTrue(expected.EqualsElements(actual));
        }

    }
}
