using System;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.IO;
using MathNet.Numerics.LinearAlgebra.Double;
using MathNet.Numerics.LinearAlgebra.Generic;


namespace IO
{
    /// <summary>
    /// Reads digits and their labels from <see href="http://yann.lecun.com/exdb/mnist/">MNIST</see> dataset.
    /// </summary>
    public class MNISTImageReader
    {

        private const int IMAGE_DESCRIPTION_SIZE = 8;
        private const int IMAGES_SIZE_HEIGHT = 28;
        private const int IMAGES_SIZE_WIDTH = 28;
        private const int IMAGES_SIZE = IMAGES_SIZE_HEIGHT * IMAGES_SIZE_WIDTH;

        private FileStream _dataFileStream;

        public int NumberOfImages { get; private set; }

        public MNISTImageReader(FileStream dataFileStream)
        {
            Contract.Requires(dataFileStream != null);
            this._dataFileStream = dataFileStream;
            Tuple<int, int> magicNumberAndItemsCount = MNISTReaderHelper.ReadMagicNumberAndNumberOfItems(dataFileStream);
            int magic = magicNumberAndItemsCount.Item1;
            int count = magicNumberAndItemsCount.Item2;
            Debug.Assert(magic == 2051);
            Debug.Assert(count >0 );
            //Contract.Requires(magic == 2051);
            //Contract.Requires(count > 0);
            NumberOfImages = count;
        }

        public Matrix<double> ReadImage(int itemNumber)
        {
            Contract.Requires(itemNumber > 0);
            Contract.Requires(itemNumber <= NumberOfImages);
            _dataFileStream.Position = MNISTReaderHelper.NUMBER_OF_UTIL_BYTES_IN_LABELS + IMAGE_DESCRIPTION_SIZE + (itemNumber-1) * IMAGES_SIZE;
            var buffer = new byte[IMAGES_SIZE];
            _dataFileStream.Read(buffer, 0, IMAGES_SIZE);
            var image = new DenseMatrix(IMAGES_SIZE_HEIGHT, IMAGES_SIZE_WIDTH);
            for (int i = 0; i < IMAGES_SIZE_HEIGHT; i++)
            {
                for (int j = 0; j < IMAGES_SIZE_WIDTH; j++)
                {
                    image[j, i] = buffer[IMAGES_SIZE_HEIGHT * i + j];
                }
            }
            return image;
        }


    }
}
