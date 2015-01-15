using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dnAnalytics.LinearAlgebra;
using dnHTM.dnAnalyticsExtensions;

namespace dnHTM
{
    /// <summary>
    /// 
    /// </summary>
    public class Memory
    {
        private double _Epsilon = 1E-5;

        private List<Matrix> _c = new List<Matrix>();

        private Matrix _transitionMatrix;

        private int _lastPatternIndex;


        public List<Matrix> C
        {
            get
            {
                return _c;
            }
            set
            {
                _c = value;
            }
        }

        public void AddPattern(Matrix pattern)
        {
            if (_transitionMatrix == null)
            {
                _transitionMatrix = new DenseMatrix(1);
                _c.Add(pattern);
                _lastPatternIndex = 0;
                return;
            }
            var index = _c.FindIndex(matrix => pattern.NearEquals(matrix, _Epsilon));
            if (index >= 0)
            {
                _transitionMatrix[_lastPatternIndex, index] += 1;
            }
            else
            {
                _c.Add(pattern);
                var newPatternIndex = _c.Count - 1;
                _transitionMatrix = _transitionMatrix.InsertColumn(newPatternIndex, new DenseVector(newPatternIndex));
                _transitionMatrix = _transitionMatrix.InsertRow(newPatternIndex, new DenseVector(_c.Count));
                _transitionMatrix[_lastPatternIndex, newPatternIndex] += 1;
                _lastPatternIndex = newPatternIndex;
            }
        }

        public Matrix NormalizedTransitionMatrix
        {
            get
            {
                var normalized = NormalizeTranzitionMatrix(_transitionMatrix);
                return normalized;

            }
        }

        private static Matrix NormalizeTranzitionMatrix(Matrix matrix)
        {
            var normalizedMatrix = matrix.Clone();
            for (var i = 0; i < normalizedMatrix.Rows; i++)
            {
                var currentRow = normalizedMatrix.GetRow(i, 0, normalizedMatrix.Columns);
                var denumerator = currentRow.Sum();
                currentRow.Divide(denumerator);
                normalizedMatrix.SetRow(i, currentRow);

            }
            return normalizedMatrix;
        }


    }


}
