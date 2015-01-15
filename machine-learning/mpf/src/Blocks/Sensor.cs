using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Blocks;
using Blocks.Extensions;
using MathNet.Numerics.Distributions;
using MathNet.Numerics.LinearAlgebra.Double;
using MathNet.Numerics.LinearAlgebra.Generic;
using MPF.Blocks;


namespace dnHTM
{




    public class Sensor
    {

        public DistanceMeasure DefaultDistanceMeasure = DistanceMeasures.EuclidianDistance;

        public IContinuousDistribution DefaultDistribution = new Normal(1, 2);

        public Sensor()
        {

        }

        /// <summary>
        /// The output is a histogram giving estimates of probability of membership of the current
        ///input pattern in each temporal group of the node.
        /// </summary>
        /// <param name="node"></param>
        /// <param name="inputPattern"></param>
        /// <returns>Vector that indicates the degree of membership of the 
        /// input pattern in each of its temporal groups.</returns>
        public Vector<double> Sence(HtmNode node, Matrix<double> inputPattern)
        {
            var numberOfGroups = node.Content.TemporalGroups.Count;
            var degreesOfMembership = new DenseVector(numberOfGroups);
            var patterns = node.Content.Patterns;
            var probabilitiesOfMatch = CalculateProbabilitiesOfMatch(patterns, inputPattern); ;
            for (var i = 0; i < numberOfGroups; i++)
            {
                var currentGroup = node.Content.TemporalGroups[i];
                var max = .0;
                for (var j = 0; j < currentGroup.PatternLabels.Count; j++)
                {
                    var currentPatternLabel = currentGroup.PatternLabels[j];
                    if (probabilitiesOfMatch[currentPatternLabel] > max)
                    {
                        max = probabilitiesOfMatch[currentPatternLabel];
                    }
                }
                degreesOfMembership[i] = max;
            }
            Normalize(degreesOfMembership);
            return degreesOfMembership;
        }

        private static void Normalize(Vector degreesOfMembership)
        {
            var sum = degreesOfMembership.Sum();
            degreesOfMembership.Divide(sum);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="patterns"></param>
        /// <param name="inputPattern"></param>
        /// <returns>Vector that indicates the probability of  of membership of the 
        /// input pattern in each of its temporal groups.</returns>
        private Vector<double> CalculateProbabilitiesOfMatch(IList<Matrix<double>> patterns, Matrix<double> inputPattern)
        {
            var numberOfPatterns = patterns.Count;
            var probabilitiesOfMatch = new DenseVector(numberOfPatterns);
            var comparableVector = inputPattern.ToRowWiseVector();
            for (var i = 0; i < numberOfPatterns; i++)
            {
                var distance =
                    DefaultDistanceMeasure(comparableVector, patterns[i].ToRowWiseVector());
                if (distance == 0)
                {
                    //TODO:Some logic.
                }
                var e = System.Math.E;
                var sigma = 2;
                var probabilityOfMatch = System.Math.Exp(-distance * distance / sigma / sigma);
                //var probabilityOfMatch = DefaultDistribution.Density(distance);
                probabilitiesOfMatch[i] = probabilityOfMatch;
            }
            return probabilitiesOfMatch;
        }

        private Vector<double> CalculateDensityVector(Vector<double> distances)
        {
            var densityVector = new DenseVector(distances.Count);
            for (var i = 0; i < distances.Count; i++)
            {
                densityVector[i] = DefaultDistribution.Density(distances[i]);
            }
            return densityVector;
        }


        public List<Vector<double>> Sence(HtmNode node, IEnumerable<KeyValuePair<int, Vector<double>>> patterns)
        {
            var coincindense = new List<Vector<double>>();
            foreach (var pair in patterns)
            {
               var coincidence = Sence(node, pair.Value.ToRowMatrix());
               coincindense.Add(coincidence);
            }
            return coincindense;
        }
    }
}
