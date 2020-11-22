
using System;

namespace csharp
{
    class bit_operations
	{
        public static void Do(string[] args)
        {
            Console.WriteLine("Hello World!");
            


            // toggle bit
            var a = 1;
            var b = 2;
            var c = a | b;
            var d = c & (~2);
            
            Console.Write("Press any key to continue . . . ");
            Console.ReadKey(true);
        }
    }
}