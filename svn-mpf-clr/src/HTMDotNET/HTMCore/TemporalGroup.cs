using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dnAnalytics.LinearAlgebra;

namespace dnHTM
{
    public class TemporalGroup
    {
        private List<int> _patternLabels=new List<int>();

        public List<int> PatternLabels
        {
            get
            {
                return _patternLabels;
            }
            set
            {
                _patternLabels = value;
            }
        }

        public void AddPatternLabel(int pattern)
        {
            _patternLabels.Add(pattern);
        }
    }
}
