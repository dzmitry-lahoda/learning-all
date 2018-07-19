  
  
  
  
  
  
  
  
  
using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;

namespace System.Collections.Unsafe
{

    public enum CellError : byte
    {
        ok,
        cannot_push_onto_filled_list,
        cannot_pop_empty_list
    }


namespace bit16
{

using static System.Collections.Unsafe.bit16.stdlib;

  public unsafe  struct intCell
  {
    public int element;
    public intCell* next;
  }

  public unsafe  struct intCellCell
  {
    public intCell* element;
    public intCellCell* next;
  }

  public unsafe  struct longCell
  {
    public long element;
    public longCell* next;
  }

  public unsafe  struct longCellCell
  {
    public longCell* element;
    public longCellCell* next;
  }

  public unsafe  struct floatCell
  {
    public float element;
    public floatCell* next;
  }

  public unsafe  struct floatCellCell
  {
    public floatCell* element;
    public floatCellCell* next;
  }

  public unsafe  struct doubleCell
  {
    public double element;
    public doubleCell* next;
  }

  public unsafe  struct doubleCellCell
  {
    public doubleCell* element;
    public doubleCellCell* next;
  }

  public unsafe  struct decimalCell
  {
    public decimal element;
    public decimalCell* next;
  }

  public unsafe  struct decimalCellCell
  {
    public decimalCell* element;
    public decimalCellCell* next;
  }

  public unsafe  struct BigIntegerCell
  {
    public BigInteger element;
    public BigIntegerCell* next;
  }

  public unsafe  struct BigIntegerCellCell
  {
    public BigIntegerCell* element;
    public BigIntegerCellCell* next;
  }

  public unsafe  struct ComplexCell
  {
    public Complex element;
    public ComplexCell* next;
  }

  public unsafe  struct ComplexCellCell
  {
    public ComplexCell* element;
    public ComplexCellCell* next;
  }

  public unsafe  struct charCell
  {
    public char element;
    public charCell* next;
  }

  public unsafe  struct charCellCell
  {
    public charCell* element;
    public charCellCell* next;
  }






    public unsafe struct intCellresult
    {
        public intCell* __value;
        public CellError error;

        public intCell* unwrap()
        {
            this.error.unwrap();
            return __value;
        }
    }

public static unsafe partial class intCellExtensions
  {
    
	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static unsafe  intCell* intCellAlloc(ushort count)
		  => (intCell*)malloc((ushort)sizeof(intCell) * count);
           
         public static unsafe  intCellCell* intCellCellAlloc(ushort count)
		  => (intCellCell*)malloc((ushort)sizeof(intCellCell) * count);


    public static ushort length(intCell* cell)
    {
            ushort counter = 0;
            while (cell != null)
            {
                counter++;
                cell = (*cell).next;
            };
            return counter;
        }

        public static void push(ref intCell* head, int newData)
            => head = newCell(newData, head);

		public static void push(intCell** head, int newData) 
            => (*head) = newCell(newData, *head);

        public static intCell* newCell(int newData)
        {
            var cell = intCellAlloc(1);
            cell->element = newData;
            cell->next = NULL;
            return cell;
        }

        public static void cellInit(ref intCell cell, int data, ref intCell next)
        {
            cell.element = data;
            fixed (intCell* ptr = &next)
            {
                cell.next = ptr;
            }
        }

        public static void cellInit(intCell* cell, int data, intCell* next)
        {
            cell->element = data;
            cell->next = next;
        }

        public static void cellInit(ref intCell cell, int data)
        {
            cell.element = data;
            cell.next = NULL;
        }

        public static void cellInit(intCell* cell, int data)
        {
            cell->element = data;
            cell->next = NULL;
        }

        public static intCell* newCell(int element, intCell* next)
        {
            var cell = intCellAlloc(1);
            cellInit(cell, element, next);
            return cell;        }

        public static bool equals(intCell* a, intCell* b)
        {
            if (a == b) return true;
            while (true)
                if (a == NULL && b == NULL)
                    return true;
                else if (a != NULL && b == NULL)
                    return false;
                else if (a == NULL && b != NULL)
                    return false;
                else if ((*a).element != (*b).element)
                    return false;
                else
                  { a = (*a).next; b = (*b).next; };
        }

		public static void free(intCell** headRef)
        {
            intCell* head = *headRef;
            while (head != null)
            {
                var toFree = head;
                head = head->next;
            }
            (*headRef) = NULL;
        }



		        public static ushort count(intCell* head, int searchFor)
        {
                ushort counter = 0;
                while (head != null)
                {
                    if (head->element == searchFor) counter++;
                    head = (*head).next;
                }
                return counter;
        }

    public static ushort length(intCellCell* cell)
    {
            ushort counter = 0;
            while (cell != null)
            {
                counter++;
                cell = (*cell).next;
            }
            return counter;
        }

        public static intCell* NULL
        {
            get
            {
                return null;
            }
        }

		public static int head(intCell* forwardLinkedList)
        {
            return (*forwardLinkedList).element;
        }

		
        public static intArray toArray(intCell* abc)
        {
            var total = length(abc);
            var arr = (int*)malloc((ushort)sizeof(int) * total);
            var counter = 0;
            while (abc != null)
            {
                arr[counter] = (*abc).element;
                abc = (*abc).next;
                counter++;
            }

            return new intArray { lenght = total, index = arr };
        }

        public static intCell* tail(intCell* forwardLinkedList)
        {
            return (*forwardLinkedList).next;
        }

        public static intCell* last(intCell* forwardLinkedList)
        {
            while (true)
            {
                if ((*forwardLinkedList).next == null) return forwardLinkedList;
                forwardLinkedList = (*forwardLinkedList).next;
            }
        }

        /// <summary>
        /// Copies payload of <paramref name="cell"/> into newly allocated cell header, <paramref name="cells"/> is set <see cref="charCell.next"/>.
        /// </summary>
        /// <param name="cell"></param>
        /// <param name="cells"></param>
        /// <param name="allocator"></param>
        /// <returns></returns>
        public static intCellresult push(intCell cell, intCell* cells)
        {
            var mem = intCellAlloc(1);
            (*mem).element = cell.element;
            (*mem).next = cells;
            return new intCellresult { __value = mem };
        }


        public static CellError push_(intCell* cell, intCell* cells)
        {
            if ((*cell).next != NULL)
                return CellError.cannot_push_onto_filled_list;
            _push_(cell, cells);
            return CellError.ok;
        }

        public static void _push_(intCell* cell, intCell* cells) => (*cell).next = cells;

        public static void toCells(int[] array, intCell* memory)
        {
            intCell* previous = null;

            foreach (var item in array)
            {
                (*memory) = new intCell { element = item };
                if (previous != null)
                {
                    (*previous).next = memory;
                }

                previous = memory;

                memory++;
            }
        }
    }


    public unsafe struct longCellresult
    {
        public longCell* __value;
        public CellError error;

        public longCell* unwrap()
        {
            this.error.unwrap();
            return __value;
        }
    }

public static unsafe partial class longCellExtensions
  {
    
	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static unsafe  longCell* longCellAlloc(ushort count)
		  => (longCell*)malloc((ushort)sizeof(longCell) * count);
           
         public static unsafe  longCellCell* longCellCellAlloc(ushort count)
		  => (longCellCell*)malloc((ushort)sizeof(longCellCell) * count);


    public static ushort length(longCell* cell)
    {
            ushort counter = 0;
            while (cell != null)
            {
                counter++;
                cell = (*cell).next;
            };
            return counter;
        }

        public static void push(ref longCell* head, long newData)
            => head = newCell(newData, head);

		public static void push(longCell** head, long newData) 
            => (*head) = newCell(newData, *head);

        public static longCell* newCell(long newData)
        {
            var cell = longCellAlloc(1);
            cell->element = newData;
            cell->next = NULL;
            return cell;
        }

        public static void cellInit(ref longCell cell, long data, ref longCell next)
        {
            cell.element = data;
            fixed (longCell* ptr = &next)
            {
                cell.next = ptr;
            }
        }

        public static void cellInit(longCell* cell, long data, longCell* next)
        {
            cell->element = data;
            cell->next = next;
        }

        public static void cellInit(ref longCell cell, long data)
        {
            cell.element = data;
            cell.next = NULL;
        }

        public static void cellInit(longCell* cell, long data)
        {
            cell->element = data;
            cell->next = NULL;
        }

        public static longCell* newCell(long element, longCell* next)
        {
            var cell = longCellAlloc(1);
            cellInit(cell, element, next);
            return cell;        }

        public static bool equals(longCell* a, longCell* b)
        {
            if (a == b) return true;
            while (true)
                if (a == NULL && b == NULL)
                    return true;
                else if (a != NULL && b == NULL)
                    return false;
                else if (a == NULL && b != NULL)
                    return false;
                else if ((*a).element != (*b).element)
                    return false;
                else
                  { a = (*a).next; b = (*b).next; };
        }

		public static void free(longCell** headRef)
        {
            longCell* head = *headRef;
            while (head != null)
            {
                var toFree = head;
                head = head->next;
            }
            (*headRef) = NULL;
        }



		        public static ushort count(longCell* head, long searchFor)
        {
                ushort counter = 0;
                while (head != null)
                {
                    if (head->element == searchFor) counter++;
                    head = (*head).next;
                }
                return counter;
        }

    public static ushort length(longCellCell* cell)
    {
            ushort counter = 0;
            while (cell != null)
            {
                counter++;
                cell = (*cell).next;
            }
            return counter;
        }

        public static longCell* NULL
        {
            get
            {
                return null;
            }
        }

		public static long head(longCell* forwardLinkedList)
        {
            return (*forwardLinkedList).element;
        }

		
        public static longArray toArray(longCell* abc)
        {
            var total = length(abc);
            var arr = (long*)malloc((ushort)sizeof(long) * total);
            var counter = 0;
            while (abc != null)
            {
                arr[counter] = (*abc).element;
                abc = (*abc).next;
                counter++;
            }

            return new longArray { lenght = total, index = arr };
        }

        public static longCell* tail(longCell* forwardLinkedList)
        {
            return (*forwardLinkedList).next;
        }

        public static longCell* last(longCell* forwardLinkedList)
        {
            while (true)
            {
                if ((*forwardLinkedList).next == null) return forwardLinkedList;
                forwardLinkedList = (*forwardLinkedList).next;
            }
        }

        /// <summary>
        /// Copies payload of <paramref name="cell"/> into newly allocated cell header, <paramref name="cells"/> is set <see cref="charCell.next"/>.
        /// </summary>
        /// <param name="cell"></param>
        /// <param name="cells"></param>
        /// <param name="allocator"></param>
        /// <returns></returns>
        public static longCellresult push(longCell cell, longCell* cells)
        {
            var mem = longCellAlloc(1);
            (*mem).element = cell.element;
            (*mem).next = cells;
            return new longCellresult { __value = mem };
        }


        public static CellError push_(longCell* cell, longCell* cells)
        {
            if ((*cell).next != NULL)
                return CellError.cannot_push_onto_filled_list;
            _push_(cell, cells);
            return CellError.ok;
        }

        public static void _push_(longCell* cell, longCell* cells) => (*cell).next = cells;

        public static void toCells(long[] array, longCell* memory)
        {
            longCell* previous = null;

            foreach (var item in array)
            {
                (*memory) = new longCell { element = item };
                if (previous != null)
                {
                    (*previous).next = memory;
                }

                previous = memory;

                memory++;
            }
        }
    }


    public unsafe struct floatCellresult
    {
        public floatCell* __value;
        public CellError error;

        public floatCell* unwrap()
        {
            this.error.unwrap();
            return __value;
        }
    }

public static unsafe partial class floatCellExtensions
  {
    
	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static unsafe  floatCell* floatCellAlloc(ushort count)
		  => (floatCell*)malloc((ushort)sizeof(floatCell) * count);
           
         public static unsafe  floatCellCell* floatCellCellAlloc(ushort count)
		  => (floatCellCell*)malloc((ushort)sizeof(floatCellCell) * count);


    public static ushort length(floatCell* cell)
    {
            ushort counter = 0;
            while (cell != null)
            {
                counter++;
                cell = (*cell).next;
            };
            return counter;
        }

        public static void push(ref floatCell* head, float newData)
            => head = newCell(newData, head);

		public static void push(floatCell** head, float newData) 
            => (*head) = newCell(newData, *head);

        public static floatCell* newCell(float newData)
        {
            var cell = floatCellAlloc(1);
            cell->element = newData;
            cell->next = NULL;
            return cell;
        }

        public static void cellInit(ref floatCell cell, float data, ref floatCell next)
        {
            cell.element = data;
            fixed (floatCell* ptr = &next)
            {
                cell.next = ptr;
            }
        }

        public static void cellInit(floatCell* cell, float data, floatCell* next)
        {
            cell->element = data;
            cell->next = next;
        }

        public static void cellInit(ref floatCell cell, float data)
        {
            cell.element = data;
            cell.next = NULL;
        }

        public static void cellInit(floatCell* cell, float data)
        {
            cell->element = data;
            cell->next = NULL;
        }

        public static floatCell* newCell(float element, floatCell* next)
        {
            var cell = floatCellAlloc(1);
            cellInit(cell, element, next);
            return cell;        }

        public static bool equals(floatCell* a, floatCell* b)
        {
            if (a == b) return true;
            while (true)
                if (a == NULL && b == NULL)
                    return true;
                else if (a != NULL && b == NULL)
                    return false;
                else if (a == NULL && b != NULL)
                    return false;
                else if ((*a).element != (*b).element)
                    return false;
                else
                  { a = (*a).next; b = (*b).next; };
        }

		public static void free(floatCell** headRef)
        {
            floatCell* head = *headRef;
            while (head != null)
            {
                var toFree = head;
                head = head->next;
            }
            (*headRef) = NULL;
        }



		        public static ushort count(floatCell* head, float searchFor)
        {
                ushort counter = 0;
                while (head != null)
                {
                    if (head->element == searchFor) counter++;
                    head = (*head).next;
                }
                return counter;
        }

    public static ushort length(floatCellCell* cell)
    {
            ushort counter = 0;
            while (cell != null)
            {
                counter++;
                cell = (*cell).next;
            }
            return counter;
        }

        public static floatCell* NULL
        {
            get
            {
                return null;
            }
        }

		public static float head(floatCell* forwardLinkedList)
        {
            return (*forwardLinkedList).element;
        }

		
        public static floatArray toArray(floatCell* abc)
        {
            var total = length(abc);
            var arr = (float*)malloc((ushort)sizeof(float) * total);
            var counter = 0;
            while (abc != null)
            {
                arr[counter] = (*abc).element;
                abc = (*abc).next;
                counter++;
            }

            return new floatArray { lenght = total, index = arr };
        }

        public static floatCell* tail(floatCell* forwardLinkedList)
        {
            return (*forwardLinkedList).next;
        }

        public static floatCell* last(floatCell* forwardLinkedList)
        {
            while (true)
            {
                if ((*forwardLinkedList).next == null) return forwardLinkedList;
                forwardLinkedList = (*forwardLinkedList).next;
            }
        }

        /// <summary>
        /// Copies payload of <paramref name="cell"/> into newly allocated cell header, <paramref name="cells"/> is set <see cref="charCell.next"/>.
        /// </summary>
        /// <param name="cell"></param>
        /// <param name="cells"></param>
        /// <param name="allocator"></param>
        /// <returns></returns>
        public static floatCellresult push(floatCell cell, floatCell* cells)
        {
            var mem = floatCellAlloc(1);
            (*mem).element = cell.element;
            (*mem).next = cells;
            return new floatCellresult { __value = mem };
        }


