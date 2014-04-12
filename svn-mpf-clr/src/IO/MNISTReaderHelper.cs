using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace IO
{
    internal static class MNISTReaderHelper
    {
        public const long MAGIC_NUMBER_BYTES = 4;
        public const long NUMBER_OF_ITEMS_BYTES = 4;

        public const long NUMBER_OF_UTIL_BYTES_IN_LABELS = MAGIC_NUMBER_BYTES + NUMBER_OF_ITEMS_BYTES;

        public static Tuple<int, int> ReadMagicNumberAndNumberOfItems(Stream file)
        {
            var buffer = new byte[MAGIC_NUMBER_BYTES];
            file.Read(buffer, 0, 4);
            Array.Reverse(buffer);
            int magicNumberValue = BitConverter.ToInt32(buffer, 0);
            file.Read(buffer, 0, 4);
            Array.Reverse(buffer);
            int numberOfItems = BitConverter.ToInt32(buffer, 0);
            return new Tuple<int, int>(magicNumberValue, numberOfItems);
        }
    }
}
