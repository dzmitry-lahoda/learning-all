using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using MatrixExtensions.Equality;
using MatrixExtensions.Manipulation.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MatrixExtensionsTestProject
{
    [TestClass]
    public class GenericArrayManipulationExtensionsTest
    {

        [TestMethod]
        public void ShiftPositiveTest()
        {
            //given a vector
            var vector = new[] { 1, 3, 5, 6, 7 };
            //we can shift vector to right by 2 positions
            //default values are used for new elements
            var actual = vector.Shift(2);

            var expected = new[] { 0, 0, 1, 3, 5 };
            Assert.IsTrue(expected.EqualsElements(actual));
        }

        [TestMethod]
        public void ShiftNegativeTest()
        {
            var vector = new[] { 1, 3, 5, 6.1, 7.1 };
            //shifts vector to left by 3 positions 
            //puts default values for new elements
            var actual = vector.Shift(-3);

            var expected = new[] { 6.1, 7.1, 0, 0, 0 };
            Assert.IsTrue(expected.EqualsElements(actual));
        }

        [TestMethod]
        public void ShiftZeroTest()
        {
            var vector = new[] { 1, 3, 5, 6, 7 };
            //does not shift vector
            //just returns copy
            var actual = vector.Shift(0);

            var expected = new[] { 1, 3, 5, 6, 7 };
            Assert.IsTrue(expected.EqualsElements(actual));
        }

        [TestMethod]
        public void CircularShiftPositiveTest()
        {
            //given a vector
            var vector = new[] { 1, 3, 5, 6, 7 };
            //shifts vector to right by 2 positions
            var actual = vector.CircularShift(2);

            var expected = new[] { 6, 7, 1, 3, 5 };
            Assert.IsTrue(expected.EqualsElements(actual));
        }

        [TestMethod]
        public void CircularShiftNegativeTest()
        {
            //given a vector
            var vector = new[] { 1, 3, 5, 6.1, 7.1 };
            //shifts vector to left by 3 positions 
            var actual = vector.CircularShift(-3);

            var expected = new[] { 6.1, 7.1, 1, 3, 5 };
            Assert.IsTrue(expected.EqualsElements(actual));
        }

        [TestMethod]
        public void CircularShiftZeroTest()
        {
            //we have a vector
            var vector = new[] { 1, 3, 5, 6, 7 };
            //does not shift vector
            //just returns copy
            var actual = vector.CircularShift(0);

            var expected = new[] { 1, 3, 5, 6, 7 };
            Assert.IsTrue(expected.EqualsElements(actual));
        }
    }
}
