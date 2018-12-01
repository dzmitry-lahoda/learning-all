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
    public class BooleanArrayExtensionsTest
    {
        [TestMethod]
        public void ToIntVectorTest()
        {
            //if you have vcetor of boolean
            var vector = new []{true,false,true};
            //we can convert it to integers
            var actual = vector.ToInt();

            var expected = new[] { 1, 0, 1 };
            Assert.IsTrue(expected.EqualsElements(actual));
        }
    }
}
