using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using MathNet.Numerics.LinearAlgebra.Generic;
using MatrixExtensions.Generic;
using NUnit.Framework;

namespace IO.Tests
{
    [TestFixture]
    public class MNISTImageReaderTests
    {
        [Test]
        public void ReadImageTest()
        {
            var labelsFile = File.OpenRead("../../../../data/MNIST/train-images.idx3-ubyte");
            var reader = new MNISTImageReader(labelsFile);
            var image = reader.ReadImage(1);
            Assert.AreEqual(28, image.RowCount);
            Assert.AreEqual(28, image.ColumnCount);

            WriteToFile(image);
        }


        private void WriteToFile(Matrix<double> image)
        {
            Bitmap bitmap = new Bitmap(image.ColumnCount, image.RowCount);
            for (int i = 0; i < image.ColumnCount; i++)
            {
                for (int j = 0; j < image.RowCount; j++)
                {
                    var color = (int)Math.Floor(image[i, j]);
                    bitmap.SetPixel(i, j, Color.FromArgb(255, color, color, color));
                }
            }
            bitmap.Save("C:/mnist_image.bmp");
        }
    }
}
