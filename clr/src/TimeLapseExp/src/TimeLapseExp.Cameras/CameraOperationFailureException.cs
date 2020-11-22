using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;

namespace TimeLapseExp.Cameras
{

    /// <summary>
    /// Represents base exception class throwed by any <see cref="CameraControllerBase">CameraControllerBase</see>
    /// derived class.
    /// </summary>
    [Serializable]
    public sealed class CameraOperationFailureException : Exception, ISerializable
    {

        private int _id;

        private String _name;

        private String _type;

        public CameraOperationFailureException() { }
        public CameraOperationFailureException(String message) : base(message) { }
        public CameraOperationFailureException(String message, Exception innerException)
            : base(message, innerException) { }

        public CameraOperationFailureException(String message, CameraControllerBase cameraController)
            : this(message, cameraController, null) { }

        public CameraOperationFailureException(String message, CameraControllerBase cameraController, Exception innerException)
            : this(message, innerException)
        {
            _id = cameraController.Id;
            _type = cameraController.GetType().ToString();
            _name = cameraController.Name;
        }

        /// <summary>
        /// Gets camera id.
        /// </summary>
        public int Id
        {
            get
            {
                return _id;
            }
        }

        /// <summary>
        /// Type of camera.
        /// </summary>
        public String Type
        {
            get
            {
                return _type;
            }
        }

        /// <summary>
        /// Name of camera.
        /// </summary>
        public String Name
        {
            get
            {
                return _name;
            }
        }

        
        public override String Message
        {
            get
            {
                if (_type == null) return base.Message;
                var message = new StringBuilder(base.Message);
                message.AppendFormatNewLine("(Camera Id={0})", _id);
                message.AppendFormatNewLine("(Camera Type={0})", _type);
                message.AppendFormatNewLine("(Camera Name={0})", _type);
                return message.ToString();
            }
        }

        private CameraOperationFailureException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
           _id = info.GetInt32("Id");
           _type = info.GetString("Type");
           _name = info.GetString("Name");
        }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(
        SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("Id", _id);
            info.AddValue("Type",_type);
            info.AddValue("Name",_name);
        }
    }


}
