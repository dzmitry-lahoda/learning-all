using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dnHTM;
using NUnit.Framework;

namespace dnHTMTests
{
    [TestFixture]
    public class TemporalGrouperTest
    {
        [Test]
        public void OneElementMatrixDoGroupingTest()
        {
            var grouper = new TemporalGrouper();
            var groups = grouper.DoGrouping(TestsHelper.OneElementTransitionMatrix,1);
            Assert.IsTrue(groups.Count == 1);
        }

        [Test]
        public void TwoElementMatrixDoGroupingTest()
        {
            var grouper = new TemporalGrouper();
            var groups = grouper.DoGrouping(TestsHelper.TwoElementTransitionMatrix, 1);
            Assert.IsTrue(groups.Count == 1);
        }

        [Test]
        public void ThreeElementMatrixDoGroupingTest()
        {
            var grouper = new TemporalGrouper();
            var groups = grouper.DoGrouping(TestsHelper.ThreeElementTransitionMatrix, 2);
            Assert.IsTrue(groups.Count == 2);
        }

        [Test]
        public void DoGroupingTest()
        {
            var grouper = new TemporalGrouper();
            var groups = grouper.DoGrouping(TestsHelper.ThreeElementTransitionMatrix, 1);
        }

        [Test]
        public void MatrixIsNotTransitionDoGroupingTest()
        {
            var grouper = new TemporalGrouper();
            var groups = grouper.DoGrouping(TestsHelper.ThreeElementTransitionMatrix, 1);
        }

        [Test]
        public void NumberOfGroupsMoreThanNumberOfPatternsDoGroupingTest()
        {
            var grouper = new TemporalGrouper();
            var groups = grouper.DoGrouping(TestsHelper.ThreeElementTransitionMatrix, 1);
        }
    }
}
