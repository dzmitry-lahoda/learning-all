using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using static System.Console;
using csharplib;
using unsafe_com;

namespace good_delegates
{
    class Program
    {
        // function calls funcion calls funtion etc
        public int DoMethodInner(int v) => (v + 1) / 2;
        public double DoMethod(int v) => (v * DoMethodInner(v)) * 1.331231231233;
        static void gc()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }

        static void Main(string[] args)
        {
            TestClass.Test();

#if DEBUG
            throw new Exception("Run in Release mode!!!");
#endif

            // TODO: use bench
            var loops = 10 * 1000 * 1000;
            var sw = new Stopwatch();
            var r = new Random(42);
            //var doDelegate = Delegate.Combine(DelegateFactory.Create(), DelegateFactory.Create());
            //var doStuct = new FF<int, double>(StuctFactory.Create(), StuctFactory.Create());

            var doDelegate = DelegateFactory.Create();
            var doStuct = StuctFactory.Create();

            WriteLine("Warm up");
            r = new Random(42);
            gc();
            sw.Restart();
            for (var i = 0; i < 10; i++)
            {
                //DoMethod(r.Next());
                doDelegate.DynamicInvoke(r.Next());
                doStuct._(r.Next());
            }
            sw.Stop();
            WriteLine(sw.ElapsedMilliseconds);

            WriteLine("Bench!");

            r = new Random(42);
            gc();
            sw.Restart();
            //for (var i = 0; i < loops; i++) DoMethod(r.Next());
            sw.Stop();
            WriteLine(sw.ElapsedMilliseconds);

            r = new Random(42);
            gc();
            sw.Restart();
            for (var i = 0; i < loops; i++) doStuct._(r.Next());
            sw.Stop();
            WriteLine(sw.ElapsedMilliseconds);

            r = new Random(42);
            gc();
            sw.Restart();
            for (var i = 0; i < loops; i++) doDelegate(r.Next());
            sw.Stop();
            WriteLine(sw.ElapsedMilliseconds);




        }
    }
}
