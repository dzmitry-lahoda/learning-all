using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TimeLapseExp.WinFormsGUI
{
    /// <summary>
    /// This static class provides methods to change <see cref="Model"/> and notifies subscribers about changes.
    /// </summary>
    internal static class Presenter
    {
        public static event EventHandler<PresenterEventArgs> OnStoreDirectoryChanged;

        public static event EventHandler<PresenterEventArgs> OnCurrentCameraApplicationStateChanged;

        public static event EventHandler<PresenterEventArgs> OnCameraIdChanged;

        public static event EventHandler<PresenterEventArgs> OnLastSessionInfoChanged;

        public static event EventHandler<PresenterEventArgs> OnExperimentCountIncreased;
        
        public static void SetStoreDirectory(String storeDirectory)
        {
            Model.StoreDirectory = storeDirectory;
            if (OnStoreDirectoryChanged != null)
            {
                OnStoreDirectoryChanged(typeof(Presenter), new PresenterEventArgs(storeDirectory));
            }
        }

        public static void SetCurrentCameraApplicationState(CameraApplicationStates cameraApplicationState)
        {
            Model.CurrentCameraApplicationState = cameraApplicationState;
            if (OnCurrentCameraApplicationStateChanged != null)
            {
                OnCurrentCameraApplicationStateChanged(typeof(Presenter), new PresenterEventArgs(cameraApplicationState));
            }
        }

        public static void SetCameraId(int id)
        {
            Model.CameraId = id;
            if (OnCameraIdChanged != null)
            {
                OnCameraIdChanged(typeof(Presenter), new PresenterEventArgs(id));
            }

        }

        public static void SetLastSessionInfo(SessionInfo lastSessionInfo)
        {
            Model.LastSessionInfo = lastSessionInfo;
            if (OnLastSessionInfoChanged != null)
            {
                OnLastSessionInfoChanged(typeof(Presenter), new PresenterEventArgs(lastSessionInfo));
            }
       }

        public static void IncreaseExperimentCount()
        {
            Model.ExperimentCount++;
            if (OnExperimentCountIncreased != null)
            {
                OnExperimentCountIncreased(typeof(Presenter), new PresenterEventArgs(Model.ExperimentCount));
            }
        }

        public static void AddNewExperiment(Experiment experiment)
        {
            throw new NotImplementedException();
        }
    }
}
