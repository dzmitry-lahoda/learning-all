using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using MatrixExtensions.Equality;

using MatrixExtensions.Conversion.Generic.Specialized;

namespace MatrixExtensionsTestProject
{
    [TestClass]
    public class GenericArray2DFuncConversionExtensionsTest
    {
        [TestMethod]
        public void ToJaggedTest()
        {
            //given a matrix
            var matrix = new[,] {{1, 2, 0},
                                 {3, 4,0}};

            //you can convert it to jagged containtaning elements of different type
            //using function
            var actual = matrix.ToJagged(x => x.ToString());

            var expected = new [] {new []{"1", "2","0"},
                                        new []{"3", "4","0"}};
            Assert.AreEqual(actual[0][0], expected[0][0]);
            Assert.AreEqual(actual[0][1], expected[0][1]);
            Assert.AreEqual(actual[0][2], expected[0][2]);
            Assert.AreEqual(actual[1][0], expected[1][0]);
            Assert.AreEqual(actual[1][1], expected[1][1]);
            Assert.AreEqual(actual[1][2], expected[1][2]);
        }

        [TestMethod]
        public void ToJaggedWithIndecesTest()
        {
            //given a matrix
            var matrix = new[,] { { 1, 2 },
                                  { 3, 4 },
                                  {1, 1.5}};

            //you can convert it to jagged containtaning elements of different type
            //outer and inner loop indeces are used in lamda expression
            var actual = matrix.ToJagged((x,r,c) => new Node(c+1,x));

            var expected = new[] { new[] { new Node(1,1), new Node(2,2) }, 
                                   new[] { new Node(1,3), new Node(2,4)},
                                   new[] { new Node(1,1), new Node(2,1.5) } };
            Assert.AreEqual(actual[0][0], expected[0][0]);
            Assert.AreEqual(actual[0][1], expected[0][1]);
            Assert.AreEqual(actual[1][0], expected[1][0]);
            Assert.AreEqual(actual[1][1], expected[1][1]);
            Assert.AreEqual(actual[2][0], expected[2][0]);
            Assert.AreEqual(actual[2][1], expected[2][1]);
        }

        [TestMethod]
        public void ToOther()
        {
            //given a matrix
            var matrix = new[,] { { 1, 2 }, { 3, 4 } };

            //you can convert it to jagged containtaning elements of different type
            //using function
            var actual = matrix.ToOther(x => x.ToString());

            var expected = new[,] { { "1", "2" }, { "3", "4" } };
            Assert.IsTrue(expected.EqualsElements(actual));
        }

        [TestMethod]
        public void ToOtherWithIndecesTest()
        {
            //given a matrix
            var matrix = new[,] { { 1, 2 },
                                  { 3, 4 } };

            //you can convert it to other containtaning elements of different type
            //outer and inner loop indeces are used in lamda expression
            var actual = matrix.ToOther((x, r, c) => new Node(c + 1, x));

            var expected = new[,] {  { new Node(1,1), new Node(2,2) }, 
                                    { new Node(1,3), new Node(2,4)  } };
            Assert.IsTrue(expected.EqualsElements(actual));
        }
    }
}
