using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace Plugin2
{
    public class Plugin2:IPlugin
    {
        public void Move(string soure, string destination)
        {
            Console.WriteLine("From \"{0}\" to \"{1}\" using \"{2}\"", soure, destination, this);
        }
    }
}
