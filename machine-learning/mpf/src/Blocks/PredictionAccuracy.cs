using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dnHTM
{
    public class PredictionAccuracy
    {
        private double _accuracyValue;

        public double AccuracyValue
        {
            get
            {
                return _accuracyValue;
            }
        }

        public PredictionAccuracy(double value)
        {
            _accuracyValue = value;
        }

        public PredictionAccuracy(int[] categories, int[] predictedCategories)
        {
            var z = 0;
            for (int i = 0; i < categories.Length; i++)
            {
                if (categories[i] == predictedCategories[i])
                {
                    z++;
                }
            }
            _accuracyValue = 1.0 * z / categories.Length;
        }

        public static PredictionAccuracy Calculate(int[] categories, int[] predictedCategories)
        {
            return new PredictionAccuracy(categories, predictedCategories);
        }

        public override string ToString()
        {
            return string.Format("AccuracyValue: {0}", _accuracyValue);
        }
    }
}
