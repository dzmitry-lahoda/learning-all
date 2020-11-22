using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace EPAM.Trainings.Zadanie2_3
{
    public delegate void MessengerEventHandler(Object sender, MessengerEventArgs e);


    public class IndexedMessage:IComparable<IndexedMessage>
    {
        public IndexedMessage(String message, int index)
        {
            this.message = message;
            this.index = index;
        }
        public String message = String.Empty;
        public int index = 0;

        int IComparable<IndexedMessage>.CompareTo(IndexedMessage other)
        {
            if (other != null)
            {
                if (other.index > this.index)
                {
                    return -1;
                }
                if (other.index < this.index)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            return 0;
        }

    }

    public class DescendingComparer:IComparer<IndexedMessage>
    {
        int IComparer<IndexedMessage>.Compare(IndexedMessage x, IndexedMessage y)
        {
            if (y != null && x != null)
            {
                if (x.index > y.index)
                {
                    return -1;
                }
                if (x.index < y.index)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            return 0;
        }
    }

    public class MessengerEventArgs : EventArgs
    {
        public MessengerEventArgs(IndexedMessage indexedMessage)
        {
            this.indexedMessage=indexedMessage;
        }
        public IndexedMessage indexedMessage;


    }

    public class Messenger:IEnumerable
    {
        public event MessengerEventHandler Sent;
        public List<IndexedMessage> list = new List<IndexedMessage>();
  
        public void Send(IndexedMessage indexedMessage)
        {
            MessengerEventArgs messengerEventArgs = new MessengerEventArgs(indexedMessage);
                if (Sent != null)
                {
                    Sent(this, messengerEventArgs);
                }
                list.Add(messengerEventArgs.indexedMessage);
        }



        IEnumerator IEnumerable.GetEnumerator()
        {

            return new Iterator(list,2);
        }
    }

    public class Iterator:IEnumerator<IndexedMessage>
    {
        public IEnumerator<IndexedMessage> internalIEnumerator;
        private List<IndexedMessage> list = new List<IndexedMessage>();
        private int index = 1;
        private int step = 1;

        public Iterator(List<IndexedMessage>  list,int step)
        {
            this.step = step;
            this.list = list;
        }

        IndexedMessage IEnumerator<IndexedMessage>.Current
        {
            get 
            { 
                index+=step;
                return list[index-step];
            }
        }

        void IDisposable.Dispose()
        {
            
        }


        object IEnumerator.Current
        {
            get 
            {
                index += step;
                return list[index - step];
            }
        }

        bool IEnumerator.MoveNext()
        {
            if (index > list.Count)
            {
                return false;
            }
            return true;
        }

        void IEnumerator.Reset()
        {
            index = 1;
        }

    }

    public class Receiver
    {
        public void OnSend(Object sender, MessengerEventArgs e)
        {
            Console.WriteLine();
            Console.WriteLine(sender.ToString() + " : " + e.indexedMessage.message+
                " "+e.indexedMessage.index.ToString());
        }
    }


    class Program
    {

        static void Main(string[] args)
        {
   
            Messenger messenger = new Messenger();
            Receiver receiver = new Receiver();
            messenger.Sent += receiver.OnSend;
            String message;
            message = Console.ReadLine();
            messenger.Send(new IndexedMessage(message,1231));
            message=Console.ReadLine();
            messenger.Send(new IndexedMessage(message,12));
            message = Console.ReadLine();
            messenger.Send(new IndexedMessage(message, 123123));

            message = Console.ReadLine();
            messenger.Send(new IndexedMessage(message, 34));

            /*message = Console.ReadLine();
            messenger.Send(new IndexedMessage(message, 522));*/

            Console.WriteLine();
            foreach ( IndexedMessage var in messenger)
            {
                Console.WriteLine(var.index);
            }
            messenger.list.Sort();
            Console.WriteLine();
            foreach (IndexedMessage var in messenger)
            {
                Console.WriteLine(var.index);
            }

            Console.WriteLine();
            messenger.list.Sort(new DescendingComparer());
            foreach (IndexedMessage var in messenger)
            {
                Console.WriteLine(var.index);
            }
            Console.ReadLine();
        }


                
    }
}
