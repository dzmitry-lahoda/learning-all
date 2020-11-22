using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace managed_memory
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Press enter to load this exe (to prevent counting types for dll loading)");
            Console.ReadKey();
            LoadDll(Assembly.GetExecutingAssembly().Location);
           

            Console.WriteLine("Press enter to load this exe another time (check that adds number near zero bytes)");
            Console.ReadKey();
            LoadDll(Assembly.GetExecutingAssembly().Location);

            Console.WriteLine("Press enter to zero-type-dll.dll");
            Console.ReadKey();
            LoadDll("zero-type-dll.dll");

            Console.WriteLine("Press enter to one-type-dll.dll");
            Console.ReadKey();
            var asm = LoadDll("one-type-dll.dll");

			// load some of Unity container as popular lib
			//https://www.nuget.org/packages/Unity/

            Console.WriteLine("Press enter to load Microsoft.Practices.ServiceLocation.dll");
            Console.ReadKey();
            var asm2 = LoadDll("Microsoft.Practices.ServiceLocation.dll");

            Console.WriteLine("Press enter to load Microsoft.Practices.Unity.dll");
            Console.ReadKey();
            var asm3 = LoadDll("Microsoft.Practices.Unity.dll");

            Console.ReadKey();
			

        }

        private static void GCCleanUp()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }



        [System.Runtime.CompilerServices.MethodImpl(MethodImplOptions.NoInlining)]
        private static Assembly LoadDll(string dllName)
        {
            var asm = Assembly.LoadFrom(dllName);
            Console.WriteLine(asm.FullName);
            GCCleanUp();
            return asm;
        }

 
    }
}
