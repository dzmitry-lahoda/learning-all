using System;
using System.Collections.Generic;
using System.Text;

namespace EPAM.Trainings.Zadanie2_6
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
