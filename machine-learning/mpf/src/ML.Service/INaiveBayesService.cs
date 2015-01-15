using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ML.Service
{
    [ServiceContract(Namespace= "mpf-clr.org")]
    public interface INaiveBayesService
    {

        [OperationContract]
        void Fit(double[][] trainingData, int[] labels);

        [OperationContract]
        int[] Predict(double[][] testingData);

    }


}
