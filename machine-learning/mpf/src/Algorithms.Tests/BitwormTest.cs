using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using AlglibNet.DataAnalysys;
using Blocks;
using Classifiers;
using dnHTM.Wrappers;
using MathNet.Numerics.LinearAlgebra.Double;
using MatrixExtensions.Conversion.Generic.Specialized;
using NUnit.Framework;
using MatrixExtensions.Conversion;


using AlglibSharp.DataAnalysys;
using dnHTM;
using dnHTM.Extensions;
using SVM;

namespace dnHTMTests
{



    public static  class ReadHelper
        {
        public static StreamReader CreateSolidReadOnlyFileStream(string filePath)
        {
            var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            var reader = new StreamReader(fileStream);
            
            return reader;
        }
    
}

    [TestFixture]
    public class BitwormTest
    {

        private static String initialPath = @"C:\Program Files\Numenta\nupic-1.7.1\share\projects\bitworm\";

        private static String trainingData = Path.Combine(initialPath, @"training_data.txt");
        private static String trainingCategories = Path.Combine(initialPath, @"training_categories.txt");
        private static String testData = Path.Combine(initialPath, "test_data.txt");
        private static String testCategories = Path.Combine(initialPath, "test_categories.txt");

        public double[,] ReadData(string filePath)
        {
            var text = File.ReadAllLines(filePath);
            var reader = new DoubleMatrixReader(" ");
            var matrix = reader.ReadMatrix(filePath);
            return matrix;
        }



        public int[] ReadCategories(string filePath)
        {
            var text = File.ReadAllLines(filePath);
            var asd = new int[text.Length];
            for (var i = 0; i < text.Length; i++)
            {
                asd[i] = Int32.Parse(text[i]);
            };
            return asd;
        }

        [Test]
        public void NeuralNetworkClassifierOnlyTest()
        {
            var cats = ReadCategories(trainingCategories);
            var data = ReadData(trainingData);
            var naiveBayes = new NeuralNetworkClassifier();
            naiveBayes.Fit(data, cats);
            var result = naiveBayes.Predict(data);
            var asd = PredictionAccuracy.Calculate(cats, result);
            cats = ReadCategories(testCategories);
            data = ReadData(testData);
            result = naiveBayes.Predict(data);
            asd = PredictionAccuracy.Calculate(cats, result);
        }
  
        

        [Test]
        public void NaiveBayesOnlyTest()
        {
            var cats = ReadCategories(trainingCategories);
            var data = ReadData(trainingData);
            var naiveBayes = new NaiveBayes();
            naiveBayes.Fit(data, cats);
            var result = naiveBayes.Predict(data);
            var asd = PredictionAccuracy.Calculate(cats, result);
            cats = ReadCategories(testCategories);
            data = ReadData(testData);
            result = naiveBayes.Predict(data);
            asd = PredictionAccuracy.Calculate(cats, result);
        }

        [Test]
        public void SVMOnlyTest()
        {
            var cats = ReadCategories(trainingCategories);
            var data = ReadData(trainingData);
            var numberOfVectors = cats.Length;
            var maxIndex = data.GetLength(1);
            var problem = new Problem(numberOfVectors, cats.ToDouble(), data.ToJagged((x,r,c)=> new SVM.Node(c,x)), maxIndex);
            var range = RangeTransform.Compute(problem);
            problem = range.Scale(problem);
            var param = new Parameter();
            var model = Training.Train(problem, param);
            var value = Prediction.Predict(problem,"problem", model,false);
            var accuracy = new PredictionAccuracy(value);
            cats = ReadCategories(testCategories).ToArray();
            data = ReadData(testData);
            var preditionResults = SVMPrediction.Predict(model, data.GetJaggedArray());
            accuracy = PredictionAccuracy.Calculate(cats, preditionResults);
 
        }

        [Test]
        public void Test()
        {

            var cats = ReadCategories(trainingCategories);
            var node = new HtmNode(1);
            node.Content = new FirstOrderMarkovChain();
            var data = ReadData(trainingData);
            foreach (var image in new DenseMatrix(data).RowEnumerator())
            {
                var qwe = new DenseVector(image.Value).CreateMatrix(1, image.Value.Count);
                qwe.SetRow(0,image.Value);
                node.Content.AddPattern(qwe);
            }
            var groups = new TemporalGrouper().DoGrouping(node.Content.NormalizedTransitionMatrix, 7);
            var sensore = new Sensor();
            node.Content.TemporalGroups = groups;
            var patterns = new DenseMatrix(data).RowEnumerator();
            var coincidences = sensore.Sence(node, patterns);
            var naiveBayes = new NaiveBayes();
            var qweqw = coincidences[0].ToRowMatrix();
            var rows = coincidences.Count;
            var cols = coincidences[0].Count;
            Matrix matirix = new DenseMatrix(rows,cols); ;
            for (int i = 0; i < rows; i++)
            {
                var qqqq = coincidences[i];
                matirix.SetRow(i,qqqq);
            }
            
            var dataS = matirix.ToArray();
            naiveBayes.Fit(dataS, cats);

            var result = naiveBayes.Predict(dataS);
            var asd = new PredictionAccuracy(cats, result);
           
            //using test data
            cats = ReadCategories(testCategories);
            data = ReadData(testData);
            patterns = new DenseMatrix( data).RowEnumerator();
            coincidences = sensore.Sence(node, patterns);
            rows = coincidences.Count;
            cols = coincidences[0].Count;
            matirix = new DenseMatrix(rows, cols); ;
            for (int i = 0; i < rows; i++)
            {
                var qqqq = coincidences[i];
                matirix.SetRow(i, qqqq);
            }
            dataS = matirix.ToArray();
            result = naiveBayes.Predict(dataS);
            asd = new PredictionAccuracy(cats, result);
            Console.Write(asd.AccuracyValue);
        }

        

        
    }
}
