using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dnHTM
{
    /// <summary>
    /// Represnt class which stores list of lables.
    /// </summary>
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

        public bool IsEmpty()
        {
            var isEmpty = _patternLabels.Count == 0;
            return isEmpty;
        }

        public TemporalGroup(params int[] lables)
        {
            AddPatternLabels(lables);
        }

        public void AddPatternLabel(int pattern)
        {
            _patternLabels.Add(pattern);
        }

        public void AddPatternLabels(params int[] patterns)
        {
            foreach (var pattern in patterns)
            {
                _patternLabels.Add(pattern);
     
            }
                   }
    }
}
