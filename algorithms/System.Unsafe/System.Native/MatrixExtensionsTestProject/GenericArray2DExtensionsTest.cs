using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

using MatrixExtensions.Generic;
using MatrixExtensions.Equality;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MatrixExtensionsTestProject
{
    [TestClass]
    public class GenericArray2DExtensionsTest
    {
        [TestMethod]
        public void IsEmptyTest()
        {
            //given a matrix
            var matrix = new [,] {{1,2 },{2,2}};
            //vector with elements considered not empty
            var actual = matrix.IsEmpty();

            var expected = false;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void RowCountTest()
        {
            //given a matrix
            var matrix = new[,] { { 1, 2 }, { 2, 2 } ,{4,31d}};
            //we can get number of rows(length of zero dimension) in it
            var actual = matrix.RowCount();

            var expected = 3;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ColumnCountTest()
        {
            //given a matrix
            var matrix = new[,] { { 1, 2 }, { 2, 2 }, { 4, 31d } };
            //we can get number of columns(length of first dimension) in it
            var actual = matrix.ColumnCount();

            var expected = 2;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetRowTest()
        {
            //given a matrix
            var matrix = new[,] { { 1, 2 }, 
                                  { 2, 2 }, 
                                  { 4, 31d } };
            //we can get a copy of row
            var actual = matrix.GetRow(1);

            var expected = new[] { 2, 2d };
            Assert.IsTrue(expected.EqualsElements(actual));
        }

        [TestMethod]
        public void GetColumnTest()
        {
            //given a matrix
            var matrix = new[,] { { 1, 2 }, 
                                  { 2, 2 }, 
                                  { 4, 31f } };
            //we can get a copy of column
            var actual = matrix.GetColumn(0);

            var expected = new[] { 1f, 2, 4};
            Assert.IsTrue(expected.EqualsElements(actual));
        }

        [TestMethod]
        public void GetLastColumnTest()
        {
            //given a matrix
            var matrix = new[,] { { 1, 2 , 3 , 5 }, 
                                  { 2, 2 ,33, 4.1}, 
                                  { 4, 31f, 1.5 ,7.1} };
            //we can get a copy of last column
            var actual = matrix.GetLastColumn();

            var expected = new[] { 5, 4.1, 7.1 };
            Assert.IsTrue(expected.EqualsElements(actual));
        }

        [TestMethod]
        public void GetSubMatrixTest()
        {
            //given a matrix
            var matrix = new[,] { { 1, 2 , 6}, 
                                  { 2, 2 ,22}, 
                                  { 4, 31, 4 } };
            //we can get a copy of sub matrixn
            var actual = matrix.GetSubMatrix(1,2,0,1);

            var expected = new[,] {{ 2}, 
                                  { 4 }};

            Assert.IsTrue(expected.EqualsElements(actual));
        }

        [TestMethod]
        public void GetRowEnumerationTest()
        {
            //given a matrix
            var matrix = new[,] { { 1, 2 }, 
                                  { 2, 2 }, 
                                  { 4, 31d } };
            //we can get a row enumerator
            var actual = matrix.GetRowEnumeration();


            foreach (var row in actual)
            {
                var c = 0;
                foreach (var value in row.Value)
                {
                    Assert.AreEqual(matrix[row.Key, c++], value);
                }
            }
        }

        [TestMethod]
        public void GetColumnEnumerationTest()
        {
            //given a matrix
            var matrix = new[,] { { 1, 2 }, 
                                  { 2, 2 }, 
                                  { 4, 31d } };
            //we can get a column enumerator
            var actual = matrix.GetColumnEnumeration();
            
            foreach (var column in actual)
            {
                var r = 0;
                foreach (var value in column.Value)
                {
                    Assert.AreEqual(matrix[r++, column.Key], value);
                }
            }
        }

        [TestMethod]
        public void ToListOfListsTest()
        {
            //given a matrix
            var matrix = new[,] { { 1, 2 }, 
                                  { 2, 2 }, 
                                  { 4, 31d } };
            //we can get a list of lists of it
            var actual = matrix.ToListOfLists();

            for (var r = 0; r < matrix.RowCount(); r++)
            {
                for (var c = 0; c < matrix.ColumnCount(); c++)
                {
                    Assert.AreEqual(actual[r][c],matrix[r,c]);
                }
            }

        }

        


    }
}