        public static CellError push_(floatCell* cell, floatCell* cells)
        {
            if ((*cell).next != NULL)
                return CellError.cannot_push_onto_filled_list;
            _push_(cell, cells);
            return CellError.ok;
        }

        public static void _push_(floatCell* cell, floatCell* cells) => (*cell).next = cells;

        public static void toCells(float[] array, floatCell* memory)
        {
            floatCell* previous = null;

            foreach (var item in array)
            {
                (*memory) = new floatCell { element = item };
                if (previous != null)
                {
                    (*previous).next = memory;
                }

                previous = memory;

                memory++;
            }
        }
    }


    public unsafe struct doubleCellresult
    {
        public doubleCell* __value;
        public CellError error;

        public doubleCell* unwrap()
        {
            this.error.unwrap();
            return __value;
        }
    }

public static unsafe partial class doubleCellExtensions
  {
    
	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static unsafe  doubleCell* doubleCellAlloc(ushort count)
		  => (doubleCell*)malloc((ushort)sizeof(doubleCell) * count);
           
         public static unsafe  doubleCellCell* doubleCellCellAlloc(ushort count)
		  => (doubleCellCell*)malloc((ushort)sizeof(doubleCellCell) * count);


    public static ushort length(doubleCell* cell)
    {
            ushort counter = 0;
            while (cell != null)
            {
                counter++;
                cell = (*cell).next;
            };
            return counter;
        }

        public static void push(ref doubleCell* head, double newData)
            => head = newCell(newData, head);

		public static void push(doubleCell** head, double newData) 
            => (*head) = newCell(newData, *head);

        public static doubleCell* newCell(double newData)
        {
            var cell = doubleCellAlloc(1);
            cell->element = newData;
            cell->next = NULL;
            return cell;
        }

        public static void cellInit(ref doubleCell cell, double data, ref doubleCell next)
        {
            cell.element = data;
            fixed (doubleCell* ptr = &next)
            {
                cell.next = ptr;
            }
        }

        public static void cellInit(doubleCell* cell, double data, doubleCell* next)
        {
            cell->element = data;
            cell->next = next;
        }

        public static void cellInit(ref doubleCell cell, double data)
        {
            cell.element = data;
            cell.next = NULL;
        }

        public static void cellInit(doubleCell* cell, double data)
        {
            cell->element = data;
            cell->next = NULL;
        }

        public static doubleCell* newCell(double element, doubleCell* next)
        {
            var cell = doubleCellAlloc(1);
            cellInit(cell, element, next);
            return cell;        }

        public static bool equals(doubleCell* a, doubleCell* b)
        {
            if (a == b) return true;
            while (true)
                if (a == NULL && b == NULL)
                    return true;
                else if (a != NULL && b == NULL)
                    return false;
                else if (a == NULL && b != NULL)
                    return false;
                else if ((*a).element != (*b).element)
                    return false;
                else
                  { a = (*a).next; b = (*b).next; };
        }

		public static void free(doubleCell** headRef)
        {
            doubleCell* head = *headRef;
            while (head != null)
            {
                var toFree = head;
                head = head->next;
            }
            (*headRef) = NULL;
        }



		        public static ushort count(doubleCell* head, double searchFor)
        {
                ushort counter = 0;
                while (head != null)
                {
                    if (head->element == searchFor) counter++;
                    head = (*head).next;
                }
                return counter;
        }

    public static ushort length(doubleCellCell* cell)
    {
            ushort counter = 0;
            while (cell != null)
            {
                counter++;
                cell = (*cell).next;
            }
            return counter;
        }

        public static doubleCell* NULL
        {
            get
            {
                return null;
            }
        }

		public static double head(doubleCell* forwardLinkedList)
        {
            return (*forwardLinkedList).element;
        }

		
        public static doubleArray toArray(doubleCell* abc)
        {
            var total = length(abc);
            var arr = (double*)malloc((ushort)sizeof(double) * total);
            var counter = 0;
            while (abc != null)
            {
                arr[counter] = (*abc).element;
                abc = (*abc).next;
                counter++;
            }

            return new doubleArray { lenght = total, index = arr };
        }

        public static doubleCell* tail(doubleCell* forwardLinkedList)
        {
            return (*forwardLinkedList).next;
        }

        public static doubleCell* last(doubleCell* forwardLinkedList)
        {
            while (true)
            {
                if ((*forwardLinkedList).next == null) return forwardLinkedList;
                forwardLinkedList = (*forwardLinkedList).next;
            }
        }

        /// <summary>
        /// Copies payload of <paramref name="cell"/> into newly allocated cell header, <paramref name="cells"/> is set <see cref="charCell.next"/>.
        /// </summary>
        /// <param name="cell"></param>
        /// <param name="cells"></param>
        /// <param name="allocator"></param>
        /// <returns></returns>
        public static doubleCellresult push(doubleCell cell, doubleCell* cells)
        {
            var mem = doubleCellAlloc(1);
            (*mem).element = cell.element;
            (*mem).next = cells;
            return new doubleCellresult { __value = mem };
        }


        public static CellError push_(doubleCell* cell, doubleCell* cells)
        {
            if ((*cell).next != NULL)
                return CellError.cannot_push_onto_filled_list;
            _push_(cell, cells);
            return CellError.ok;
        }

        public static void _push_(doubleCell* cell, doubleCell* cells) => (*cell).next = cells;

        public static void toCells(double[] array, doubleCell* memory)
        {
            doubleCell* previous = null;

            foreach (var item in array)
            {
                (*memory) = new doubleCell { element = item };
                if (previous != null)
                {
                    (*previous).next = memory;
                }

                previous = memory;

                memory++;
            }
        }
    }


    public unsafe struct decimalCellresult
    {
        public decimalCell* __value;
        public CellError error;

        public decimalCell* unwrap()
        {
            this.error.unwrap();
            return __value;
        }
    }

public static unsafe partial class decimalCellExtensions
  {
    
	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static unsafe  decimalCell* decimalCellAlloc(ushort count)
		  => (decimalCell*)malloc((ushort)sizeof(decimalCell) * count);
           
         public static unsafe  decimalCellCell* decimalCellCellAlloc(ushort count)
		  => (decimalCellCell*)malloc((ushort)sizeof(decimalCellCell) * count);


    public static ushort length(decimalCell* cell)
    {
            ushort counter = 0;
            while (cell != null)
            {
                counter++;
                cell = (*cell).next;
            };
            return counter;
        }

        public static void push(ref decimalCell* head, decimal newData)
            => head = newCell(newData, head);

		public static void push(decimalCell** head, decimal newData) 
            => (*head) = newCell(newData, *head);

        public static decimalCell* newCell(decimal newData)
        {
            var cell = decimalCellAlloc(1);
            cell->element = newData;
            cell->next = NULL;
            return cell;
        }

        public static void cellInit(ref decimalCell cell, decimal data, ref decimalCell next)
        {
            cell.element = data;
            fixed (decimalCell* ptr = &next)
            {
                cell.next = ptr;
            }
        }

        public static void cellInit(decimalCell* cell, decimal data, decimalCell* next)
        {
            cell->element = data;
            cell->next = next;
        }

        public static void cellInit(ref decimalCell cell, decimal data)
        {
            cell.element = data;
            cell.next = NULL;
        }

        public static void cellInit(decimalCell* cell, decimal data)
        {
            cell->element = data;
            cell->next = NULL;
        }

        public static decimalCell* newCell(decimal element, decimalCell* next)
        {
            var cell = decimalCellAlloc(1);
            cellInit(cell, element, next);
            return cell;        }

        public static bool equals(decimalCell* a, decimalCell* b)
        {
            if (a == b) return true;
            while (true)
                if (a == NULL && b == NULL)
                    return true;
                else if (a != NULL && b == NULL)
                    return false;
                else if (a == NULL && b != NULL)
                    return false;
                else if ((*a).element != (*b).element)
                    return false;
                else
                  { a = (*a).next; b = (*b).next; };
        }

		public static void free(decimalCell** headRef)
        {
            decimalCell* head = *headRef;
            while (head != null)
            {
                var toFree = head;
                head = head->next;
            }
            (*headRef) = NULL;
        }



		        public static ushort count(decimalCell* head, decimal searchFor)
        {
                ushort counter = 0;
                while (head != null)
                {
                    if (head->element == searchFor) counter++;
                    head = (*head).next;
                }
                return counter;
        }

    public static ushort length(decimalCellCell* cell)
    {
            ushort counter = 0;
            while (cell != null)
            {
                counter++;
                cell = (*cell).next;
            }
            return counter;
        }

        public static decimalCell* NULL
        {
            get
            {
                return null;
            }
        }

		public static decimal head(decimalCell* forwardLinkedList)
        {
            return (*forwardLinkedList).element;
        }

		
        public static decimalArray toArray(decimalCell* abc)
        {
            var total = length(abc);
            var arr = (decimal*)malloc((ushort)sizeof(decimal) * total);
            var counter = 0;
            while (abc != null)
            {
                arr[counter] = (*abc).element;
                abc = (*abc).next;
                counter++;
            }

            return new decimalArray { lenght = total, index = arr };
        }

        public static decimalCell* tail(decimalCell* forwardLinkedList)
        {
            return (*forwardLinkedList).next;
        }

        public static decimalCell* last(decimalCell* forwardLinkedList)
        {
            while (true)
            {
                if ((*forwardLinkedList).next == null) return forwardLinkedList;
                forwardLinkedList = (*forwardLinkedList).next;
            }
        }

        /// <summary>
        /// Copies payload of <paramref name="cell"/> into newly allocated cell header, <paramref name="cells"/> is set <see cref="charCell.next"/>.
        /// </summary>
        /// <param name="cell"></param>
        /// <param name="cells"></param>
        /// <param name="allocator"></param>
        /// <returns></returns>
        public static decimalCellresult push(decimalCell cell, decimalCell* cells)
        {
            var mem = decimalCellAlloc(1);
            (*mem).element = cell.element;
            (*mem).next = cells;
            return new decimalCellresult { __value = mem };
        }


        public static CellError push_(decimalCell* cell, decimalCell* cells)
        {
            if ((*cell).next != NULL)
                return CellError.cannot_push_onto_filled_list;
            _push_(cell, cells);
            return CellError.ok;
        }

        public static void _push_(decimalCell* cell, decimalCell* cells) => (*cell).next = cells;

        public static void toCells(decimal[] array, decimalCell* memory)
        {
            decimalCell* previous = null;

            foreach (var item in array)
            {
                (*memory) = new decimalCell { element = item };
                if (previous != null)
                {
                    (*previous).next = memory;
                }

                previous = memory;

                memory++;
            }
        }
    }


    public unsafe struct BigIntegerCellresult
    {
        public BigIntegerCell* __value;
        public CellError error;

        public BigIntegerCell* unwrap()
        {
            this.error.unwrap();
            return __value;
        }
    }

public static unsafe partial class BigIntegerCellExtensions
  {
    
	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static unsafe  BigIntegerCell* BigIntegerCellAlloc(ushort count)
		  => (BigIntegerCell*)malloc((ushort)sizeof(BigIntegerCell) * count);
           
         public static unsafe  BigIntegerCellCell* BigIntegerCellCellAlloc(ushort count)
		  => (BigIntegerCellCell*)malloc((ushort)sizeof(BigIntegerCellCell) * count);


    public static ushort length(BigIntegerCell* cell)
    {
            ushort counter = 0;
            while (cell != null)
            {
                counter++;
                cell = (*cell).next;
            };
            return counter;
        }

        public static void push(ref BigIntegerCell* head, BigInteger newData)
            => head = newCell(newData, head);

		public static void push(BigIntegerCell** head, BigInteger newData) 
            => (*head) = newCell(newData, *head);

        public static BigIntegerCell* newCell(BigInteger newData)
        {
            var cell = BigIntegerCellAlloc(1);
            cell->element = newData;
            cell->next = NULL;
            return cell;
        }

        public static void cellInit(ref BigIntegerCell cell, BigInteger data, ref BigIntegerCell next)
        {
            cell.element = data;
            fixed (BigIntegerCell* ptr = &next)
            {
                cell.next = ptr;
            }
        }

        public static void cellInit(BigIntegerCell* cell, BigInteger data, BigIntegerCell* next)
        {
            cell->element = data;
            cell->next = next;
        }

        public static void cellInit(ref BigIntegerCell cell, BigInteger data)
        {
            cell.element = data;
            cell.next = NULL;
        }

        public static void cellInit(BigIntegerCell* cell, BigInteger data)
        {
            cell->element = data;
            cell->next = NULL;
        }

        public static BigIntegerCell* newCell(BigInteger element, BigIntegerCell* next)
        {
            var cell = BigIntegerCellAlloc(1);
            cellInit(cell, element, next);
            return cell;        }

        public static bool equals(BigIntegerCell* a, BigIntegerCell* b)
        {
            if (a == b) return true;
            while (true)
                if (a == NULL && b == NULL)
                    return true;
                else if (a != NULL && b == NULL)
                    return false;
                else if (a == NULL && b != NULL)
                    return false;
                else if ((*a).element != (*b).element)
                    return false;
                else
                  { a = (*a).next; b = (*b).next; };
        }

		public static void free(BigIntegerCell** headRef)
        {
            BigIntegerCell* head = *headRef;
            while (head != null)
            {
                var toFree = head;
                head = head->next;
            }
            (*headRef) = NULL;
        }



		        public static ushort count(BigIntegerCell* head, BigInteger searchFor)
        {
                ushort counter = 0;
                while (head != null)
                {
                    if (head->element == searchFor) counter++;
                    head = (*head).next;
                }
                return counter;
        }

    public static ushort length(BigIntegerCellCell* cell)
    {
            ushort counter = 0;
            while (cell != null)
            {
                counter++;
                cell = (*cell).next;
            }
            return counter;
        }

        public static BigIntegerCell* NULL
        {
            get
            {
                return null;
            }
        }

		public static BigInteger head(BigIntegerCell* forwardLinkedList)
        {
            return (*forwardLinkedList).element;
        }

		
        public static BigIntegerArray toArray(BigIntegerCell* abc)
        {
            var total = length(abc);
            var arr = (BigInteger*)malloc((ushort)sizeof(BigInteger) * total);
            var counter = 0;
            while (abc != null)
            {
                arr[counter] = (*abc).element;
                abc = (*abc).next;
                counter++;
            }

            return new BigIntegerArray { lenght = total, index = arr };
        }

        public static BigIntegerCell* tail(BigIntegerCell* forwardLinkedList)
        {
            return (*forwardLinkedList).next;
        }

        public static BigIntegerCell* last(BigIntegerCell* forwardLinkedList)
        {
            while (true)
            {
                if ((*forwardLinkedList).next == null) return forwardLinkedList;
                forwardLinkedList = (*forwardLinkedList).next;
            }
        }

        /// <summary>
        /// Copies payload of <paramref name="cell"/> into newly allocated cell header, <paramref name="cells"/> is set <see cref="charCell.next"/>.
        /// </summary>
        /// <param name="cell"></param>
        /// <param name="cells"></param>
        /// <param name="allocator"></param>
        /// <returns></returns>
        public static BigIntegerCellresult push(BigIntegerCell cell, BigIntegerCell* cells)
        {
            var mem = BigIntegerCellAlloc(1);
            (*mem).element = cell.element;
            (*mem).next = cells;
            return new BigIntegerCellresult { __value = mem };
        }


