using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace AcceptanceTests.Helpers
{
    public static class BufferBlockExtensions
    {
        public static IEnumerable<T> Receive<T>(this BufferBlock<T> current, int count, TimeSpan timeout)
        {
            var items = new System.Collections.Concurrent.ConcurrentBag<T>();
            var autoResetEvent = new AutoResetEvent(false);
            Parallel.For(0, count, i =>
                                       {
                                           var value = current.Receive(timeout);
                                           items.Add(value);
                                           if (items.Count >= count)
                                           {
                                               autoResetEvent.Set();
                                           }
                                       });
            autoResetEvent.WaitOne(timeout);

            return items;
        }
    }
}