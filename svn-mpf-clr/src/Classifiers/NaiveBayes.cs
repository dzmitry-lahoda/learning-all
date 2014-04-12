using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using MatrixExtensions.Generic;
using MatrixExtensions.Manipulation;
using MatrixExtensions.Operations;

namespace Classifiers
{
    public class NaiveBayes 
    {
        private double[] _model;
        private int _numberOfClasses;

        private readonly Dictionary<int, int> _labe2NumberlMapping = new Dictionary<int, int>();
        private readonly Dictionary<int, int> _number2LableMapping = new Dictionary<int, int>();
        private readonly List<int> _trainNumberedClasses = new List<int>();

        public NaiveBayes() { }

        public void Fit(double[,] trainingData, int[] labels)
        {

            Contract.Requires<ArgumentException>(trainingData.RowCount() == labels.Length);
            foreach (var label in labels)
            {
                if (!_labe2NumberlMapping.ContainsKey(label))
                {
                    _labe2NumberlMapping[label] = _numberOfClasses;
                    _number2LableMapping[_numberOfClasses] = label;
                    _trainNumberedClasses.Add(_numberOfClasses);
                    _numberOfClasses++;

                }
                else
                {
                    var currentLabel = _labe2NumberlMapping[label];
                    _trainNumberedClasses.Add(currentLabel);
                }

            }
            var transferData = trainingData.Append(labels);
            var numberOfPoints = trainingData.GetLength(0);
            var numberOfVariables = trainingData.GetLength(1);
            var nominalValues = new int[numberOfVariables];
            var errorInfo = 0;
            _model = new double[0];
            var report = new alglib.bayes.nbcreport();
            alglib.bayes.nbcbuild(
                ref transferData,
                numberOfPoints,ref nominalValues,
                numberOfVariables, _numberOfClasses,
                1,ref errorInfo,
                ref _model,ref report
                );
            switch (errorInfo)
            {
                case -3:
                    throw new ArgumentException("в случае, если F[i]<0 (неверное число значений для i-ой переменной)");
                case -2:
                    throw new ArgumentException("если один из элементов XY имеет неверный номер переменной (за пределами диапазона, определенного массивом F) или неверный номер класса.");
                case -1:
                    throw new ArgumentException("в случае, если переданые неверные параметры (NPoints<0,NVars<1, NClasses<1).");
                case 1:
                    break;
                default:
                    throw new Exception("Unknown exception");
            }    
        }

        public int[] Predict(double[,] testingData)
        {
            var reponses = new List<int>();
            for (var i = 0; i < testingData.RowCount(); i++)
            {
                var sample = testingData.GetRow(i).ToArray();  
                var result = new double[_numberOfClasses];
                alglib.bayes.nbcprocess(ref _model, ref sample, ref result);
                var max = result.MaxPosition();
                reponses.Add(_number2LableMapping[max]);
            }
            var predictedLables = reponses.ToArray();
            return predictedLables;
        }
    }
}