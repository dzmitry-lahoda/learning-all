using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using dnAnalytics.LinearAlgebra;
using dnHTM.dnAnalyticsExtensions;

namespace dnHTM
{
    public class AgglomerativeHierarchicalClustering
    {
        private List<Tuple<int, int>> _junctions = new List<Tuple<int, int>>();

        public List<TemporalGroup> DoClustering(Matrix normalizedTranzitionMatrix, int maximumNumberOfTemporalGroups)
        {
            Contract.Requires(normalizedTranzitionMatrix.IsQuadratic());
            Contract.Requires(normalizedTranzitionMatrix.IsDiagonalSymmetric());
            Contract.Requires(normalizedTranzitionMatrix.Rows >= maximumNumberOfTemporalGroups);
            var pairWiseSimilarities = GetPairWiseSimilarities(normalizedTranzitionMatrix);
   
            var transitionMatrix = normalizedTranzitionMatrix.Clone();
                
            var clusters = new List<TemporalGroup>();
            while (transitionMatrix.Rows>maximumNumberOfTemporalGroups)
            {
                
                pairWiseSimilarities = GetPairWiseSimilarities(transitionMatrix);
                var currentPair = new Tuple<int, int>(0, 1);
                var max = 0d;
                foreach (var pair in pairWiseSimilarities.Keys)
                {
                    var value = pairWiseSimilarities[pair];
                    if (value > max)
                    {
                        max = value;
                        currentPair = pair;
                    }

                }
                _junctions.Add(currentPair);
                var firstRow = transitionMatrix.GetRow(currentPair.Item1);
                var secondRow = transitionMatrix.GetRow(currentPair.Item2);
                var newRow = firstRow + secondRow;

                transitionMatrix.SetRow(currentPair.Item1, newRow);
                var zeros = new double[transitionMatrix.Columns];

                transitionMatrix.SetRow(currentPair.Item2, zeros);
                transitionMatrix.SetColumn(currentPair.Item2, zeros); 
            }
            


            return clusters;
        }

        private Dictionary<Tuple<int, int>, double> GetPairWiseSimilarities(Matrix normalizedTranzitionMatrix)
        {
            var transitionMatrix = normalizedTranzitionMatrix.Clone();
            var pairWiseSimilarities = new Dictionary<Tuple<int, int>, double>();
            for (var i = 0; i < transitionMatrix.Rows; i++)
            {
                for (var j = i; j < transitionMatrix.Columns; j++)
                {
                    var pair = new Tuple<int, int>(i, j);
                    var similarityMeasurment = transitionMatrix[i, j] + transitionMatrix[j, i];
                    pairWiseSimilarities.Add(pair, similarityMeasurment);
                }
            }
            return pairWiseSimilarities;
        }
    }
}
