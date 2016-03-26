using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ConsoleApplication1
{
    class Program
    {

        class WorkState
        {
            public Actor Actor;
            
            unsafe internal int* numOfTickest;
        }

        class Actor
        {
            public int Number { get; set; }
        }

        public static char[] buffer = new char[8];

        public static object syncRoot = new object();

        public static ReaderWriterLock asd;

        static void Main(string[] args)
        {
            unsafe
            {
                int numOfTickets = 100;
                for (int i = 0; i < 4; i++)
                {
                    var state = new WorkState();
                    state.numOfTickest = &numOfTickets;
                    state.Actor = new Actor { Number = i };
                    ThreadPool.QueueUserWorkItem(SellTickets, state);
                }
                Thread.Sleep(10000);
                //System.Threading.WaitHandle[] forks2 = new WaitHandle[5];
                //Semaphore[] forks = new Semaphore[5];
            }
        }
        public static void SellTickets(object state)
        {
            WorkState actor = (WorkState)state;
            while (true)
            {
                //lock (syncRoot)
                {
                    unsafe
                    {
                        var pointer = actor.numOfTickest;
                        if (*pointer > 0)
                        {
                            (*pointer)--;
                            Console.WriteLine(actor.Actor.Number + " " + *pointer);
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                Thread.Sleep(100);
            }
        }

        public void Reader()
        {
        }
    }
}
