using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Blocks;
using dnHTM;
using dnHTM.ImageLayer;
using MathNet.Numerics.LinearAlgebra.Double;
using NUnit.Framework;


namespace dnHTMTests
{
    [TestFixture]
    public class NodeTests
    {
        [Test]
        public void AddOnePatternTest()
        {
            var node = new HtmNode(1);
            node.Content = new FirstOrderMarkovChain();
            var images = Directory.GetFiles(@"../../data/E:\DataSets\Images\A\");
            foreach (var image in images)
            {
                var matrix = ImageLayer.LoadBlackAndWhiteQuadraticImageMatrix(image);
                var subMatrix = new DenseMatrix(matrix).SubMatrix(20, 4, 20, 4);
                node.Content.AddPattern(subMatrix);
            }
            var groups = new TemporalGrouper().DoGrouping(node.Content.NormalizedTransitionMatrix, 10);
        }

        [Test]
        public void AddTest()
        {
            //add MNIST, form gropus
            var node = new HtmNode(1);
            node.Content = new FirstOrderMarkovChain();
            var images = Directory.GetFiles(@"../../data/E:\DataSets\Images\A\");
            foreach (var image in images)
            {
                var matrix = ImageLayer.LoadBlackAndWhiteQuadraticImageMatrix(image);
                var subMatrix = new DenseMatrix(matrix).SubMatrix(20, 4, 20, 4);
                node.Content.AddPattern(subMatrix);
            }
            var groups = new TemporalGrouper().DoGrouping(node.Content.NormalizedTransitionMatrix, 10);
        }


    }
}
