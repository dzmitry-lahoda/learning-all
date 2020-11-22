using System;

namespace TimeLapseExp.WinFormsGUI
{
    /// <summary>
    /// Provides data for the Session's events
    /// </summary>
    [Serializable]
    internal class SessionEventArgs : EventArgs
    {
        protected string message;

        protected DateTime dateTime;

        public SessionEventArgs(String message)
        {
            this.message = message;
            dateTime = DateTime.Now;
        }

        /// <summary>
        /// Gets current event message
        /// </summary>
        public String Message
        {
            get
            {
                 return message;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public DateTime DateTime
        {
            get
            {
                return dateTime;
            }
        }
    }
}
