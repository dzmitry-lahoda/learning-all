using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharplib
{
    public class primitive_capture
    {

        public void Invoke(Action d)
        {
            d();
        }

        public void Do()
        {
            var x = 0;
            Invoke(() => x++);
            if (x == 1)
            {
                Console.WriteLine("1");
                Debug.WriteLine("1");
            }
            else
            {
                Console.WriteLine("0");
                Debug.WriteLine("0");
            }

        }

    }
}
