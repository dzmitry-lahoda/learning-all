using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
//using Amib.Threading;

namespace clr_threads
{

    public class WrappingSynchronizationContext : SynchronizationContext
    {
        private SynchronizationContext original;

        public WrappingSynchronizationContext(SynchronizationContext existing = null)
        {
            this.original = original;
        }

        public override void Post(SendOrPostCallback d, object state)
        {
            SendOrPostCallback w = (g) =>
            {
                Console.WriteLine("Post Before " + Thread.CurrentThread.ManagedThreadId);
                d(g);
                Console.WriteLine("Post After " + Thread.CurrentThread.ManagedThreadId);
            };

            if (original != null)
            {
                original.Post(w, state);
            }
            else
            {
                base.Post(w, state);
            }

        }

        public override void Send(SendOrPostCallback d, object state)
        {
            SendOrPostCallback w = (g) =>
            {
                Console.WriteLine("Send Before " + Thread.CurrentThread.ManagedThreadId);
                d(g);
                Console.WriteLine("Send After " + Thread.CurrentThread.ManagedThreadId);
            };
            if (original != null)
            {
                original.Send(w, state);
            }
            else
            {
                base.Send(w, state);
            }
        }


    }

    class Program
    {
        static void Main(string[] args)
        {
            SynchronizationContext.SetSynchronizationContext(new SynchronizationContext());
            Console.WriteLine("Before DoRoot " + Thread.CurrentThread.ManagedThreadId);
            Console.WriteLine(new WrappingSynchronizationContext() == new WrappingSynchronizationContext());
            Parallel.For(0, 16, new ParallelOptions { MaxDegreeOfParallelism = 16 }, x => DoRoot());
            
            Console.WriteLine("After DoRoot " + Thread.CurrentThread.ManagedThreadId);
            Console.ReadKey();
        }

        public static async Task DoRoot()
        {
            Console.WriteLine("Before Do " + Thread.CurrentThread.ManagedThreadId);
            var a = await Do();
            Console.WriteLine(a);
            Console.WriteLine("After Do " + Thread.CurrentThread.ManagedThreadId);
        }

        public static async Task<int> Do()
        {
            var original = SynchronizationContext.Current;
            SynchronizationContext wrap = null;
            wrap = new WrappingSynchronizationContext(original);
            try
            {
                SynchronizationContext.SetSynchronizationContext(wrap);
                var r = new System.Net.WebClient();
                var data = await r.DownloadStringTaskAsync(new Uri("http://localhost"));
                Console.WriteLine(data.Length);
                //var awaiter = new AsyncAwaiter();
                var r2 = new System.Net.WebClient();
                
                var data2 = await r2.DownloadStringTaskAsync(new Uri("http://localhost"));
                var qwe = "ASdasd";
                await Task.Delay(5);

                return data2.Length + data.Length;
            }
            finally
            {
                Console.WriteLine("Finally " + Thread.CurrentThread.ManagedThreadId);
                if (SynchronizationContext.Current == wrap)
                {
                    SynchronizationContext.SetSynchronizationContext(original);
                }
                else
                {
                    throw new Exception();
                }
            }
        }
    }
}
