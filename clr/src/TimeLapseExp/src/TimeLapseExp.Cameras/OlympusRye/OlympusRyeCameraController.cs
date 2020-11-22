using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Text;

using Olympus.OIMA.RyeNET;


namespace TimeLapseExp.Cameras
{
    /// <summary>
    /// Represents Olympus camera.
    /// </summary>
    public  sealed partial class OlympusRyeCameraController : CameraControllerBase
    {
        //TODO:Use C5.GuardedDictionary or PowerCollections.ReadOnlyDictionaryBase
        /// <summary>
        /// Read only dictionary.
        /// </summary>
        private static Dictionary<int, string> _lastTimeEnabledCameras;

        private static Dictionary<int, bool> _camerasInUse;

        /// <summary>
        /// True if <see cref="GetCameras">GetCameras</see> was called.
        /// </summary>
        private static bool _wasUsed;

        /// <summary>
        /// Returns Olympus.OIMA.RyeNET.CRyeNET object, which represents base functionality of camera's control. 
        /// Is wrapper around Driver.
        /// </summary>
        public static readonly CRyeNET NativeController;

        private static readonly Type ControllerType = typeof(OlympusRyeCameraController);

        static OlympusRyeCameraController()
        {
            NativeController = new CRyeNET();
            var returnValue = NativeController.InitControl();
            if (returnValue != 0)
            {
                var message = new StringBuilder();
                message.AppendFormatNewLine("{0} initialization of controller failed.", ControllerType);
                message.AppendFormatNewLine("Reason : {0}", Enum.GetName(typeof(RYE_ERROR), returnValue));
                throw new CameraOperationFailureException(message.ToString());
            }
            //register the event handlers
            //cRyeNET.TransferProgressEvent += new TransferProgressEventDelegate(TransferProgress);
            //cRyeNET.CommandDoneEvent += new CommandDoneEventDelegate(CommandDone);
        }

        /// <summary>
        /// Returns table where key is number of camera and value-it's name.
        /// </summary>
        /// <returns></returns>
        public static IDictionary<int, String> GetCameras()
        {
            _lastTimeEnabledCameras = new Dictionary<int, string>();
            var cameraCount = NativeController.CameraCount;
            for (var i = 0; i < cameraCount; i++)
            {
                NativeController.CurrentCamera = i;
                _lastTimeEnabledCameras.Add(i, NativeController.ConnectCamera);

            }
            return _lastTimeEnabledCameras;
        }

        private OlympusRyeCameraController() : base() { }

        /// <summary>
        /// Create new instance of <see cref="OlympusRyeCameraController"/> with specified Id.
        /// </summary>
        /// <param name="id"></param>
        public OlympusRyeCameraController(int id)
        {
            GetCameras();
            String name;
            try
            {
                name = _lastTimeEnabledCameras[id];
            }
            catch (KeyNotFoundException ex)
            {
                var message = String.Format("{0} with Id={1} does not exist.{2}", ControllerType, id, Environment.NewLine);
                throw new ArgumentException(message, ex);
            }
            _id = id;
            _name = name;
            Connect();
            _trasport = new OlympusRyeTransport(this);
        }

        /// <summary>
        /// Method points to driver to current camera.
        /// </summary>
        internal static void SetCurrentCamera(int id)
        {
            //TODO:Change if it will be used in multicamera environments.
            NativeController.Connect(id);
            NativeController.CurrentCamera = id;
        }

        private static CameraInfo InternalGetAllCameraInfo(int cameraId)
        {
            var cameraInfo = new CameraInfo();
            var exposureProperty = new RyeExposureProperty();
            var captureProperty = new RyeCaptureProperty();
            cameraInfo.Name = NativeController.ConnectCamera;
            NativeController.GetExposureProp(cameraId, ref exposureProperty);
            NativeController.GetCaptureProp(cameraId, ref captureProperty);
            cameraInfo.MemoryCardInfo = InternalGetMemoryCardInfo(cameraId);
            cameraInfo.CaptureInfo.Capture = new Capture((CaptureModes)captureProperty.take_mode, captureProperty.continuous_time, captureProperty.continuous_number);
            cameraInfo.CaptureInfo.Exposure = new Exposure((ExposureModes)exposureProperty.program_mode, exposureProperty.exposure_time, exposureProperty.aperture_number_numerator);
            cameraInfo.CaptureInfo.Resolution = (Resolutions)NativeController.Resolution;
            cameraInfo.CaptureInfo.ExposureBias = NativeController.ExposureBias;
            cameraInfo.CaptureInfo.OpticalZoom = NativeController.OpticalZoom;
            cameraInfo.CaptureInfo.WhiteBalance = (WhiteBalance)NativeController.WhiteBalance;
            cameraInfo.CaptureInfo.Focus = new Focus((FocusModes)NativeController.FocusMode, NativeController.FocusPostion);
            cameraInfo.CaptureInfo.ISOSpeed = (ISOSpeed)NativeController.ISOSpeed;
            cameraInfo.CaptureInfo.FlashMode = (FlashModes)NativeController.FlashMode;
            cameraInfo.PictureCount = NativeController.PictureCount;
            cameraInfo.RemainPictureCount = NativeController.RemainCount;
            return cameraInfo;
        }

