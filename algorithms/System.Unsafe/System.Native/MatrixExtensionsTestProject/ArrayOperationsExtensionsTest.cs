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
    public class ArrayOperationsExtensionsTest
    {
        [TestMethod]
        public void AddTest()
        {
            //given two vectors
            var first = new[] { 1f, 2, 3 };
            var second = new[] { 2, 3, 2 };
            //we can add elements of second to first
            var actual = first.Add(second);

            var expected = new[] { 3f, 5, 5 };
            Assert.IsTrue(expected.EqualsElements(actual));
        }

        [TestMethod]
        public void SubstractTest()
        {
            //given two vectors
            var first = new[] { 1d, 2, 3 };
            var second = new[] { 2, 3, 2 };
            //we can add elements of second from first
            var actual = first.Substract(second);

            var expected = new[] { -1d, -1, 1 };
            Assert.IsTrue(expected.EqualsElements(actual));
        }

        [TestMethod]
        public void PointwiseMultiplyTest()
        {
            //given two vectors
            var first = new[] { 1m, 2, 3 };
            var second = new[] { 2, 4, 5L };
            //we can create new which is result of per element multipication of those two
            var actual = first.PointwiseMultiply(second);

            var expected = new[] { 2, 8, 15m };
            Assert.IsTrue(expected.EqualsElements(actual));
        }
    }
}
