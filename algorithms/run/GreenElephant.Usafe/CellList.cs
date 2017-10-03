using System;
using System.Collections.Generic;
using System.Text;

namespace GreenElephant.Usafe.bit64
{
    public unsafe partial class intCellExtensions
    {

    }

    public unsafe partial class charCellExtensions
    {
        public charCellCell* prefixes(charCell* abc, IcharCellCellAllocator alloc, IAllocator allocator)
        {
            charCellCell* head = null;

            while (abc != this.empty)
            {

            }

            return head;

            //var total = this.length(abc);
            //var a = alloc.Apply(total);
            //ulong counter = 1;
            //while (abc != this.empty)
            //{
            //    for (ulong i = counter; i < total-1; i++)
            //    {
            //        var last = this.last(a[i]);
            //        (*last).next = abc;
            //        abc = (*abc).next;
            //    }
            //    counter++;
            //}

            //return a;
        }

    }
}
