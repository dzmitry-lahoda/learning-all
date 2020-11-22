using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace EPAM.Trainings.Zadanie2_5
{
    public delegate void MessengerEventHandler<T>(Object sender, T e); 

    public interface IIndexedMessage
    {
        String Message
        {
            get;
        }

        int Index
        {
            get;
        }
    }

    public class HashIndexedMessage : IIndexedMessage, IComparable<HashIndexedMessage>
    {
        public HashIndexedMessage(String message,String info)
        {
            this.message = message;
            this.index = message.GetHashCode();
            this.info = info;
        }
        protected String message = String.Empty;
        protected int index = 0;
        protected String info =String.Empty;

        public String Message
        {
            get
            {
                return message;
            }
        }

        public int Index
        {
            get
            {
                return index;
            }
        }

        public String Info
        {
            get
            {
                return info;
            }
        }

        int IComparable<HashIndexedMessage>.CompareTo(HashIndexedMessage other)
        {
            if (other != null)
            {
                if (other.Index > this.Index)
                {
                    return -1;
                }
                if (other.Index < this.Index)
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

    public class DescendingComparer<T>: IComparer<T> where T:IIndexedMessage
    {
        int IComparer<T>.Compare(T x, T y)
        {
            if (y != null && x != null)
            {
                if (x.Index > y.Index)
                {
                    return -1;
                }
                if (x.Index < y.Index)
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

    public interface IIndexedMessageMessengerEventArgs
    {
        IIndexedMessage IndexedMessage
        {
            get;
            set;
        }
    }

    public class MessengerEventArgs :EventArgs, IIndexedMessageMessengerEventArgs
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

    public class IndexedMessengerEventArgs : EventArgs,IIndexedMessageMessengerEventArgs
    {
        public IndexedMessengerEventArgs()
        {
        }

        public IndexedMessengerEventArgs(IIndexedMessage indexedMessage)
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

    public class Messenger<T, K> : IEnumerable<T>
        where T : IIndexedMessage
        where K : class, IIndexedMessageMessengerEventArgs, new()
    {
        public event MessengerEventHandler<K> Sent;
        public List<T> list = new List<T>();

        public void Send(T indexedMessage)
        {
            
            K messengerEventArgs = new K();
            messengerEventArgs.IndexedMessage = indexedMessage;
                if (Sent != null)
                {
                    Sent(this, messengerEventArgs);
                }
                list.Add(indexedMessage);
        }

        public List<T> Find(Predicate<T> match)
        {
            return list.FindAll(match);
        }

        public List<T> FindPositive()
        {
            List<T> l = new List<T>();
            foreach (T var in list)
            {
                if (var.Index > 0)
                {
                    l.Add(var);
                }
            }
            return l;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return null;
            //return new Iterator<T>(list, 1);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new Iterator<T>(list, 1);
        }

    }

    public class Iterator<T> : IEnumerator<T> where T : IIndexedMessage
    {
        public IEnumerator<T> internalIEnumerator;
        private List<T> list = new List<T>();
        private int index = -1;
        private int step = 1;

        public Iterator(List<T> list, int step)
        {
            this.step = step;
            this.list = list;
        }

        T IEnumerator<T>.Current
        {
            get 
            {
                try
                {
                    return list[index];
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                } 
            }
        }

        void IDisposable.Dispose()
        {
            
        }


        object IEnumerator.Current
        {
            get 
            {
                try
                {
                    return list[index];
                }
                catch (IndexOutOfRangeException)
                { 
                    throw new InvalidOperationException();
                } 
            }
        }

        bool IEnumerator.MoveNext()
        {
            index += step;
            return (index < list.Count);

        }

        void IEnumerator.Reset()
        {
            index = -1;
        }

    }

    public class Receiver<T> where T:IIndexedMessage
    {
        public void OnSend(Object sender, MessengerEventArgs e)
        {
            Console.WriteLine();
            Console.WriteLine(e.IndexedMessage.Message+" "+e.IndexedMessage.Index.ToString());
        }

        public bool FindPositive(T target)
        {
            return target.Index > 0;
        }
    }

    public class IndexedReceiver<T> where T : IIndexedMessage
    {
        public void OnSend(Object sender, IndexedMessengerEventArgs e)
        {
            Console.WriteLine();
            Console.WriteLine(e.IndexedMessage.Message + " " + e.IndexedMessage.Index.ToString());
        }

        public bool FindPositive(T target)
        {
            return target.Index > 0;
        }
    }


    class Program
    {

        static void Main(string[] args)
        {

            Messenger<HashIndexedMessage, MessengerEventArgs> messenger = new Messenger<HashIndexedMessage, MessengerEventArgs>();
            Receiver<HashIndexedMessage> receiver = new Receiver<HashIndexedMessage>();
            messenger.Sent += receiver.OnSend;
            String message;

            message = Console.ReadLine();
            messenger.Send(new HashIndexedMessage(message, "1"));
            message=Console.ReadLine();
            messenger.Send(new HashIndexedMessage(message, "2"));
            message = Console.ReadLine();
            messenger.Send(new HashIndexedMessage(message, "3"));
            message = Console.ReadLine();
            messenger.Send(new HashIndexedMessage(message, "4"));
            message = Console.ReadLine();
            messenger.Send(new HashIndexedMessage(message, "5"));

            Console.WriteLine();
            foreach ( IIndexedMessage var in messenger)
            {
                Console.WriteLine(var.Index);
            }
            messenger.list.Sort();
            Console.WriteLine("\n After IComparable sorting");
            foreach (IIndexedMessage var in messenger)
            {
                Console.WriteLine(var.Index);
            }

            Console.WriteLine("\n After IComparer sorting");
            messenger.list.Sort(new DescendingComparer<HashIndexedMessage>());
            foreach (IIndexedMessage var in messenger)
            {
                Console.WriteLine(var.Index);
            }

            Console.WriteLine("\n Only positive");
            List < HashIndexedMessage> list=messenger.Find(new Predicate<HashIndexedMessage>(receiver.FindPositive));
            list = messenger.FindPositive();
            foreach (HashIndexedMessage var in list)
            {
                Console.WriteLine(var.Index);
            }

            Console.ReadLine();
        }


                
    }
}
