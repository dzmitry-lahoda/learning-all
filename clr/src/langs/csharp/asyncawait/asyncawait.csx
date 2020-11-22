#r "System.Net.Http.dll"
#r "System.Web.dll"
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Web;
using static System.Console;

var hang = true;

WriteLine("Before Task.FromResult");
Task.FromResult("").Wait();
WriteLine("After Task.FromResult");

WriteLine();
WriteLine("Before Task.Delay");
Task.Delay(0).Wait();
WriteLine("After Task.Delay");

WriteLine();
WriteLine("Before Task.Run");
Task.Run(() => { }).Wait();
WriteLine("After Task.Run");

void Test4()
{
    WriteLine();
    WriteLine("Before new Task Start");
    var t = new Task(() => { });
    t.Start();
    t.Wait();
    WriteLine("After new Task Start");
}

Test4();

WriteLine();
WriteLine("Before File.OpenRead ReadAsync");
var b = new byte[1];
var file = System.Reflection.Assembly.GetEntryAssembly().Location;
System.IO.File.OpenRead(file).ReadAsync(b, 0, 1).Wait();
WriteLine("After File.OpenRead ReadAsync");

WriteLine();
WriteLine("Before WebClient await DownloadStringTaskAsync");
var r = new System.Net.WebClient();
var result = await r.DownloadStringTaskAsync(new Uri("http://localhost"));
WriteLine("After WebClient await DownloadStringTaskAsync");


// TODO: try next with SynchronizationContext 

WriteLine();
WriteLine("Before WebClient WhenAll DownloadStringTaskAsync");
var r1 = new System.Net.WebClient();
var tt1 = r1.DownloadStringTaskAsync(new Uri("http://localhost"));
await Task.WhenAll(tt1);
WriteLine("After WebClient WhenAll DownloadStringTaskAsync");

WriteLine();
WriteLine("Before WebClient WhenAll DownloadStringTaskAsync And FromResult");
var r2 = new System.Net.WebClient();
var tt2 = r2.DownloadStringTaskAsync(new Uri("http://localhost"));
await Task.WhenAll(tt2, Task.FromResult<string>(""));
WriteLine("After WebClient WhenAll DownloadStringTaskAsync  And FromResult");

if (hang)
{
    
    WriteLine();
    WriteLine("Before WebClient DownloadStringTaskAsync");
    var r = new System.Net.WebClient();
    var tt = r.DownloadStringTaskAsync(new Uri("http://localhost"));
    Console.WriteLine(tt.Result);
    //tt.Wait();
    WriteLine("After WebClient DownloadStringTaskAsync");

    WriteLine();
    WriteLine("Before new Task");
    var t1r = new Task<int>(() => { return 1; });
    t1r.Wait();
    WriteLine("After new Task");
}