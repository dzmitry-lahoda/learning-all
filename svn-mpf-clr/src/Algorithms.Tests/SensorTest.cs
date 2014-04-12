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
    public class SensorTest
    {
        [Test]
        public void AddOnePattern()
        {
            var node = new HtmNode(1);
            node.Content = new FirstOrderMarkovChain();
            var images = Directory.GetFiles(@"E:\DataSets\Images\Different\");
            foreach (var image in images)
            {
                var matrix = ImageLayer.LoadBlackAndWhiteQuadraticImageMatrix(image);
                var subMatrix = new DenseMatrix(matrix).SubMatrix(20, 4, 20, 4);
                node.Content.AddPattern(subMatrix);
            }
            var groups = new TemporalGrouper().DoGrouping(node.Content.NormalizedTransitionMatrix, 5);
            node.Content.TemporalGroups = groups;
            var sensor = new Sensor();
            var testMatrix = ImageLayer.LoadBlackAndWhiteQuadraticImageMatrix(images[11]);
            var testSubMatrix = new DenseMatrix(testMatrix).SubMatrix(20, 4, 20, 4);
            var vector = sensor.Sence(node, testSubMatrix);
        }

    }
}
