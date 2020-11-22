using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Text;
using Olympus.OIMA.RyeNET;
using TimeLapseExp.Instrumentation;

namespace TimeLapseExp.Cameras
{
    /// <summary>
    /// Represents class which has methods to get pictures from flash and to erase them.
    /// </summary>
    public sealed partial class OlympusRyeCameraController : CameraControllerBase
    {

        private class OlympusRyeTransport : TransportBase
        {

            public override event TransportEventHandler PicturesSaved;

            private readonly OlympusRyeCameraController _olympusRyeCameraController;

            internal OlympusRyeTransport(OlympusRyeCameraController olympusRyeCameraController)
            {
                _olympusRyeCameraController = olympusRyeCameraController;
            }

            public override IList<String> GetAllPictures(bool eraseAfter, String storeDirectory,bool rename)
            {

                NativeController.CurrentCamera = _olympusRyeCameraController.Id;
                var numberOfPictures = NativeController.PictureCount;
                var pictureFullPathes = GetPictures(0, numberOfPictures, eraseAfter, storeDirectory, rename);
                return pictureFullPathes;
            }

            public override IList<String> GetPictures(int initialIndex, int photoCount, bool eraseAfter, String storeDirectory,bool rename)
            {
                //Contract.Requires(initialIndex >= 0);
                Contract.Requires(photoCount > 0);
                Contract.Requires(String.IsNullOrEmpty(storeDirectory) == false);
                IList<String> pictureFullPathes;
                try
                {
                    pictureFullPathes = InternalGetPictures(initialIndex, photoCount, eraseAfter,
                                                                     storeDirectory, rename);
                }
                catch (CRyeNETException ex)
                {

                    var messageBuilder = new StringBuilder("Getting pictures from memory card failed:");
                    messageBuilder.AppendFormatNewLine(ex.Message);
                    throw new TransportingFailureException(messageBuilder.ToString(), ex);
                }
                NotifyThatPicturesSaved(pictureFullPathes);
                return pictureFullPathes;
            }

            public override String GetPreview(String storeDirectory)
            {
                var previewFile = GetPictures(-1, 1, false, storeDirectory, false)[0];
                return previewFile;
            }

            /// <summary>
            /// Call Camera's Connect method before use and Camera Disconnect after.
            /// </summary>
            /// <param name="initialIndex"></param>
            /// <param name="photoCount"></param>
            /// <param name="eraseAfter"></param>
            /// <param name="storeDirectory"></param>
            /// <returns></returns>
            private IList<String> InternalGetPictures(int initialIndex, int photoCount, bool eraseAfter, String storeDirectory, bool rename)
            {
                var pictureFullPathes = new List<String>(photoCount);

                if (String.IsNullOrEmpty(storeDirectory) && Uri.IsWellFormedUriString(storeDirectory, UriKind.RelativeOrAbsolute))
                {
                    //throw new ArgumentException("storeDirectory should be not null or empty valid path.");
                }
                var pictureNamePrefix = FileNamingEngine.GetPictureNamePrefix();
                for (var i = initialIndex; i < photoCount + initialIndex; i++)
                {
                    var picture = InternalGetPicture(i);
                    pictureFullPathes.Add(WritePictureToFile(storeDirectory, pictureNamePrefix, picture,rename));
                }
                if (eraseAfter)
                {
                    InternalErase(initialIndex + 1, photoCount);
                }

                return pictureFullPathes;
            }

            private void NotifyThatPicturesSaved(IList<string> pictureFullPathes)
            {
                var picturesSaved = PicturesSaved;
                if (picturesSaved != null)
                {
                    PicturesSaved(null, new TransportEventArgs(pictureFullPathes));
                }
            }

            /// <summary>
            /// Download picture from memory card. Call Camera's Connect method before use and Camera Disconnect after.
            /// </summary>
            /// <param name="index">Index of picture on memory card.</param>
            /// <returns></returns>
            private PictureFileData InternalGetPicture(int index)
            {
                NativeController.CurrentPicture = index + 1;
                var name = NativeController.FileName;
                var size = NativeController.PictureSize;
                var bytes = new byte[size];
                NativeController.GetPicture(_olympusRyeCameraController.Id, size, bytes);
                return new PictureFileData(bytes, name);
            }

            /// <summary>
            /// Writes picture to file.
            /// </summary>
            /// <param name="storeDirectory">Folder for storing picture.</param>
            /// <param name="pictureNamePrefix">Prefix, added to picture name</param>
            /// <param name="picture">Picture object for saving.</param>
            /// <returns>Full picture file path.</returns>
            private static String WritePictureToFile(String storeDirectory, String pictureNamePrefix, PictureFileData picture,bool rename)
            {
                var pictureFullPath = "";
                if (rename)
                {
                    pictureFullPath = Path.Combine(storeDirectory, pictureNamePrefix + picture.Name.Remove(0, 4));
                }
                else
                {
                    pictureFullPath = Path.Combine(storeDirectory, picture.Name);
        
                }
                using (var fileStream = new FileStream(pictureFullPath, FileMode.Create))
                {
                    fileStream.Write(picture.Bytes, 0, picture.Size);
                    fileStream.Flush();
                }
                return pictureFullPath;
            }

            public override void EraseAllPictures()
            {
                try
                {
                    NativeController.EraseAll(_olympusRyeCameraController.Id);
                }
                catch (CRyeNETException ex)
                {
                    throw new TransportingFailureException("Erasing all pictures failed", ex);
                }
            }

            public override void EraseLast()
            {
                NativeController.EraseLast(_olympusRyeCameraController.Id);
            }

            public override void Erase(int initialIndex, int photoCount)
            {
                try
                {
                    InternalErase(initialIndex, photoCount);
                }
                catch (Exception ex)
                {
                    throw new TransportingFailureException("Erasing pictures failed", ex);
                }
            }

            private void InternalErase(int initialIndex, int photoCount)
            {
                NativeController.CurrentPicture = initialIndex;
                for (int i = initialIndex; i < photoCount + initialIndex; i++)
                {
                    //NativeController.ErasePicture(_olympusRyeCameraController.Id);
                    //TODO:Check if picture was realy erased.
                }
            }



        }
    }
}