        public static CellError push_(BigIntegerCell* cell, BigIntegerCell* cells)
        {
            if ((*cell).next != NULL)
                return CellError.cannot_push_onto_filled_list;
            _push_(cell, cells);
            return CellError.ok;
        }

        public static void _push_(BigIntegerCell* cell, BigIntegerCell* cells) => (*cell).next = cells;

        public static void toCells(BigInteger[] array, BigIntegerCell* memory)
        {
            BigIntegerCell* previous = null;

            foreach (var item in array)
            {
                (*memory) = new BigIntegerCell { element = item };
                if (previous != null)
                {
                    (*previous).next = memory;
                }

                previous = memory;

                memory++;
            }
        }
    }


    public unsafe struct ComplexCellresult
    {
        public ComplexCell* __value;
        public CellError error;

        public ComplexCell* unwrap()
        {
            this.error.unwrap();
            return __value;
        }
    }

public static unsafe partial class ComplexCellExtensions
  {
    
	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static unsafe  ComplexCell* ComplexCellAlloc(ushort count)
		  => (ComplexCell*)malloc((ushort)sizeof(ComplexCell) * count);
           
         public static unsafe  ComplexCellCell* ComplexCellCellAlloc(ushort count)
		  => (ComplexCellCell*)malloc((ushort)sizeof(ComplexCellCell) * count);


    public static ushort length(ComplexCell* cell)
    {
            ushort counter = 0;
            while (cell != null)
            {
                counter++;
                cell = (*cell).next;
            };
            return counter;
        }

        public static void push(ref ComplexCell* head, Complex newData)
            => head = newCell(newData, head);

		public static void push(ComplexCell** head, Complex newData) 
            => (*head) = newCell(newData, *head);

        public static ComplexCell* newCell(Complex newData)
        {
            var cell = ComplexCellAlloc(1);
            cell->element = newData;
            cell->next = NULL;
            return cell;
        }

        public static void cellInit(ref ComplexCell cell, Complex data, ref ComplexCell next)
        {
            cell.element = data;
            fixed (ComplexCell* ptr = &next)
            {
                cell.next = ptr;
            }
        }

        public static void cellInit(ComplexCell* cell, Complex data, ComplexCell* next)
        {
            cell->element = data;
            cell->next = next;
        }

        public static void cellInit(ref ComplexCell cell, Complex data)
        {
            cell.element = data;
            cell.next = NULL;
        }

        public static void cellInit(ComplexCell* cell, Complex data)
        {
            cell->element = data;
            cell->next = NULL;
        }

        public static ComplexCell* newCell(Complex element, ComplexCell* next)
        {
            var cell = ComplexCellAlloc(1);
            cellInit(cell, element, next);
            return cell;        }

        public static bool equals(ComplexCell* a, ComplexCell* b)
        {
            if (a == b) return true;
            while (true)
                if (a == NULL && b == NULL)
                    return true;
                else if (a != NULL && b == NULL)
                    return false;
                else if (a == NULL && b != NULL)
                    return false;
                else if ((*a).element != (*b).element)
                    return false;
                else
                  { a = (*a).next; b = (*b).next; };
        }

		public static void free(ComplexCell** headRef)
        {
            ComplexCell* head = *headRef;
            while (head != null)
            {
                var toFree = head;
                head = head->next;
            }
            (*headRef) = NULL;
        }



		        public static ushort count(ComplexCell* head, Complex searchFor)
        {
                ushort counter = 0;
                while (head != null)
                {
                    if (head->element == searchFor) counter++;
                    head = (*head).next;
                }
                return counter;
        }

    public static ushort length(ComplexCellCell* cell)
    {
            ushort counter = 0;
            while (cell != null)
            {
                counter++;
                cell = (*cell).next;
            }
            return counter;
        }

        public static ComplexCell* NULL
        {
            get
            {
                return null;
            }
        }

		public static Complex head(ComplexCell* forwardLinkedList)
        {
            return (*forwardLinkedList).element;
        }

		
        public static ComplexArray toArray(ComplexCell* abc)
        {
            var total = length(abc);
            var arr = (Complex*)malloc((ushort)sizeof(Complex) * total);
            var counter = 0;
            while (abc != null)
            {
                arr[counter] = (*abc).element;
                abc = (*abc).next;
                counter++;
            }

            return new ComplexArray { lenght = total, index = arr };
        }

        public static ComplexCell* tail(ComplexCell* forwardLinkedList)
        {
            return (*forwardLinkedList).next;
        }

        public static ComplexCell* last(ComplexCell* forwardLinkedList)
        {
            while (true)
            {
                if ((*forwardLinkedList).next == null) return forwardLinkedList;
                forwardLinkedList = (*forwardLinkedList).next;
            }
        }

        /// <summary>
        /// Copies payload of <paramref name="cell"/> into newly allocated cell header, <paramref name="cells"/> is set <see cref="charCell.next"/>.
        /// </summary>
        /// <param name="cell"></param>
        /// <param name="cells"></param>
        /// <param name="allocator"></param>
        /// <returns></returns>
        public static ComplexCellresult push(ComplexCell cell, ComplexCell* cells)
        {
            var mem = ComplexCellAlloc(1);
            (*mem).element = cell.element;
            (*mem).next = cells;
            return new ComplexCellresult { __value = mem };
        }


        public static CellError push_(ComplexCell* cell, ComplexCell* cells)
        {
            if ((*cell).next != NULL)
                return CellError.cannot_push_onto_filled_list;
            _push_(cell, cells);
            return CellError.ok;
        }

        public static void _push_(ComplexCell* cell, ComplexCell* cells) => (*cell).next = cells;

        public static void toCells(Complex[] array, ComplexCell* memory)
        {
            ComplexCell* previous = null;

            foreach (var item in array)
            {
                (*memory) = new ComplexCell { element = item };
                if (previous != null)
                {
                    (*previous).next = memory;
                }

                previous = memory;

                memory++;
            }
        }
    }


    public unsafe struct charCellresult
    {
        public charCell* __value;
        public CellError error;

        public charCell* unwrap()
        {
            this.error.unwrap();
            return __value;
        }
    }

public static unsafe partial class charCellExtensions
  {
    
	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static unsafe  charCell* charCellAlloc(ushort count)
		  => (charCell*)malloc((ushort)sizeof(charCell) * count);
           
         public static unsafe  charCellCell* charCellCellAlloc(ushort count)
		  => (charCellCell*)malloc((ushort)sizeof(charCellCell) * count);


    public static ushort length(charCell* cell)
    {
            ushort counter = 0;
            while (cell != null)
            {
                counter++;
                cell = (*cell).next;
            };
            return counter;
        }

        public static void push(ref charCell* head, char newData)
            => head = newCell(newData, head);

		public static void push(charCell** head, char newData) 
            => (*head) = newCell(newData, *head);

        public static charCell* newCell(char newData)
        {
            var cell = charCellAlloc(1);
            cell->element = newData;
            cell->next = NULL;
            return cell;
        }

        public static void cellInit(ref charCell cell, char data, ref charCell next)
        {
            cell.element = data;
            fixed (charCell* ptr = &next)
            {
                cell.next = ptr;
            }
        }

        public static void cellInit(charCell* cell, char data, charCell* next)
        {
            cell->element = data;
            cell->next = next;
        }

        public static void cellInit(ref charCell cell, char data)
        {
            cell.element = data;
            cell.next = NULL;
        }

        public static void cellInit(charCell* cell, char data)
        {
            cell->element = data;
            cell->next = NULL;
        }

        public static charCell* newCell(char element, charCell* next)
        {
            var cell = charCellAlloc(1);
            cellInit(cell, element, next);
            return cell;        }

        public static bool equals(charCell* a, charCell* b)
        {
            if (a == b) return true;
            while (true)
                if (a == NULL && b == NULL)
                    return true;
                else if (a != NULL && b == NULL)
                    return false;
                else if (a == NULL && b != NULL)
                    return false;
                else if ((*a).element != (*b).element)
                    return false;
                else
                  { a = (*a).next; b = (*b).next; };
        }

		public static void free(charCell** headRef)
        {
            charCell* head = *headRef;
            while (head != null)
            {
                var toFree = head;
                head = head->next;
            }
            (*headRef) = NULL;
        }



		        public static ushort count(charCell* head, char searchFor)
        {
                ushort counter = 0;
                while (head != null)
                {
                    if (head->element == searchFor) counter++;
                    head = (*head).next;
                }
                return counter;
        }

    public static ushort length(charCellCell* cell)
    {
            ushort counter = 0;
            while (cell != null)
            {
                counter++;
                cell = (*cell).next;
            }
            return counter;
        }

        public static charCell* NULL
        {
            get
            {
                return null;
            }
        }

		public static char head(charCell* forwardLinkedList)
        {
            return (*forwardLinkedList).element;
        }

		
        public static charArray toArray(charCell* abc)
        {
            var total = length(abc);
            var arr = (char*)malloc((ushort)sizeof(char) * total);
            var counter = 0;
            while (abc != null)
            {
                arr[counter] = (*abc).element;
                abc = (*abc).next;
                counter++;
            }

            return new charArray { lenght = total, index = arr };
        }

        public static charCell* tail(charCell* forwardLinkedList)
        {
            return (*forwardLinkedList).next;
        }

        public static charCell* last(charCell* forwardLinkedList)
        {
            while (true)
            {
                if ((*forwardLinkedList).next == null) return forwardLinkedList;
                forwardLinkedList = (*forwardLinkedList).next;
            }
        }

        /// <summary>
        /// Copies payload of <paramref name="cell"/> into newly allocated cell header, <paramref name="cells"/> is set <see cref="charCell.next"/>.
        /// </summary>
        /// <param name="cell"></param>
        /// <param name="cells"></param>
        /// <param name="allocator"></param>
        /// <returns></returns>
        public static charCellresult push(charCell cell, charCell* cells)
        {
            var mem = charCellAlloc(1);
            (*mem).element = cell.element;
            (*mem).next = cells;
            return new charCellresult { __value = mem };
        }


        public static CellError push_(charCell* cell, charCell* cells)
        {
            if ((*cell).next != NULL)
                return CellError.cannot_push_onto_filled_list;
            _push_(cell, cells);
            return CellError.ok;
        }

        public static void _push_(charCell* cell, charCell* cells) => (*cell).next = cells;

        public static void toCells(char[] array, charCell* memory)
        {
            charCell* previous = null;

            foreach (var item in array)
            {
                (*memory) = new charCell { element = item };
                if (previous != null)
                {
                    (*previous).next = memory;
                }

                previous = memory;

                memory++;
            }
        }
    }
}
namespace bit32
{

using static System.Collections.Unsafe.bit32.stdlib;

  public unsafe  struct intCell
  {
    public int element;
    public intCell* next;
  }

  public unsafe  struct intCellCell
  {
    public intCell* element;
    public intCellCell* next;
  }

  public unsafe  struct longCell
  {
    public long element;
    public longCell* next;
  }

  public unsafe  struct longCellCell
  {
    public longCell* element;
    public longCellCell* next;
  }

  public unsafe  struct floatCell
  {
    public float element;
    public floatCell* next;
  }

  public unsafe  struct floatCellCell
  {
    public floatCell* element;
    public floatCellCell* next;
  }

  public unsafe  struct doubleCell
  {
    public double element;
    public doubleCell* next;
  }

  public unsafe  struct doubleCellCell
  {
    public doubleCell* element;
    public doubleCellCell* next;
  }

  public unsafe  struct decimalCell
  {
    public decimal element;
    public decimalCell* next;
  }

  public unsafe  struct decimalCellCell
  {
    public decimalCell* element;
    public decimalCellCell* next;
  }

  public unsafe  struct BigIntegerCell
  {
    public BigInteger element;
    public BigIntegerCell* next;
  }

  public unsafe  struct BigIntegerCellCell
  {
    public BigIntegerCell* element;
    public BigIntegerCellCell* next;
  }

  public unsafe  struct ComplexCell
  {
    public Complex element;
    public ComplexCell* next;
  }

  public unsafe  struct ComplexCellCell
  {
    public ComplexCell* element;
    public ComplexCellCell* next;
  }

  public unsafe  struct charCell
  {
    public char element;
    public charCell* next;
  }

  public unsafe  struct charCellCell
  {
    public charCell* element;
    public charCellCell* next;
  }






    public unsafe struct intCellresult
    {
        public intCell* __value;
        public CellError error;

        public intCell* unwrap()
        {
            this.error.unwrap();
            return __value;
        }
    }

public static unsafe partial class intCellExtensions
  {
    
	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static unsafe  intCell* intCellAlloc(uint count)
		  => (intCell*)malloc((uint)sizeof(intCell) * count);
           
         public static unsafe  intCellCell* intCellCellAlloc(uint count)
		  => (intCellCell*)malloc((uint)sizeof(intCellCell) * count);


    public static uint length(intCell* cell)
    {
            uint counter = 0;
            while (cell != null)
            {
                counter++;
                cell = (*cell).next;
            };
            return counter;
        }

        public static void push(ref intCell* head, int newData)
            => head = newCell(newData, head);

		public static void push(intCell** head, int newData) 
            => (*head) = newCell(newData, *head);

        public static intCell* newCell(int newData)
        {
            var cell = intCellAlloc(1);
            cell->element = newData;
            cell->next = NULL;
            return cell;
        }

        public static void cellInit(ref intCell cell, int data, ref intCell next)
        {
            cell.element = data;
            fixed (intCell* ptr = &next)
            {
                cell.next = ptr;
            }
        }

        public static void cellInit(intCell* cell, int data, intCell* next)
        {
            cell->element = data;
            cell->next = next;
        }

        public static void cellInit(ref intCell cell, int data)
        {
            cell.element = data;
            cell.next = NULL;
        }

        public static void cellInit(intCell* cell, int data)
        {
            cell->element = data;
            cell->next = NULL;
        }

        public static intCell* newCell(int element, intCell* next)
        {
            var cell = intCellAlloc(1);
            cellInit(cell, element, next);
            return cell;        }

        public static bool equals(intCell* a, intCell* b)
        {
            if (a == b) return true;
            while (true)
                if (a == NULL && b == NULL)
                    return true;
                else if (a != NULL && b == NULL)
                    return false;
                else if (a == NULL && b != NULL)
                    return false;
                else if ((*a).element != (*b).element)
                    return false;
                else
                  { a = (*a).next; b = (*b).next; };
        }

		public static void free(intCell** headRef)
        {
            intCell* head = *headRef;
            while (head != null)
            {
                var toFree = head;
                head = head->next;
            }
            (*headRef) = NULL;
        }



		        public static uint count(intCell* head, int searchFor)
        {
                uint counter = 0;
                while (head != null)
                {
                    if (head->element == searchFor) counter++;
                    head = (*head).next;
                }
                return counter;
        }

    public static uint length(intCellCell* cell)
    {
            uint counter = 0;
            while (cell != null)
            {
                counter++;
                cell = (*cell).next;
            }
            return counter;
        }

        public static intCell* NULL
        {
            get
            {
                return null;
            }
        }

		public static int head(intCell* forwardLinkedList)
        {
            return (*forwardLinkedList).element;
        }

		
        public static intArray toArray(intCell* abc)
        {
            var total = length(abc);
            var arr = (int*)malloc((uint)sizeof(int) * total);
            var counter = 0;
            while (abc != null)
            {
                arr[counter] = (*abc).element;
                abc = (*abc).next;
                counter++;
            }

            return new intArray { lenght = total, index = arr };
        }

