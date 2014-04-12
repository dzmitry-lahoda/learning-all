using System;
using System.Collections.Generic;
using Blocks.Extensions;
using dnHTM;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using MathNet.Numerics.LinearAlgebra.Generic;

namespace Blocks
{
    /// <summary>
    /// Represnets class which stores pattenrs and transition probabilities.
    /// Each <see cref="HtmNode"/> has it's <see cref="FirstOrderMarkovChain"/>.
    /// </summary>
    public class FirstOrderMarkovChain
    {
        public const int MaximumNumberOfTemporalGroups = 5;

       //public List<TemporalGroup> TemporalGroups;

        private List<Matrix<double>> _patterns = new List<Matrix<double>>();

        private Matrix<double> _transitionMatrix;

        private int _lastPatternIndex;

        /// <summary>
        /// Gets all patterns presented to memory.
        /// </summary>
        public List<Matrix<double>> Patterns
        {
            get
            {
                return _patterns;
            }
            set
            {
                _patterns = value;
            }
        }

        /// <summary>
        /// Get normalized Transition probabilites matrix.
        /// </summary>
        public Matrix<double> NormalizedTransitionMatrix
        {
            get
            {
                var normalized = NormalizeTranzitionMatrix(_transitionMatrix);
                return normalized;

            }
        }

        /// <summary>
        /// Get transition matrix.
        /// </summary>
        public Matrix<double> TransitionMatrix
        {
            get
            {
                return _transitionMatrix;
            }
        }

        public List<TemporalGroup> TemporalGroups;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="transitionMatrix"></param>
        /// <returns></returns>
        public static bool IsValidTransitionMatrix(Matrix<double> transitionMatrix)
        {
            //if (!transitionMatrix.IsQuadratic()) return false;
            for (int i = 0; i < transitionMatrix.RowCount; i++)
            {
                var row = transitionMatrix.Row(i);
                if (!DefaultComparing.NearEqual(row.Sum(), 1))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Adds new pattern and changes Transition matix.
        /// </summary>
        /// <param name="pattern"></param>
        public void AddPattern(Matrix<double> pattern)
        {
            if (_transitionMatrix == null)
            {
                InitializeTransitionMatrixIfFirstPattern(pattern);
                return;
            }
            var index = _patterns.FindIndex(matrix => pattern.NearEquals(matrix));
            if (index >= 0)
            {
                _transitionMatrix[_lastPatternIndex, index] += 1;
            }
            else
            {
                AddNewPatternAndUpdateTransitionMatrix(pattern);
            }
        }

        /// <summary>
        /// Adds new patterns in a sequence.
        /// </summary>
        /// <param name="patterns"></param>
        public void AddPatterns(params Matrix<double>[] patterns)
        {
            foreach (var pattern in patterns)
            {
                  AddPattern(pattern);
            }
        }

        private void AddNewPatternAndUpdateTransitionMatrix(Matrix<double> pattern)
        {
            _patterns.Add(pattern);
            var newPatternIndex = _patterns.Count - 1;
            _transitionMatrix = _transitionMatrix.InsertColumn(newPatternIndex, new DenseVector(newPatternIndex));
            _transitionMatrix = _transitionMatrix.InsertRow(newPatternIndex, new DenseVector(_patterns.Count));
            _transitionMatrix[_lastPatternIndex, newPatternIndex] += 1;
            _lastPatternIndex = newPatternIndex;
        }

        /// <summary>
        /// Adds new patterns and set ups initial state of transition matrix.
        /// </summary>
        /// <param name="pattern"></param>
        private void InitializeTransitionMatrixIfFirstPattern(Matrix<double> pattern)
        {
            _transitionMatrix = new DenseMatrix(1);
            _transitionMatrix[0, 0] = 0;
            _patterns.Add(pattern);
            _lastPatternIndex = 0;
        }



        /// <summary>
        /// Makes sum of all possible trasintion probabilities from one vertex to 1.
        /// </summary>
        /// <param name="matrix"></param>
        /// <returns></returns>
        private static Matrix<double> NormalizeTranzitionMatrix(Matrix<double> matrix)
        {
            var normalizedMatrix = matrix.Clone();
            for (var i = 0; i < normalizedMatrix.RowCount; i++)
            {
                var currentRow = normalizedMatrix.Row(i, 0, normalizedMatrix.ColumnCount);
                var denumerator = currentRow.Sum();
                var canDivide = DefaultComparing.Compare(denumerator, 0) != 0;
                if (canDivide)
                {
                   currentRow =  currentRow.Divide(denumerator);
                }
                normalizedMatrix.SetRow(i, currentRow);
            }
            return normalizedMatrix;
        }

        public override string ToString()
        {
            return String.Format(
                "Number of patterns: {0}, Transition matrix size: {1}, Transition matrix: \n\t{2}", 
                _patterns.Count, 
                _transitionMatrix.RowCount,
                NormalizeTranzitionMatrix(_transitionMatrix)
                );
        }
    }


}
