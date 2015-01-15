using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ML.Service
{
    public class NaiveBayesService:INaiveBayesService
    {
        public void Fit(double[][] trainingData, int[] labels) {  }
        public int[] Predict(double[][] testingData) { return null; }
    }


}
