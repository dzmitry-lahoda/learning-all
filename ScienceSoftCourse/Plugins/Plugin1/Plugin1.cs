using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Common;

namespace Plugin1
{
    public class Plugin1:IPlugin
    {
        public void Move(string soure, string destination)
        {
            Console.WriteLine("From \"{0}\" to \"{1}\" using \"{2}\"", soure, destination, this);
        }
    }
}
