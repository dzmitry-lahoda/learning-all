using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using Microsoft.VisualStudio.TestTools.UnitTesting;

using MatrixExtensions.Manipulation;
using MatrixExtensions.Equality;
using MatrixExtensions.Operations;

namespace MatrixExtensionsTestProject
{
    [TestClass]
    public class Array2DOperationsExtensionsTest
    {
        [TestMethod]
        public void AddTest()
        {
            //given two matrices
            var first = new[,] {{ 1, 2 },
                                 { 2, -0.5 }};
            var second = new[,] {{ 1, 2 },
                                 { 0, 1 }};
            //we can add elements of second to first
            var actual = first.Add(second);

            var expected = new[,] {{ 2,4 },
                                 { 2, 0.5 }};
            Assert.IsTrue(expected.EqualsElements(actual, (x, y) => Math.Abs(x - y) < 0.000001));
        }

        [TestMethod]
        public void SubstractTest()
        {
            //given two matrices
            var first = new[,] {{ 1, 2 },
                                 { 2, -0.5m }};
            var second = new[,] {{ 1, 2 },
                                 { 0, 1 }};
            //we can add elements of second from first
            var actual = first.Substract(second);

            var expected = new[,] {{ 0, 0 },
                                   { 2, -1.5m }};
            Assert.IsTrue(expected.EqualsElements(actual));
        }

        [TestMethod]
        public void PointwiseMultiplyTest()
        {
            //given two matrices
            var first = new[,] {{ 1, 1.5 },
                                 { 2, 0d }};
            var second = new[,] {{ 1, 2 },
                                 { 4, 1L }};
            //we can multiply each element of first with second 
            var actual = first.PointwiseMultiply(second);

            var expected = new[,] {{ 1, 3 },
                                   { 8, 0d }};
            Assert.IsTrue(expected.EqualsElements(actual));
        }

        [TestMethod]
        public void MultiplyTest()
        {
            //given two matrices
            var first = new[,] {{ 1, -1, 2},
                                 { 0, 2,-3 }};
            var second = new[,] {{ 1, 2 },
                                 { -1, 3 },
                                 { 5, -2}};
            //we can multiply them
            var actual = first.Multiply(second);

            var expected = new[,] {{ 12, -5 },
                                   { -17, 12 }};
            Assert.IsTrue(expected.EqualsElements(actual));
        }

        [TestMethod]
        public void MultiplyWithSingleColumnMatrixTest()
        {
            //given two matrices
            var first = new[,] {{ 1, 2, 3},
                                 { 4, 5, 6 },
                                 {7, 8, 9},
                                 {10, 11, 12}};
            var second = new[,] {{ -2},
                                 { 1},
                                 { 0}};
            //we can multiply them
            var actual = first.Multiply(second);

            var expected = new[,] {{ 0 },
                                   { -3},
                                   {-6 },
                                   {-9}};
            Assert.IsTrue(expected.EqualsElements(actual));
        }

        [TestMethod]
        public void MultiplyWithVectorTest()
        {
            //given a matrix
            var matrix = new[,] {{ 1, 2, 3},
                                 { 4, 5, 6 },
                                 {7, 8, 9},
                                 {10, 11, 12}};
            //and a vector
            var vector = new[] { -2,
                                  1,
                                  0};
            //we can multiply matrix with vector
            var actual = matrix.Multiply(vector);

            var expected = new[] {0 ,
                                  -3,
                                  -6 ,
                                  -9};
            Assert.IsTrue(expected.EqualsElements(actual));
        }
    }
}
