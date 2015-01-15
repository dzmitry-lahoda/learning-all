using System;

namespace MPF.Blocks
{
    public class KMeansOptions
    {
        private int _numberOfRestarts = 1;

        public KMeansOptions(){}

        public KMeansOptions(int numberOfRestarts)
        {
            _numberOfRestarts = numberOfRestarts;
        }

        public int NumberOfRestarts
        {
            get
            {
                return _numberOfRestarts;
            }
            set
            {
                if (value<=0)
                {
                    throw new ArgumentException("Number of restarts should be >0.");
                }
                _numberOfRestarts = value;

            }
        }
       
    }
}
