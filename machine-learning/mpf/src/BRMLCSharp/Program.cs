using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MicrosoftResearch.Infer;
using MicrosoftResearch.Infer.Models;

namespace BRMLCSharp
{
    class Program
    {
        static void Main(string[] args)
        {
           // QuickGraph.AdjacencyGraph<double,DoubleEdge> adjacencyGraph;
            //var a =new Variable("A");
            //a.Add(1, 0.65);
            //a.Add(0, 0.35);

            //var b = new Variable("B");
            //b.Add(1,0.77);
            //b.Add(0, 0.23);
          MicrosoftResearch.Infer.VariableGroup variableGroup = new VariableGroup();
            variableGroup.Name = "A";
            

            var c = new Variable("C");
            //And a0b0 = new And(0,0,0,1);
            //And a0b0 = new And(0, 0, 0, 1);

            //And a0b0 = new And(0, 0, 0, 1);

            //And a0b0 = new And(0, 0, 0, 1);

        }
    }

    //internal class DoubleEdge : IEdge<double>
    //{
    //    public double Source
    //    {
    //        get { return 1;  }
    //    }

    //    public double Target
    //    {
    //        get { return 1; }
    //    }
    //}
}
