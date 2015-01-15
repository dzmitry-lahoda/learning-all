using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using dnAnalytics.LinearAlgebra;
using MathWorks.MATLAB.NET.Arrays;
using MatlabHTMImage;

namespace HTMImageLayer
{
    public static class ImageLayer
    {
        private static readonly HTMImage HtmImage = new HTMImage();

        public static double[,] LoadImageMatrix(string fileName)
        {
#if MATLAB
            var matlabFileName = new MWCharArray(fileName);
            var matlabMatrix = (MWNumericArray)HtmImage.LoadImageMatrix(matlabFileName);
            var imageMatrix = (double[,])matlabMatrix.ToArray(MWArrayComponent.Real);
#endif
            return imageMatrix;

        }

        public static List<Matrix> FrameImageMatrix(double[,] imageMatrix, int frameSize)
        {
            #if MATLAB
            var matlabImageMatrix = new MWNumericArray(imageMatrix);
            var matlabFrameSize = new MWNumericArray(frameSize);
            var framesCells = (MWCellArray)HtmImage.FrameImageMatrix(matlabImageMatrix, matlabFrameSize);
            var frames = new List<Matrix>();
            for (var i = 1; i <= framesCells.NumberOfElements; i++)
            {
                var matlabFrame = (MWNumericArray) framesCells[i];
                var frame = (double[,])matlabFrame.ToArray(MWArrayComponent.Real);
                frames.Add(new DenseMatrix(frame));
            }
            #endif
            return frames;
        }


    }
}

