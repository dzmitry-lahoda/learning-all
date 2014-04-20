using System;
using System.Collections;

namespace Itransition.Training.Task03
{
    [Serializable]
    public class ListEventArgs:EventArgs
    {
        protected readonly string msg;

        public ListEventArgs(String message)
        {
            msg = message;
        }

        public String Message
        {
            get { return msg; }
        }
    }

    public delegate void ListEventHandler(Object sender,ListEventArgs e);

    /// <summary>
    /// Represents a List which rises Changed event when its size changed  
    /// </summary>
    public class List: ArrayList
    {
        public event ListEventHandler Changed;

        #region Overrided methods
        public override void Insert(int index, object value)
        {
            base.Insert(index, value);
            if (Changed != null)
            {
                Changed(this, new ListEventArgs("Object inserted"));
            }
        }

        public override int Add(object value)
        {
            int index=base.Add(value);
            if (Changed != null)
            {
                Changed(this, new ListEventArgs("Object added"));
            }
            return index;
        }
        public override void RemoveAt(int index)
        {
            base.RemoveAt(index);
            if (Changed != null)
            {
                Changed(this, new ListEventArgs(String.Format("Object at {0} removed", index)));
            }
        }
        public override void AddRange(ICollection c)
        {
            base.AddRange(c);
            if (Changed != null)
            {
                Changed(this, new ListEventArgs("Range added"));
            }
        }
        public override void Clear()
        {
            base.Clear();
            if (Changed != null)
            {
                Changed(this, new ListEventArgs("List cleared"));
            }
        }
        public override void Remove(object obj)
        {
            base.Remove(obj);
            if (Changed != null)
            {
                Changed(this, new ListEventArgs("Object removed"));
            }        
        }
        public override void RemoveRange(int index, int count)
        {
            base.RemoveRange(index, count);
            if (Changed != null)
            {
                Changed(this, new ListEventArgs(String.Format("Range from {0} to {1} removed", index, index + count - 1)));
            }
        }
        #endregion
    }
}
