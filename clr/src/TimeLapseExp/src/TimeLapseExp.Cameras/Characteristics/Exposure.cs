using System;

namespace TimeLapseExp.Cameras
{
    /// <summary>
    /// Represents class which stores information about exposure mode
    /// </summary>
    [Serializable]
    //TODO:Implemtns ToString, Equal, GetHashCode and Clone methods.
    public sealed class Exposure
    {

        private ExposureModes mode = ExposureModes.Manual;

        private int time;

        private int aperture;

        public Exposure()
        {
        }

        public Exposure(ExposureModes mode, int time, int aperture)
        {
            Mode = mode;
            Time = time;
            Aperture = aperture;
        }

        public Exposure Clone()
        {
            return new Exposure(Mode, Time, Aperture);
        }


        public int Time
        {
            get
            {
                return time;
            }
            set
            {
                time = value;
            }
        }


        public ExposureModes Mode
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


        public int Aperture
        {
            get
            {
                return aperture;
            }
            set
            {
                aperture = value;
            }
        }

        public void AddExposureTime(int exposureTimeStep)
        {
            Time += exposureTimeStep;
        }
    }
}
