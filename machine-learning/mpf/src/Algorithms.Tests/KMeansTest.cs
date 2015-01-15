using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AlglibNet.DataAnalysys;
using AlglibSharp.DataAnalysys;
using NUnit.Framework;

namespace NETStatistics
{
    [TestFixture]
    public class KMeansTest
    {

        private  double[,] trainingData = 
            new double[,] {
            { 1, 2, 34 }, 
            { 1, 23, 3 }, 
            { 1, 23, 3 }, 
            { 11, 23, 3 } };
        [TestFixtureSetUp]
        public void SetUp()
        {

        }

        [Test]
        public void CalculateTest()
        {
            double[,] clusterCenters;
            var clusterIndices = KMeans.Calculate(trainingData, 2);
           
        }

    }
}
