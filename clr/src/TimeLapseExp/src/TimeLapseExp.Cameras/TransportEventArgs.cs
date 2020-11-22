using System;
using System.Collections.Generic;
using System.IO;

namespace TimeLapseExp.Cameras
{
    /// <summary>
    /// Provides data for the OlympusRyeTransport's events.
    /// </summary>
    [Serializable]
    public sealed class TransportEventArgs : EventArgs
    {

        private readonly IList<String> _pictureFullPathes;


        public TransportEventArgs(IList<String> pictureFullPathes)
        {
            _pictureFullPathes = pictureFullPathes;
        }


        public IList<string> PictureFullPathes
        {
            get { return _pictureFullPathes; }
        }

        public String GetPictureFullPath(int index)
        {
            return _pictureFullPathes[index];
        }

        public String GetPictureFullName(int index)
        {
            return Path.GetFileName(_pictureFullPathes[index]);
        }
    }
}
