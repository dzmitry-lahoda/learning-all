using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace EPAM.Trainings
{
    public class Subscriber<T> where T : IIndexedMessage
    {

        private Random r = new Random();

        public Messenger<HashIndexedMessage, MessengerEventArgs> messenger;

        public delegate void Adder();

        public Adder adder;

        public void OnSend(Object sender, MessengerEventArgs e)
        {
            Console.WriteLine();
            Console.WriteLine(e.IndexedMessage.Message + " " + e.IndexedMessage.Index.ToString());
        }

        public bool FindPositive(T target)
        {
            return target.Index > 0;
        }

        public void Start()
        {
            adder = new Adder(Add);
            adder.BeginInvoke(InvokeIsDone, null);
        }
        private void InvokeIsDone(IAsyncResult asyncResult)
        {
            Adder a = (Adder)asyncResult.AsyncState;
            try
            {
                adder.EndInvoke(asyncResult);
            }
            catch
            {
                
            }
        }

        public void Add()
        {
                    messenger.list.Add(new HashIndexedMessage(
                        String.Format("Message from {0} ",Thread.CurrentThread.ManagedThreadId)+
                        r.Next(1000, 2000).ToString(),
                        r.Next(1000, 2000).ToString()));
         }
    }
}
