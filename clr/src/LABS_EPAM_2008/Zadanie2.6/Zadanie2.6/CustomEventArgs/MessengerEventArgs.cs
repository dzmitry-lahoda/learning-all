using System;
using System.Collections.Generic;
using System.Text;

namespace EPAM.Trainings.Zadanie2_6
{

    [Serializable]
    public class MessengerEventArgs : EventArgs, IIndexedMessageMessengerEventArgs
    {
        public MessengerEventArgs()
        {
        }

        public MessengerEventArgs(IIndexedMessage indexedMessage)
        {
            this.indexedMessage = indexedMessage;
        }

        private IIndexedMessage indexedMessage;

        public IIndexedMessage IndexedMessage
        {
            get
            {
                return indexedMessage;
            }
            set
            {
                indexedMessage = value;
            }
        }
    }
}
