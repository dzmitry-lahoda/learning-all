using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using Olympus.OIMA.RyeNET;
using TimeLapseExp.Imaging;
using TimeLapseExp.Cameras;
using TimeLapseExp.Cameras;
using TimeLapseExp.Imaging.Contract;

namespace TimeLapseExp.WinFormsGUI
{
    //TODO: Consider usage of WF or some patters of long running processes.
    /// <summary>
    /// Represents immutable class wich makes sequence of pictures.
    /// </summary>
    internal sealed class Session
    {
        #region Events
        //TODO:Use dictionary fo storing events.
        public event SessionEventHandler SessionStarted;

        public event SessionEventHandler SequenceStarted;

        public event SessionEventHandler CapturingStarted;

        public event SessionEventHandler CapturingFinished;

        public event SessionEventHandler TransportingStarted;

        public event SessionEventHandler TransportingFinished;

        public event ImageCharacteristicsEventHandler ContrastMeasurmentNumberComputed;

        public event ImageCharacteristicsEventHandler ShannonEntropyComputed;

        public event SessionEventHandler SequenceFinished;

        public event SessionEventHandler ProgressChanged;

        public event SessionEventHandler SessionFinished;

        public event SessionEventHandler SessionCanceled;

        #endregion

        private IImageCharacteristics _imageCharacteristics = new ImageCharacteristics();

        private BackgroundWorker bw = new BackgroundWorker();

        private List<Tuple<int, int>> _usedExposureTimesAndFocusPostions = new List<Tuple<int, int>>();

        private SessionInfo _sessionInfo;

        private CameraControllerBase _controller;

        #region Properties

        /// <summary>
        /// Gets amount of pictures which would be captured until end of current session.
        /// </summary>
        public int NumberOfPicturesToBeCaptured
        {
            get
            {
                var value = _sessionInfo.ConsideredTotalNumberOfPictures -  NumberOfCapturedPictures;
                return value;
            }
        }

        /// <summary>
        /// Gets number of pictures already taken during this session.
        /// </summary>
        public int NumberOfCapturedPictures
        {
            get { return _usedExposureTimesAndFocusPostions.Count; }
        }
        
        /// <summary>
        /// Gets collection of used exposure time and focus postions for taking images.
        /// Last element is setting used to take last picture(
        ///  <see cref="CapturingFinished"/>
        /// </summary>
        public List<Tuple<int, int>> UsedExposureTimesAndFocusPostions
        {
            get
            {
                var clone = (List<Tuple<int, int>>) _usedExposureTimesAndFocusPostions.ToArray().Clone();
                return clone;
            }
        }

        /// <summary>
        /// Gets <see cref="SessionInfo"/> used by current <see cref="Session"/>
        /// </summary>
        public SessionInfo SessionInfo
        {
            get
            {
                return _sessionInfo;
            }
        }

        /// <summary>
        /// Returns true if session is on:
        /// camera taking pictures, pictures downloaded from memory card, some values are calculated.
        /// </summary>
        public bool IsCapturing
        {
            get
            {
                return bw.IsBusy;
            }
        }

        /// <summary>
        /// Gets current camera controller used by session.
        /// </summary>
        public CameraControllerBase Controller
        {
            get
            {
                return _controller;
            }
        }

        #endregion

#region Constructors
        public Session(CameraControllerBase cameraController)
        {
            _controller = cameraController;
            bw.WorkerReportsProgress = true;
            bw.WorkerSupportsCancellation = true;
            bw.DoWork += bw_DoWork;
            bw.ProgressChanged += bw_ProgressChanged;
            bw.RunWorkerCompleted += BwRunWorkerCompleted;
        }

#endregion


        void BwRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            _controller.Transport.PicturesSaved -= Transport_PicturesSaved;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Start(SessionInfo sessionInfo)
        {
            _sessionInfo = sessionInfo;
            _controller.Transport.PicturesSaved += Transport_PicturesSaved;
            _usedExposureTimesAndFocusPostions.Clear();
            if (!bw.IsBusy)
            {
                bw.RunWorkerAsync();
            }
        }

        public void Cancel()
        {
            bw.CancelAsync();
        }

