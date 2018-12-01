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
    public class Array2DConversionExtensionsTest
    {
        [TestMethod]
        public void MatrixConversionTest()
        {
            //given a matrix of long numbers
            var matrix = new[,]{{9L,2,1},
                                  {2,22,2}};

            //we can convert it to matrix of decimals 
            var actual = matrix.ToDecimal();

            var expected = new [,]{{9m,2,1},
                                  {2,22,2}};

            Assert.IsTrue(expected.EqualsElements(actual));
        }
    }
}
