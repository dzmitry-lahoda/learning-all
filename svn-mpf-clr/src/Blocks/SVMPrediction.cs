using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MatrixExtensions.Operations;
using SVM;

namespace dnHTM.Wrappers
{
    public static class SVMPrediction
    {
        public static int[] Predict(Model model, Node[][] points)
        {
            var predictedCategories = new int[points.Length];

            for (var i = 0; i < points.Length; i++)
            {
                var probabilities = Prediction.PredictProbability(model, points[i]);
                var category = probabilities.MaxPosition();
                predictedCategories[i] = category;
            }
            return predictedCategories;
        }
    }
}
