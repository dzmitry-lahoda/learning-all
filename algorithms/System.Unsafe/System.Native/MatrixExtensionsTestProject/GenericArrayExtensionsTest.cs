using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using MatrixExtensions.Equality;
using MatrixExtensions.Generic;

namespace MatrixExtensionsTestProject
{
   [TestClass]
    public class GenericArrayExtensionsTest
    {


       [TestMethod]
       public void IsEmptyTest()
       {
           //given any vector
           var vector = new int[] {};       
           //vector without elements considered empty
           var actual = vector.IsEmpty();

           var expected = true;
           Assert.AreEqual(expected, actual);
       }


       
    }
}
