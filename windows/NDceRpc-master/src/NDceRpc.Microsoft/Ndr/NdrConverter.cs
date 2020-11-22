using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NDceRpc.Ndr
{
    public static class NdrConverter
    {
        public static uint NDR_LOCAL_DATA_REPRESENTATION = 0X00000010u;



        public static uint NDR_LOCAL_ENDIAN = NDR_LITTLE_ENDIAN;
        public const uint NDR_LITTLE_ENDIAN = 0X00000010u;
        public const uint NDR_BIG_ENDIAN = 0X00000000u;

        static NdrConverter()
        {
            var platform = Environment.OSVersion.Platform;
            if (platform == PlatformID.MacOSX || platform == PlatformID.Unix)
            {
                NDR_LOCAL_ENDIAN = NDR_BIG_ENDIAN;
                NDR_LOCAL_DATA_REPRESENTATION = 0X00000000u;
            }
        }

        public static byte[] NdrFcLong(int s)
        {
            return new byte[4]
                {
                    (byte
                    )
                    (s & 0xff),
                    (byte
                    )
                    ((s & 0x0000ff00) >> 8),
                    (byte
                    )
                    ((s & 0x00ff0000) >> 16),
                    (byte
                    )
                    (s >> 24)
                };
        }
    }
}
