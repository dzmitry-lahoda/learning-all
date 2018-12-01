using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using MatrixExtensions.Operations;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MeasureIt;


namespace MatrixExtensionsPerformanceEvaluation
{


    class Program
    {

        public void Do()
        {
            

        }

        public static void Method1()
        {
            Thread.Sleep(100);
        }
        public static void Method2()
        {
            Thread.Sleep(300);
        }

        static void Main(string[] args)
        {
            TextWriter output = System.Console.Out;
            SpeedTesting.Do(output, 
                () => Method1(), 
                () => Method2());

            DelegateCallVsMethod();



            //TODO: rows vs columns
            //TODO: "for" vs 1 "Parallel.FOR" vs 2 "Parallel.FOR"
            var vector = CreateRandom(10000000);

            var sw = Stopwatch.StartNew();
            var minPos = vector.MaxPosition();
            Console.WriteLine(minPos);
            sw.Stop();
            Console.WriteLine("elapsed ms with spinlock: {0}", sw.ElapsedTicks);

            sw = Stopwatch.StartNew();
            minPos = MaxPosition(vector);
            Console.WriteLine(minPos);
            sw.Stop();
            Console.WriteLine("elapsed ms without parallel: {0}", sw.ElapsedTicks);
            Console.ReadKey();
        }

        private static void DelegateCallVsMethod()
        {
            int [] x= new int[1];
            int loop = 10000000;
            Action<int[]> doDummyWork = matrix => DoDummyWork(matrix);
            var sw = Stopwatch.StartNew();

            Expression<Action> MethodLoop = () => ASD(loop, x);
            
            sw.Stop();
            Console.WriteLine("elapsed ms with method: {0}", sw.ElapsedTicks);


            sw = Stopwatch.StartNew();
            //ParallelOptions asd;
            //Parallel.For<double>(0, 1, () => 5.0, (index, state, v1) => 1.0, v=> loop +=1 );
            //Parallel.Invoke
            Parallel.For( 0 ,loop,(i)=> DoDummyWork(x));
            //Parallel.For(0, loop / 2, (i) => DoDummyWork(x));
            //Parallel.ForEach(x,(v) => DoDummyWork(x));
            sw.Stop();
            Console.WriteLine("elapsed ms with parallel for: {0}", sw.ElapsedTicks);
            
            sw = Stopwatch.StartNew();
            Parallel.ForEach(x,(v) => DoDummyWork(x));
            sw.Stop();
            Console.WriteLine("elapsed ms with parallel enumerator: {0}", sw.ElapsedTicks);

            sw = Stopwatch.StartNew();

            for (int i = 0; i < loop; i++)
            {
                doDummyWork(x);
                
            }
            sw.Stop();
            Console.WriteLine("elapsed ms with delegate: {0}", sw.ElapsedTicks);
        }

        private static void ASD(int loop, int[] x)
        {
            for (int i = 0; i < loop; i++)
            {
                DoDummyWork(x);
            }
        }

        private static void DoDummyWork(int[] x)
        {
            x[0] = 0;
        }

        private static int MaxPosition(double[] vector)
        {
            var maxPos = 0;
            var max = vector[maxPos];
            for (int i = 1; i < vector.Length; i++)
            {
                if (vector[i] > max)
                {
                    max = vector[i];
                    maxPos = i;
                }
                
            }
            return maxPos;
        }

        private static double[] CreateRandom(int count)
        {
            var newVector = new double[count];
             var r = new Random();
            for (int i = 0; i < newVector.Length; i++)
			{
			 newVector[i] = r.NextDouble();

			} 
            return newVector;
        }


    }
}
