using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using MatrixExtensions.Equality;
using MatrixExtensions.Generic;
using MatrixExtensions.Manipulation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MatrixExtensionsTestProject
{
    [TestClass]
    public class ArrayManipulationExtensionsTest
    {

        [TestMethod]
        public void AppendVectorTest()
        {
            //given two vectors
            var first = new[] {1.2, 2.2};
            var second = new []{1,0,1};
            //we can append second to the rigth side of first
            var actual = first.Append(second);

            var expected = new [] {1.2, 2.2, 1, 0, 1};
            Assert.IsTrue(expected.EqualsElements(actual));
        }

        [TestMethod]
        public void StackVectorTest()
        {
            //given two vectors with equal number of elements
            var first = new[]{1.2, 2.2,2};
            var second = new[] { 1, -1, 0.1 };
            //we can stack second to the bottom of the first
            var actual = first.Stack(second);

            var expected = new[,]{{1.2, 2.2,2},
                                  {1,-1,0.1}};
            Assert.IsTrue(expected.EqualsElements(actual));
        }

        [TestMethod]
        public void AppendMatrixTest()
        {
            //given a vector
            var vector = new[]{1,
                               0,
                               1};
            //given a matrix with rows count equal to vector's length
            var matrix = new[,]{{1.2, 2.2},
                                {0.4, 0.6},
                                {1.7, 0.1}};

            //we can append matrix to the rigth side of vector
            var actual = vector.Append(matrix);

            var expected = new[,]{{1, 1.2, 2.2},
                                  {0, 0.4, 0.6},
                                  {1,1.7, 0.1}};

            Assert.IsTrue(expected.EqualsElements(actual));
        }

        [TestMethod]
        public void StackMatrixTest()
        {
            //given a vecotr 
            var vector = new[] { 1, -1, 0 };

            //given a matrix with column coumnt equal to length of vector
            var matrix = new[,]{{1.2m, 2.2m,2},
                                {1.7m, 0.1m,10}};
            //we can stack matrix to the bottom of the vector
            var actual = vector.Stack(matrix);

            var expected = new[,]{{1,-1,0},
                                  {1.2m, 2.2m,2},
                                  {1.7m, 0.1m,10}};

            Assert.IsTrue(expected.EqualsElements(actual));
        }

     
    }
}