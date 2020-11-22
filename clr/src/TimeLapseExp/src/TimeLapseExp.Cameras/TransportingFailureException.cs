using System;

namespace TimeLapseExp.Cameras
{
    //TODO: Make class meaningfull.
    /// <summary>
    /// Represents base exception class throwed by <see cref="TransportBase"/>.
    /// </summary>
    [Serializable]
    public sealed class TransportingFailureException : ApplicationException
    {
        public TransportingFailureException(String message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}
