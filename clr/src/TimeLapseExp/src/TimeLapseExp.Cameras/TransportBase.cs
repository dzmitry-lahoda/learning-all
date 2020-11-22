using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TimeLapseExp.Cameras;
using TimeLapseExp.Instrumentation;

namespace TimeLapseExp.Cameras
{
    public abstract class TransportBase
    {

        private bool _cancellationRequested;

        public FileNamingCallback FileNamingCallback = CreateImageName;

        /// <summary>
        /// Invoked when requested number of pictures was saved.
        /// </summary>
        public abstract event TransportEventHandler PicturesSaved;

        //        public abstract event TransportEventHandeler ReportProgress;
        //                
        public void Cancel()
        {
            _cancellationRequested = true;
        }
        //
        //        public abstract bool IsTransfering();

        /// <summary>
        /// Gets photos from camera's flash.
        /// </summary>
        /// <param name="initialIndex">Zero based index of first photo.</param>
        /// <param name="photoCount">Specifies number of photos to read.</param>
        /// <param name="eraseAfter">true if photos after reading should will be deleted;otherwise, false.</param>
        /// <param name="storeDirectory">The directory into what photos will be written.</param>
        /// <returns>List of file names.</returns>
        public abstract IList<String> GetPictures(int initialIndex, int photoCount, bool eraseAfter,
                                                  String storeDirectory, bool rename);

        public abstract IList<String> GetAllPictures(bool eraseAfter,
                                          String storeDirectory, bool rename);

        /// <summary>
        /// Gets prview picture.
        /// </summary>
        /// <returns></returns>
        public abstract String GetPreview(String storeDirectory);

        /// <summary>
        /// Erases all pictures from flash card.
        /// </summary>
        public abstract void EraseAllPictures();

        /// <summary>
        /// Erases last picture from flash.
        /// </summary>
        public abstract void EraseLast();

        /// <summary>
        /// Erases picture from flash.
        /// </summary>
        /// <param name="pictureIndex"></param>
        public void Erase(int pictureIndex)
        {
            Erase(pictureIndex, 1);
        }

        /// <summary>
        /// Erases pictures from flash.
        /// </summary>
        /// <param name="initialIndex"></param>
        /// <param name="photoCount"></param>
        public abstract void Erase(int initialIndex, int photoCount);

        private static String CreateImageName(String fileNameOnMemoryCard)
        {
            var newFileName = FileNamingEngine.GetPictureNamePrefix() + fileNameOnMemoryCard;
            return newFileName;
        }

    }
}
