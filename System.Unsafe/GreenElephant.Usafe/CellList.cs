using System;
using System.Collections.Generic;
using System.Text;
using static System.Collections.Unsafe.bit64.stdlib;


namespace System.Collections.Unsafe.bit64
{

    public static unsafe partial class longCellExtensions
    {

        public static longCellPtr ctor(longCell* cell)
        {
            return new longCellPtr(cell);
        }

        public struct longCellPtr : IDisposable
        {
            public longCell* cell;

            public longCellPtr(longCell* cell)
            {
                this.cell = cell;
            }

            public void Dispose()
            {
                fixed (longCell** c = &this.cell)
                {
                    free(c);
                }
            }
        }

        public static long pop(longCell** headRef)
        {
            var head = (*headRef);
            var result = head->element;
            (*headRef) = head->next;
            stdlib.free(head);
            return result;
        }

        public static void sortedInsertAsc(longCell** headRef, longCell* newNode)
        {
            if (*headRef == null)
                push(headRef, newNode);
            else
                for (var i = (*headRef); i != NULL; i = i->next)
                {
                    var next = i->next;
                    if (i->element < newNode->element)
                    {
                        i->next = newNode;
                        newNode->next = next;
                        break;
                    }
                    if (next == NULL)
                    {
                        i->next = newNode;
                        break;
                    }
                }
        }

        private static void push(longCell** headRef, longCell* newNode)
        {
            throw new NotImplementedException();
        }

        public static void insert(longCell** headRef, ulong index, long value)
        {
            if (*headRef == NULL)
                push(headRef, value);
            else
            {
                var current = (*headRef);
                while (index > 1)
                {
                    current = current->next;
                    index--;
                }
                current->next = newCell(value, current == NULL ? NULL : current->next);
            }
        }

        public static long index(longCell* head, ulong i)
        {
            ulong counter = 0;
            while (head != null)
            {
                if (counter == i) break;
                counter++;
                head = (*head).next;
            }

            return head->element;
        }
    }

    public static unsafe partial class charCellExtensions
    {
        public static charCellresult pop_(charCell* charCell)
        {
            if (charCell == NULL)
                return new charCellresult { error = CellError.cannot_pop_empty_list };
            return new charCellresult { __value = _pop_(charCell) };
        }

        public static charCell* _pop_(charCell* charCell)
        {
            var next = (*charCell).next;
            (*charCell).next = null;
            return next;
        }



        public static charCellCell* prefixes(charCell* original)
        {
            var head = new charCellCell { element = null };
            var previous = head;
            while (original != NULL)
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

        public static charCell* _concat_(charCell* a, charCell* b)
        {
            if (a == NULL) return b;
            if (b == NULL) return a;
            //            this.last)
            return null;
        }
    }
}
