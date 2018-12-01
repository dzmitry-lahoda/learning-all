using System.Collections.Generic;
using System.Threading;
using System;
using System.Linq;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using MatrixExtensions.Manipulation;
using MatrixExtensions.Equality;


namespace MatrixExtensionsTestProject
{
    [TestClass]
    public class Array2DSimpleManipulationExtensionsTest
    {

        [TestMethod]
        public void ToColumnWiseArray()
        {
            //given a matrix
            var matrix = new[,] {{1,2,3},
                                 {4,5,6},
                                 {7,8,9}};
            //you can create vector which is concatenation of matrix's columns
            var actual = matrix.ToColumnWiseArray();

            var expected = new[] { 1, 4, 7, 2, 5, 8, 3, 6, 9 };
            Assert.IsTrue(expected.EqualsElements(actual));

        }

        [TestMethod]
        public void ToRowWiseArray()
        {
            //given a matrix
            var matrix = new[,] {{1,2,3},
                                 {4,5,6},
                                 {7,8,9}};
            //you can create vector which is concatenation of matrix's rows
            var actual = matrix.ToRowWiseArray();

            var expected = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            Assert.IsTrue(expected.EqualsElements(actual));
        }

        [TestMethod]
        public void OneRowMatrixToColumnWiseArray()
        {
            //given a matrix with one row
            var matrix = new[,] {{1,2,3}};
            //you can create vector which is concatenation of matrix's columns
            var actual = matrix.ToColumnWiseArray();

            var expected = new[] { 1,2,3 };
            Assert.IsTrue(expected.EqualsElements(actual));

        }

        [TestMethod]
        public void OneRowMatrixToRowWiseArray()
        {
            //given a matrix with one row
            var matrix = new[,] {{1,8,5}};
            //you can create vector which is concatenation of matrix's rows
            var actual = matrix.ToRowWiseArray();

            var expected = new[] { 1, 8, 5};
            Assert.IsTrue(expected.EqualsElements(actual));
        }
        [TestMethod]
        public void OneColumnMatrixToColumnWiseArray()
        {
            //given a matrix with one column
            var matrix = new[,] { {1},
                                  {0}, 
                                  {3}};
            //you can create vector which is concatenation of matrix's columns
            var actual = matrix.ToColumnWiseArray();

            var expected = new[] { 1, 0, 3 };
            Assert.IsTrue(expected.EqualsElements(actual));

        }

        [TestMethod]
        public void OneColumnMatrixToRowWiseArray()
        {
            //given a matrix with one column
            var matrix = new[,] { { 1 }, 
                                  { -1 }, 
                                  { 3 } };
            //you can create vector which is concatenation of matrix's rows
            var actual = matrix.ToRowWiseArray();

            var expected = new[] { 1, -1, 3 };
            Assert.IsTrue(expected.EqualsElements(actual));
        }
    }
}