using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Runtime.Remoting;
using System.Text;
using Common;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Type source->");
            var source = Console.ReadLine();
            Console.WriteLine("Type destination->");
            var destination = Console.ReadLine();
            var path=Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            Console.WriteLine("Plugin catalog= {0}",path);
            var domain = AppDomain.CreateDomain("PluginContainer");
            var manager = (PluginManager)
                 domain.CreateInstanceAndUnwrap(Assembly.GetExecutingAssembly().FullName, "Client.PluginManager");
            var plugins=  manager.GetPlugins(path);
            Console.WriteLine("Type number:");
            for (int i = 0; i < plugins.Count; i++)
            {
                Console.WriteLine("{0} {1}",i,plugins[i]);
            }
            var number= Int32.Parse(Console.ReadLine());
            manager.Execute(source, destination, plugins[number]);
            AppDomain.Unload(domain);
            foreach (var s in AppDomain.CurrentDomain.GetAssemblies())
            {
                Console.WriteLine(s.FullName);
            } 
            Console.ReadLine();
        }
    }
}
