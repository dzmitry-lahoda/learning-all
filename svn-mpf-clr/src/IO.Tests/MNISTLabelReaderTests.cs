using System.IO;
using NUnit.Framework;

namespace IO.Tests
{
    [TestFixture]
    public class MNISTLabelReaderTests
    {
        [Test]
        public void ReadLabelTest()
        {
            var labelsFile = File.OpenRead("../../../../data/MNIST/t10k-labels.idx1-ubyte");
            var reader = new MNISTLabelReader(labelsFile);
            
            for (int i = 1; i <= reader.NumberOfLabels; i++)
            {
                var label = reader.ReadLabel(i);
                Assert.IsTrue(label<=9);
            }
        }
    }
}
