using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MathNet.Numerics.LinearAlgebra.Double;
using NUnit.Framework;

using PCA;

namespace PCA.Tests
{
    [TestFixture]
    public class PCATests
    {

        private const double Delta = 0.01;

        private double[]  x = new double[] { 9, 15, 25, 14, 10, 18, 0, 16, 5, 19, 16, 20 };

        private double[] y = new double[] { 39, 56, 93, 61, 50, 75, 32, 85, 42, 70, 66, 80 };

        [Test]
        public void mean()
        {
            double mean = PCA.mean(new double[] {0, 8, 12, 20});
            Assert.AreEqual(10, mean, Delta);
        }

        [Test]
        public void std()
        {
            double std = PCA.std(new double[] { 0, 8, 12, 20 });
            Assert.AreEqual(8.3266, std, Delta);
        }

        [Test]
        public void cov()
        {
            double cov = PCA.cov(x,y);
            Assert.AreEqual(122.946, cov, Delta);
        }

        [Test]
        public void adjustByMean()
        {
           var adjustByMean = PCA.adjustByMean(x);

            Assert.AreEqual(-4.92, adjustByMean.Item1.First(), Delta);
            Assert.AreEqual(6.08,adjustByMean.Item1.Last(), Delta);
        }

        [Test]
        public void squared()
        {
            var x = new double[] { -4.92, 1.08,11.08 };
            var y = new double[] { -23.42 , -6.42,30.58 };
            IEnumerable<double> cov = PCA.squared(x, y);
            Assert.AreEqual(115.23,cov.First(), Delta);
            Assert.AreEqual(338.83,cov.Last(), Delta);
        }

        [Test]
        public void covMatrix()
        {
            var data = new DenseMatrix(x.Length, 2);
            data.SetColumn(0,x);
            data.SetColumn(1, y);
            var covM = PCA.covMatrix(data);
 
        }

    }
}
