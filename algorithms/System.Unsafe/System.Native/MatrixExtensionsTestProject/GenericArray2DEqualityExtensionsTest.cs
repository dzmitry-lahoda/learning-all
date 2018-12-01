using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using MatrixExtensions.Equality;
using MatrixExtensions.Boolean;

namespace MatrixExtensionsTestProject
{


    [TestClass]
    public class GenericArray2DEqualityExtensionsTest
    {
        [TestMethod]
        public void TrueEqualsElementsTest()
        {
            //given a two to equalsize matrices
            var matrix = new [,]{{1, 2, 3}, {2, 3, 4}};
            var otherMatrix = new [,]{ { 1, 2, 3 }, { 2, 3, 4 } };
            //we can check if they contain equal elements
            var actual = matrix.EqualsElements(otherMatrix);

            Assert.IsTrue(actual);
        }


        [TestMethod]
        public void FalseEqualsElementsTest()
        {
            //given a two to equalsize matrices
            var matrix = new[,] { { 1, 2, 3 }, { 5, 3, 1 } };
            var otherMatrix = new[,] { { 1, 2, 3 }, { 2, 3, 4 } };
            //we can check if they contain equal elements
            var actual = matrix.EqualsElements(otherMatrix);

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void TrueEqualsElementsWithComparisonTest()
        {
            //given a two to equalsize matrices
            var matrix = new[,] { { 1, 2, 2.5m }, { 2, 3m, 4 } };
            var otherMatrix = new[,] { { 2.1d, 2, 3 }, { 2, 3, 4 } };
            //we can check if they contain equal elements
            //one element equal to other if lambda expression returns true
            var actual = matrix.EqualsElements(otherMatrix, (x, y) => Math.Abs(x - (decimal)y) < 2);

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void FalseEqualsElementsWithComparisonTest()
        {
            //given a two to equalsize matrices
            var matrix = new[,] { { 1, 2, 2.5m }, { 2, 3m, 4 } };
            var otherMatrix = new[,] { { 2.1d, 2, 3 }, { 2, 3, 4 } };
            //we can check if they contain equal elements
            //one element equal to other if lambda expression returns true
            var actual = matrix.EqualsElements(otherMatrix, (x, y) => Math.Abs(x - (decimal)y) < 1);

            Assert.IsFalse(actual);
        }

    }
}