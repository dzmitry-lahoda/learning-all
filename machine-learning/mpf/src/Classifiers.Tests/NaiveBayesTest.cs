using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Classifiers;
using NUnit.Framework;


namespace AlglibSharpTests
{
    [TestFixture]
    public class NaiveBayesTest
    {

        private  double[,] trainingData = new double[,] { 
        { 1, 2, 34 }, 
        { 1, 23, 3 }, 
        { 1, 23, 3 }, 
        { 11, 23, 3 } };

        //sex
        private int[] labels = new []{0,0,0,0,1,1,1,1};

        // height/weight/size
        private double[,] trainingSet=

	new [,]{ {6,	180,	12},
	{5.92, 	190,	11},
	{5.58, 	170,	12},
	{5.92, 165,	10},
	{5,	100,	6},
	{5.5, 	150,	8},
	{5.42, 	130,	7},
    {	5.75,	150,	9}};

        [TestFixtureSetUp]
        public void SetUp()
        {

        
        }

        [Test]
        public void DataTest()
        {
            var naiveBayes = new NaiveBayes();
            naiveBayes.Fit(trainingSet, labels );
            int[] result = naiveBayes.Predict(new double[,] { { 6, 130, 8 } });
            Assert.AreEqual(1, result[0]);//female
        }

        [Test]
        public void FitTest()
        {
            var naiveBayes = new NaiveBayes();
            naiveBayes.Fit(trainingData, new[] { 1, 2, 2, 1 });
        }

        [Test]
        public void PredictTest()
        {
            var naiveBayes = new NaiveBayes();
            naiveBayes.Fit(trainingData, new[] { 1, 2, 2, 1 });
            int[] result = naiveBayes.Predict(new double[,] { { 1, 23, 3 } });
            
            Assert.AreEqual(result[0], 2);
        }

    }
}