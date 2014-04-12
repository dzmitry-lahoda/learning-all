using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MPF.Blocks;

namespace AlglibNet.DataAnalysys
{
    public class KMeans
    {

        private static KMeansOptions _kMeansOptions=new KMeansOptions();

        public static KMeansInfo Calculate(double[,] points, int numberOfClusters)
        {
            return Calculate(points, numberOfClusters,_kMeansOptions);
        }

        public static KMeansInfo Calculate(double[,] points, int numberOfClusters,KMeansOptions options)
        {

            var numberOfPoints = points.GetLength(0);
            var numberOfVariables = points.GetLength(1);
            var restarts = options.NumberOfRestarts;
            var info = 0;
            var clusterCenters = new double[0, 0];
         
            var clusterIndices = new int[0];
            alglib.kmeans.kmeansgenerate(points, numberOfPoints, numberOfVariables, numberOfClusters, restarts,
                                  ref info, ref clusterCenters, ref clusterIndices);
            
            var message = "";
            switch (info)
            {
                case -3:
                    message = "taskis degenerate (number of distinct points is less than K";
                    throw new ArgumentException(message);
                case -1:
                    message = "incorrect NPoints/NFeatures/K/Restarts was passed";
                    throw new ArgumentException(message);

                case 1:
                    break;
                default:
                    throw new Exception(message);
            }
            var kMeanInfo = new KMeansInfo{ClusterCenters = clusterCenters, ClusterIndices = clusterIndices}; 
            return kMeanInfo;
        }

    }
}