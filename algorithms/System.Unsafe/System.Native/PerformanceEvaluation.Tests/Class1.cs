using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.IO;
using MatrixExtensions.Equality;
using MatrixExtensions.Operations;
using MatrixExtensionsTestProject;
using MeasureIt;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;

namespace PerformanceEvaluation.Tests
{
    [TestFixture]
    public class CompositeSpeedTesterTests
    {

        [TestMethod]
        public void Multiply()
        {
            int[,] x = {{2, 2, 3, 4}, 
                       {5, 6, 7, 8}, 
                       {5, 1, 3, 4},
                       {6, 9, 7, 8}};
            int[,] y = {{9, 2, 7, 4}, 
                       {4, 6, 7, 8}, 
                       {3, 2, 1, 5},
                       {7, 3, 2, 6}};

            int[,] c = x.Multiply(y);

            int[,] cs = x.MultiplyStrassen(y);

            int[,] e =  {{ 63, 34	,39	,63	},
		{146,	10	,100	,151	},
			{86 ,34,	53,	67	},
			{167,	104	,128,	179}};



            e.EqualsElements(c);
            e.EqualsElements(cs);

        }

        private int[,] _m;

        [Test]
        public void Do()
        {
            var output = System.Console.Out;
            _m = new int[16, 16];
            SpeedTesting.Do(output, () => Method1(), () => Method2());

        }

        public void Method1()
        {
            _m.MultiplyStrassen(_m);
        }
        public void Method2()
        {
            _m.Multiply(_m);
        }
    }
}
