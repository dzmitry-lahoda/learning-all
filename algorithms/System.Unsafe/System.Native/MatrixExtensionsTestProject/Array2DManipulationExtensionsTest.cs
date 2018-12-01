using System.Collections.Generic;
using System.Threading;
using System;
using System.Linq;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using MatrixExtensions.Equality;
using MatrixExtensions.Manipulation;

namespace MatrixExtensionsTestProject
{
    [TestClass]
    public  class Array2DManipulationExtensionsTest
    {

        [TestMethod]
        public void AppendVectorTest()
        {
            //given a matrix 
            var data = new [,]{{1.2, 2.2},
                               {0.4, 0.6},
                               {1.7, 0.1}};
            //given a vector with length equal to number of matrix's rows
            var categories = new[]{1,
                                   0,
                                   1};
            //we can append vector to the rigth side of matrix
            var actual = data.Append(categories);

            var expected = new[,]{{1.2, 2.2,1},
                                  {0.4, 0.6, 0},
                                  {1.7, 0.1, 1}};

            Assert.IsTrue(expected.EqualsElements(actual));
        }

        [TestMethod]
        public void AppendMatrixTest()
        {
            //given two matrices with equal number of rows
            var first = new[,]{{1.2, 2.2},
                               {0.4, 0.6},
                               {1.7, 0.1}};
            var second = new[,]{{1.7,2},
                                {0.2,4},
                                {1.1,6}};
            //we can second to the rigth side of first
            var actual = first.Append(second);

            var expected = new[,]{{1.2, 2.2,1.7,2},
                                  {0.4, 0.6, 0.2,4},
                                  {1.7, 0.1, 1.1,6}};

            Assert.IsTrue(expected.EqualsElements(actual));
        }

        [TestMethod]
        public void StackVectorTest()
        {
            //given a matrix 
            var matrix = new[,]{{1.2, 2.2,2},
                                {1.7, 0.1,10}};
            //given a vector with length equal to number of matrix's columns
            var vector = new[] { 1, -1, 0.1 };
            //we can stack vector to the bottom of the matrix
            var actual = matrix.Stack(vector);

            var expected = new[,]{{1.2, 2.2,2},
                                  {1.7, 0.1,10},
                                  {1,-1,0.1}};

            Assert.IsTrue(expected.EqualsElements(actual));
        }

        [TestMethod]
        public void StackMatrixTest()
        {
            //given two matrices with equal number of columns
            var first = new [,]{{1.2m, 2.2m,2},
                                {1.7m, 0.1m,10}};
            var second = new[,] {{ 1L, -1, 11 },
                                 { 2, -1, 12 }};
            //we can stack second to the bottom of the first
            var actual = first.Stack(second);

            var expected = new[,]{{1.2m, 2.2m,2},
                                  {1.7m, 0.1m,10},
                                  { 1L, -1, 11 },
                                  { 2, -1, 12 }};

            Assert.IsTrue(expected.EqualsElements(actual));
        }


    }
}