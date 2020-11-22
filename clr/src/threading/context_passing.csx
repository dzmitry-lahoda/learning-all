// Context may be massed via 
// ThreadStatic
// Thread.Current or other object which do hold ThreadStatic Dictionary
// Global process/AppDomain wide variable
// Method Invocation Parameter
// AsyncLocal - to allow to pass same context into another Thread taken for IO callback
// ThreadLocal
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Threading;

var v = new string(new[] { 'a' });
ThreadLocal<string> a = new ThreadLocal<string>(() => v);

void Do()
{
    Console.WriteLine("----------------------");
    Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
    Console.WriteLine(a.IsValueCreated);
    Console.WriteLine(a.Value);
    Console.WriteLine("----------------------");
}

Parallel.Invoke(Do, Do, Do, Do);