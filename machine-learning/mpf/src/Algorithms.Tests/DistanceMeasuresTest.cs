using Blocks;
using MathNet.Numerics.LinearAlgebra.Double;
using NUnit.Framework;

namespace MPF.Algorithms.Tests
{
    [TestFixture]
    public class DistanceMeasuresTest
    {
        [Test]
        public void EuclidianDistanceTest()
        {
            var vector1 = new DenseVector(new double[] { 0, 0 });
            var vector2 = new DenseVector(new double[] { 2, 0 });
            var distance0 = DistanceMeasures.EuclidianDistance(vector2, vector2);
            var distance2 = DistanceMeasures.EuclidianDistance(vector1, vector2);
            Assert.IsTrue(DefaultComparing.NearEqual(distance0, 0), distance0.ToString());
            Assert.IsTrue(DefaultComparing.NearEqual(distance2, 2), distance2.ToString());
        }
    }
}
