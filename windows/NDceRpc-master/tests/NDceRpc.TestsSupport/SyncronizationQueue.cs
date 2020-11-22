using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Collections;

namespace StaThreadSyncronizer
{
   internal interface IItemQueue : IDisposable
   {
      bool IsEmpty { get; }
      SendOrPostCallbackItem Dequeue();
   }

   internal class SyncronizationQueue : IItemQueue, IDisposable
   {
      private Queue<SendOrPostCallbackItem> mQueue;
      private object mSync = new object();
      private AutoResetEvent mItemsInQueueEvent = new AutoResetEvent(false);

      public SyncronizationQueue()
      {
         mQueue = new Queue<SendOrPostCallbackItem>();
      }

      public void AddItem(SendOrPostCallbackItem item)
      {
         lock (((ICollection)mQueue).SyncRoot)
         {            
            mQueue.Enqueue(item);
            mItemsInQueueEvent.Set();
         }
      }

      public void Dispose()
      {
         lock (((ICollection)mQueue).SyncRoot)
         {
            // release any lock 
            mItemsInQueueEvent.Set();
            mQueue.Clear();            
         }
         

      }

      public bool IsEmpty
      {
         get
         {
            lock (mSync)
            {
               return mQueue.Count == 0;
            }
         }
      }

      public SendOrPostCallbackItem Dequeue()
      {
         Console.WriteLine("Before " + mQueue.Count);
         mItemsInQueueEvent.WaitOne();
         Console.WriteLine("After");
         SendOrPostCallbackItem item = null;
         lock (((ICollection)mQueue).SyncRoot)
         {
           if (mQueue.Count > 0)
             item = mQueue.Dequeue();
         }
         

         return item;
      }
   }
}
