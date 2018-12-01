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
    public class GenericArrayEqualityExtensionsTest
    {

        [TestMethod]
        public void TrueEqualsElementsTest()
        {
            //given a two vectors with equal length
            var vector = new[] { 2.2, 2.1, -111 };
            var otherVector = new[] { 2.2, 2.1, -111 };
            //we can check if they contain equal elements
            var actual = vector.EqualsElements(otherVector);
           
            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void FalseEqualsElementsTest()
        {
            //given a two vectors with equal length
            var vector = new[] {  2, 3, 4  };
            var otherVector = new[] { 1, 2, 3 };
            //we can check if they contain equal elements
            var actual = vector.EqualsElements(otherVector);

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void TrueEqualsElementsWithComparisonTest()
        {
            //given a two vectors with equal length
            var vector = new[] { 1.2, 2.1, -11.4 };
            var otherVector = new[] { 0.7, 2.1, -11 };
            //we can check if they contain equal elements
            //one element equal to other if lambda expression returns true
            var actual = vector.EqualsElements(otherVector, (x, y) => Math.Abs(x-y)<2);

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void FalseEqualsElementsWithComparisonTest()
        {
            //given a two vectors with equal length
            var vector = new[] { 2, 3, 11 };
            var otherVector = new[] { 1, 2, 3 };
            //we can check if they contain equal elements
            //one element equal to other if lambda expression returns true
            var actual = vector.EqualsElements(otherVector, (x, y) => Math.Abs(x - y) < 1);

            Assert.IsFalse(actual);
        }
    }
}