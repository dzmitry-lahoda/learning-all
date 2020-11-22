using System;
using System.Collections.Generic;
using System.Text;

namespace EPAM.Trainings.Zadanie2_6
{
    public class Subscriber<T> where T : IIndexedMessage
    {
        public void OnSend(Object sender, MessengerEventArgs e)
        {
            Console.WriteLine();
            Console.WriteLine(e.IndexedMessage.Message + " " + e.IndexedMessage.Index.ToString());
        }

        public bool FindPositive(T target)
        {
            return target.Index > 0;
        }
    }
}
