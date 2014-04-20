using System;
using System.Collections.Generic;
using System.Text;

namespace CrossLanguage
{
    class Program
    {
        static void Main(string[] args)
        {
            HelloCPP helloCPP = new HelloCPP();
            helloCPP.Hello();
            HelloCS helloCS = new HelloCS();
            helloCS.Hello();
            Console.ReadLine();
        }
    }
}
