using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MatrixExtensions.Equality;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using MatrixExtensions.Conversion;

namespace MatrixExtensionsTestProject
{
    [TestClass]
    public class ArrayConversionExtensionsTest
    {

        [TestMethod]
        public void VectorConversionTest()
        {
            //given a vector of int numbers
            var vector = new[] { 1, 22 };

            //we can convert it to vector of long numbers
            var actual = vector.ToLong();

            var expected = new[] { 1L, 22 };
            
            expected.SequenceEqual(actual);
            Assert.IsTrue(expected.EqualsElements(actual));
        }
    }
}