        public static intCell* tail(intCell* forwardLinkedList)
        {
            return (*forwardLinkedList).next;
        }

        public static intCell* last(intCell* forwardLinkedList)
        {
            while (true)
            {
                if ((*forwardLinkedList).next == null) return forwardLinkedList;
                forwardLinkedList = (*forwardLinkedList).next;
            }
        }

        /// <summary>
        /// Copies payload of <paramref name="cell"/> into newly allocated cell header, <paramref name="cells"/> is set <see cref="charCell.next"/>.
        /// </summary>
        /// <param name="cell"></param>
        /// <param name="cells"></param>
        /// <param name="allocator"></param>
        /// <returns></returns>
        public static intCellresult push(intCell cell, intCell* cells)
        {
            var mem = intCellAlloc(1);
            (*mem).element = cell.element;
            (*mem).next = cells;
            return new intCellresult { __value = mem };
        }


        public static CellError push_(intCell* cell, intCell* cells)
        {
            if ((*cell).next != NULL)
                return CellError.cannot_push_onto_filled_list;
            _push_(cell, cells);
            return CellError.ok;
        }

        public static void _push_(intCell* cell, intCell* cells) => (*cell).next = cells;

        public static void toCells(int[] array, intCell* memory)
        {
            intCell* previous = null;

            foreach (var item in array)
            {
                (*memory) = new intCell { element = item };
                if (previous != null)
                {
                    (*previous).next = memory;
                }

                previous = memory;

                memory++;
            }
        }
    }


    public unsafe struct longCellresult
    {
        public longCell* __value;
        public CellError error;

        public longCell* unwrap()
        {
            this.error.unwrap();
            return __value;
        }
    }

public static unsafe partial class longCellExtensions
  {
    
	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static unsafe  longCell* longCellAlloc(uint count)
		  => (longCell*)malloc((uint)sizeof(longCell) * count);
           
         public static unsafe  longCellCell* longCellCellAlloc(uint count)
		  => (longCellCell*)malloc((uint)sizeof(longCellCell) * count);


    public static uint length(longCell* cell)
    {
            uint counter = 0;
            while (cell != null)
            {
                counter++;
                cell = (*cell).next;
            };
            return counter;
        }

        public static void push(ref longCell* head, long newData)
            => head = newCell(newData, head);

		public static void push(longCell** head, long newData) 
            => (*head) = newCell(newData, *head);

        public static longCell* newCell(long newData)
        {
            var cell = longCellAlloc(1);
            cell->element = newData;
            cell->next = NULL;
            return cell;
        }

        public static void cellInit(ref longCell cell, long data, ref longCell next)
        {
            cell.element = data;
            fixed (longCell* ptr = &next)
            {
                cell.next = ptr;
            }
        }

        public static void cellInit(longCell* cell, long data, longCell* next)
        {
            cell->element = data;
            cell->next = next;
        }

        public static void cellInit(ref longCell cell, long data)
        {
            cell.element = data;
            cell.next = NULL;
        }

        public static void cellInit(longCell* cell, long data)
        {
            cell->element = data;
            cell->next = NULL;
        }

        public static longCell* newCell(long element, longCell* next)
        {
            var cell = longCellAlloc(1);
            cellInit(cell, element, next);
            return cell;        }

        public static bool equals(longCell* a, longCell* b)
        {
            if (a == b) return true;
            while (true)
                if (a == NULL && b == NULL)
                    return true;
                else if (a != NULL && b == NULL)
                    return false;
                else if (a == NULL && b != NULL)
                    return false;
                else if ((*a).element != (*b).element)
                    return false;
                else
                  { a = (*a).next; b = (*b).next; };
        }

		public static void free(longCell** headRef)
        {
            longCell* head = *headRef;
            while (head != null)
            {
                var toFree = head;
                head = head->next;
            }
            (*headRef) = NULL;
        }



		        public static uint count(longCell* head, long searchFor)
        {
                uint counter = 0;
                while (head != null)
                {
                    if (head->element == searchFor) counter++;
                    head = (*head).next;
                }
                return counter;
        }

    public static uint length(longCellCell* cell)
    {
            uint counter = 0;
            while (cell != null)
            {
                counter++;
                cell = (*cell).next;
            }
            return counter;
        }

        public static longCell* NULL
        {
            get
            {
                return null;
            }
        }

		public static long head(longCell* forwardLinkedList)
        {
            return (*forwardLinkedList).element;
        }

		
        public static longArray toArray(longCell* abc)
        {
            var total = length(abc);
            var arr = (long*)malloc((uint)sizeof(long) * total);
            var counter = 0;
            while (abc != null)
            {
                arr[counter] = (*abc).element;
                abc = (*abc).next;
                counter++;
            }

            return new longArray { lenght = total, index = arr };
        }

        public static longCell* tail(longCell* forwardLinkedList)
        {
            return (*forwardLinkedList).next;
        }

        public static longCell* last(longCell* forwardLinkedList)
        {
            while (true)
            {
                if ((*forwardLinkedList).next == null) return forwardLinkedList;
                forwardLinkedList = (*forwardLinkedList).next;
            }
        }

        /// <summary>
        /// Copies payload of <paramref name="cell"/> into newly allocated cell header, <paramref name="cells"/> is set <see cref="charCell.next"/>.
        /// </summary>
        /// <param name="cell"></param>
        /// <param name="cells"></param>
        /// <param name="allocator"></param>
        /// <returns></returns>
        public static longCellresult push(longCell cell, longCell* cells)
        {
            var mem = longCellAlloc(1);
            (*mem).element = cell.element;
            (*mem).next = cells;
            return new longCellresult { __value = mem };
        }


        public static CellError push_(longCell* cell, longCell* cells)
        {
            if ((*cell).next != NULL)
                return CellError.cannot_push_onto_filled_list;
            _push_(cell, cells);
            return CellError.ok;
        }

        public static void _push_(longCell* cell, longCell* cells) => (*cell).next = cells;

        public static void toCells(long[] array, longCell* memory)
        {
            longCell* previous = null;

            foreach (var item in array)
            {
                (*memory) = new longCell { element = item };
                if (previous != null)
                {
                    (*previous).next = memory;
                }

                previous = memory;

                memory++;
            }
        }
    }


    public unsafe struct floatCellresult
    {
        public floatCell* __value;
        public CellError error;

        public floatCell* unwrap()
        {
            this.error.unwrap();
            return __value;
        }
    }

public static unsafe partial class floatCellExtensions
  {
    
	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static unsafe  floatCell* floatCellAlloc(uint count)
		  => (floatCell*)malloc((uint)sizeof(floatCell) * count);
           
         public static unsafe  floatCellCell* floatCellCellAlloc(uint count)
		  => (floatCellCell*)malloc((uint)sizeof(floatCellCell) * count);


    public static uint length(floatCell* cell)
    {
            uint counter = 0;
            while (cell != null)
            {
                counter++;
                cell = (*cell).next;
            };
            return counter;
        }

        public static void push(ref floatCell* head, float newData)
            => head = newCell(newData, head);

		public static void push(floatCell** head, float newData) 
            => (*head) = newCell(newData, *head);

        public static floatCell* newCell(float newData)
        {
            var cell = floatCellAlloc(1);
            cell->element = newData;
            cell->next = NULL;
            return cell;
        }

        public static void cellInit(ref floatCell cell, float data, ref floatCell next)
        {
            cell.element = data;
            fixed (floatCell* ptr = &next)
            {
                cell.next = ptr;
            }
        }

        public static void cellInit(floatCell* cell, float data, floatCell* next)
        {
            cell->element = data;
            cell->next = next;
        }

        public static void cellInit(ref floatCell cell, float data)
        {
            cell.element = data;
            cell.next = NULL;
        }

        public static void cellInit(floatCell* cell, float data)
        {
            cell->element = data;
            cell->next = NULL;
        }

        public static floatCell* newCell(float element, floatCell* next)
        {
            var cell = floatCellAlloc(1);
            cellInit(cell, element, next);
            return cell;        }

        public static bool equals(floatCell* a, floatCell* b)
        {
            if (a == b) return true;
            while (true)
                if (a == NULL && b == NULL)
                    return true;
                else if (a != NULL && b == NULL)
                    return false;
                else if (a == NULL && b != NULL)
                    return false;
                else if ((*a).element != (*b).element)
                    return false;
                else
                  { a = (*a).next; b = (*b).next; };
        }

		public static void free(floatCell** headRef)
        {
            floatCell* head = *headRef;
            while (head != null)
            {
                var toFree = head;
                head = head->next;
            }
            (*headRef) = NULL;
        }



		        public static uint count(floatCell* head, float searchFor)
        {
                uint counter = 0;
                while (head != null)
                {
                    if (head->element == searchFor) counter++;
                    head = (*head).next;
                }
                return counter;
        }

    public static uint length(floatCellCell* cell)
    {
            uint counter = 0;
            while (cell != null)
            {
                counter++;
                cell = (*cell).next;
            }
            return counter;
        }

        public static floatCell* NULL
        {
            get
            {
                return null;
            }
        }

		public static float head(floatCell* forwardLinkedList)
        {
            return (*forwardLinkedList).element;
        }

		
        public static floatArray toArray(floatCell* abc)
        {
            var total = length(abc);
            var arr = (float*)malloc((uint)sizeof(float) * total);
            var counter = 0;
            while (abc != null)
            {
                arr[counter] = (*abc).element;
                abc = (*abc).next;
                counter++;
            }

            return new floatArray { lenght = total, index = arr };
        }

        public static floatCell* tail(floatCell* forwardLinkedList)
        {
            return (*forwardLinkedList).next;
        }

        public static floatCell* last(floatCell* forwardLinkedList)
        {
            while (true)
            {
                if ((*forwardLinkedList).next == null) return forwardLinkedList;
                forwardLinkedList = (*forwardLinkedList).next;
            }
        }

        /// <summary>
        /// Copies payload of <paramref name="cell"/> into newly allocated cell header, <paramref name="cells"/> is set <see cref="charCell.next"/>.
        /// </summary>
        /// <param name="cell"></param>
        /// <param name="cells"></param>
        /// <param name="allocator"></param>
        /// <returns></returns>
        public static floatCellresult push(floatCell cell, floatCell* cells)
        {
            var mem = floatCellAlloc(1);
            (*mem).element = cell.element;
            (*mem).next = cells;
            return new floatCellresult { __value = mem };
        }


        public static CellError push_(floatCell* cell, floatCell* cells)
        {
            if ((*cell).next != NULL)
                return CellError.cannot_push_onto_filled_list;
            _push_(cell, cells);
            return CellError.ok;
        }

        public static void _push_(floatCell* cell, floatCell* cells) => (*cell).next = cells;

        public static void toCells(float[] array, floatCell* memory)
        {
            floatCell* previous = null;

            foreach (var item in array)
            {
                (*memory) = new floatCell { element = item };
                if (previous != null)
                {
                    (*previous).next = memory;
                }

                previous = memory;

                memory++;
            }
        }
    }


    public unsafe struct doubleCellresult
    {
        public doubleCell* __value;
        public CellError error;

        public doubleCell* unwrap()
        {
            this.error.unwrap();
            return __value;
        }
    }

public static unsafe partial class doubleCellExtensions
  {
    
	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static unsafe  doubleCell* doubleCellAlloc(uint count)
		  => (doubleCell*)malloc((uint)sizeof(doubleCell) * count);
           
         public static unsafe  doubleCellCell* doubleCellCellAlloc(uint count)
		  => (doubleCellCell*)malloc((uint)sizeof(doubleCellCell) * count);


    public static uint length(doubleCell* cell)
    {
            uint counter = 0;
            while (cell != null)
            {
                counter++;
                cell = (*cell).next;
            };
            return counter;
        }

        public static void push(ref doubleCell* head, double newData)
            => head = newCell(newData, head);

		public static void push(doubleCell** head, double newData) 
            => (*head) = newCell(newData, *head);

        public static doubleCell* newCell(double newData)
        {
            var cell = doubleCellAlloc(1);
            cell->element = newData;
            cell->next = NULL;
            return cell;
        }

        public static void cellInit(ref doubleCell cell, double data, ref doubleCell next)
        {
            cell.element = data;
            fixed (doubleCell* ptr = &next)
            {
                cell.next = ptr;
            }
        }

        public static void cellInit(doubleCell* cell, double data, doubleCell* next)
        {
            cell->element = data;
            cell->next = next;
        }

        public static void cellInit(ref doubleCell cell, double data)
        {
            cell.element = data;
            cell.next = NULL;
        }

        public static void cellInit(doubleCell* cell, double data)
        {
            cell->element = data;
            cell->next = NULL;
        }

        public static doubleCell* newCell(double element, doubleCell* next)
        {
            var cell = doubleCellAlloc(1);
            cellInit(cell, element, next);
            return cell;        }

        public static bool equals(doubleCell* a, doubleCell* b)
        {
            if (a == b) return true;
            while (true)
                if (a == NULL && b == NULL)
                    return true;
                else if (a != NULL && b == NULL)
                    return false;
                else if (a == NULL && b != NULL)
                    return false;
                else if ((*a).element != (*b).element)
                    return false;
                else
                  { a = (*a).next; b = (*b).next; };
        }

		public static void free(doubleCell** headRef)
        {
            doubleCell* head = *headRef;
            while (head != null)
            {
                var toFree = head;
                head = head->next;
            }
            (*headRef) = NULL;
        }



		        public static uint count(doubleCell* head, double searchFor)
        {
                uint counter = 0;
                while (head != null)
                {
                    if (head->element == searchFor) counter++;
                    head = (*head).next;
                }
                return counter;
        }

    public static uint length(doubleCellCell* cell)
    {
            uint counter = 0;
            while (cell != null)
            {
                counter++;
                cell = (*cell).next;
            }
            return counter;
        }

        public static doubleCell* NULL
        {
            get
            {
                return null;
            }
        }

		public static double head(doubleCell* forwardLinkedList)
        {
            return (*forwardLinkedList).element;
        }

		
        public static doubleArray toArray(doubleCell* abc)
        {
            var total = length(abc);
            var arr = (double*)malloc((uint)sizeof(double) * total);
            var counter = 0;
            while (abc != null)
            {
                arr[counter] = (*abc).element;
                abc = (*abc).next;
                counter++;
            }

            return new doubleArray { lenght = total, index = arr };
        }

        public static doubleCell* tail(doubleCell* forwardLinkedList)
        {
            return (*forwardLinkedList).next;
        }

        public static doubleCell* last(doubleCell* forwardLinkedList)
        {
            while (true)
            {
                if ((*forwardLinkedList).next == null) return forwardLinkedList;
                forwardLinkedList = (*forwardLinkedList).next;
            }
        }

        /// <summary>
        /// Copies payload of <paramref name="cell"/> into newly allocated cell header, <paramref name="cells"/> is set <see cref="charCell.next"/>.
        /// </summary>
        /// <param name="cell"></param>
        /// <param name="cells"></param>
        /// <param name="allocator"></param>
        /// <returns></returns>
        public static doubleCellresult push(doubleCell cell, doubleCell* cells)
        {
            var mem = doubleCellAlloc(1);
            (*mem).element = cell.element;
            (*mem).next = cells;
            return new doubleCellresult { __value = mem };
        }


        public static CellError push_(doubleCell* cell, doubleCell* cells)
        {
            if ((*cell).next != NULL)
                return CellError.cannot_push_onto_filled_list;
            _push_(cell, cells);
            return CellError.ok;
        }

