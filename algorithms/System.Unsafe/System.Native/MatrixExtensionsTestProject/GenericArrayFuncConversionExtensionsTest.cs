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
    public class GenericArrayFuncConversionExtensionsTest
    {
        [TestMethod]
        public void ToOther()
        {
            //given a vector
            var vector = new[] {  4, 1  };

            //you can convert it to other containtaning elements of different type
            //using function
            var actual = vector.ToOther(x => x.ToString());

            var expected = new[] {  "4", "1"  };
            Assert.IsTrue(expected.EqualsElements(actual));
        }

        [TestMethod]
        public void ToOtherWithIndexTest()
        {
            //given a vector
            var vector = new [] {1.1, 2};

            //you can convert it to other containtaning elements of different type
            //loop's index is used in lamda expression
            var actual = vector.ToOther((x, i) => new Node(i + 1, x));

            var expected = new[] {  new Node(1,1.1), new Node(2,2) } ;
            Assert.IsTrue(expected.EqualsElements(actual));
        }
    }
}
