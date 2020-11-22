using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace TimeLapseExp.WinFormsGUI
{
    /// <summary>
    /// This static class is used to store data which is renderd and returned back from UI.
    /// Accessed througth <see cref="Presenter"/>
    /// </summary>
    internal static class Model
    {
        private static string _storeDirectory;

        private static CameraApplicationStates _currentCameraApplicationState;

        private static int _cameraId;

        private static int _experimentCount;

        private static Experiment _lastExperiment;

        private static SessionInfo _lastSessionInfo;

        public static SessionInfo LastSessionInfo
        {
            get { return _lastSessionInfo; }
            set { _lastSessionInfo = value; }
        }

        /// <summary>
        /// Represents number of started experiments during current start of application. 
        /// </summary>
        public static int ExperimentCount
        {
            get { return _experimentCount; }
            set { _experimentCount = value; }
        }

        /// <summary>
        /// Represnt current application state.
        /// </summary>
        public static CameraApplicationStates CurrentCameraApplicationState
        {
            get { return _currentCameraApplicationState; }
            set { _currentCameraApplicationState = value; }
        }

        /// <summary>
        /// Full directory path for storing copied images and log files.
        /// </summary>
        public static string StoreDirectory
        {
            get { return _storeDirectory; }
            set { _storeDirectory = value; }
        }

        /// <summary>
        /// Id of camera which is currently in use.
        /// </summary>
        public static int CameraId
        {
            get
            {
                return _cameraId;
            }
            set
            {
                _cameraId = value;
            }
        }
    }

}
