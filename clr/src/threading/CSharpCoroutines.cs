using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Linq.Expressions;

namespace ConsoleApplication8
{
    class Program2
    {

        private class NumberGenerator
        {
            public int NumbersGenerated { get; set; }

            public IEnumerable<int> GenerateNumbers()
            {
                for (int i = 1; i < int.MaxValue; i++)
                {
                    Console.WriteLine("NumberGenerator generated a number");
                    NumbersGenerated++;
                    yield return i;
                }
            }
        }

        static void Main2(string[] args)
        {
            NumberGenerator ng = new NumberGenerator();
            var ns = ng.GenerateNumbers();
            ns.GetEnumerator().MoveNext();
            
            //DoWork().First();
            Console.ReadKey();
        }

        static IEnumerable<WaitFor> DoWork()
        {
            Console.WriteLine("Starting up");
            yield return new WaitFor(20);
            Console.WriteLine("After resuming execution");
        }

        //state machine
        //static void do_work(void* state)
        //{
        //    MyState* ms = (MyState*)state;

        //    switch (ms->state)
        //    {
        //        case 0:
        //            printf("starting up\n");
        //            ms->state = 1;
        //            g_timeout_add(20 * msec, do_work, state);
        //            break;
        //        case 1:
        //            printf("20 seconds later");
        //    }
        //}
    }

    internal class WaitFor
    {
        public WaitFor(int i)
        {
            Thread.Sleep(10000);
        }
    }
}
