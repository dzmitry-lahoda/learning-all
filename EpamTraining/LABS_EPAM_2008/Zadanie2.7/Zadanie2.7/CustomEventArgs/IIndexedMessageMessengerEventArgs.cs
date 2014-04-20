using System;
using System.Collections.Generic;
using System.Text;

namespace EPAM.Trainings
{
    public interface IIndexedMessageMessengerEventArgs
    {

        IIndexedMessage IndexedMessage
        {
            get;
            set;
        }
    }

}
