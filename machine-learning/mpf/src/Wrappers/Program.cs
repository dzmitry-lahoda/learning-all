using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wrappers
{
    class Program
    {
        static void Main(string[] args)
        {
            var x = new []{1.2,2.2,3.1};
            var y = new []{1.2,2.3,3.3};
            var m = 2;
            var n = 2;
            int info;
            alglib.barycentricinterpolant b;
            alglib.barycentricinterpolant b1;
            alglib.polynomialfitreport rep;
            //alglib.polynomialbuild(x, y,out b1);
            
            alglib.polynomialfit(x, y, 5, out info, out b, out rep);
            double yy =alglib.barycentriccalc(b, 2.2);
        }
    }
}
