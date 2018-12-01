using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MatrixExtensions.Conversion.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MatrixExtensionsTestProject
{
     [TestClass]
    public class GenericArray2DConversionExtensionsTest
    {
         [TestMethod]
         public void ToJaggedTest()
        {
             //given a matrix
            var matrix = new [,] { { 0.2, 0.3, 0.8 }, 
                                   { 2.1, 3.3, 5.7} };
            
             //you can convert it to jagged array
             var actual = matrix.ToJagged();

             var expected = new [] { new[] { 0.2, 0.3, 0.8 } ,
                                             new []{ 2.1, 3.3, 5.7}};
             Assert.AreEqual(actual[0][0], expected[0][0]);
             Assert.AreEqual(actual[0][1], expected[0][1]);
             Assert.AreEqual(actual[0][2], expected[0][2]);
             Assert.AreEqual(actual[1][0], expected[1][0]);
             Assert.AreEqual(actual[1][1], expected[1][1]);
             Assert.AreEqual(actual[1][2], expected[1][2]);
        }

         [TestMethod]
         public void ToJaggedOneRowMatrixTest()
         {
             //given a one row matrix
             var matrix = new[,] { { 0.2, 0.3, 0.8 }};
             //you can convert it to jagged array
             var actual = matrix.ToJagged();

             var expected = new[] { new[] { 0.2, 0.3, 0.8 }};
             Assert.AreEqual(actual[0][0], expected[0][0]);
             Assert.AreEqual(actual[0][1], expected[0][1]);
             Assert.AreEqual(actual[0][2], expected[0][2]);
         }
    }
}
