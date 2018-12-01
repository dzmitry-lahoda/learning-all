using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MatrixExtensions.Equality;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using MatrixExtensions.Conversion.Explicit;

namespace MatrixExtensionsTestProject
{
    [TestClass]
    public class ArrayExplicitConversionExtensionsTest
    {

        [TestMethod]
        public void VecorExplicitConversionTest()
        {
            //given a vector of double numbers
            var vector = new[] { 1.2, 22 };

            //we can convert it to vector of long numbers
            //some data will be lost
            var actual = vector.ToLong();

            var expected = new[] { 1L, 22 };
            
            expected.SequenceEqual(actual);
            Assert.IsTrue(expected.EqualsElements(actual));
        }
    }
}
