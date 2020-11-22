using System;

namespace TimeLapseExp.Cameras
{
    /// <summary>
    /// Contains information about current camera setups
    /// which influence how picture will look like.
    /// </summary>
    [Serializable]
    public class CaptureInfo
    {

        private Capture _capture = new Capture();

        private Exposure _exposure = new Exposure();

        private Focus _focus = new Focus();

        private int _exposureBias;

        private int _opticalZoom;

        private Resolutions _resolution;

        private FlashModes _flashMode;

        private ISOSpeed _isoSpeed;

        private WhiteBalance _whiteBalance;


        public CaptureInfo()
        {
            
        }

        /// <summary>
        /// Get or sets _capture.
        /// </summary>
        public Capture Capture
        {
            get
            {
                return _capture;
            }
            set
            {
                _capture = value;
            }
        }


        /// <summary>
        /// Get or sets exposure.
        /// </summary>
        public Exposure Exposure
        {
            get
            {
                return _exposure;
            }
            set
            {
                _exposure = value;
            }
        }


        /// <summary>
        /// Get or sets focus.
        /// </summary>
        public Focus Focus
        {
            get
            {
                return _focus;
            }
            set
            {
                _focus = value;
            }
        }

        /// <summary>
        /// Gets or sets exposure bias.
        /// </summary>
        public int ExposureBias
        {
            get
            {
                return _exposureBias;
            }
            set
            {
                _exposureBias = value;
            }
        }


        /// <summary>
        /// Gets or sets resolution and image format.
        /// </summary>
        public Resolutions Resolution
        {
            get
            {
                return _resolution;
            }
            set
            {
                _resolution = value;
            }
        }


        /// <summary>
        /// Gets or sets flash mode.
        /// </summary>
        public FlashModes FlashMode
        {
            get
            {
                return _flashMode;
            }
            set
            {
                _flashMode = value;
            }
        }

        /// <summary>
        /// Gets or sets optical zoom.
        /// </summary>
        public int OpticalZoom
        {
            get
            {
                return _opticalZoom;
            }
            set
            {
                _opticalZoom = value;
            }
        }

        /// <summary>
        /// Gets or sets ISO speed(RYE_ISO_AUTO, RYE_ISO_ISO100, RYE_ISO_ISO200, RYE_ISO_ISO400). 
        /// ISO speed is the measure of a photographic sensor's sensitivity to light.
        /// Sensor with lower sensitivity (lower ISO/ASA speed) requires a longer _exposure and is thus called a slow sensor, 
        /// while stock with higher sensitivity (higher ISO/ASA speed) can shoot the same scene with a shorter _exposure and is called a fast sensor.
        /// </summary>
        public ISOSpeed ISOSpeed
        {
            get
            {
                return _isoSpeed;
            }
            set
            {
                _isoSpeed = value;
            }
        }

        /// <summary>
        /// Gets or sets white balance.
        /// White balance refers to the adjustment of the relative amounts of red, green, and blue primary colors in an image such
        /// that neutral colors are reproduced correctly.
        /// Color balance changes the overall mixture of colors in an image and is used for generalized color correction.
        /// (White balance refers to the adjustment of the color temperature of an image in order to achieve a white display of objects that are usually percieved as white).
        /// </summary>
        public WhiteBalance WhiteBalance
        {
            get
            {
                return _whiteBalance;
            }
            set
            {
                _whiteBalance = value;
            }
        }

    }
}
