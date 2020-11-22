using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TimeLapseExp.Cameras
{
    /// <summary>
    /// Reserved. Should be used in multiple camera types environment.
    /// </summary>
    /// <typeparam name="TCameraControllerBase"></typeparam>
    /// <typeparam name="TSpecilizedLowLevelController"></typeparam>
    public abstract class CameraControllerFactory<TCameraControllerBase, TSpecilizedLowLevelController> where TCameraControllerBase : CameraControllerBase
    {
        protected TSpecilizedLowLevelController _specilizedLowLevelController;

        public abstract TCameraControllerBase CreateNew(int id);

        public abstract Dictionary<int, string> GetCameras();
    }
}
