using Module1;
using NUnit.Framework;
using MathNet.Numerics.LinearAlgebra.Double;

namespace NaiveBayes.Tests
{
    [TestFixture]
    public class NaiveBayes2Test
    {

        private double[,] trainingData = new double[,] { 
        { 1, 2, 34 }, 
        { 1, 23, 3 }, 
        { 1, 23, 3 }, 
        { 11, 23, 3 } };

        //sex
        private int[] labels = new[] { 0, 0, 0, 0, 1, 1, 1, 1 };

        // height/weight/size
        private double[,] trainingSet =

    new[,]{ {6,	180,	12},
	{5.92, 	190,	11},
	{5.58, 	170,	12},
	{5.92, 165,	10},
	{5,	100,	6},
	{5.5, 	150,	8},
	{5.42, 	130,	7},
    {	5.75,	150,	9}};

        private DenseMatrix TrainingData;
        private DenseMatrix TrainingSet;

        [TestFixtureSetUp]
        public void SetUp()
        {
            TrainingSet = new DenseMatrix(trainingSet);
                            TrainingData = new DenseMatrix(trainingData);
        }

        [Test]
        public void DataTest()
        {
            var NaiveBayes2 = new NaiveBayes2();
            NaiveBayes2.Fit(TrainingSet, labels);
            var result = NaiveBayes2.Predict(new DenseMatrix(new double[,] { { 6, 130, 8 } }));
            Assert.AreEqual(1, result[0]);//female
        }

        [Test]
        public void FitTest()
        {
            var NaiveBayes2 = new NaiveBayes2();
            NaiveBayes2.Fit(TrainingData, new[] { 1, 2, 2, 1 });
        }

        [Test]
        public void PredictTest()
        {
            var NaiveBayes2 = new NaiveBayes2();
            NaiveBayes2.Fit(TrainingData, new[] { 1, 2, 2, 1 });
            var result = NaiveBayes2.Predict(new DenseMatrix(new double[,] { { 1, 23, 3 } }));

            Assert.AreEqual(result[0], 2);
        }

    }
}