        public static void _push_(doubleCell* cell, doubleCell* cells) => (*cell).next = cells;

        public static void toCells(double[] array, doubleCell* memory)
        {
            doubleCell* previous = null;

            foreach (var item in array)
            {
                (*memory) = new doubleCell { element = item };
                if (previous != null)
                {
                    (*previous).next = memory;
                }

                previous = memory;

                memory++;
            }
        }
    }


    public unsafe struct decimalCellresult
    {
        public decimalCell* __value;
        public CellError error;

        public decimalCell* unwrap()
        {
            this.error.unwrap();
            return __value;
        }
    }

public static unsafe partial class decimalCellExtensions
  {
    
	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static unsafe  decimalCell* decimalCellAlloc(uint count)
		  => (decimalCell*)malloc((uint)sizeof(decimalCell) * count);
           
         public static unsafe  decimalCellCell* decimalCellCellAlloc(uint count)
		  => (decimalCellCell*)malloc((uint)sizeof(decimalCellCell) * count);


    public static uint length(decimalCell* cell)
    {
            uint counter = 0;
            while (cell != null)
            {
                counter++;
                cell = (*cell).next;
            };
            return counter;
        }

        public static void push(ref decimalCell* head, decimal newData)
            => head = newCell(newData, head);

		public static void push(decimalCell** head, decimal newData) 
            => (*head) = newCell(newData, *head);

        public static decimalCell* newCell(decimal newData)
        {
            var cell = decimalCellAlloc(1);
            cell->element = newData;
            cell->next = NULL;
            return cell;
        }

        public static void cellInit(ref decimalCell cell, decimal data, ref decimalCell next)
        {
            cell.element = data;
            fixed (decimalCell* ptr = &next)
            {
                cell.next = ptr;
            }
        }

        public static void cellInit(decimalCell* cell, decimal data, decimalCell* next)
        {
            cell->element = data;
            cell->next = next;
        }

        public static void cellInit(ref decimalCell cell, decimal data)
        {
            cell.element = data;
            cell.next = NULL;
        }

        public static void cellInit(decimalCell* cell, decimal data)
        {
            cell->element = data;
            cell->next = NULL;
        }

        public static decimalCell* newCell(decimal element, decimalCell* next)
        {
            var cell = decimalCellAlloc(1);
            cellInit(cell, element, next);
            return cell;        }

        public static bool equals(decimalCell* a, decimalCell* b)
        {
            if (a == b) return true;
            while (true)
                if (a == NULL && b == NULL)
                    return true;
                else if (a != NULL && b == NULL)
                    return false;
                else if (a == NULL && b != NULL)
                    return false;
                else if ((*a).element != (*b).element)
                    return false;
                else
                  { a = (*a).next; b = (*b).next; };
        }

		public static void free(decimalCell** headRef)
        {
            decimalCell* head = *headRef;
            while (head != null)
            {
                var toFree = head;
                head = head->next;
            }
            (*headRef) = NULL;
        }



		        public static uint count(decimalCell* head, decimal searchFor)
        {
                uint counter = 0;
                while (head != null)
                {
                    if (head->element == searchFor) counter++;
                    head = (*head).next;
                }
                return counter;
        }

    public static uint length(decimalCellCell* cell)
    {
            uint counter = 0;
            while (cell != null)
            {
                counter++;
                cell = (*cell).next;
            }
            return counter;
        }

        public static decimalCell* NULL
        {
            get
            {
                return null;
            }
        }

		public static decimal head(decimalCell* forwardLinkedList)
        {
            return (*forwardLinkedList).element;
        }

		
        public static decimalArray toArray(decimalCell* abc)
        {
            var total = length(abc);
            var arr = (decimal*)malloc((uint)sizeof(decimal) * total);
            var counter = 0;
            while (abc != null)
            {
                arr[counter] = (*abc).element;
                abc = (*abc).next;
                counter++;
            }

            return new decimalArray { lenght = total, index = arr };
        }

        public static decimalCell* tail(decimalCell* forwardLinkedList)
        {
            return (*forwardLinkedList).next;
        }

        public static decimalCell* last(decimalCell* forwardLinkedList)
        {
            while (true)
            {
                if ((*forwardLinkedList).next == null) return forwardLinkedList;
                forwardLinkedList = (*forwardLinkedList).next;
            }
        }

        /// <summary>
        /// Copies payload of <paramref name="cell"/> into newly allocated cell header, <paramref name="cells"/> is set <see cref="charCell.next"/>.
        /// </summary>
        /// <param name="cell"></param>
        /// <param name="cells"></param>
        /// <param name="allocator"></param>
        /// <returns></returns>
        public static decimalCellresult push(decimalCell cell, decimalCell* cells)
        {
            var mem = decimalCellAlloc(1);
            (*mem).element = cell.element;
            (*mem).next = cells;
            return new decimalCellresult { __value = mem };
        }


        public static CellError push_(decimalCell* cell, decimalCell* cells)
        {
            if ((*cell).next != NULL)
                return CellError.cannot_push_onto_filled_list;
            _push_(cell, cells);
            return CellError.ok;
        }

        public static void _push_(decimalCell* cell, decimalCell* cells) => (*cell).next = cells;

        public static void toCells(decimal[] array, decimalCell* memory)
        {
            decimalCell* previous = null;

            foreach (var item in array)
            {
                (*memory) = new decimalCell { element = item };
                if (previous != null)
                {
                    (*previous).next = memory;
                }

                previous = memory;

                memory++;
            }
        }
    }


    public unsafe struct BigIntegerCellresult
    {
        public BigIntegerCell* __value;
        public CellError error;

        public BigIntegerCell* unwrap()
        {
            this.error.unwrap();
            return __value;
        }
    }

public static unsafe partial class BigIntegerCellExtensions
  {
    
	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static unsafe  BigIntegerCell* BigIntegerCellAlloc(uint count)
		  => (BigIntegerCell*)malloc((uint)sizeof(BigIntegerCell) * count);
           
         public static unsafe  BigIntegerCellCell* BigIntegerCellCellAlloc(uint count)
		  => (BigIntegerCellCell*)malloc((uint)sizeof(BigIntegerCellCell) * count);


    public static uint length(BigIntegerCell* cell)
    {
            uint counter = 0;
            while (cell != null)
            {
                counter++;
                cell = (*cell).next;
            };
            return counter;
        }

        public static void push(ref BigIntegerCell* head, BigInteger newData)
            => head = newCell(newData, head);

		public static void push(BigIntegerCell** head, BigInteger newData) 
            => (*head) = newCell(newData, *head);

        public static BigIntegerCell* newCell(BigInteger newData)
        {
            var cell = BigIntegerCellAlloc(1);
            cell->element = newData;
            cell->next = NULL;
            return cell;
        }

        public static void cellInit(ref BigIntegerCell cell, BigInteger data, ref BigIntegerCell next)
        {
            cell.element = data;
            fixed (BigIntegerCell* ptr = &next)
            {
                cell.next = ptr;
            }
        }

        public static void cellInit(BigIntegerCell* cell, BigInteger data, BigIntegerCell* next)
        {
            cell->element = data;
            cell->next = next;
        }

        public static void cellInit(ref BigIntegerCell cell, BigInteger data)
        {
            cell.element = data;
            cell.next = NULL;
        }

        public static void cellInit(BigIntegerCell* cell, BigInteger data)
        {
            cell->element = data;
            cell->next = NULL;
        }

        public static BigIntegerCell* newCell(BigInteger element, BigIntegerCell* next)
        {
            var cell = BigIntegerCellAlloc(1);
            cellInit(cell, element, next);
            return cell;        }

        public static bool equals(BigIntegerCell* a, BigIntegerCell* b)
        {
            if (a == b) return true;
            while (true)
                if (a == NULL && b == NULL)
                    return true;
                else if (a != NULL && b == NULL)
                    return false;
                else if (a == NULL && b != NULL)
                    return false;
                else if ((*a).element != (*b).element)
                    return false;
                else
                  { a = (*a).next; b = (*b).next; };
        }

		public static void free(BigIntegerCell** headRef)
        {
            BigIntegerCell* head = *headRef;
            while (head != null)
            {
                var toFree = head;
                head = head->next;
            }
            (*headRef) = NULL;
        }



		        public static uint count(BigIntegerCell* head, BigInteger searchFor)
        {
                uint counter = 0;
                while (head != null)
                {
                    if (head->element == searchFor) counter++;
                    head = (*head).next;
                }
                return counter;
        }

    public static uint length(BigIntegerCellCell* cell)
    {
            uint counter = 0;
            while (cell != null)
            {
                counter++;
                cell = (*cell).next;
            }
            return counter;
        }

        public static BigIntegerCell* NULL
        {
            get
            {
                return null;
            }
        }

		public static BigInteger head(BigIntegerCell* forwardLinkedList)
        {
            return (*forwardLinkedList).element;
        }

		
        public static BigIntegerArray toArray(BigIntegerCell* abc)
        {
            var total = length(abc);
            var arr = (BigInteger*)malloc((uint)sizeof(BigInteger) * total);
            var counter = 0;
            while (abc != null)
            {
                arr[counter] = (*abc).element;
                abc = (*abc).next;
                counter++;
            }

            return new BigIntegerArray { lenght = total, index = arr };
        }

        public static BigIntegerCell* tail(BigIntegerCell* forwardLinkedList)
        {
            return (*forwardLinkedList).next;
        }

        public static BigIntegerCell* last(BigIntegerCell* forwardLinkedList)
        {
            while (true)
            {
                if ((*forwardLinkedList).next == null) return forwardLinkedList;
                forwardLinkedList = (*forwardLinkedList).next;
            }
        }

        /// <summary>
        /// Copies payload of <paramref name="cell"/> into newly allocated cell header, <paramref name="cells"/> is set <see cref="charCell.next"/>.
        /// </summary>
        /// <param name="cell"></param>
        /// <param name="cells"></param>
        /// <param name="allocator"></param>
        /// <returns></returns>
        public static BigIntegerCellresult push(BigIntegerCell cell, BigIntegerCell* cells)
        {
            var mem = BigIntegerCellAlloc(1);
            (*mem).element = cell.element;
            (*mem).next = cells;
            return new BigIntegerCellresult { __value = mem };
        }


        public static CellError push_(BigIntegerCell* cell, BigIntegerCell* cells)
        {
            if ((*cell).next != NULL)
                return CellError.cannot_push_onto_filled_list;
            _push_(cell, cells);
            return CellError.ok;
        }

        public static void _push_(BigIntegerCell* cell, BigIntegerCell* cells) => (*cell).next = cells;

        public static void toCells(BigInteger[] array, BigIntegerCell* memory)
        {
            BigIntegerCell* previous = null;

            foreach (var item in array)
            {
                (*memory) = new BigIntegerCell { element = item };
                if (previous != null)
                {
                    (*previous).next = memory;
                }

                previous = memory;

                memory++;
            }
        }
    }


    public unsafe struct ComplexCellresult
    {
        public ComplexCell* __value;
        public CellError error;

        public ComplexCell* unwrap()
        {
            this.error.unwrap();
            return __value;
        }
    }

public static unsafe partial class ComplexCellExtensions
  {
    
	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static unsafe  ComplexCell* ComplexCellAlloc(uint count)
		  => (ComplexCell*)malloc((uint)sizeof(ComplexCell) * count);
           
         public static unsafe  ComplexCellCell* ComplexCellCellAlloc(uint count)
		  => (ComplexCellCell*)malloc((uint)sizeof(ComplexCellCell) * count);


    public static uint length(ComplexCell* cell)
    {
            uint counter = 0;
            while (cell != null)
            {
                counter++;
                cell = (*cell).next;
            };
            return counter;
        }

        public static void push(ref ComplexCell* head, Complex newData)
            => head = newCell(newData, head);

		public static void push(ComplexCell** head, Complex newData) 
            => (*head) = newCell(newData, *head);

        public static ComplexCell* newCell(Complex newData)
        {
            var cell = ComplexCellAlloc(1);
            cell->element = newData;
            cell->next = NULL;
            return cell;
        }

        public static void cellInit(ref ComplexCell cell, Complex data, ref ComplexCell next)
        {
            cell.element = data;
            fixed (ComplexCell* ptr = &next)
            {
                cell.next = ptr;
            }
        }

        public static void cellInit(ComplexCell* cell, Complex data, ComplexCell* next)
        {
            cell->element = data;
            cell->next = next;
        }

        public static void cellInit(ref ComplexCell cell, Complex data)
        {
            cell.element = data;
            cell.next = NULL;
        }

        public static void cellInit(ComplexCell* cell, Complex data)
        {
            cell->element = data;
            cell->next = NULL;
        }

        public static ComplexCell* newCell(Complex element, ComplexCell* next)
        {
            var cell = ComplexCellAlloc(1);
            cellInit(cell, element, next);
            return cell;        }

        public static bool equals(ComplexCell* a, ComplexCell* b)
        {
            if (a == b) return true;
            while (true)
                if (a == NULL && b == NULL)
                    return true;
                else if (a != NULL && b == NULL)
                    return false;
                else if (a == NULL && b != NULL)
                    return false;
                else if ((*a).element != (*b).element)
                    return false;
                else
                  { a = (*a).next; b = (*b).next; };
        }

		public static void free(ComplexCell** headRef)
        {
            ComplexCell* head = *headRef;
            while (head != null)
            {
                var toFree = head;
                head = head->next;
            }
            (*headRef) = NULL;
        }



		        public static uint count(ComplexCell* head, Complex searchFor)
        {
                uint counter = 0;
                while (head != null)
                {
                    if (head->element == searchFor) counter++;
                    head = (*head).next;
                }
                return counter;
        }

    public static uint length(ComplexCellCell* cell)
    {
            uint counter = 0;
            while (cell != null)
            {
                counter++;
                cell = (*cell).next;
            }
            return counter;
        }

        public static ComplexCell* NULL
        {
            get
            {
                return null;
            }
        }

		public static Complex head(ComplexCell* forwardLinkedList)
        {
            return (*forwardLinkedList).element;
        }

		
        public static ComplexArray toArray(ComplexCell* abc)
        {
            var total = length(abc);
            var arr = (Complex*)malloc((uint)sizeof(Complex) * total);
            var counter = 0;
            while (abc != null)
            {
                arr[counter] = (*abc).element;
                abc = (*abc).next;
                counter++;
            }

            return new ComplexArray { lenght = total, index = arr };
        }

        public static ComplexCell* tail(ComplexCell* forwardLinkedList)
        {
            return (*forwardLinkedList).next;
        }

        public static ComplexCell* last(ComplexCell* forwardLinkedList)
        {
            while (true)
            {
                if ((*forwardLinkedList).next == null) return forwardLinkedList;
                forwardLinkedList = (*forwardLinkedList).next;
            }
        }

        /// <summary>
        /// Copies payload of <paramref name="cell"/> into newly allocated cell header, <paramref name="cells"/> is set <see cref="charCell.next"/>.
        /// </summary>
        /// <param name="cell"></param>
        /// <param name="cells"></param>
        /// <param name="allocator"></param>
        /// <returns></returns>
        public static ComplexCellresult push(ComplexCell cell, ComplexCell* cells)
        {
            var mem = ComplexCellAlloc(1);
            (*mem).element = cell.element;
            (*mem).next = cells;
            return new ComplexCellresult { __value = mem };
        }


        public static CellError push_(ComplexCell* cell, ComplexCell* cells)
        {
            if ((*cell).next != NULL)
                return CellError.cannot_push_onto_filled_list;
            _push_(cell, cells);
            return CellError.ok;
        }

