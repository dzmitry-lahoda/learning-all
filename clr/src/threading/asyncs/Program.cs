using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace asyncs
{
    class Program
    {

        public static async Task TwoSteps(int cnt){
            Console.WriteLine($"{cnt} Before Await1 " + Thread.CurrentThread.ManagedThreadId);
            await new HttpClient().GetStringAsync("http://1.1.1.1").ConfigureAwait(false);    
            Console.WriteLine($"{cnt} After Await 1 Before Await2 " + Thread.CurrentThread.ManagedThreadId);
            await new HttpClient().GetStringAsync("http://1.1.1.1").ConfigureAwait(false);    
            Console.WriteLine($"{cnt} After Await2 " + Thread.CurrentThread.ManagedThreadId);
        }

        static void Main(string[] args)
        {
            var ts = Process.GetCurrentProcess();
            Console.WriteLine("Look ma no threads! {0}", ts.Threads.Count);        
            TwoSteps(1);
            TwoSteps(2);
            Console.WriteLine("Look ma no threads! {0}", ts.Threads.Count);        
            for (var i = 3; i < 21;i++){
                TwoSteps(i);
            }
            while (true){
                Thread.Sleep(100);
                Console.WriteLine(ts.Threads.Count);
            }
            Thread.Sleep(10000);
        }
    }
}
