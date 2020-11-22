using System;

namespace TimeLapseExp.Cameras
{
    /// <summary>
    /// Is used to transfer and save raw picture data.
    /// </summary>
    [Serializable]
    public sealed class PictureFileData
    {
        private readonly String _name;

        private readonly byte[] _bytes;

        public PictureFileData(byte[] bytes, String name)
        {
            _bytes = (byte[])bytes.Clone();
            _name = name;
        }

        /// <summary>
        /// Gets size of picture file in bytes.
        /// </summary>
        public int Size
        {
            get
            {
                return _bytes.Length;
            }
        }

        public byte[] Bytes
        {
            get
            {
                return _bytes;
            }
        }

        /// <summary>
        /// Image filename with extension.
        /// </summary>
        public String Name
        {
            get
            {
                return _name;
            }
        }

    }
}