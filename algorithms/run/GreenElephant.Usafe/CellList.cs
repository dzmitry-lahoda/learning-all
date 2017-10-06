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
        /// <summary>
        /// Copies payload of <paramref name="cell"/> into newly allocated cell header, <paramref name="cells"/> is set <see cref="charCell.next"/>.
        /// </summary>
        /// <param name="cell"></param>
        /// <param name="cells"></param>
        /// <param name="allocator"></param>
        /// <returns></returns>
        public charCellresult push(charCell cell, charCell* cells, IcharCellAllocator allocator)
        {
            var mem = allocator.Apply(1);
            (*mem).element = cell.element;
            (*mem).next = cells;
            return new charCellresult { __value = mem };
        }

        public charCellresult pop_(charCell* charCell)
        {
            if (charCell == this.empty)
            {
                return new charCellresult { error = CellError.cannot_pop_empty_list };
            }
            return new charCellresult { __value = this._pop_(charCell) };
        }

        public charCell* _pop_(charCell* charCell)
        {
            var next = (*charCell).next;
            (*charCell).next = null;
            return next;
        }

        public CellError push_(charCell* cell, charCell* cells)
        {
            if ((*cell).next != this.empty)
            {
                return CellError.cannot_push_onto_filled_list;
            };
            _push_(cell, cells);
            return CellError.ok;
        }

        public void _push_(charCell* cell, charCell* cells)
        {
            (*cell).next = cells;
        }

        public charCellCell* prefixes(charCell* original, IcharCellCellAllocator alloc, IcharNew allocator)
        {
            var head = new charCellCell { element = null };
            var previous = head;
            while (original != this.empty)
            {
                var next = new charCellCell { element = original };
                previous.next = &next;
                previous = next;
                original = (*original).next;
            }

            return &head;

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

        public charCell* _concat_(charCell* a, charCell* b)
        {
            if (a == this.empty) return b;
            if (b == this.empty) return a;
            //            this.last)
            return null;
        }
    }
}
