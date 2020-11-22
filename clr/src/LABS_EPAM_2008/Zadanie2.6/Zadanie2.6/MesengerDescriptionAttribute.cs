using System;
using System.Collections.Generic;
using System.Text;

namespace EPAM.Trainings.Zadanie2_6
{
    [AttributeUsage(AttributeTargets.Class,AllowMultiple=false,Inherited=false)]
    class MessengerDescriptionAttribute : Attribute
    {
        private string msgData;

        public MessengerDescriptionAttribute(string description)
        { 
            msgData = description; 
        }
        public MessengerDescriptionAttribute() { }

        public string Description
        {
            get { return msgData; }
            set { msgData = value; }
        }
    }
}
