using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Web;
using System.Web.Services;

namespace EPAM.Trainings.Zadanie2_6
{
    public delegate void MessengerEventHandler<T>(Object sender, T e);

    [MessengerDescriptionAttribute("Good messenge")]
    public class Messenger<T, K> : IEnumerable<T>
        where T : IIndexedMessage
        where K : class, IIndexedMessageMessengerEventArgs, new()
    {
        public event MessengerEventHandler<K> Sent;
        public List<T> list = new List<T>();

        [WebMethod]
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
}
