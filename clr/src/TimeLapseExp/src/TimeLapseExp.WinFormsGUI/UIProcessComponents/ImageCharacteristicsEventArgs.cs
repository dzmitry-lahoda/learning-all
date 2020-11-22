using System;

namespace TimeLapseExp.WinFormsGUI
{
    /// <summary>
    /// Provides data for the ImageCharacteristics's events.
    /// </summary>
    [Serializable]
    internal sealed class ImageCharacteristicsEventArgs : EventArgs
    {

        private readonly double _returnedValue=Int32.MinValue;

        private readonly String _pictureFullName;

        private readonly int _exposureTime = Int32.MinValue;

        private readonly int _focusPosition = Int32.MinValue;

        private String _pictureDescription;

        public ImageCharacteristicsEventArgs(
            double returnedValue,
            String pictureFullName,
            int exposureTime,
            int focusPosition,
            String pictureDescription)
        {
            _returnedValue = returnedValue;
            _pictureDescription = pictureDescription;
            _pictureFullName = pictureFullName;
            _exposureTime = exposureTime;
            _focusPosition = focusPosition;
            

        }

        public double ReturnedValue
        {
            get
            {
                return _returnedValue;
            }
        }

        public String PictureDescription
        {
            get
            {
                return _pictureDescription;
            }
        }

        public String PictureFullName
        {
            get
            {
                return _pictureFullName;
            }
        }

        public int ExposureTime
        {
            get
            {
                return _exposureTime;
            }
        }

        public int FocusPosition
        {
            get
            {
                return _focusPosition;
            }
        }

    }
}