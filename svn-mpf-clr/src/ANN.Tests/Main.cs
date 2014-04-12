using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MathNet.Numerics.LinearAlgebra.Double;
using Module1;

namespace ANN.Tests
{
    public class Main1
    {
        public static void Main(string[] args)
        {

            FeedforwardNetwork nn = new FeedforwardNetwork();
            nn.AddLayer(new FeedforwardLayer(1));
            nn.Layers[0].LayerMatrix[0, 0] = 3;
            DeltaRuleTrain t = new DeltaRuleTrain(nn, 2, 5, 0.5);
            t.Iteration();
            t.Iteration();
            t.Iteration();
        }

        public void HopfiledNetwork_Works()
        {
            HopfiledNetwork hn = new HopfiledNetwork(4);
            double[] pattern = new double[] { 1, 1, 0, 0 };
            var known = new DenseVector(pattern);
            hn.Train(known);

            var answer = hn.Present(known);

            double[] unKnown = new double[] { 1, 0, 0, 0 };
            var unKnown1 = new DenseVector(unKnown);
            hn.Train(unKnown1);
            var answer2 = hn.Present(unKnown1);
        }

        public void AndNetwork_Works()
        {
            ANDNetwork a = new ANDNetwork();
            var val = a.Answer(1, 1);
        }
    }
}