        public static void _push_(ComplexCell* cell, ComplexCell* cells) => (*cell).next = cells;

        public static void toCells(Complex[] array, ComplexCell* memory)
        {
            ComplexCell* previous = null;

            foreach (var item in array)
            {
                (*memory) = new ComplexCell { element = item };
                if (previous != null)
                {
                    (*previous).next = memory;
                }

                previous = memory;

                memory++;
            }
        }
    }


    public unsafe struct charCellresult
    {
        public charCell* __value;
        public CellError error;

        public charCell* unwrap()
        {
            this.error.unwrap();
            return __value;
        }
    }

public static unsafe partial class charCellExtensions
  {
    
	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static unsafe  charCell* charCellAlloc(uint count)
		  => (charCell*)malloc((uint)sizeof(charCell) * count);
           
         public static unsafe  charCellCell* charCellCellAlloc(uint count)
		  => (charCellCell*)malloc((uint)sizeof(charCellCell) * count);


    public static uint length(charCell* cell)
    {
            uint counter = 0;
            while (cell != null)
            {
                counter++;
                cell = (*cell).next;
            };
            return counter;
        }

        public static void push(ref charCell* head, char newData)
            => head = newCell(newData, head);

		public static void push(charCell** head, char newData) 
            => (*head) = newCell(newData, *head);

        public static charCell* newCell(char newData)
        {
            var cell = charCellAlloc(1);
            cell->element = newData;
            cell->next = NULL;
            return cell;
        }

        public static void cellInit(ref charCell cell, char data, ref charCell next)
        {
            cell.element = data;
            fixed (charCell* ptr = &next)
            {
                cell.next = ptr;
            }
        }

        public static void cellInit(charCell* cell, char data, charCell* next)
        {
            cell->element = data;
            cell->next = next;
        }

        public static void cellInit(ref charCell cell, char data)
        {
            cell.element = data;
            cell.next = NULL;
        }

        public static void cellInit(charCell* cell, char data)
        {
            cell->element = data;
            cell->next = NULL;
        }

        public static charCell* newCell(char element, charCell* next)
        {
            var cell = charCellAlloc(1);
            cellInit(cell, element, next);
            return cell;        }

        public static bool equals(charCell* a, charCell* b)
        {
            if (a == b) return true;
            while (true)
                if (a == NULL && b == NULL)
                    return true;
                else if (a != NULL && b == NULL)
                    return false;
                else if (a == NULL && b != NULL)
                    return false;
                else if ((*a).element != (*b).element)
                    return false;
                else
                  { a = (*a).next; b = (*b).next; };
        }

		public static void free(charCell** headRef)
        {
            charCell* head = *headRef;
            while (head != null)
            {
                var toFree = head;
                head = head->next;
            }
            (*headRef) = NULL;
        }



		        public static uint count(charCell* head, char searchFor)
        {
                uint counter = 0;
                while (head != null)
                {
                    if (head->element == searchFor) counter++;
                    head = (*head).next;
                }
                return counter;
        }

    public static uint length(charCellCell* cell)
    {
            uint counter = 0;
            while (cell != null)
            {
                counter++;
                cell = (*cell).next;
            }
            return counter;
        }

        public static charCell* NULL
        {
            get
            {
                return null;
            }
        }

		public static char head(charCell* forwardLinkedList)
        {
            return (*forwardLinkedList).element;
        }

		
        public static charArray toArray(charCell* abc)
        {
            var total = length(abc);
            var arr = (char*)malloc((uint)sizeof(char) * total);
            var counter = 0;
            while (abc != null)
            {
                arr[counter] = (*abc).element;
                abc = (*abc).next;
                counter++;
            }

            return new charArray { lenght = total, index = arr };
        }

        public static charCell* tail(charCell* forwardLinkedList)
        {
            return (*forwardLinkedList).next;
        }

        public static charCell* last(charCell* forwardLinkedList)
        {
            while (true)
            {
                if ((*forwardLinkedList).next == null) return forwardLinkedList;
                forwardLinkedList = (*forwardLinkedList).next;
            }
        }

        /// <summary>
        /// Copies payload of <paramref name="cell"/> into newly allocated cell header, <paramref name="cells"/> is set <see cref="charCell.next"/>.
        /// </summary>
        /// <param name="cell"></param>
        /// <param name="cells"></param>
        /// <param name="allocator"></param>
        /// <returns></returns>
        public static charCellresult push(charCell cell, charCell* cells)
        {
            var mem = charCellAlloc(1);
            (*mem).element = cell.element;
            (*mem).next = cells;
            return new charCellresult { __value = mem };
        }


        public static CellError push_(charCell* cell, charCell* cells)
        {
            if ((*cell).next != NULL)
                return CellError.cannot_push_onto_filled_list;
            _push_(cell, cells);
            return CellError.ok;
        }

        public static void _push_(charCell* cell, charCell* cells) => (*cell).next = cells;

        public static void toCells(char[] array, charCell* memory)
        {
            charCell* previous = null;

            foreach (var item in array)
            {
                (*memory) = new charCell { element = item };
                if (previous != null)
                {
                    (*previous).next = memory;
                }

                previous = memory;

                memory++;
            }
        }
    }
}
namespace bit64
{

using static System.Collections.Unsafe.bit64.stdlib;

  public unsafe  struct intCell
  {
    public int element;
    public intCell* next;
  }

  public unsafe  struct intCellCell
  {
    public intCell* element;
    public intCellCell* next;
  }

  public unsafe  struct longCell
  {
    public long element;
    public longCell* next;
  }

  public unsafe  struct longCellCell
  {
    public longCell* element;
    public longCellCell* next;
  }

  public unsafe  struct floatCell
  {
    public float element;
    public floatCell* next;
  }

  public unsafe  struct floatCellCell
  {
    public floatCell* element;
    public floatCellCell* next;
  }

  public unsafe  struct doubleCell
  {
    public double element;
    public doubleCell* next;
  }

  public unsafe  struct doubleCellCell
  {
    public doubleCell* element;
    public doubleCellCell* next;
  }

  public unsafe  struct decimalCell
  {
    public decimal element;
    public decimalCell* next;
  }

  public unsafe  struct decimalCellCell
  {
    public decimalCell* element;
    public decimalCellCell* next;
  }

  public unsafe  struct BigIntegerCell
  {
    public BigInteger element;
    public BigIntegerCell* next;
  }

  public unsafe  struct BigIntegerCellCell
  {
    public BigIntegerCell* element;
    public BigIntegerCellCell* next;
  }

  public unsafe  struct ComplexCell
  {
    public Complex element;
    public ComplexCell* next;
  }

  public unsafe  struct ComplexCellCell
  {
    public ComplexCell* element;
    public ComplexCellCell* next;
  }

  public unsafe  struct charCell
  {
    public char element;
    public charCell* next;
  }

  public unsafe  struct charCellCell
  {
    public charCell* element;
    public charCellCell* next;
  }






    public unsafe struct intCellresult
    {
        public intCell* __value;
        public CellError error;

        public intCell* unwrap()
        {
            this.error.unwrap();
            return __value;
        }
    }

public static unsafe partial class intCellExtensions
  {
    
	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static unsafe  intCell* intCellAlloc(ulong count)
		  => (intCell*)malloc((ulong)sizeof(intCell) * count);
           
         public static unsafe  intCellCell* intCellCellAlloc(ulong count)
		  => (intCellCell*)malloc((ulong)sizeof(intCellCell) * count);


    public static ulong length(intCell* cell)
    {
            ulong counter = 0;
            while (cell != null)
            {
                counter++;
                cell = (*cell).next;
            };
            return counter;
        }

        public static void push(ref intCell* head, int newData)
            => head = newCell(newData, head);

		public static void push(intCell** head, int newData) 
            => (*head) = newCell(newData, *head);

        public static intCell* newCell(int newData)
        {
            var cell = intCellAlloc(1);
            cell->element = newData;
            cell->next = NULL;
            return cell;
        }

        public static void cellInit(ref intCell cell, int data, ref intCell next)
        {
            cell.element = data;
            fixed (intCell* ptr = &next)
            {
                cell.next = ptr;
            }
        }

        public static void cellInit(intCell* cell, int data, intCell* next)
        {
            cell->element = data;
            cell->next = next;
        }

        public static void cellInit(ref intCell cell, int data)
        {
            cell.element = data;
            cell.next = NULL;
        }

        public static void cellInit(intCell* cell, int data)
        {
            cell->element = data;
            cell->next = NULL;
        }

        public static intCell* newCell(int element, intCell* next)
        {
            var cell = intCellAlloc(1);
            cellInit(cell, element, next);
            return cell;        }

        public static bool equals(intCell* a, intCell* b)
        {
            if (a == b) return true;
            while (true)
                if (a == NULL && b == NULL)
                    return true;
                else if (a != NULL && b == NULL)
                    return false;
                else if (a == NULL && b != NULL)
                    return false;
                else if ((*a).element != (*b).element)
                    return false;
                else
                  { a = (*a).next; b = (*b).next; };
        }

		public static void free(intCell** headRef)
        {
            intCell* head = *headRef;
            while (head != null)
            {
                var toFree = head;
                head = head->next;
            }
            (*headRef) = NULL;
        }



		        public static ulong count(intCell* head, int searchFor)
        {
                ulong counter = 0;
                while (head != null)
                {
                    if (head->element == searchFor) counter++;
                    head = (*head).next;
                }
                return counter;
        }

    public static ulong length(intCellCell* cell)
    {
            ulong counter = 0;
            while (cell != null)
            {
                counter++;
                cell = (*cell).next;
            }
            return counter;
        }

        public static intCell* NULL
        {
            get
            {
                return null;
            }
        }

		public static int head(intCell* forwardLinkedList)
        {
            return (*forwardLinkedList).element;
        }

		
        public static intArray toArray(intCell* abc)
        {
            var total = length(abc);
            var arr = (int*)malloc((ulong)sizeof(int) * total);
            var counter = 0;
            while (abc != null)
            {
                arr[counter] = (*abc).element;
                abc = (*abc).next;
                counter++;
            }

            return new intArray { lenght = total, index = arr };
        }

        public static intCell* tail(intCell* forwardLinkedList)
        {
            return (*forwardLinkedList).next;
        }

        public static intCell* last(intCell* forwardLinkedList)
        {
            while (true)
            {
                if ((*forwardLinkedList).next == null) return forwardLinkedList;
                forwardLinkedList = (*forwardLinkedList).next;
            }
        }

        /// <summary>
        /// Copies payload of <paramref name="cell"/> into newly allocated cell header, <paramref name="cells"/> is set <see cref="charCell.next"/>.
        /// </summary>
        /// <param name="cell"></param>
        /// <param name="cells"></param>
        /// <param name="allocator"></param>
        /// <returns></returns>
        public static intCellresult push(intCell cell, intCell* cells)
        {
            var mem = intCellAlloc(1);
            (*mem).element = cell.element;
            (*mem).next = cells;
            return new intCellresult { __value = mem };
        }


        public static CellError push_(intCell* cell, intCell* cells)
        {
            if ((*cell).next != NULL)
                return CellError.cannot_push_onto_filled_list;
            _push_(cell, cells);
            return CellError.ok;
        }

        public static void _push_(intCell* cell, intCell* cells) => (*cell).next = cells;

        public static void toCells(int[] array, intCell* memory)
        {
            intCell* previous = null;

            foreach (var item in array)
            {
                (*memory) = new intCell { element = item };
                if (previous != null)
                {
                    (*previous).next = memory;
                }

                previous = memory;

                memory++;
            }
        }
    }


    public unsafe struct longCellresult
    {
        public longCell* __value;
        public CellError error;

        public longCell* unwrap()
        {
            this.error.unwrap();
            return __value;
        }
    }

public static unsafe partial class longCellExtensions
  {
    
	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static unsafe  longCell* longCellAlloc(ulong count)
		  => (longCell*)malloc((ulong)sizeof(longCell) * count);
           
         public static unsafe  longCellCell* longCellCellAlloc(ulong count)
		  => (longCellCell*)malloc((ulong)sizeof(longCellCell) * count);


    public static ulong length(longCell* cell)
    {
            ulong counter = 0;
            while (cell != null)
            {
                counter++;
                cell = (*cell).next;
            };
            return counter;
        }

        public static void push(ref longCell* head, long newData)
            => head = newCell(newData, head);

		public static void push(longCell** head, long newData) 
            => (*head) = newCell(newData, *head);

        public static longCell* newCell(long newData)
        {
            var cell = longCellAlloc(1);
            cell->element = newData;
            cell->next = NULL;
            return cell;
        }

        public static void cellInit(ref longCell cell, long data, ref longCell next)
        {
            cell.element = data;
            fixed (longCell* ptr = &next)
            {
                cell.next = ptr;
            }
        }

        public static void cellInit(longCell* cell, long data, longCell* next)
        {
            cell->element = data;
            cell->next = next;
        }

        public static void cellInit(ref longCell cell, long data)
        {
            cell.element = data;
            cell.next = NULL;
        }

        public static void cellInit(longCell* cell, long data)
        {
            cell->element = data;
            cell->next = NULL;
        }

        public static longCell* newCell(long element, longCell* next)
        {
            var cell = longCellAlloc(1);
            cellInit(cell, element, next);
            return cell;        }

        public static bool equals(longCell* a, longCell* b)
        {
            if (a == b) return true;
            while (true)
                if (a == NULL && b == NULL)
                    return true;
                else if (a != NULL && b == NULL)
                    return false;
                else if (a == NULL && b != NULL)
                    return false;
                else if ((*a).element != (*b).element)
                    return false;
                else
                  { a = (*a).next; b = (*b).next; };
        }

		public static void free(longCell** headRef)
        {
            longCell* head = *headRef;
            while (head != null)
            {
                var toFree = head;
                head = head->next;
            }
            (*headRef) = NULL;
        }



		        public static ulong count(longCell* head, long searchFor)
        {
                ulong counter = 0;
                while (head != null)
                {
                    if (head->element == searchFor) counter++;
                    head = (*head).next;
                }
                return counter;
        }

    public static ulong length(longCellCell* cell)
    {
            ulong counter = 0;
            while (cell != null)
            {
                counter++;
                cell = (*cell).next;
            }
            return counter;
        }

        public static longCell* NULL
        {
            get
            {
                return null;
            }
        }

		public static long head(longCell* forwardLinkedList)
        {
            return (*forwardLinkedList).element;
        }

		
        public static longArray toArray(longCell* abc)
        {
            var total = length(abc);
            var arr = (long*)malloc((ulong)sizeof(long) * total);
            var counter = 0;
            while (abc != null)
            {
                arr[counter] = (*abc).element;
                abc = (*abc).next;
                counter++;
            }

            return new longArray { lenght = total, index = arr };
        }

        public static longCell* tail(longCell* forwardLinkedList)
        {
            return (*forwardLinkedList).next;
        }

        public static longCell* last(longCell* forwardLinkedList)
        {
            while (true)
            {
                if ((*forwardLinkedList).next == null) return forwardLinkedList;
                forwardLinkedList = (*forwardLinkedList).next;
            }
        }

        /// <summary>
        /// Copies payload of <paramref name="cell"/> into newly allocated cell header, <paramref name="cells"/> is set <see cref="charCell.next"/>.
        /// </summary>
        /// <param name="cell"></param>
        /// <param name="cells"></param>
        /// <param name="allocator"></param>
        /// <returns></returns>
        public static longCellresult push(longCell cell, longCell* cells)
        {
            var mem = longCellAlloc(1);
            (*mem).element = cell.element;
            (*mem).next = cells;
            return new longCellresult { __value = mem };
        }


