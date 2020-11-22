using System;
using System.Threading;
using System.Threading.Tasks;

Task.Factory.StartNew(() =>
{
    throw new Exception("Will not crash. Will not log.");
});

Thread.Sleep(5000);

Console.WriteLine("Error lost.");
