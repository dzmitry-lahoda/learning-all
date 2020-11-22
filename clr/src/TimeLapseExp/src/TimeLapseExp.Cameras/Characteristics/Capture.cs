namespace TimeLapseExp.Cameras
{
    //TODO:Implemtns ToString, Equal, GetHashCode and Clone methods.
    public sealed class Capture
    {
        private int continuousTime;

        private int continuousNumber;
        
        private CaptureModes mode = CaptureModes.Normal;

        public Capture()
        {

        }

        /// <summary>
        /// Constructs new Capture object.
        /// </summary>
        /// <param name="mode"></param>
        /// <param name="continuousTime"></param>
        /// <param name="continuousNumber"></param>
        public Capture(CaptureModes mode, int continuousTime, int continuousNumber)
        {
            Mode = mode;
            ContinuousTime = continuousTime;
            ContinuousNumber = continuousNumber;
        }


        /// <summary>
        /// Gets or sets capture mode.
        /// </summary>
        public CaptureModes Mode
        {
            get
            {
                return mode;
            }
            set
            {
                mode = value;
            }
        }


        /// <summary>
        /// Gets or sets an interval in milliseconds(10E-3 second)  between captures in sequential(continuous) shooting(capture) mode.
        /// </summary>
        public int ContinuousTime
        {
            get
            {
                return continuousTime;
            }
            set
            {
                if (continuousTime > 0)
                {
                    continuousTime = value;
                }
            }
        }


        /// <summary>
        /// Gets or sets quantity(number) of frames to capture(shoot) in sequential(continuous) shooting(capture) mode.
        /// </summary>
        public int ContinuousNumber
        {
            get
            {
                return continuousNumber;
            }
            set
            {
                if (continuousNumber>0)
                {
                    continuousNumber = value;
                }
            }
        }
    }
}