        private void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            var progressChanged = ProgressChanged;
            if (progressChanged != null)
            {
                ProgressChanged(null, new SessionEventArgs(e.ProgressPercentage.ToString()));
            }
        }

        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            InternalStart();
        }


        private void Transport_PicturesSaved(object sender, TransportEventArgs e)
        {

            for (int i = 0; i < e.PictureFullPathes.Count; i++)
            {
                var indexOfImageSettings = NumberOfCapturedPictures + i - SessionInfo.SequenceInfo.SummedCount;
                var contrastMeasurmentNumberComputed = ContrastMeasurmentNumberComputed;
                if (contrastMeasurmentNumberComputed != null)
                {
                    var returnedValue = _imageCharacteristics.ComputeContrastMeasurmentNumber(
                        e.GetPictureFullPath(i));
                    var evenArgs = new ImageCharacteristicsEventArgs(
                        returnedValue,
                        e.GetPictureFullName(i),
                        _usedExposureTimesAndFocusPostions[indexOfImageSettings].Item1,
                        _usedExposureTimesAndFocusPostions[indexOfImageSettings].Item2,
                        ""
                        );
                    ContrastMeasurmentNumberComputed(this, evenArgs);
                }
                var shannonEntropyComputed = ShannonEntropyComputed;
                if (shannonEntropyComputed != null)
                {
                    var returnedValue = _imageCharacteristics.ComputeShannonEntropy(
                        e.GetPictureFullPath(i));
                    var evenArgs = new ImageCharacteristicsEventArgs(
                        returnedValue,
                         e.GetPictureFullName(i),
                         _usedExposureTimesAndFocusPostions[indexOfImageSettings].Item1,
                       _usedExposureTimesAndFocusPostions[indexOfImageSettings].Item2,
                          ""
    );
                    ShannonEntropyComputed(this, evenArgs);
                }
            }



        }

        public void InternalStart()
        {
            NotifyThatSessionStarted();
            
            for (int i = 0; i < _sessionInfo.SequenceCount; i++)
            {
                var startSequenceDateTime = DateTime.Now;
                NotifyThatSequenceStarted();
                NotifyThatCapturingStarted();
                
                OlympusRyeCameraController.NativeController.Connect(_controller.Id);
                var asd = _controller.GetAllCameraInfo();
                var qwe = OlympusRyeCameraController.NativeController.PictureCount;
//                var zzz = new RyeMemoryCardInfo();
//                OlympusRyeCameraController.NativeController.GetMemoryProp(_controller.Id,ref zzz);
//                var folderProp = new RyeFolderProperty();
//                folderProp.folder_name = "createdBySDK";
//                var q1 = OlympusRyeCameraController.NativeController.CameraID;
//                var q2 = OlympusRyeCameraController.NativeController.FolderCount;
//                var q3 = OlympusRyeCameraController.NativeController.FolderName;
//               // OlympusRyeCameraController.NativeController.FolderName = "crSDK";
//                //var q4 = OlympusRyeCameraController.NativeController.FolderNumber;
//                //OlympusRyeCameraController.NativeController.GetFolderProp(_controller.Id, ref folderProp);
//
//                var rty = new RyeCameraProperty();
//                OlympusRyeCameraController.NativeController.GetCameraProp(_controller.Id,ref rty);
//                var zxczxc = new RyePictureProperty();
//                OlympusRyeCameraController.NativeController.GetPictureProp(_controller.Id, ref zxczxc);
//                var qweqwe = new RyePicTagProperty();



                
                InternalCaptureSequence();
                if (CleanIfCancelationPending())
                {
                    return;
                }
                NotifyThatCapturingFinished();
                NotifyThatTransportingStarted();
               
                if (_sessionInfo.CopyImages)
                {
                   _controller.Transport.GetPictures(
                        qwe,
                        _sessionInfo.SequenceInfo.SummedCount,
                        true,
                        _sessionInfo.StoreDirectory,
                        true);
                }
                NotifyThatTransportingFinished();
                if (CleanIfCancelationPending())
                {
                    return;
                }
                if (_sessionInfo.SequenceCount != i + 1)
                {
                    Sleep(startSequenceDateTime + _sessionInfo.TimeSpanBetweenSequences - DateTime.Now);
                }
                NotifyThatSequenceFinished();
                bw.ReportProgress(100 * (i + 1) / _sessionInfo.SequenceCount);
            }
            InternalSetInitialSettings();
            NotifyThatSessionFinished();
        }

        private void NotifyThatSessionFinished()
        {
            var sessionFinished = SessionFinished;
            if (sessionFinished != null)
            {
                SessionFinished(null, new SessionEventArgs("Session finished."));
            }
        }

        private void NotifyThatSessionStarted()
        {
            var sessionStarted = SessionStarted;
            if (sessionStarted != null)
            {
                SessionStarted(null, new SessionEventArgs("Session started."));
            }
        }

        private void NotifyThatSequenceFinished()
        {
            var sequenceFinished = SequenceFinished;
            if (sequenceFinished != null)
            {
                SequenceFinished(null, new SessionEventArgs("Sequence finished."));
            }
        }

        private void NotifyThatTransportingFinished()
        {
            var transportingFinished = TransportingFinished;
            if (transportingFinished != null)
            {
                TransportingFinished(null, new SessionEventArgs("Transporting finished."));
            }
        }

        private void NotifyThatTransportingStarted()
        {
            var transportingStarted = TransportingStarted;
            if (transportingStarted != null)
            {
                TransportingStarted(null, new SessionEventArgs("Transporting started."));
            }
        }

        private void NotifyThatCapturingFinished()
        {
            var capturingFinished = CapturingFinished;
            if (capturingFinished != null)
            {
                CapturingFinished(null, new SessionEventArgs("Capturing finished."));
            }
        }

        private void NotifyThatCapturingStarted()
        {
            var capturingStarted = CapturingStarted;
            if (capturingStarted != null)
            {
                
                CapturingStarted(null, new SessionEventArgs("Capturing started."));
            }
        }

        private void NotifyThatSequenceStarted()
        {
            var sequenceStarted = SequenceStarted;
            if (sequenceStarted != null)
            {
                SequenceStarted(null, new SessionEventArgs("Sequence started."));
            }
        }

        private bool CleanIfCancelationPending()
        {
            if (bw.CancellationPending)
            {
                //_controller.Transport.EraseAllPictures();
                InternalSetInitialSettings();
                NotifyThatSessionCanceled();
                return true;
            }
            return false;
        }

        private void NotifyThatSessionCanceled()
        {
            var sessionCanceled = SessionCanceled;
            if (sessionCanceled != null)
            {
                SessionCanceled(null, new SessionEventArgs("Session capturing canceled."));
            }
        }

        private void InternalSetInitialSettings()
        {
            _controller.SetExposure(_sessionInfo.SequenceInfo.CameraInfo.CaptureInfo.Exposure);
            _controller.SetFocus(_sessionInfo.SequenceInfo.CameraInfo.CaptureInfo.Focus);
        }

        private void InternalCaptureSequence()
        {
            var currentExposure = _sessionInfo.SequenceInfo.CameraInfo.CaptureInfo.Exposure.Clone();
            var currentFocus = _sessionInfo.SequenceInfo.CameraInfo.CaptureInfo.Focus.Clone();
            if (_sessionInfo.SequenceInfo.SequencePriority == SequencePriorities.Exposure)
            {
                InternalCaptureFocusSequence(currentFocus, currentExposure);
            }
            else
            {
                InternalCaptureExposureSequence(currentExposure, currentFocus);
            }
        }

        private void Sleep(TimeSpan sleepTimeSpan)
        {

            if (sleepTimeSpan < TimeSpan.FromSeconds(0))
            {
                throw new SessionException(String.Format("Timespan lack: {0}.", sleepTimeSpan), null);
            }
            else
            {
                Thread.Sleep(sleepTimeSpan);
            }
        }

        private void InternalCaptureExposureSequence(Exposure currentExposure, Focus currentFocus)
        {
            for (var j = 0; j < _sessionInfo.SequenceInfo.ExposureSequenceCount; j++)
            {
                if (bw.CancellationPending)
                {
                    return;
                }
                currentExposure.Time = _sessionInfo.SequenceInfo.ExposureTimes[j];
                if (_sessionInfo.SequenceInfo.SequencePriority == SequencePriorities.Focus)
                {
                    InternalCaptureFocusSequence(currentFocus, currentExposure);
                }
                else
                {
                        CaptureThis(currentExposure, currentFocus);
                }
            }
        }

        private void InternalCaptureFocusSequence(Focus currentFocus, Exposure currentExposure)
        {
            for (int j = 0; j < _sessionInfo.SequenceInfo.FocusSequenceCount; j++)
            {
                if (bw.CancellationPending)
                {
                    return;
                }
                currentFocus.Position = _sessionInfo.SequenceInfo.FocusPositions[j];
                if (_sessionInfo.SequenceInfo.SequencePriority == SequencePriorities.Exposure)
                {
                    InternalCaptureExposureSequence(currentExposure, currentFocus);
                }
                else
                {
                   CaptureThis(currentExposure, currentFocus);
                }
            }
        }

        private void CaptureThis(Exposure currentExposure, Focus currentFocus)
        {
            _controller.SetFocus(currentFocus);
            _controller.SetExposure(currentExposure);
            _controller.Capture();
            _usedExposureTimesAndFocusPostions.Add(new Tuple<int, int>(currentExposure.Time, currentFocus.Position));
        }
    }
}
