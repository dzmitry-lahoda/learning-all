using System;

namespace TimeLapseExp.Cameras
{
    /// <summary>
    /// Contains information about camera.
    /// </summary>
    [Serializable]
    //TODO:Implemtns ToString, Equal, GetHashCode and Clone methods.
    public sealed class CameraInfo:IEquatable<CameraInfo>
    {
        private String _name;

        private int _remainPictureCount;

        private int _pictureCount;

        private MemoryCardInfo _memoryCardInfo = new MemoryCardInfo();

        private CaptureInfo _captureInfo= new CaptureInfo();

        public CameraInfo()
        {
            
        }

        public CaptureInfo CaptureInfo
        {
            get
            {
                return _captureInfo;
            }
            set
            {
                _captureInfo = value;
            }
        }

        /// <summary>
        /// Gets camera _name.
        /// </summary>
        public String Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }



        /// <summary>
        /// Gets count of pictures on the flash.
        /// </summary>
        public int PictureCount
        {
            get
            {
                return _pictureCount;
            }
            set
            {
                _pictureCount = value;
            }
        }

        /// <summary>
        /// Gets number of pictures of current _resolution and format can be written to flash.
        /// </summary>
        public int RemainPictureCount
        {
            get
            {
                return _remainPictureCount;
            }
            set
            {
                _remainPictureCount = value;
            }
        }

        public MemoryCardInfo MemoryCardInfo
        {
            get
            {
                return _memoryCardInfo;
            }
            set
            {
                _memoryCardInfo = value;
            }
        }



        public bool Equals(CameraInfo other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other._name, _name) && other._remainPictureCount == _remainPictureCount && other._pictureCount == _pictureCount && Equals(other._memoryCardInfo, _memoryCardInfo) && Equals(other._captureInfo, _captureInfo);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (CameraInfo)) return false;
            return Equals((CameraInfo) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int result = (_name != null ? _name.GetHashCode() : 0);
                result = (result*397) ^ _remainPictureCount;
                result = (result*397) ^ _pictureCount;
                result = (result*397) ^ (_memoryCardInfo != null ? _memoryCardInfo.GetHashCode() : 0);
                result = (result*397) ^ (_captureInfo != null ? _captureInfo.GetHashCode() : 0);
                return result;
            }
        }
    }
}
