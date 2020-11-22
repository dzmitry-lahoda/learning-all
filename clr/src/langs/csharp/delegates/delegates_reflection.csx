using System;
using System.Reflection;
using System.Linq;
using static System.Console;

Console.WriteLine(typeof(Action).GetMembers().Select(x => x.Name).Aggregate((acc, e) => acc + Environment.NewLine + e));



Console.WriteLine(typeof(Action).GetMethod("Invoke").ReturnType);
Console.WriteLine(typeof(Func<string>).GetMethod("Invoke").ReturnType);