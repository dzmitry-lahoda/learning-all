using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace TimeLapseExp.Cameras
{
    public sealed class MemoryCardInfo
    {
        private long _size;
        private long _free;

        public MemoryCardInfo()
        {
            
        }

        public MemoryCardInfo(long size, long free)
        {
            Contract.Requires((free <= size) && (size > 0) && (free >= 0));
            _size = size;
            _free = free;
        }

        /// <summary>
        /// Gets size of flash card memory in bytes.
        /// </summary>
        public long Size
        {
            get
            {
                return _size;
            }
            set
            {
                _size = value;
            }
        }

        /// <summary>
        /// Gets number of free bytes on flash card memory.
        /// </summary>
        public long Free
        {
            get
            {
                return _free;
            }

            set
            {
                _free = value;
            }
        }


    }
}
