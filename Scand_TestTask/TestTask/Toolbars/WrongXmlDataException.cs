using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;
using System.Xml;
using System.Runtime.InteropServices;

namespace Scand.Toolbars
{
    /// <summary>
    /// The exception that is thrown when the xml file with description of
    /// toolbar missing some data or have wrong values about buttons.
    /// </summary>
    [Serializable]
    public sealed class WrongXmlDataException : Exception
    {
        private string xmlUri;

        private int buttonNodeNumber;

        private string paramName;

        private string paramValue;

        public WrongXmlDataException() { }
        public WrongXmlDataException(string message) :
            base(message) { }
        public WrongXmlDataException(string message, Exception innerException)
            : base(message, innerException) { }

        /// <summary>
        /// Initializes a new instance of the Scand.Toolbars.WrongXmlDataException class. 
        /// </summary>
        /// <param name="message">The error message that explains the reason for this exception.</param>
        /// <param name="xmlUri">The Uri of the document with incorrect data.</param>
        public WrongXmlDataException(string message, string xmlUri)
            : this(message)
        {
            this.xmlUri = xmlUri;
        }


        /// <summary>
        /// Initializes a new instance of the Scand.Toolbars.WrongXmlDataException class. 
        /// </summary>
        /// <param name="message">The error message that explains the reason for this exception.</param>
        /// <param name="xmlUri">The Uri of the document with incorrect data.</param>
        /// <param name="buttonNodeNumber">The node's number in the document with wrong data.</param>
        public WrongXmlDataException(string message, string xmlUri, int buttonNodeNumber)
            : this(message, xmlUri)
        {
            this.buttonNodeNumber = buttonNodeNumber;
        }

        /// <summary>
        /// Initializes a new instance of the Scand.Toolbars.WrongXmlDataException class. 
        /// </summary>
        /// <param name="message">The error message that explains the reason for this exception.</param>
        /// <param name="xmlUri">The Uri of the document with incorrect data.</param>
        /// <param name="buttonNodeNumber">The node's number in the document with wrong data.</param>
        /// <param name="paramName">The name of the wrong element.</param>
        public WrongXmlDataException(string message, string xmlUri, int buttonNodeNumber, string paramName)
            : this(message, xmlUri, buttonNodeNumber)
        {
            this.paramName = paramName;
        }

        /// <summary>
        /// Initializes a new instance of the WrongXmlDataException class. 
        /// </summary>
        /// <param name="message">The error message that explains the reason for this exception.</param>
        /// <param name="xmlUri">The Uri of the document with incorrect data.</param>
        /// <param name="buttonNodeNumber">The node's number in the document with wrong data.</param>
        /// <param name="paramName">The name of the wrong element.</param>
        /// <param name="paramValue">Wrong value. </param>
        public WrongXmlDataException(string message, string xmlUri, int buttonNodeNumber, string paramName, string paramValue)
            : this(message, xmlUri, buttonNodeNumber, paramName)
        {
            this.paramValue = paramValue;
        }

        /// <summary>
        /// Gets name of the parameter which contains incorrect data.
        /// </summary>
        public string ParamName
        {
            get { return paramName; }
        }

        /// <summary>
        /// Gets value of parameter.
        /// </summary>
        public string ParamValue
        {
            get { return paramValue; }
        }

        /// <summary>
        /// Gets Uri of Xml.
        /// </summary>
        public string XmlUri
        {
            get { return xmlUri; }
        }

        /// <summary>
        /// Gets number of node with incorrect data. 
        /// </summary>
        public int ButtonNodeNumber
        {
            get { return buttonNodeNumber; }
        }

        /// <summary>
        /// Gets the error message with specified Uri, node number with wrong info, parameter name and value,
        /// or less things if they are not specified.
        /// 
        /// Return Value
        /// A text string describing the details of the exception.
        /// If additional values passed to constructor are not null or not zero length(zero),
        /// these values are appened to message string. 
        /// </summary>
        public override string Message
        {
            get
            {
                if (String.IsNullOrEmpty(xmlUri) && String.IsNullOrEmpty(paramName)
                    && String.IsNullOrEmpty(paramValue) && buttonNodeNumber < 1)
                {
                    return base.Message;
                }
                var message = new StringBuilder(base.Message);
                message.AppendLine();
                if (!String.IsNullOrEmpty(xmlUri)) message.AppendFormat("(XmlUri={0}){1}", xmlUri, Environment.NewLine);
                if (buttonNodeNumber > 0) message.AppendFormat("(ButtonNodeNumber={0}){1}", buttonNodeNumber, Environment.NewLine);
                if (!String.IsNullOrEmpty(paramName)) message.AppendFormat("(ParamName={0}){1}", paramName, Environment.NewLine);
                if (!String.IsNullOrEmpty(paramValue)) message.AppendFormat("(ParamValue={0}){1}", paramValue, Environment.NewLine);
                return message.ToString();
            }
        }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
            {
                throw new ArgumentNullException("info");
            }
            base.GetObjectData(info, context);
            info.AddValue("ParamName", paramName);
            info.AddValue("ParamValue", paramValue);
            info.AddValue("XmlUri", xmlUri);
            info.AddValue("ButtonNodeNumber", buttonNodeNumber);
        }

        private WrongXmlDataException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            xmlUri = info.GetString("XmlUri");
            paramName = info.GetString("ParamName");
            paramValue = info.GetString("ParamValue");
            buttonNodeNumber = info.GetInt32("ButtonNodeNumber");
        }
    }
}
