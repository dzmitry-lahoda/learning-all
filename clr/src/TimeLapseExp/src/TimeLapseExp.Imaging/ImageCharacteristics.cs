using System;
using System.ComponentModel.Composition;
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;
using AForge.Imaging;
using AForge.Math;
using TimeLapseExp.Imaging.Contract;

namespace TimeLapseExp.Imaging
{
    [Export(typeof(IImageCharacteristics))]
    public sealed class ImageCharacteristics : IImageCharacteristics
    {

        public double ComputeShannonEntropy(String fileName)
        {
            using (var bitmap = new Bitmap(fileName)){
                
            var stats = new ImageStatistics(bitmap);

            var redHisogram = stats.Red.Values;
            var greenHisogram = stats.Green.Values;
            var blueHisogram = stats.Blue.Values;
            var numberOfBins = redHisogram.Length;
            var hisogram = new int[numberOfBins];
            for (var i = 0; i < numberOfBins; i++)
            {
                hisogram[i] = redHisogram[i] + greenHisogram[i] + blueHisogram[i];
            }
            var entropy = Statistics.Entropy(hisogram);
         
            return entropy;
            } 
        }

        public double ComputeContrastMeasurmentNumber(String fileName)
        {
       using (var bitmap = new Bitmap(fileName))
            {
            var bitmapData = bitmap.LockBits(new Rectangle(0, 0,
                                     bitmap.Width, bitmap.Height),
                                     ImageLockMode.ReadOnly,
                                     PixelFormat.Format24bppRgb);
            var sum = UnsafeGetSum( bitmapData.Scan0, bitmapData.Height, bitmapData.Width, bitmapData.Stride - (bitmapData.Width * 3));
            bitmap.UnlockBits(bitmapData);
            return sum;
            } 
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="scan0">The address of the first pixel data in the bitmap in 24 bits RGB format.</param>
        /// <param name="Y">The pixel height of the bitmap.</param>
        /// <param name="X">The pixel width of the bitmap.</param>
        /// <param name="strideOffset">The stride offset width of the bitmap.</param>
        /// <returns></returns>
        private static double UnsafeGetSum(IntPtr scan0, int Y, int X, int strideOffset)
        {
        double sum = 0;
            unsafe
            {
                byte* image = (byte*)scan0;
                for (int y = 0; y < Y - 1; y++)
                {
                    for (int x = 0; x < X - 2; x++)
                    {
                        sum += Math.Abs(image[0] - image[3]);//Blue
                        sum += Math.Abs(image[1] - image[4]);//Green
                        sum += Math.Abs(image[2] - image[5]);//Red
                        image += 3;
                    }
                    image += strideOffset;
                }
            }
            return sum/(Y * X * 6);
        }
    }
}
