using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
namespace asd
{
    class Program
    {
        static void Main(string[] args)
        {
            if (!FastWildcard.FastWildcard.IsMatch("Abc/42/wwe/42", "Abc*wwe*"))
                throw new Exception("Should match ?*");

            if (FastWildcard.FastWildcard.IsMatch("/path/*/hello", "/path/*/hello/*"))
                throw new Exception("Should match ?*");

            //while(true)
            {
                
            }
            // var s = new HttpClient();
            // var cnt = 0;
            // var cancel1 = new System.Threading.CancellationTokenSource(3000);
            // var tkn = cancel1.Token;
            // //cancel1.Cancel();
            // cancel1.Dispose();
            // Thread.Sleep(4000);
            // if (!tkn.IsCancellationRequested) {
            //     throw new Exception("ASDAS");
            // }
            // CancellationTokenSource cancel = new System.Threading.CancellationTokenSource();
            // TaskCompletionSource<int> a;

            // while (true)
            // {
            //     if (cnt % 100000 == -10)
            //     {
            //         cancel.Cancel();
            //         cancel.Dispose();
            //         cancel = new CancellationTokenSource();
            //     }
            //     s.GetAsync("https://www.google.com", cancel.Token);
            // }
            
            // Console.WriteLine("Hello World!");
        }
    }
}
