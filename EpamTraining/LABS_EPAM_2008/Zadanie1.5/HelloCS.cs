using System;
using System.Collections.Generic;
using System.Text;

namespace CrossLanguage
{
    class HelloCS:CrossLanguage.HelloCPP
    {
        public override void Hello()
        {
            Console.WriteLine("Hello, C#");
        }
        public int Substruct(int val1, int val2)
        {
            return val1 - val2;
        }
    }
}
