using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MatrixExtensions.Equality;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using MatrixExtensions.Operations;

namespace MatrixExtensionsTestProject
{
    [TestClass]
   public class ArraySimpleOperationsExtensionsTest
    {
        [TestMethod] 
        public void  MaxPositionTest()
        {
            //given a vector
            var vector = new[] {1m, 2.5m, 2.7m, 2};
            //we can get position of maximal value
            var actual = vector.MaxPosition();

            var expected = 2;
            Assert.AreEqual(actual,expected);
        }

        [TestMethod]
        public void   AddTest()
        {
            //given a vector
            var vector = new [] {1f,2,3 };
            //we can add scalar to each element of vector
            var actual = vector.Add(1);

            var expected =new [] {2,3,4f };
            Assert.IsTrue(expected.EqualsElements(actual));
        }

        [TestMethod]
        public void SubstractTest()
        {
            //given a vector
            var vector = new[] { 1f, 2, 3 };
            //we can substract scalar from each element of vector
            var actual = vector.Substract(1);

            var expected = new[] { 0, 1, 2f };
            Assert.IsTrue(expected.EqualsElements(actual));
        }

        [TestMethod]
        public void NegateTest()
        {
            //given a vector
            var vector = new [] { 1, 2, 3m };
            //we can negate each element of it
            var actual = vector.Negate();

            var expected = new [] { -1, -2,-3m };
            Assert.IsTrue(expected.EqualsElements(actual));
        }

        [TestMethod]
        public void PointwiseMultiplyTest()
        {
            //given a vector
            var vector = new [] { 1, 2, 3 };
            //we can multiply each element of vector by scalar 
            var actual = vector.PointwiseMultiply(2);

            var expected = new [] { 2, 4, 6 };
            Assert.IsTrue(expected.EqualsElements(actual));
        }

        [TestMethod]
        public void DivideEachElementTest()
        {
            //given a vector
            var vector = new[] { 1L, 2, 3 };
            //we can divide each number of vector by scalar 
            //returned vector will be of type which it had before
            var actual = vector.DivideEachElement(2);

            var expected = new[] { 0L, 1, 1 };
            Assert.IsTrue(expected.EqualsElements(actual));
        }
    }
}
