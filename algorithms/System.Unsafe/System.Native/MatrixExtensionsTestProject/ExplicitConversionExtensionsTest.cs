using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.VisualStudio.TestTools.UnitTesting;


using MatrixExtensions.Conversion.Explicit;
using MatrixExtensions.Equality;

namespace MatrixExtensionsTestProject
{
    [TestClass]
    public class ExplicitConversionExtensionsTest
    {

        [TestMethod]
        public void DataLostMatrixConversionTest()
        {
            //given a matrix of decimal numbers
            var matrix = new [,]{{1.1m,2.6m},
                                  {2,22}};

            //we can convert it to matrix of longs
            //fraction part will be lost
            var actual = matrix.ToLong();

            var expected = new[,]{{1L,2},
                                  {2,22}};

            Assert.IsTrue(expected.EqualsElements(actual));
        }

        [TestMethod]
        public void DataLostVecorConversionTest()
        {
            //given a vector of double numbers
            var vector = new [] { 1.1, 2.600000000000001 };

            //we can convert it to vector of float numbers
            //some data will be lost
            var actual = vector.ToFloat();

            var expected = new [] { 1.1f, 2.6f };
            Assert.IsTrue(expected.EqualsElements(actual));
        }

    }
}
