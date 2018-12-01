using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using MatrixExtensions.Equality;
using MatrixExtensions.Operations;

namespace MatrixExtensionsTestProject
{
    [TestClass]
    public class Array2DSimpleOperationsExtensionsTest
    {
        [TestMethod]
        public void MaxPositionTest()
        {
            //given a matrix
            var matrix = new[,] {{ 1m, 2.5m, 2.7m },
                                 { 2, 3m,     4 }};
            //we can get position of maximal value
            var actual = matrix.MaxPosition();

            var expected = new[] {1, 2};
            Assert.IsTrue(expected.EqualsElements(actual));
        }

        [TestMethod]
        public void AddTest()
        {
            //given a matrix
            var matrix = new [,] {{ 1, 2.5, 2.7 },
                                 { 2, 3.1,   0.4 }};
            //we can add scalar to each element of matrix
            var actual = matrix.Add(-1);

            var expected = new[,] {{ 0, 1.5, 1.7 },
                                 { 1, 2.1,   -0.6 }};
            Assert.IsTrue(expected.EqualsElements(actual,(x,y) => Math.Abs(x-y)<0.000001));
        }

        [TestMethod]
        public void SubstractTest()
        {
            //given a matrix
            var matrix = new[,] {{ 1, 2 },
                                 { 2, 3 }};
            //we can substract scalar from each element of matrix
            var actual = matrix.Substract(2);

            var expected = new[,] {{ -1, 0 },
                                   { 0, 1 }};
            Assert.IsTrue(expected.EqualsElements(actual));
        }

        [TestMethod]
        public void NegateTest()
        {
            //given a matrix
            var matrix = new[,] {{ 1, 2.5, 2.7 },
                                 { 2, 3.1,   0.4 }};
            //we can negate each element of it
            var actual = matrix.Negate();

            var expected = new[,] {{ -1, -2.5, -2.7 },
                                 { -2, -3.1,   -0.4 }};
            Assert.IsTrue(expected.EqualsElements(actual));
        }

        [TestMethod]
        public void PointwiseMultiplyTest()
        {
            //given a matrix
            var matrix = new[,] {{ 1, 2 },
                                 { 2, 3 }};
            //we can multiply each element of matrix by scalar 
            var actual = matrix.PointwiseMultiply(2);

            var expected = new[,] {{ 2, 4 },
                                   { 4, 6 }};
            Assert.IsTrue(expected.EqualsElements(actual));
        }

        [TestMethod]
        public void DivideEachElementTest()
        {
            //given a matrix
            var matrix = new[,] {{ 1, 2 },
                                 { 2, 5 }};
            //we can divide each element of matrix by scalar 
            //returned matrix will be of type which it had before
            var actual = matrix.DivideEachElement(2);

            var expected = new[,] {{ 0, 1 },
                                   { 1, 2 }};
            Assert.IsTrue(expected.EqualsElements(actual));

        }
    }
}
