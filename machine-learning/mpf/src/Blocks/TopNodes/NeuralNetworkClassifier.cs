using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using MatrixExtensions.Generic;
using MatrixExtensions.Manipulation;
using MatrixExtensions.Operations;

namespace AlglibSharp.DataAnalysys
{
    public class NeuralNetworkClassifier
    {
        private alglib.mlpbase.multilayerperceptron _network = new alglib.mlpbase.multilayerperceptron();

        public void Fit(double[,] trainingData, int[] labels)
        {
            var numberOfInput = trainingData.GetLength(1);
            var numberOfOut = labels.Distinct().Count();
            var numberOfPoints = trainingData.GetLength(0);
            var info = 0;
            var report = new alglib.mlptrain.mlpreport();
            alglib.mlpbase.mlpcreatec0(numberOfInput, numberOfOut, _network);
            var data = trainingData.Append(labels);
            alglib.mlptrain.mlptrainlm(
                 _network,
                data,
                numberOfPoints,
                0.001, numberOfOut, ref info, report);
           switch (info)
            {
                case 2:
                    break;
                default:
                    throw new Exception(
                        @"* -9, if internal matrix inverse subroutine failed
* -2, if there is a point with class number outside of [0..NOut-1].
* -1, if wrong parameters specified (NPoints<0, Restarts<1).");
            }    
        }

        public int[] Predict(double[,] testingData)
        {
            var rows = testingData.RowCount();
            var columns = testingData.ColumnCount();
            var preditionResults = new int[rows];
            for (int i = 0; i < rows; i++)
            {
                var probabilities = new double[columns];
                var point = testingData.GetRow(i);
                alglib.mlpbase.mlpprocess(_network, point, ref probabilities);
                preditionResults[i] = probabilities.MaxPosition();
            }
            return preditionResults;
        }

    }
}