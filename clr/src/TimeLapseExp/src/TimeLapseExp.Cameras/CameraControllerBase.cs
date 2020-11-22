using System.Diagnostics.Contracts;
using System.IO;
using System;

namespace TimeLapseExp.Cameras
{
    /// <summary>
    /// This is base class for any class which takes on responsibility of control
    /// and manipulating of camera.
    /// </summary>
    public abstract class CameraControllerBase : IDisposable
    {

        protected int _id;

        protected String _name;

        protected TransportBase _trasport;

        protected bool _cancellationRequested;

        private volatile bool _isConnected;

        /// <summary>
        /// Gets Id of camera.
        /// </summary>
        public virtual int Id
        {
            get
            {
                return _id;
            }
        }

        /// <summary>
        /// Gets attached name of camera.
        /// </summary>
        public virtual String Name
        {
            get
            {
                return _name;
            }
        }

        /// <summary>
        /// Gets realization of interface responsible for transporting images from flash card of camera.
        /// </summary>
        public virtual TransportBase Transport
        {
            get
            {
                return _trasport;
            }
        }

        protected CameraControllerBase()
        {
            _id = -1;
        }

        protected CameraControllerBase(int id) : this(id, null) { }

        protected CameraControllerBase(int id, String name)
        {
            _id = id;
            if (name == null)
            {
                _name = name;
            }
            else
            {
                _name = String.Format("Camera of {0} type", GetType());
            }
        }

        protected virtual void Connect()
        {
            Contract.Ensures(_isConnected);
            _isConnected = true;
        }

        /// <summary>
        /// Returns true if controller connected to camera.
        /// </summary>
        public virtual bool IsConnected()
        {
            return _isConnected;
        }

        /// <summary>
        /// Disconnectes controller from hardware.
        /// </summary>
        public virtual void Disconnect()
        {
            if (_isConnected)
            {
                _isConnected = false;
            }
        }

        /// <summary>
        /// Captures image using camera. Image stored on flash card.
        /// </summary>
        public abstract void Capture();

        /// <summary>
        /// Captures several pictures. Images stored on flash card.
        /// </summary>
        /// <param name="pictureCount">Number of pictures to make.</param>
        public void Capture(int pictureCount)
        {
            for (var i = 0; i < pictureCount; i++)
            {
                Capture();
            }
        }

        /// <summary>
        /// Cancels current operation.
        /// </summary>
        public void Cancel()
        {
            _cancellationRequested = true;
        }

        /// <summary>
        /// Captures law resolution preview image. Image stored on flash card.
        /// </summary>
        public abstract void Preview();

        /// <summary>
        /// Returns true, if camera ready to accept commands.
        /// </summary>
        public abstract bool IsReady();

        public abstract void SetAllCameraInfo(CameraInfo info);
        
        public abstract CameraInfo GetAllCameraInfo();

        /// <summary>
        /// Disconnects camera and do cleaning job.
        /// </summary>
        public virtual void Dispose()
        {
            Contract.Ensures(IsConnected() == false);
            Disconnect();
        }

        ~CameraControllerBase()
        {
            Dispose();
        }

        public abstract void SetExposure(Exposure exposure);
        public abstract void SetFocus(Focus focus);
    }
}