        public static CellError push_(longCell* cell, longCell* cells)
        {
            if ((*cell).next != NULL)
                return CellError.cannot_push_onto_filled_list;
            _push_(cell, cells);
            return CellError.ok;
        }

        public static void _push_(longCell* cell, longCell* cells) => (*cell).next = cells;

        public static void toCells(long[] array, longCell* memory)
        {
            longCell* previous = null;

            foreach (var item in array)
            {
                (*memory) = new longCell { element = item };
                if (previous != null)
                {
                    (*previous).next = memory;
                }

                previous = memory;

                memory++;
            }
        }
    }


    public unsafe struct floatCellresult
    {
        public floatCell* __value;
        public CellError error;

        public floatCell* unwrap()
        {
            this.error.unwrap();
            return __value;
        }
    }

public static unsafe partial class floatCellExtensions
  {
    
	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static unsafe  floatCell* floatCellAlloc(ulong count)
		  => (floatCell*)malloc((ulong)sizeof(floatCell) * count);
           
         public static unsafe  floatCellCell* floatCellCellAlloc(ulong count)
		  => (floatCellCell*)malloc((ulong)sizeof(floatCellCell) * count);


    public static ulong length(floatCell* cell)
    {
            ulong counter = 0;
            while (cell != null)
            {
                counter++;
                cell = (*cell).next;
            };
            return counter;
        }

        public static void push(ref floatCell* head, float newData)
            => head = newCell(newData, head);

		public static void push(floatCell** head, float newData) 
            => (*head) = newCell(newData, *head);

        public static floatCell* newCell(float newData)
        {
            var cell = floatCellAlloc(1);
            cell->element = newData;
            cell->next = NULL;
            return cell;
        }

        public static void cellInit(ref floatCell cell, float data, ref floatCell next)
        {
            cell.element = data;
            fixed (floatCell* ptr = &next)
            {
                cell.next = ptr;
            }
        }

        public static void cellInit(floatCell* cell, float data, floatCell* next)
        {
            cell->element = data;
            cell->next = next;
        }

        public static void cellInit(ref floatCell cell, float data)
        {
            cell.element = data;
            cell.next = NULL;
        }

        public static void cellInit(floatCell* cell, float data)
        {
            cell->element = data;
            cell->next = NULL;
        }

        public static floatCell* newCell(float element, floatCell* next)
        {
            var cell = floatCellAlloc(1);
            cellInit(cell, element, next);
            return cell;        }

        public static bool equals(floatCell* a, floatCell* b)
        {
            if (a == b) return true;
            while (true)
                if (a == NULL && b == NULL)
                    return true;
                else if (a != NULL && b == NULL)
                    return false;
                else if (a == NULL && b != NULL)
                    return false;
                else if ((*a).element != (*b).element)
                    return false;
                else
                  { a = (*a).next; b = (*b).next; };
        }

		public static void free(floatCell** headRef)
        {
            floatCell* head = *headRef;
            while (head != null)
            {
                var toFree = head;
                head = head->next;
            }
            (*headRef) = NULL;
        }



		        public static ulong count(floatCell* head, float searchFor)
        {
                ulong counter = 0;
                while (head != null)
                {
                    if (head->element == searchFor) counter++;
                    head = (*head).next;
                }
                return counter;
        }

    public static ulong length(floatCellCell* cell)
    {
            ulong counter = 0;
            while (cell != null)
            {
                counter++;
                cell = (*cell).next;
            }
            return counter;
        }

        public static floatCell* NULL
        {
            get
            {
                return null;
            }
        }

		public static float head(floatCell* forwardLinkedList)
        {
            return (*forwardLinkedList).element;
        }

		
        public static floatArray toArray(floatCell* abc)
        {
            var total = length(abc);
            var arr = (float*)malloc((ulong)sizeof(float) * total);
            var counter = 0;
            while (abc != null)
            {
                arr[counter] = (*abc).element;
                abc = (*abc).next;
                counter++;
            }

            return new floatArray { lenght = total, index = arr };
        }

        public static floatCell* tail(floatCell* forwardLinkedList)
        {
            return (*forwardLinkedList).next;
        }

        public static floatCell* last(floatCell* forwardLinkedList)
        {
            while (true)
            {
                if ((*forwardLinkedList).next == null) return forwardLinkedList;
                forwardLinkedList = (*forwardLinkedList).next;
            }
        }

        /// <summary>
        /// Copies payload of <paramref name="cell"/> into newly allocated cell header, <paramref name="cells"/> is set <see cref="charCell.next"/>.
        /// </summary>
        /// <param name="cell"></param>
        /// <param name="cells"></param>
        /// <param name="allocator"></param>
        /// <returns></returns>
        public static floatCellresult push(floatCell cell, floatCell* cells)
        {
            var mem = floatCellAlloc(1);
            (*mem).element = cell.element;
            (*mem).next = cells;
            return new floatCellresult { __value = mem };
        }


        public static CellError push_(floatCell* cell, floatCell* cells)
        {
            if ((*cell).next != NULL)
                return CellError.cannot_push_onto_filled_list;
            _push_(cell, cells);
            return CellError.ok;
        }

        public static void _push_(floatCell* cell, floatCell* cells) => (*cell).next = cells;

        public static void toCells(float[] array, floatCell* memory)
        {
            floatCell* previous = null;

            foreach (var item in array)
            {
                (*memory) = new floatCell { element = item };
                if (previous != null)
                {
                    (*previous).next = memory;
                }

                previous = memory;

                memory++;
            }
        }
    }


    public unsafe struct doubleCellresult
    {
        public doubleCell* __value;
        public CellError error;

        public doubleCell* unwrap()
        {
            this.error.unwrap();
            return __value;
        }
    }

public static unsafe partial class doubleCellExtensions
  {
    
	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static unsafe  doubleCell* doubleCellAlloc(ulong count)
		  => (doubleCell*)malloc((ulong)sizeof(doubleCell) * count);
           
         public static unsafe  doubleCellCell* doubleCellCellAlloc(ulong count)
		  => (doubleCellCell*)malloc((ulong)sizeof(doubleCellCell) * count);


    public static ulong length(doubleCell* cell)
    {
            ulong counter = 0;
            while (cell != null)
            {
                counter++;
                cell = (*cell).next;
            };
            return counter;
        }

        public static void push(ref doubleCell* head, double newData)
            => head = newCell(newData, head);

		public static void push(doubleCell** head, double newData) 
            => (*head) = newCell(newData, *head);

        public static doubleCell* newCell(double newData)
        {
            var cell = doubleCellAlloc(1);
            cell->element = newData;
            cell->next = NULL;
            return cell;
        }

        public static void cellInit(ref doubleCell cell, double data, ref doubleCell next)
        {
            cell.element = data;
            fixed (doubleCell* ptr = &next)
            {
                cell.next = ptr;
            }
        }

        public static void cellInit(doubleCell* cell, double data, doubleCell* next)
        {
            cell->element = data;
            cell->next = next;
        }

        public static void cellInit(ref doubleCell cell, double data)
        {
            cell.element = data;
            cell.next = NULL;
        }

        public static void cellInit(doubleCell* cell, double data)
        {
            cell->element = data;
            cell->next = NULL;
        }

        public static doubleCell* newCell(double element, doubleCell* next)
        {
            var cell = doubleCellAlloc(1);
            cellInit(cell, element, next);
            return cell;        }

        public static bool equals(doubleCell* a, doubleCell* b)
        {
            if (a == b) return true;
            while (true)
                if (a == NULL && b == NULL)
                    return true;
                else if (a != NULL && b == NULL)
                    return false;
                else if (a == NULL && b != NULL)
                    return false;
                else if ((*a).element != (*b).element)
                    return false;
                else
                  { a = (*a).next; b = (*b).next; };
        }

		public static void free(doubleCell** headRef)
        {
            doubleCell* head = *headRef;
            while (head != null)
            {
                var toFree = head;
                head = head->next;
            }
            (*headRef) = NULL;
        }



		        public static ulong count(doubleCell* head, double searchFor)
        {
                ulong counter = 0;
                while (head != null)
                {
                    if (head->element == searchFor) counter++;
                    head = (*head).next;
                }
                return counter;
        }

    public static ulong length(doubleCellCell* cell)
    {
            ulong counter = 0;
            while (cell != null)
            {
                counter++;
                cell = (*cell).next;
            }
            return counter;
        }

        public static doubleCell* NULL
        {
            get
            {
                return null;
            }
        }

		public static double head(doubleCell* forwardLinkedList)
        {
            return (*forwardLinkedList).element;
        }

		
        public static doubleArray toArray(doubleCell* abc)
        {
            var total = length(abc);
            var arr = (double*)malloc((ulong)sizeof(double) * total);
            var counter = 0;
            while (abc != null)
            {
                arr[counter] = (*abc).element;
                abc = (*abc).next;
                counter++;
            }

            return new doubleArray { lenght = total, index = arr };
        }

        public static doubleCell* tail(doubleCell* forwardLinkedList)
        {
            return (*forwardLinkedList).next;
        }

        public static doubleCell* last(doubleCell* forwardLinkedList)
        {
            while (true)
            {
                if ((*forwardLinkedList).next == null) return forwardLinkedList;
                forwardLinkedList = (*forwardLinkedList).next;
            }
        }

        /// <summary>
        /// Copies payload of <paramref name="cell"/> into newly allocated cell header, <paramref name="cells"/> is set <see cref="charCell.next"/>.
        /// </summary>
        /// <param name="cell"></param>
        /// <param name="cells"></param>
        /// <param name="allocator"></param>
        /// <returns></returns>
        public static doubleCellresult push(doubleCell cell, doubleCell* cells)
        {
            var mem = doubleCellAlloc(1);
            (*mem).element = cell.element;
            (*mem).next = cells;
            return new doubleCellresult { __value = mem };
        }


        public static CellError push_(doubleCell* cell, doubleCell* cells)
        {
            if ((*cell).next != NULL)
                return CellError.cannot_push_onto_filled_list;
            _push_(cell, cells);
            return CellError.ok;
        }

        public static void _push_(doubleCell* cell, doubleCell* cells) => (*cell).next = cells;

        public static void toCells(double[] array, doubleCell* memory)
        {
            doubleCell* previous = null;

            foreach (var item in array)
            {
                (*memory) = new doubleCell { element = item };
                if (previous != null)
                {
                    (*previous).next = memory;
                }

                previous = memory;

                memory++;
            }
        }
    }


    public unsafe struct decimalCellresult
    {
        public decimalCell* __value;
        public CellError error;

        public decimalCell* unwrap()
        {
            this.error.unwrap();
            return __value;
        }
    }

public static unsafe partial class decimalCellExtensions
  {
    
	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static unsafe  decimalCell* decimalCellAlloc(ulong count)
		  => (decimalCell*)malloc((ulong)sizeof(decimalCell) * count);
           
         public static unsafe  decimalCellCell* decimalCellCellAlloc(ulong count)
		  => (decimalCellCell*)malloc((ulong)sizeof(decimalCellCell) * count);


    public static ulong length(decimalCell* cell)
    {
            ulong counter = 0;
            while (cell != null)
            {
                counter++;
                cell = (*cell).next;
            };
            return counter;
        }

        public static void push(ref decimalCell* head, decimal newData)
            => head = newCell(newData, head);

		public static void push(decimalCell** head, decimal newData) 
            => (*head) = newCell(newData, *head);

        public static decimalCell* newCell(decimal newData)
        {
            var cell = decimalCellAlloc(1);
            cell->element = newData;
            cell->next = NULL;
            return cell;
        }

        public static void cellInit(ref decimalCell cell, decimal data, ref decimalCell next)
        {
            cell.element = data;
            fixed (decimalCell* ptr = &next)
            {
                cell.next = ptr;
            }
        }

        public static void cellInit(decimalCell* cell, decimal data, decimalCell* next)
        {
            cell->element = data;
            cell->next = next;
        }

        public static void cellInit(ref decimalCell cell, decimal data)
        {
            cell.element = data;
            cell.next = NULL;
        }

        public static void cellInit(decimalCell* cell, decimal data)
        {
            cell->element = data;
            cell->next = NULL;
        }

        public static decimalCell* newCell(decimal element, decimalCell* next)
        {
            var cell = decimalCellAlloc(1);
            cellInit(cell, element, next);
            return cell;        }

        public static bool equals(decimalCell* a, decimalCell* b)
        {
            if (a == b) return true;
            while (true)
                if (a == NULL && b == NULL)
                    return true;
                else if (a != NULL && b == NULL)
                    return false;
                else if (a == NULL && b != NULL)
                    return false;
                else if ((*a).element != (*b).element)
                    return false;
                else
                  { a = (*a).next; b = (*b).next; };
        }

		public static void free(decimalCell** headRef)
        {
            decimalCell* head = *headRef;
            while (head != null)
            {
                var toFree = head;
                head = head->next;
            }
            (*headRef) = NULL;
        }



		        public static ulong count(decimalCell* head, decimal searchFor)
        {
                ulong counter = 0;
                while (head != null)
                {
                    if (head->element == searchFor) counter++;
                    head = (*head).next;
                }
                return counter;
        }

    public static ulong length(decimalCellCell* cell)
    {
            ulong counter = 0;
            while (cell != null)
            {
                counter++;
                cell = (*cell).next;
            }
            return counter;
        }

        public static decimalCell* NULL
        {
            get
            {
                return null;
            }
        }

		public static decimal head(decimalCell* forwardLinkedList)
        {
            return (*forwardLinkedList).element;
        }

		
        public static decimalArray toArray(decimalCell* abc)
        {
            var total = length(abc);
            var arr = (decimal*)malloc((ulong)sizeof(decimal) * total);
            var counter = 0;
            while (abc != null)
            {
                arr[counter] = (*abc).element;
                abc = (*abc).next;
                counter++;
            }

            return new decimalArray { lenght = total, index = arr };
        }

        public static decimalCell* tail(decimalCell* forwardLinkedList)
        {
            return (*forwardLinkedList).next;
        }

        public static decimalCell* last(decimalCell* forwardLinkedList)
        {
            while (true)
            {
                if ((*forwardLinkedList).next == null) return forwardLinkedList;
                forwardLinkedList = (*forwardLinkedList).next;
            }
        }

        /// <summary>
        /// Copies payload of <paramref name="cell"/> into newly allocated cell header, <paramref name="cells"/> is set <see cref="charCell.next"/>.
        /// </summary>
        /// <param name="cell"></param>
        /// <param name="cells"></param>
        /// <param name="allocator"></param>
        /// <returns></returns>
        public static decimalCellresult push(decimalCell cell, decimalCell* cells)
        {
            var mem = decimalCellAlloc(1);
            (*mem).element = cell.element;
            (*mem).next = cells;
            return new decimalCellresult { __value = mem };
        }


        public static CellError push_(decimalCell* cell, decimalCell* cells)
        {
            if ((*cell).next != NULL)
                return CellError.cannot_push_onto_filled_list;
            _push_(cell, cells);
            return CellError.ok;
        }

        public static void _push_(decimalCell* cell, decimalCell* cells) => (*cell).next = cells;

        public static void toCells(decimal[] array, decimalCell* memory)
        {
            decimalCell* previous = null;

            foreach (var item in array)
            {
                (*memory) = new decimalCell { element = item };
                if (previous != null)
                {
                    (*previous).next = memory;
                }

                previous = memory;

                memory++;
            }
        }
    }


