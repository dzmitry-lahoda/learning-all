using System;

namespace TimeLapseExp.WinFormsGUI
{
    /// <summary>
    /// Represents base exception class throwed by TimeLapseExp.Cameras.Session.
    /// </summary>
    [Serializable]
    public class SessionException : ApplicationException
    {
        public SessionException(String message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}
