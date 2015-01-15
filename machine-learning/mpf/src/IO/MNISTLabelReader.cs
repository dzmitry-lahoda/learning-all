using System;
using System.Diagnostics.Contracts;
using System.IO;

namespace IO
{
    /// <summary>
    /// Reads digits and their labels from <see href="http://yann.lecun.com/exdb/mnist/">MNIST</see> dataset.
    /// </summary>
    public class MNISTLabelReader
    {

        private FileStream _dataFileStream;

        public int NumberOfLabels { get; private set; }

        public MNISTLabelReader(FileStream dataFileStream)
        {
            Contract.Requires(dataFileStream!=null);
            this._dataFileStream = dataFileStream;
            Tuple<int,int> magicNumberAndItemsCount = MNISTReaderHelper.ReadMagicNumberAndNumberOfItems(_dataFileStream);
            int magic = magicNumberAndItemsCount.Item1;
            int count = magicNumberAndItemsCount.Item2;
            //Contract.Requires(magic == 2049);
            //Contract.Requires(count > 0);
            NumberOfLabels = count;
        }

        public int ReadLabel(long itemNumber)
        {
            Contract.Requires(itemNumber>0);
            Contract.Requires(itemNumber <= NumberOfLabels);
            _dataFileStream.Position = MNISTReaderHelper.NUMBER_OF_UTIL_BYTES_IN_LABELS + itemNumber-1;
            return _dataFileStream.ReadByte();
        }
    }
}
