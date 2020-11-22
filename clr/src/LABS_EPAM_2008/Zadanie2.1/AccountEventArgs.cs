using System;
using System.Collections.Generic;
using System.Text;

namespace EPAM.Trainings.Zadanie2_1
{
    #region AccountEventArgs class

    /// <summary>
    /// Provides data for the Account events
    /// </summary>
    [Serializable]
    public class AccountEventArgs : EventArgs
    {
        protected string message;



        /// <summary>
        ///  Initializes a new instance of the 
        /// Accountclass with a specified message
        /// </summary>
        /// <param name="message">The event message</param>
        public AccountEventArgs(String message)
        {
            this.message = message;
        }

        /// <summary>
        /// Gets current event message
        /// </summary>
        public String Message
        {
            get { return message; }
        }

    }
    #endregion

    /// <summary>
    /// Represents the method that will handle an Account events 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e">AccountEventArgs</param>
    public delegate void AccountEventHandler(Object sender, AccountEventArgs e);

}