    public unsafe struct BigIntegerCellresult
    {
        public BigIntegerCell* __value;
        public CellError error;

        public BigIntegerCell* unwrap()
        {
            this.error.unwrap();
            return __value;
        }
    }

public static unsafe partial class BigIntegerCellExtensions
  {
    
	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static unsafe  BigIntegerCell* BigIntegerCellAlloc(ulong count)
		  => (BigIntegerCell*)malloc((ulong)sizeof(BigIntegerCell) * count);
           
         public static unsafe  BigIntegerCellCell* BigIntegerCellCellAlloc(ulong count)
		  => (BigIntegerCellCell*)malloc((ulong)sizeof(BigIntegerCellCell) * count);


    public static ulong length(BigIntegerCell* cell)
    {
            ulong counter = 0;
            while (cell != null)
            {
                counter++;
                cell = (*cell).next;
            };
            return counter;
        }

        public static void push(ref BigIntegerCell* head, BigInteger newData)
            => head = newCell(newData, head);

		public static void push(BigIntegerCell** head, BigInteger newData) 
            => (*head) = newCell(newData, *head);

        public static BigIntegerCell* newCell(BigInteger newData)
        {
            var cell = BigIntegerCellAlloc(1);
            cell->element = newData;
            cell->next = NULL;
            return cell;
        }

        public static void cellInit(ref BigIntegerCell cell, BigInteger data, ref BigIntegerCell next)
        {
            cell.element = data;
            fixed (BigIntegerCell* ptr = &next)
            {
                cell.next = ptr;
            }
        }

        public static void cellInit(BigIntegerCell* cell, BigInteger data, BigIntegerCell* next)
        {
            cell->element = data;
            cell->next = next;
        }

        public static void cellInit(ref BigIntegerCell cell, BigInteger data)
        {
            cell.element = data;
            cell.next = NULL;
        }

        public static void cellInit(BigIntegerCell* cell, BigInteger data)
        {
            cell->element = data;
            cell->next = NULL;
        }

        public static BigIntegerCell* newCell(BigInteger element, BigIntegerCell* next)
        {
            var cell = BigIntegerCellAlloc(1);
            cellInit(cell, element, next);
            return cell;        }

        public static bool equals(BigIntegerCell* a, BigIntegerCell* b)
        {
            if (a == b) return true;
            while (true)
                if (a == NULL && b == NULL)
                    return true;
                else if (a != NULL && b == NULL)
                    return false;
                else if (a == NULL && b != NULL)
                    return false;
                else if ((*a).element != (*b).element)
                    return false;
                else
                  { a = (*a).next; b = (*b).next; };
        }

		public static void free(BigIntegerCell** headRef)
        {
            BigIntegerCell* head = *headRef;
            while (head != null)
            {
                var toFree = head;
                head = head->next;
            }
            (*headRef) = NULL;
        }



		        public static ulong count(BigIntegerCell* head, BigInteger searchFor)
        {
                ulong counter = 0;
                while (head != null)
                {
                    if (head->element == searchFor) counter++;
                    head = (*head).next;
                }
                return counter;
        }

    public static ulong length(BigIntegerCellCell* cell)
    {
            ulong counter = 0;
            while (cell != null)
            {
                counter++;
                cell = (*cell).next;
            }
            return counter;
        }

        public static BigIntegerCell* NULL
        {
            get
            {
                return null;
            }
        }

		public static BigInteger head(BigIntegerCell* forwardLinkedList)
        {
            return (*forwardLinkedList).element;
        }

		
        public static BigIntegerArray toArray(BigIntegerCell* abc)
        {
            var total = length(abc);
            var arr = (BigInteger*)malloc((ulong)sizeof(BigInteger) * total);
            var counter = 0;
            while (abc != null)
            {
                arr[counter] = (*abc).element;
                abc = (*abc).next;
                counter++;
            }

            return new BigIntegerArray { lenght = total, index = arr };
        }

        public static BigIntegerCell* tail(BigIntegerCell* forwardLinkedList)
        {
            return (*forwardLinkedList).next;
        }

        public static BigIntegerCell* last(BigIntegerCell* forwardLinkedList)
        {
            while (true)
            {
                if ((*forwardLinkedList).next == null) return forwardLinkedList;
                forwardLinkedList = (*forwardLinkedList).next;
            }
        }

        /// <summary>
        /// Copies payload of <paramref name="cell"/> into newly allocated cell header, <paramref name="cells"/> is set <see cref="charCell.next"/>.
        /// </summary>
        /// <param name="cell"></param>
        /// <param name="cells"></param>
        /// <param name="allocator"></param>
        /// <returns></returns>
        public static BigIntegerCellresult push(BigIntegerCell cell, BigIntegerCell* cells)
        {
            var mem = BigIntegerCellAlloc(1);
            (*mem).element = cell.element;
            (*mem).next = cells;
            return new BigIntegerCellresult { __value = mem };
        }


        public static CellError push_(BigIntegerCell* cell, BigIntegerCell* cells)
        {
            if ((*cell).next != NULL)
                return CellError.cannot_push_onto_filled_list;
            _push_(cell, cells);
            return CellError.ok;
        }

        public static void _push_(BigIntegerCell* cell, BigIntegerCell* cells) => (*cell).next = cells;

        public static void toCells(BigInteger[] array, BigIntegerCell* memory)
        {
            BigIntegerCell* previous = null;

            foreach (var item in array)
            {
                (*memory) = new BigIntegerCell { element = item };
                if (previous != null)
                {
                    (*previous).next = memory;
                }

                previous = memory;

                memory++;
            }
        }
    }


    public unsafe struct ComplexCellresult
    {
        public ComplexCell* __value;
        public CellError error;

        public ComplexCell* unwrap()
        {
            this.error.unwrap();
            return __value;
        }
    }

public static unsafe partial class ComplexCellExtensions
  {
    
	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static unsafe  ComplexCell* ComplexCellAlloc(ulong count)
		  => (ComplexCell*)malloc((ulong)sizeof(ComplexCell) * count);
           
         public static unsafe  ComplexCellCell* ComplexCellCellAlloc(ulong count)
		  => (ComplexCellCell*)malloc((ulong)sizeof(ComplexCellCell) * count);


    public static ulong length(ComplexCell* cell)
    {
            ulong counter = 0;
            while (cell != null)
            {
                counter++;
                cell = (*cell).next;
            };
            return counter;
        }

        public static void push(ref ComplexCell* head, Complex newData)
            => head = newCell(newData, head);

		public static void push(ComplexCell** head, Complex newData) 
            => (*head) = newCell(newData, *head);

        public static ComplexCell* newCell(Complex newData)
        {
            var cell = ComplexCellAlloc(1);
            cell->element = newData;
            cell->next = NULL;
            return cell;
        }

        public static void cellInit(ref ComplexCell cell, Complex data, ref ComplexCell next)
        {
            cell.element = data;
            fixed (ComplexCell* ptr = &next)
            {
                cell.next = ptr;
            }
        }

        public static void cellInit(ComplexCell* cell, Complex data, ComplexCell* next)
        {
            cell->element = data;
            cell->next = next;
        }

        public static void cellInit(ref ComplexCell cell, Complex data)
        {
            cell.element = data;
            cell.next = NULL;
        }

        public static void cellInit(ComplexCell* cell, Complex data)
        {
            cell->element = data;
            cell->next = NULL;
        }

        public static ComplexCell* newCell(Complex element, ComplexCell* next)
        {
            var cell = ComplexCellAlloc(1);
            cellInit(cell, element, next);
            return cell;        }

        public static bool equals(ComplexCell* a, ComplexCell* b)
        {
            if (a == b) return true;
            while (true)
                if (a == NULL && b == NULL)
                    return true;
                else if (a != NULL && b == NULL)
                    return false;
                else if (a == NULL && b != NULL)
                    return false;
                else if ((*a).element != (*b).element)
                    return false;
                else
                  { a = (*a).next; b = (*b).next; };
        }

		public static void free(ComplexCell** headRef)
        {
            ComplexCell* head = *headRef;
            while (head != null)
            {
                var toFree = head;
                head = head->next;
            }
            (*headRef) = NULL;
        }



		        public static ulong count(ComplexCell* head, Complex searchFor)
        {
                ulong counter = 0;
                while (head != null)
                {
                    if (head->element == searchFor) counter++;
                    head = (*head).next;
                }
                return counter;
        }

    public static ulong length(ComplexCellCell* cell)
    {
            ulong counter = 0;
            while (cell != null)
            {
                counter++;
                cell = (*cell).next;
            }
            return counter;
        }

        public static ComplexCell* NULL
        {
            get
            {
                return null;
            }
        }

		public static Complex head(ComplexCell* forwardLinkedList)
        {
            return (*forwardLinkedList).element;
        }

		
        public static ComplexArray toArray(ComplexCell* abc)
        {
            var total = length(abc);
            var arr = (Complex*)malloc((ulong)sizeof(Complex) * total);
            var counter = 0;
            while (abc != null)
            {
                arr[counter] = (*abc).element;
                abc = (*abc).next;
                counter++;
            }

            return new ComplexArray { lenght = total, index = arr };
        }

        public static ComplexCell* tail(ComplexCell* forwardLinkedList)
        {
            return (*forwardLinkedList).next;
        }

        public static ComplexCell* last(ComplexCell* forwardLinkedList)
        {
            while (true)
            {
                if ((*forwardLinkedList).next == null) return forwardLinkedList;
                forwardLinkedList = (*forwardLinkedList).next;
            }
        }

        /// <summary>
        /// Copies payload of <paramref name="cell"/> into newly allocated cell header, <paramref name="cells"/> is set <see cref="charCell.next"/>.
        /// </summary>
        /// <param name="cell"></param>
        /// <param name="cells"></param>
        /// <param name="allocator"></param>
        /// <returns></returns>
        public static ComplexCellresult push(ComplexCell cell, ComplexCell* cells)
        {
            var mem = ComplexCellAlloc(1);
            (*mem).element = cell.element;
            (*mem).next = cells;
            return new ComplexCellresult { __value = mem };
        }


        public static CellError push_(ComplexCell* cell, ComplexCell* cells)
        {
            if ((*cell).next != NULL)
                return CellError.cannot_push_onto_filled_list;
            _push_(cell, cells);
            return CellError.ok;
        }

        public static void _push_(ComplexCell* cell, ComplexCell* cells) => (*cell).next = cells;

        public static void toCells(Complex[] array, ComplexCell* memory)
        {
            ComplexCell* previous = null;

            foreach (var item in array)
            {
                (*memory) = new ComplexCell { element = item };
                if (previous != null)
                {
                    (*previous).next = memory;
                }

                previous = memory;

                memory++;
            }
        }
    }


    public unsafe struct charCellresult
    {
        public charCell* __value;
        public CellError error;

        public charCell* unwrap()
        {
            this.error.unwrap();
            return __value;
        }
    }

public static unsafe partial class charCellExtensions
  {
    
	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static unsafe  charCell* charCellAlloc(ulong count)
		  => (charCell*)malloc((ulong)sizeof(charCell) * count);
           
         public static unsafe  charCellCell* charCellCellAlloc(ulong count)
		  => (charCellCell*)malloc((ulong)sizeof(charCellCell) * count);


    public static ulong length(charCell* cell)
    {
            ulong counter = 0;
            while (cell != null)
            {
                counter++;
                cell = (*cell).next;
            };
            return counter;
        }

        public static void push(ref charCell* head, char newData)
            => head = newCell(newData, head);

		public static void push(charCell** head, char newData) 
            => (*head) = newCell(newData, *head);

        public static charCell* newCell(char newData)
        {
            var cell = charCellAlloc(1);
            cell->element = newData;
            cell->next = NULL;
            return cell;
        }

        public static void cellInit(ref charCell cell, char data, ref charCell next)
        {
            cell.element = data;
            fixed (charCell* ptr = &next)
            {
                cell.next = ptr;
            }
        }

        public static void cellInit(charCell* cell, char data, charCell* next)
        {
            cell->element = data;
            cell->next = next;
        }

        public static void cellInit(ref charCell cell, char data)
        {
            cell.element = data;
            cell.next = NULL;
        }

        public static void cellInit(charCell* cell, char data)
        {
            cell->element = data;
            cell->next = NULL;
        }

        public static charCell* newCell(char element, charCell* next)
        {
            var cell = charCellAlloc(1);
            cellInit(cell, element, next);
            return cell;        }

        public static bool equals(charCell* a, charCell* b)
        {
            if (a == b) return true;
            while (true)
                if (a == NULL && b == NULL)
                    return true;
                else if (a != NULL && b == NULL)
                    return false;
                else if (a == NULL && b != NULL)
                    return false;
                else if ((*a).element != (*b).element)
                    return false;
                else
                  { a = (*a).next; b = (*b).next; };
        }

		public static void free(charCell** headRef)
        {
            charCell* head = *headRef;
            while (head != null)
            {
                var toFree = head;
                head = head->next;
            }
            (*headRef) = NULL;
        }



		        public static ulong count(charCell* head, char searchFor)
        {
                ulong counter = 0;
                while (head != null)
                {
                    if (head->element == searchFor) counter++;
                    head = (*head).next;
                }
                return counter;
        }

    public static ulong length(charCellCell* cell)
    {
            ulong counter = 0;
            while (cell != null)
            {
                counter++;
                cell = (*cell).next;
            }
            return counter;
        }

        public static charCell* NULL
        {
            get
            {
                return null;
            }
        }

		public static char head(charCell* forwardLinkedList)
        {
            return (*forwardLinkedList).element;
        }

		
        public static charArray toArray(charCell* abc)
        {
            var total = length(abc);
            var arr = (char*)malloc((ulong)sizeof(char) * total);
            var counter = 0;
            while (abc != null)
            {
                arr[counter] = (*abc).element;
                abc = (*abc).next;
                counter++;
            }

            return new charArray { lenght = total, index = arr };
        }

        public static charCell* tail(charCell* forwardLinkedList)
        {
            return (*forwardLinkedList).next;
        }

        public static charCell* last(charCell* forwardLinkedList)
        {
            while (true)
            {
                if ((*forwardLinkedList).next == null) return forwardLinkedList;
                forwardLinkedList = (*forwardLinkedList).next;
            }
        }

        /// <summary>
        /// Copies payload of <paramref name="cell"/> into newly allocated cell header, <paramref name="cells"/> is set <see cref="charCell.next"/>.
        /// </summary>
        /// <param name="cell"></param>
        /// <param name="cells"></param>
        /// <param name="allocator"></param>
        /// <returns></returns>
        public static charCellresult push(charCell cell, charCell* cells)
        {
            var mem = charCellAlloc(1);
            (*mem).element = cell.element;
            (*mem).next = cells;
            return new charCellresult { __value = mem };
        }


        public static CellError push_(charCell* cell, charCell* cells)
        {
            if ((*cell).next != NULL)
                return CellError.cannot_push_onto_filled_list;
            _push_(cell, cells);
            return CellError.ok;
        }

        public static void _push_(charCell* cell, charCell* cells) => (*cell).next = cells;

        public static void toCells(char[] array, charCell* memory)
        {
            charCell* previous = null;

            foreach (var item in array)
            {
                (*memory) = new charCell { element = item };
                if (previous != null)
                {
                    (*previous).next = memory;
                }

                previous = memory;

                memory++;
            }
        }
    }
}
}

