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
    public class Array2DExplicitConversionExtensionsTest
    {

        [TestMethod]
        public void MatrixExplicitConversionTest()
        {
            //given a matrix of double numbers
            var matrix = new[,]{{9d,2,1},
                                  {2,22,2}};

            //we can convert it to matrix of long numbers
            //some data will be lost
            var actual = matrix.ToLong();

            var expected = new[,]{{9L,2,1},
                                  {2,22,2}};

            Assert.IsTrue(expected.EqualsElements(actual));
        }


    }
}
