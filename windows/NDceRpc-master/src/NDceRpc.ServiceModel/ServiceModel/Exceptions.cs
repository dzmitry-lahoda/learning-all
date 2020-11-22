using System;
using System.Runtime.Serialization;
using System.Security.Permissions;



namespace NDceRpc.ServiceModel
{

    [Serializable]
    public class CommunicationObjectFaultedException : CommunicationException
    {
        public CommunicationObjectFaultedException() : base() { }
        public CommunicationObjectFaultedException(string msg) : base(msg) { }
        public CommunicationObjectFaultedException(string msg, Exception inner)
            : base(msg, inner) { }
        protected CommunicationObjectFaultedException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }

    [Serializable]
    public class EndpointNotFoundException : CommunicationException
    {
        public EndpointNotFoundException() : base() { }
        public EndpointNotFoundException(string msg) : base(msg) { }
        public EndpointNotFoundException(string msg, Exception inner) : base(msg, inner) { }
        protected EndpointNotFoundException(SerializationInfo info, StreamingContext context) :
            base(info, context) { }
    }

    [Serializable]
    public class ActionNotSupportedException : CommunicationException
    {
        public ActionNotSupportedException() : base() { }
        public ActionNotSupportedException(string msg) : base(msg) { }
        public ActionNotSupportedException(string msg, Exception inner) : base(msg, inner) { }
        protected ActionNotSupportedException(SerializationInfo info, StreamingContext context) :
            base(info, context) { }
    }

    [Serializable]
    public class CommunicationException : SystemException
    {
        public CommunicationException() : base() { }
        public CommunicationException(string msg) : base(msg) { }
        public CommunicationException(string msg, Exception inner) : base(msg, inner) { }
        protected CommunicationException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }

    [Serializable]
    public class FaultException : CommunicationException
    {

        public FaultException()
        {
        }

        public FaultException(string message)
            : base(message)
        {
        }

        public FaultException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected FaultException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }

    [Serializable]
    public class FaultException<TDetail> : FaultException where TDetail : class
    {
        private const String DETAILS = "Details";     // For (de)serialization
        private readonly TDetail _details;

        /// <summary>Returns a reference to this exception's additional arguments.</summary>
        public TDetail Details { get { return _details; } }

        /// <summary>
        /// Initializes a new instance of the Exception class with a specified error message 
        /// and a reference to the inner exception that is the cause of this exception. 
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, 
        /// or a null reference if no inner exception is specified.</param>
        public FaultException(String message, Exception innerException)
            : this(null, message, innerException) { }

        // The fourth public constructor because there is a field
        /// <summary>
        /// Initializes a new instance of the Exception class with additional arguments, 
        /// a specified error message, and a reference to the inner exception 
        /// that is the cause of this exception. 
        /// </summary>
        /// <param name="details">The exception's additional arguments.</param>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, 
        /// or a null reference if no inner exception is specified.</param>
        public FaultException(TDetail details, String message, Exception innerException)
            : base(message, innerException)
        {
            _details = details;
        }

        // Because at least 1 field is defined, define the special deserialization constructor
        // Since this class is sealed, this constructor is private
        // If this class were not sealed, this constructor should be protected
        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
        private FaultException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { // Let the base deserialize its fields
            _details = (TDetail)info.GetValue(DETAILS, typeof(TDetail));
        }

        // Because at least 1 field is defined, define the serialization method
        /// <summary>
        /// When overridden in a derived class, sets the SerializationInfo with information about the exception.
        /// </summary>
        /// <param name="info">The SerializationInfo that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The StreamingContext that contains contextual information about the source or destination.</param>
        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(DETAILS, _details);
            base.GetObjectData(info, context);
        }


        /// <summary>
        /// Determines whether the specified Object is equal to the current Object.
        /// </summary>
        /// <param name="obj">The Object to compare with the current Object. </param>
        /// <returns>true if the specified Object is equal to the current Object; otherwise, false.</returns>
        public override Boolean Equals(Object obj)
        {
            FaultException<TDetail> other = obj as FaultException<TDetail>;
            if (obj == null) return false;
            return Object.Equals(_details, other._details) && base.Equals(obj);
        }
        public override int GetHashCode() { return base.GetHashCode(); }
    }
}