        private static MemoryCardInfo InternalGetMemoryCardInfo(int cameraId)
        {
            var ryeMemoryCardInfo = new RyeMemoryCardInfo();
            NativeController.GetMemoryProp(cameraId, ref ryeMemoryCardInfo);
            var memoryCardInfo = new MemoryCardInfo(ryeMemoryCardInfo.memory_size, ryeMemoryCardInfo.memory_free);
            return memoryCardInfo;
        }

        private static void InternalSetAllCameraInfo(int cameraId, CameraInfo cameraInfo)
        {
            NativeController.Resolution = (Int32)cameraInfo.CaptureInfo.Resolution;
            NativeController.ExposureBias = cameraInfo.CaptureInfo.ExposureBias;
            NativeController.OpticalZoom = cameraInfo.CaptureInfo.OpticalZoom;
            NativeController.WhiteBalance = (Int32)cameraInfo.CaptureInfo.WhiteBalance;
            NativeController.FlashMode = (Int32)cameraInfo.CaptureInfo.FlashMode;
            NativeController.ISOSpeed = (Int32)cameraInfo.CaptureInfo.ISOSpeed;
            InternalSetFocus(cameraId, cameraInfo.CaptureInfo.Focus);
            InternalSetExposure(cameraId, cameraInfo.CaptureInfo.Exposure);
            InternalSetCapture(cameraId, cameraInfo.CaptureInfo.Capture);
        }

        private static void InternalSetCapture(int cameraNumber, Capture capture)
        {
            var captureProperty = new RyeCaptureProperty
            {
                take_mode = (RYE_CAPTURE_MODE)capture.Mode,
                continuous_number = capture.ContinuousNumber,
                continuous_time = capture.ContinuousTime
            };
            NativeController.SetCaptureProp(cameraNumber, ref captureProperty);
        }

        private static void InternalSetExposure(int cameraId, Exposure exposure)
        {
            var ryeExposureProperty = new RyeExposureProperty
            {
                program_mode = (RYE_EXPOSURE_MODE)exposure.Mode,
                exposure_time = exposure.Time,
                aperture_number_numerator = exposure.Aperture
            };
            NativeController.SetExposureProp(cameraId, ref ryeExposureProperty);
        }

        private static void InternalSetFocus(int cameraId, Focus focus)
        {
            NativeController.FocusMode = (int)focus.Mode;
            if (focus.Mode == FocusModes.Manual)
            {
                NativeController.FocusPosition = focus.Position;
            }
        }

        public override void Disconnect()
        {
            try
            {
                var nativeController = NativeController;
                if (nativeController != null)
                {
                    nativeController.DisConnect(_id);
                }
                base.Disconnect();
            }
            catch (CRyeNETException ex)
            {
                throw new CameraOperationFailureException("Disconnection failed.", this, ex);
            }

        }

        public override bool IsReady()
        {
            NativeController.CurrentCamera = _id;
            var isReady = NativeController.CameraReady;
            return isReady;
        }

        public override void Capture()
        {
           
            NativeController.Connect(_id);
            NativeController.CurrentCamera = _id;
            //makes photo. waits for the camera to complete the command.
            NativeController.Capture(_id, false);
        }

        public override void Preview()
        {
            NativeController.CurrentCamera = _id;
            NativeController.Preview(_id, false);
        }

        /// <summary>
        /// Returns information about camera.
        /// </summary>
        /// <param name="cameraNumber"></param>
        /// <returns></returns>
        public override CameraInfo GetAllCameraInfo()
        {
            SetCurrentCamera(_id);
            var cameraInfo = InternalGetAllCameraInfo(_id);
            return cameraInfo;
        }

        protected override void Connect()
        {
            try
            {
                NativeController.Connect(_id);
                NativeController.CurrentCamera = _id;
                base.Connect();
            }
            catch (CRyeNETException ex)
            {
                throw new CameraOperationFailureException("Connection failed.", this, ex);
            }
        }

        /// <summary>
        /// Set up camera with specified CameraInfo object.
        /// </summary>
        /// <param name="cameraInfo"></param>
        public override void SetAllCameraInfo(CameraInfo cameraInfo)
        {
            SetCurrentCamera(_id);
            InternalSetAllCameraInfo(_id, cameraInfo);
        }

        public override void SetExposure(Exposure exposure)
        {
            SetCurrentCamera(_id);
            InternalSetExposure(_id, exposure);
        }

        public override void SetFocus(Focus focus)
        {
            SetCurrentCamera(_id);
            InternalSetFocus(_id, focus);
        }

        public override void Dispose()
        {
            Disconnect();
            base.Dispose();
        }

        /// <summary>
        /// Get low and high boundaries of camera parameters.
        /// </summary>
        /// <returns></returns>
        public Tuple<CameraInfo, CameraInfo> GetBoundaries()
        {
            throw new NotImplementedException();
        }

    }
}
