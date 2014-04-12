using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using Blocks.Extensions;
using MathNet.Numerics.LinearAlgebra.Generic;


namespace dnHTM
{
    /// <summary>
    /// This class provides functionality for temporal grouping 
    /// of Markov graph nodes represented by transition matrix.
    ///  </summary>
    public class TemporalGrouper
    {

        /// <summary>
        /// Returns list of <see cref="TemporalGroup"/>.
        /// Agglomerative Hierarchical Clustering is used.
        /// Summed probability is used as distance measure.
        /// </summary>
        /// <param name="normalizedTranzitionMatrix"></param>
        /// <param name="maximumNumberOfTemporalGroups"></param>
        /// <returns></returns>
        //TODO: Build Dendrogram if nesserary.
        public List<TemporalGroup> DoGrouping(Matrix<double> normalizedTranzitionMatrix, int maximumNumberOfTemporalGroups)
        {
            Contract.Requires(normalizedTranzitionMatrix.IsQuadratic());
            Contract.Requires(normalizedTranzitionMatrix.RowCount >= maximumNumberOfTemporalGroups);

            var transitionMatrix = normalizedTranzitionMatrix.Clone();
            var clusters = new List<TemporalGroup>(transitionMatrix.RowCount);
            for (var i = 0; i < transitionMatrix.RowCount; i++)
            {
                clusters.Add(new TemporalGroup(i));
            }

            var numberOfSteps = transitionMatrix.RowCount - maximumNumberOfTemporalGroups;

            for (int i = 0; i < numberOfSteps; i++)
            {                
                var pairWiseSimilarities = GetPairWiseSimilarities(transitionMatrix);
                var currentPair = new Tuple<int, int>(0, 1);
                var max = 0d;
                foreach (var pair in pairWiseSimilarities.Keys)
                {
                    var value = pairWiseSimilarities[pair];
                    if (value <= max) continue;
                    max = value;
                    currentPair = pair;
                }
                
                var firstRow = transitionMatrix.Row(currentPair.Item1);
                var secondRow = transitionMatrix.Row(currentPair.Item2);
                var newRow = firstRow + secondRow;

                clusters[currentPair.Item1].
                    AddPatternLabels(clusters[currentPair.Item2].PatternLabels.ToArray());
                transitionMatrix.SetRow(currentPair.Item1, newRow);
                
                clusters[currentPair.Item2].PatternLabels.Clear();
                var zeros = new double[transitionMatrix.ColumnCount];
                transitionMatrix.SetRow(currentPair.Item2, zeros);
                transitionMatrix.SetColumn(currentPair.Item2, zeros); 
            }
            var temporalClusters = new List<TemporalGroup>();
            foreach (var group in clusters)
            {
                if (!group.IsEmpty())
                {
                    temporalClusters.Add(@group);
                }
            }
            return temporalClusters;
        }

        /// <summary>
        /// This method calculate and returns <see cref="Dictionary{TKey,TValue}"/> 
        /// where key is <see cref="Tuple{TItem1,TItem2}"/> which pointers on
        /// elements' indexes and value is summed probability of transition 
        /// form one state to another and vice versa.  
        /// </summary>
        /// <param name="normalizedTranzitionMatrix"></param>
        /// <returns></returns>
        public static Dictionary<Tuple<int, int>, double> GetPairWiseSimilarities(Matrix<double> normalizedTranzitionMatrix)
        {
            var transitionMatrix = normalizedTranzitionMatrix.Clone();
            var pairWiseSimilarities = new Dictionary<Tuple<int, int>, double>();
            var steps = transitionMatrix.RowCount;

            for (var i = 0; i < steps; i++)
            {
                for (var j = i+1; j < steps; j++)
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
