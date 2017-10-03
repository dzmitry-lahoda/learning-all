  
using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;

namespace GreenElephant.Usafe
{

namespace bit16
{

    public unsafe partial interface IAllocator
    {
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        void** Apply(ushort count);
    }



  public unsafe  struct intCell
  {
    public int element;
    public intCell* next;
  }

  public unsafe  struct intCellCell
  {
    public intCell element;
    public intCellCell* next;
  }

  	public unsafe struct intArray
    {
        public ushort lenght;
        public int* index;
    }

  public unsafe  struct longCell
  {
    public long element;
    public longCell* next;
  }

  public unsafe  struct longCellCell
  {
    public longCell element;
    public longCellCell* next;
  }

  	public unsafe struct longArray
    {
        public ushort lenght;
        public long* index;
    }

  public unsafe  struct floatCell
  {
    public float element;
    public floatCell* next;
  }

  public unsafe  struct floatCellCell
  {
    public floatCell element;
    public floatCellCell* next;
  }

  	public unsafe struct floatArray
    {
        public ushort lenght;
        public float* index;
    }

  public unsafe  struct doubleCell
  {
    public double element;
    public doubleCell* next;
  }

  public unsafe  struct doubleCellCell
  {
    public doubleCell element;
    public doubleCellCell* next;
  }

  	public unsafe struct doubleArray
    {
        public ushort lenght;
        public double* index;
    }

  public unsafe  struct decimalCell
  {
    public decimal element;
    public decimalCell* next;
  }

  public unsafe  struct decimalCellCell
  {
    public decimalCell element;
    public decimalCellCell* next;
  }

  	public unsafe struct decimalArray
    {
        public ushort lenght;
        public decimal* index;
    }

  public unsafe  struct BigIntegerCell
  {
    public BigInteger element;
    public BigIntegerCell* next;
  }

  public unsafe  struct BigIntegerCellCell
  {
    public BigIntegerCell element;
    public BigIntegerCellCell* next;
  }

  	public unsafe struct BigIntegerArray
    {
        public ushort lenght;
        public BigInteger* index;
    }

  public unsafe  struct ComplexCell
  {
    public Complex element;
    public ComplexCell* next;
  }

  public unsafe  struct ComplexCellCell
  {
    public ComplexCell element;
    public ComplexCellCell* next;
  }

  	public unsafe struct ComplexArray
    {
        public ushort lenght;
        public Complex* index;
    }

  public unsafe  struct charCell
  {
    public char element;
    public charCell* next;
  }

  public unsafe  struct charCellCell
  {
    public charCell element;
    public charCellCell* next;
  }

  	public unsafe struct charArray
    {
        public ushort lenght;
        public char* index;
    }




        public unsafe partial interface IintCellAllocator
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            intCell* Apply(ushort count);
        }

		public unsafe partial interface IintCellCellAllocator
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            intCellCell* Apply(ushort count);
        }

public unsafe partial class intCellExtensions
  {
    public ushort length(intCell* cell)
    {
            ushort counter = 0;
            while (cell != null)
            {
                counter++;
                cell = (*cell).next;
            };
            return counter;
        }

        public intCell* empty
        {
            get
            {
                return null;
            }
        }

		public int head(intCell* forwardLinkedList)
        {
            return (*forwardLinkedList).element;
        }

		
        public intArray toArray(intCell* abc, IAllocator allocator)
        {
            var total = this.length(abc);
            var arr = (int*)allocator.Apply(total);
            var counter = 0;
            while (abc != null)
            {
                arr[counter] = (*abc).element;
                abc = (*abc).next;
                counter++;
            }

            return new intArray { lenght = total, index = arr };
        }

        public intCell* tail(intCell* forwardLinkedList)
        {
            return (*forwardLinkedList).next;
        }

        public intCell* last(intCell* forwardLinkedList)
        {
            while (true)
            {
                if ((*forwardLinkedList).next == null) return forwardLinkedList;
                forwardLinkedList = (*forwardLinkedList).next;
            }
        }

        public void toCells(int[] array, intCell* memory)
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
        public unsafe partial interface IlongCellAllocator
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            longCell* Apply(ushort count);
        }

		public unsafe partial interface IlongCellCellAllocator
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            longCellCell* Apply(ushort count);
        }

public unsafe partial class longCellExtensions
  {
    public ushort length(longCell* cell)
    {
            ushort counter = 0;
            while (cell != null)
            {
                counter++;
                cell = (*cell).next;
            };
            return counter;
        }

        public longCell* empty
        {
            get
            {
                return null;
            }
        }

		public long head(longCell* forwardLinkedList)
        {
            return (*forwardLinkedList).element;
        }

		
        public longArray toArray(longCell* abc, IAllocator allocator)
        {
            var total = this.length(abc);
            var arr = (long*)allocator.Apply(total);
            var counter = 0;
            while (abc != null)
            {
                arr[counter] = (*abc).element;
                abc = (*abc).next;
                counter++;
            }

            return new longArray { lenght = total, index = arr };
        }

        public longCell* tail(longCell* forwardLinkedList)
        {
            return (*forwardLinkedList).next;
        }

        public longCell* last(longCell* forwardLinkedList)
        {
            while (true)
            {
                if ((*forwardLinkedList).next == null) return forwardLinkedList;
                forwardLinkedList = (*forwardLinkedList).next;
            }
        }

        public void toCells(long[] array, longCell* memory)
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
        public unsafe partial interface IfloatCellAllocator
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            floatCell* Apply(ushort count);
        }

		public unsafe partial interface IfloatCellCellAllocator
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            floatCellCell* Apply(ushort count);
        }

public unsafe partial class floatCellExtensions
  {
    public ushort length(floatCell* cell)
    {
            ushort counter = 0;
            while (cell != null)
            {
                counter++;
                cell = (*cell).next;
            };
            return counter;
        }

        public floatCell* empty
        {
            get
            {
                return null;
            }
        }

		public float head(floatCell* forwardLinkedList)
        {
            return (*forwardLinkedList).element;
        }

		
        public floatArray toArray(floatCell* abc, IAllocator allocator)
        {
            var total = this.length(abc);
            var arr = (float*)allocator.Apply(total);
            var counter = 0;
            while (abc != null)
            {
                arr[counter] = (*abc).element;
                abc = (*abc).next;
                counter++;
            }

            return new floatArray { lenght = total, index = arr };
        }

        public floatCell* tail(floatCell* forwardLinkedList)
        {
            return (*forwardLinkedList).next;
        }

        public floatCell* last(floatCell* forwardLinkedList)
        {
            while (true)
            {
                if ((*forwardLinkedList).next == null) return forwardLinkedList;
                forwardLinkedList = (*forwardLinkedList).next;
            }
        }

        public void toCells(float[] array, floatCell* memory)
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
        public unsafe partial interface IdoubleCellAllocator
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            doubleCell* Apply(ushort count);
        }

		public unsafe partial interface IdoubleCellCellAllocator
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            doubleCellCell* Apply(ushort count);
        }

public unsafe partial class doubleCellExtensions
  {
    public ushort length(doubleCell* cell)
    {
            ushort counter = 0;
            while (cell != null)
            {
                counter++;
                cell = (*cell).next;
            };
            return counter;
        }

        public doubleCell* empty
        {
            get
            {
                return null;
            }
        }

		public double head(doubleCell* forwardLinkedList)
        {
            return (*forwardLinkedList).element;
        }

		
        public doubleArray toArray(doubleCell* abc, IAllocator allocator)
        {
            var total = this.length(abc);
            var arr = (double*)allocator.Apply(total);
            var counter = 0;
            while (abc != null)
            {
                arr[counter] = (*abc).element;
                abc = (*abc).next;
                counter++;
            }

            return new doubleArray { lenght = total, index = arr };
        }

        public doubleCell* tail(doubleCell* forwardLinkedList)
        {
            return (*forwardLinkedList).next;
        }

        public doubleCell* last(doubleCell* forwardLinkedList)
        {
            while (true)
            {
                if ((*forwardLinkedList).next == null) return forwardLinkedList;
                forwardLinkedList = (*forwardLinkedList).next;
            }
        }

        public void toCells(double[] array, doubleCell* memory)
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
        public unsafe partial interface IdecimalCellAllocator
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            decimalCell* Apply(ushort count);
        }

		public unsafe partial interface IdecimalCellCellAllocator
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            decimalCellCell* Apply(ushort count);
        }

public unsafe partial class decimalCellExtensions
  {
    public ushort length(decimalCell* cell)
    {
            ushort counter = 0;
            while (cell != null)
            {
                counter++;
                cell = (*cell).next;
            };
            return counter;
        }

        public decimalCell* empty
        {
            get
            {
                return null;
            }
        }

		public decimal head(decimalCell* forwardLinkedList)
        {
            return (*forwardLinkedList).element;
        }

		
        public decimalArray toArray(decimalCell* abc, IAllocator allocator)
        {
            var total = this.length(abc);
            var arr = (decimal*)allocator.Apply(total);
            var counter = 0;
            while (abc != null)
            {
                arr[counter] = (*abc).element;
                abc = (*abc).next;
                counter++;
            }

            return new decimalArray { lenght = total, index = arr };
        }

        public decimalCell* tail(decimalCell* forwardLinkedList)
        {
            return (*forwardLinkedList).next;
        }

        public decimalCell* last(decimalCell* forwardLinkedList)
        {
            while (true)
            {
                if ((*forwardLinkedList).next == null) return forwardLinkedList;
                forwardLinkedList = (*forwardLinkedList).next;
            }
        }

        public void toCells(decimal[] array, decimalCell* memory)
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
        public unsafe partial interface IBigIntegerCellAllocator
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            BigIntegerCell* Apply(ushort count);
        }

		public unsafe partial interface IBigIntegerCellCellAllocator
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            BigIntegerCellCell* Apply(ushort count);
        }

public unsafe partial class BigIntegerCellExtensions
  {
    public ushort length(BigIntegerCell* cell)
    {
            ushort counter = 0;
            while (cell != null)
            {
                counter++;
                cell = (*cell).next;
            };
            return counter;
        }

        public BigIntegerCell* empty
        {
            get
            {
                return null;
            }
        }

		public BigInteger head(BigIntegerCell* forwardLinkedList)
        {
            return (*forwardLinkedList).element;
        }

		
        public BigIntegerArray toArray(BigIntegerCell* abc, IAllocator allocator)
        {
            var total = this.length(abc);
            var arr = (BigInteger*)allocator.Apply(total);
            var counter = 0;
            while (abc != null)
            {
                arr[counter] = (*abc).element;
                abc = (*abc).next;
                counter++;
            }

            return new BigIntegerArray { lenght = total, index = arr };
        }

        public BigIntegerCell* tail(BigIntegerCell* forwardLinkedList)
        {
            return (*forwardLinkedList).next;
        }

        public BigIntegerCell* last(BigIntegerCell* forwardLinkedList)
        {
            while (true)
            {
                if ((*forwardLinkedList).next == null) return forwardLinkedList;
                forwardLinkedList = (*forwardLinkedList).next;
            }
        }

        public void toCells(BigInteger[] array, BigIntegerCell* memory)
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
        public unsafe partial interface IComplexCellAllocator
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            ComplexCell* Apply(ushort count);
        }

		public unsafe partial interface IComplexCellCellAllocator
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            ComplexCellCell* Apply(ushort count);
        }

public unsafe partial class ComplexCellExtensions
  {
    public ushort length(ComplexCell* cell)
    {
            ushort counter = 0;
            while (cell != null)
            {
                counter++;
                cell = (*cell).next;
            };
            return counter;
        }

        public ComplexCell* empty
        {
            get
            {
                return null;
            }
        }

		public Complex head(ComplexCell* forwardLinkedList)
        {
            return (*forwardLinkedList).element;
        }

		
        public ComplexArray toArray(ComplexCell* abc, IAllocator allocator)
        {
            var total = this.length(abc);
            var arr = (Complex*)allocator.Apply(total);
            var counter = 0;
            while (abc != null)
            {
                arr[counter] = (*abc).element;
                abc = (*abc).next;
                counter++;
            }

            return new ComplexArray { lenght = total, index = arr };
        }

        public ComplexCell* tail(ComplexCell* forwardLinkedList)
        {
            return (*forwardLinkedList).next;
        }

        public ComplexCell* last(ComplexCell* forwardLinkedList)
        {
            while (true)
            {
                if ((*forwardLinkedList).next == null) return forwardLinkedList;
                forwardLinkedList = (*forwardLinkedList).next;
            }
        }

        public void toCells(Complex[] array, ComplexCell* memory)
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
        public unsafe partial interface IcharCellAllocator
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            charCell* Apply(ushort count);
        }

		public unsafe partial interface IcharCellCellAllocator
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            charCellCell* Apply(ushort count);
        }

public unsafe partial class charCellExtensions
  {
    public ushort length(charCell* cell)
    {
            ushort counter = 0;
            while (cell != null)
            {
                counter++;
                cell = (*cell).next;
            };
            return counter;
        }

        public charCell* empty
        {
            get
            {
                return null;
            }
        }

		public char head(charCell* forwardLinkedList)
        {
            return (*forwardLinkedList).element;
        }

		
        public charArray toArray(charCell* abc, IAllocator allocator)
        {
            var total = this.length(abc);
            var arr = (char*)allocator.Apply(total);
            var counter = 0;
            while (abc != null)
            {
                arr[counter] = (*abc).element;
                abc = (*abc).next;
                counter++;
            }

            return new charArray { lenght = total, index = arr };
        }

        public charCell* tail(charCell* forwardLinkedList)
        {
            return (*forwardLinkedList).next;
        }

        public charCell* last(charCell* forwardLinkedList)
        {
            while (true)
            {
                if ((*forwardLinkedList).next == null) return forwardLinkedList;
                forwardLinkedList = (*forwardLinkedList).next;
            }
        }

        public void toCells(char[] array, charCell* memory)
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

    public unsafe partial interface IAllocator
    {
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        void** Apply(uint count);
    }



  public unsafe  struct intCell
  {
    public int element;
    public intCell* next;
  }

  public unsafe  struct intCellCell
  {
    public intCell element;
    public intCellCell* next;
  }

  	public unsafe struct intArray
    {
        public uint lenght;
        public int* index;
    }

  public unsafe  struct longCell
  {
    public long element;
    public longCell* next;
  }

  public unsafe  struct longCellCell
  {
    public longCell element;
    public longCellCell* next;
  }

  	public unsafe struct longArray
    {
        public uint lenght;
        public long* index;
    }

  public unsafe  struct floatCell
  {
    public float element;
    public floatCell* next;
  }

  public unsafe  struct floatCellCell
  {
    public floatCell element;
    public floatCellCell* next;
  }

  	public unsafe struct floatArray
    {
        public uint lenght;
        public float* index;
    }

  public unsafe  struct doubleCell
  {
    public double element;
    public doubleCell* next;
  }

  public unsafe  struct doubleCellCell
  {
    public doubleCell element;
    public doubleCellCell* next;
  }

  	public unsafe struct doubleArray
    {
        public uint lenght;
        public double* index;
    }

  public unsafe  struct decimalCell
  {
    public decimal element;
    public decimalCell* next;
  }

  public unsafe  struct decimalCellCell
  {
    public decimalCell element;
    public decimalCellCell* next;
  }

  	public unsafe struct decimalArray
    {
        public uint lenght;
        public decimal* index;
    }

  public unsafe  struct BigIntegerCell
  {
    public BigInteger element;
    public BigIntegerCell* next;
  }

  public unsafe  struct BigIntegerCellCell
  {
    public BigIntegerCell element;
    public BigIntegerCellCell* next;
  }

  	public unsafe struct BigIntegerArray
    {
        public uint lenght;
        public BigInteger* index;
    }

  public unsafe  struct ComplexCell
  {
    public Complex element;
    public ComplexCell* next;
  }

  public unsafe  struct ComplexCellCell
  {
    public ComplexCell element;
    public ComplexCellCell* next;
  }

  	public unsafe struct ComplexArray
    {
        public uint lenght;
        public Complex* index;
    }

  public unsafe  struct charCell
  {
    public char element;
    public charCell* next;
  }

  public unsafe  struct charCellCell
  {
    public charCell element;
    public charCellCell* next;
  }

  	public unsafe struct charArray
    {
        public uint lenght;
        public char* index;
    }




        public unsafe partial interface IintCellAllocator
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            intCell* Apply(uint count);
        }

		public unsafe partial interface IintCellCellAllocator
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            intCellCell* Apply(uint count);
        }

public unsafe partial class intCellExtensions
  {
    public uint length(intCell* cell)
    {
            uint counter = 0;
            while (cell != null)
            {
                counter++;
                cell = (*cell).next;
            };
            return counter;
        }

        public intCell* empty
        {
            get
            {
                return null;
            }
        }

		public int head(intCell* forwardLinkedList)
        {
            return (*forwardLinkedList).element;
        }

		
        public intArray toArray(intCell* abc, IAllocator allocator)
        {
            var total = this.length(abc);
            var arr = (int*)allocator.Apply(total);
            var counter = 0;
            while (abc != null)
            {
                arr[counter] = (*abc).element;
                abc = (*abc).next;
                counter++;
            }

            return new intArray { lenght = total, index = arr };
        }

        public intCell* tail(intCell* forwardLinkedList)
        {
            return (*forwardLinkedList).next;
        }

        public intCell* last(intCell* forwardLinkedList)
        {
            while (true)
            {
                if ((*forwardLinkedList).next == null) return forwardLinkedList;
                forwardLinkedList = (*forwardLinkedList).next;
            }
        }

        public void toCells(int[] array, intCell* memory)
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
        public unsafe partial interface IlongCellAllocator
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            longCell* Apply(uint count);
        }

		public unsafe partial interface IlongCellCellAllocator
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            longCellCell* Apply(uint count);
        }

public unsafe partial class longCellExtensions
  {
    public uint length(longCell* cell)
    {
            uint counter = 0;
            while (cell != null)
            {
                counter++;
                cell = (*cell).next;
            };
            return counter;
        }

        public longCell* empty
        {
            get
            {
                return null;
            }
        }

		public long head(longCell* forwardLinkedList)
        {
            return (*forwardLinkedList).element;
        }

		
        public longArray toArray(longCell* abc, IAllocator allocator)
        {
            var total = this.length(abc);
            var arr = (long*)allocator.Apply(total);
            var counter = 0;
            while (abc != null)
            {
                arr[counter] = (*abc).element;
                abc = (*abc).next;
                counter++;
            }

            return new longArray { lenght = total, index = arr };
        }

        public longCell* tail(longCell* forwardLinkedList)
        {
            return (*forwardLinkedList).next;
        }

        public longCell* last(longCell* forwardLinkedList)
        {
            while (true)
            {
                if ((*forwardLinkedList).next == null) return forwardLinkedList;
                forwardLinkedList = (*forwardLinkedList).next;
            }
        }

        public void toCells(long[] array, longCell* memory)
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
        public unsafe partial interface IfloatCellAllocator
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            floatCell* Apply(uint count);
        }

		public unsafe partial interface IfloatCellCellAllocator
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            floatCellCell* Apply(uint count);
        }

public unsafe partial class floatCellExtensions
  {
    public uint length(floatCell* cell)
    {
            uint counter = 0;
            while (cell != null)
            {
                counter++;
                cell = (*cell).next;
            };
            return counter;
        }

        public floatCell* empty
        {
            get
            {
                return null;
            }
        }

		public float head(floatCell* forwardLinkedList)
        {
            return (*forwardLinkedList).element;
        }

		
        public floatArray toArray(floatCell* abc, IAllocator allocator)
        {
            var total = this.length(abc);
            var arr = (float*)allocator.Apply(total);
            var counter = 0;
            while (abc != null)
            {
                arr[counter] = (*abc).element;
                abc = (*abc).next;
                counter++;
            }

            return new floatArray { lenght = total, index = arr };
        }

        public floatCell* tail(floatCell* forwardLinkedList)
        {
            return (*forwardLinkedList).next;
        }

        public floatCell* last(floatCell* forwardLinkedList)
        {
            while (true)
            {
                if ((*forwardLinkedList).next == null) return forwardLinkedList;
                forwardLinkedList = (*forwardLinkedList).next;
            }
        }

        public void toCells(float[] array, floatCell* memory)
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
        public unsafe partial interface IdoubleCellAllocator
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            doubleCell* Apply(uint count);
        }

		public unsafe partial interface IdoubleCellCellAllocator
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            doubleCellCell* Apply(uint count);
        }

public unsafe partial class doubleCellExtensions
  {
    public uint length(doubleCell* cell)
    {
            uint counter = 0;
            while (cell != null)
            {
                counter++;
                cell = (*cell).next;
            };
            return counter;
        }

        public doubleCell* empty
        {
            get
            {
                return null;
            }
        }

		public double head(doubleCell* forwardLinkedList)
        {
            return (*forwardLinkedList).element;
        }

		
        public doubleArray toArray(doubleCell* abc, IAllocator allocator)
        {
            var total = this.length(abc);
            var arr = (double*)allocator.Apply(total);
            var counter = 0;
            while (abc != null)
            {
                arr[counter] = (*abc).element;
                abc = (*abc).next;
                counter++;
            }

            return new doubleArray { lenght = total, index = arr };
        }

        public doubleCell* tail(doubleCell* forwardLinkedList)
        {
            return (*forwardLinkedList).next;
        }

        public doubleCell* last(doubleCell* forwardLinkedList)
        {
            while (true)
            {
                if ((*forwardLinkedList).next == null) return forwardLinkedList;
                forwardLinkedList = (*forwardLinkedList).next;
            }
        }

        public void toCells(double[] array, doubleCell* memory)
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
        public unsafe partial interface IdecimalCellAllocator
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            decimalCell* Apply(uint count);
        }

		public unsafe partial interface IdecimalCellCellAllocator
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            decimalCellCell* Apply(uint count);
        }

public unsafe partial class decimalCellExtensions
  {
    public uint length(decimalCell* cell)
    {
            uint counter = 0;
            while (cell != null)
            {
                counter++;
                cell = (*cell).next;
            };
            return counter;
        }

        public decimalCell* empty
        {
            get
            {
                return null;
            }
        }

		public decimal head(decimalCell* forwardLinkedList)
        {
            return (*forwardLinkedList).element;
        }

		
        public decimalArray toArray(decimalCell* abc, IAllocator allocator)
        {
            var total = this.length(abc);
            var arr = (decimal*)allocator.Apply(total);
            var counter = 0;
            while (abc != null)
            {
                arr[counter] = (*abc).element;
                abc = (*abc).next;
                counter++;
            }

            return new decimalArray { lenght = total, index = arr };
        }

        public decimalCell* tail(decimalCell* forwardLinkedList)
        {
            return (*forwardLinkedList).next;
        }

        public decimalCell* last(decimalCell* forwardLinkedList)
        {
            while (true)
            {
                if ((*forwardLinkedList).next == null) return forwardLinkedList;
                forwardLinkedList = (*forwardLinkedList).next;
            }
        }

        public void toCells(decimal[] array, decimalCell* memory)
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
        public unsafe partial interface IBigIntegerCellAllocator
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            BigIntegerCell* Apply(uint count);
        }

		public unsafe partial interface IBigIntegerCellCellAllocator
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            BigIntegerCellCell* Apply(uint count);
        }

public unsafe partial class BigIntegerCellExtensions
  {
    public uint length(BigIntegerCell* cell)
    {
            uint counter = 0;
            while (cell != null)
            {
                counter++;
                cell = (*cell).next;
            };
            return counter;
        }

        public BigIntegerCell* empty
        {
            get
            {
                return null;
            }
        }

		public BigInteger head(BigIntegerCell* forwardLinkedList)
        {
            return (*forwardLinkedList).element;
        }

		
        public BigIntegerArray toArray(BigIntegerCell* abc, IAllocator allocator)
        {
            var total = this.length(abc);
            var arr = (BigInteger*)allocator.Apply(total);
            var counter = 0;
            while (abc != null)
            {
                arr[counter] = (*abc).element;
                abc = (*abc).next;
                counter++;
            }

            return new BigIntegerArray { lenght = total, index = arr };
        }

        public BigIntegerCell* tail(BigIntegerCell* forwardLinkedList)
        {
            return (*forwardLinkedList).next;
        }

        public BigIntegerCell* last(BigIntegerCell* forwardLinkedList)
        {
            while (true)
            {
                if ((*forwardLinkedList).next == null) return forwardLinkedList;
                forwardLinkedList = (*forwardLinkedList).next;
            }
        }

        public void toCells(BigInteger[] array, BigIntegerCell* memory)
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
        public unsafe partial interface IComplexCellAllocator
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            ComplexCell* Apply(uint count);
        }

		public unsafe partial interface IComplexCellCellAllocator
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            ComplexCellCell* Apply(uint count);
        }

public unsafe partial class ComplexCellExtensions
  {
    public uint length(ComplexCell* cell)
    {
            uint counter = 0;
            while (cell != null)
            {
                counter++;
                cell = (*cell).next;
            };
            return counter;
        }

        public ComplexCell* empty
        {
            get
            {
                return null;
            }
        }

		public Complex head(ComplexCell* forwardLinkedList)
        {
            return (*forwardLinkedList).element;
        }

		
        public ComplexArray toArray(ComplexCell* abc, IAllocator allocator)
        {
            var total = this.length(abc);
            var arr = (Complex*)allocator.Apply(total);
            var counter = 0;
            while (abc != null)
            {
                arr[counter] = (*abc).element;
                abc = (*abc).next;
                counter++;
            }

            return new ComplexArray { lenght = total, index = arr };
        }

        public ComplexCell* tail(ComplexCell* forwardLinkedList)
        {
            return (*forwardLinkedList).next;
        }

        public ComplexCell* last(ComplexCell* forwardLinkedList)
        {
            while (true)
            {
                if ((*forwardLinkedList).next == null) return forwardLinkedList;
                forwardLinkedList = (*forwardLinkedList).next;
            }
        }

        public void toCells(Complex[] array, ComplexCell* memory)
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
        public unsafe partial interface IcharCellAllocator
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            charCell* Apply(uint count);
        }

		public unsafe partial interface IcharCellCellAllocator
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            charCellCell* Apply(uint count);
        }

public unsafe partial class charCellExtensions
  {
    public uint length(charCell* cell)
    {
            uint counter = 0;
            while (cell != null)
            {
                counter++;
                cell = (*cell).next;
            };
            return counter;
        }

        public charCell* empty
        {
            get
            {
                return null;
            }
        }

		public char head(charCell* forwardLinkedList)
        {
            return (*forwardLinkedList).element;
        }

		
        public charArray toArray(charCell* abc, IAllocator allocator)
        {
            var total = this.length(abc);
            var arr = (char*)allocator.Apply(total);
            var counter = 0;
            while (abc != null)
            {
                arr[counter] = (*abc).element;
                abc = (*abc).next;
                counter++;
            }

            return new charArray { lenght = total, index = arr };
        }

        public charCell* tail(charCell* forwardLinkedList)
        {
            return (*forwardLinkedList).next;
        }

        public charCell* last(charCell* forwardLinkedList)
        {
            while (true)
            {
                if ((*forwardLinkedList).next == null) return forwardLinkedList;
                forwardLinkedList = (*forwardLinkedList).next;
            }
        }

        public void toCells(char[] array, charCell* memory)
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

    public unsafe partial interface IAllocator
    {
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        void** Apply(ulong count);
    }



  public unsafe  struct intCell
  {
    public int element;
    public intCell* next;
  }

  public unsafe  struct intCellCell
  {
    public intCell element;
    public intCellCell* next;
  }

  	public unsafe struct intArray
    {
        public ulong lenght;
        public int* index;
    }

  public unsafe  struct longCell
  {
    public long element;
    public longCell* next;
  }

  public unsafe  struct longCellCell
  {
    public longCell element;
    public longCellCell* next;
  }

  	public unsafe struct longArray
    {
        public ulong lenght;
        public long* index;
    }

  public unsafe  struct floatCell
  {
    public float element;
    public floatCell* next;
  }

  public unsafe  struct floatCellCell
  {
    public floatCell element;
    public floatCellCell* next;
  }

  	public unsafe struct floatArray
    {
        public ulong lenght;
        public float* index;
    }

  public unsafe  struct doubleCell
  {
    public double element;
    public doubleCell* next;
  }

  public unsafe  struct doubleCellCell
  {
    public doubleCell element;
    public doubleCellCell* next;
  }

  	public unsafe struct doubleArray
    {
        public ulong lenght;
        public double* index;
    }

  public unsafe  struct decimalCell
  {
    public decimal element;
    public decimalCell* next;
  }

  public unsafe  struct decimalCellCell
  {
    public decimalCell element;
    public decimalCellCell* next;
  }

  	public unsafe struct decimalArray
    {
        public ulong lenght;
        public decimal* index;
    }

  public unsafe  struct BigIntegerCell
  {
    public BigInteger element;
    public BigIntegerCell* next;
  }

  public unsafe  struct BigIntegerCellCell
  {
    public BigIntegerCell element;
    public BigIntegerCellCell* next;
  }

  	public unsafe struct BigIntegerArray
    {
        public ulong lenght;
        public BigInteger* index;
    }

  public unsafe  struct ComplexCell
  {
    public Complex element;
    public ComplexCell* next;
  }

  public unsafe  struct ComplexCellCell
  {
    public ComplexCell element;
    public ComplexCellCell* next;
  }

  	public unsafe struct ComplexArray
    {
        public ulong lenght;
        public Complex* index;
    }

  public unsafe  struct charCell
  {
    public char element;
    public charCell* next;
  }

  public unsafe  struct charCellCell
  {
    public charCell element;
    public charCellCell* next;
  }

  	public unsafe struct charArray
    {
        public ulong lenght;
        public char* index;
    }




        public unsafe partial interface IintCellAllocator
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            intCell* Apply(ulong count);
        }

		public unsafe partial interface IintCellCellAllocator
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            intCellCell* Apply(ulong count);
        }

public unsafe partial class intCellExtensions
  {
    public ulong length(intCell* cell)
    {
            ulong counter = 0;
            while (cell != null)
            {
                counter++;
                cell = (*cell).next;
            };
            return counter;
        }

        public intCell* empty
        {
            get
            {
                return null;
            }
        }

		public int head(intCell* forwardLinkedList)
        {
            return (*forwardLinkedList).element;
        }

		
        public intArray toArray(intCell* abc, IAllocator allocator)
        {
            var total = this.length(abc);
            var arr = (int*)allocator.Apply(total);
            var counter = 0;
            while (abc != null)
            {
                arr[counter] = (*abc).element;
                abc = (*abc).next;
                counter++;
            }

            return new intArray { lenght = total, index = arr };
        }

        public intCell* tail(intCell* forwardLinkedList)
        {
            return (*forwardLinkedList).next;
        }

        public intCell* last(intCell* forwardLinkedList)
        {
            while (true)
            {
                if ((*forwardLinkedList).next == null) return forwardLinkedList;
                forwardLinkedList = (*forwardLinkedList).next;
            }
        }

        public void toCells(int[] array, intCell* memory)
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
        public unsafe partial interface IlongCellAllocator
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            longCell* Apply(ulong count);
        }

		public unsafe partial interface IlongCellCellAllocator
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            longCellCell* Apply(ulong count);
        }

public unsafe partial class longCellExtensions
  {
    public ulong length(longCell* cell)
    {
            ulong counter = 0;
            while (cell != null)
            {
                counter++;
                cell = (*cell).next;
            };
            return counter;
        }

        public longCell* empty
        {
            get
            {
                return null;
            }
        }

		public long head(longCell* forwardLinkedList)
        {
            return (*forwardLinkedList).element;
        }

		
        public longArray toArray(longCell* abc, IAllocator allocator)
        {
            var total = this.length(abc);
            var arr = (long*)allocator.Apply(total);
            var counter = 0;
            while (abc != null)
            {
                arr[counter] = (*abc).element;
                abc = (*abc).next;
                counter++;
            }

            return new longArray { lenght = total, index = arr };
        }

        public longCell* tail(longCell* forwardLinkedList)
        {
            return (*forwardLinkedList).next;
        }

        public longCell* last(longCell* forwardLinkedList)
        {
            while (true)
            {
                if ((*forwardLinkedList).next == null) return forwardLinkedList;
                forwardLinkedList = (*forwardLinkedList).next;
            }
        }

        public void toCells(long[] array, longCell* memory)
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
        public unsafe partial interface IfloatCellAllocator
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            floatCell* Apply(ulong count);
        }

		public unsafe partial interface IfloatCellCellAllocator
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            floatCellCell* Apply(ulong count);
        }

public unsafe partial class floatCellExtensions
  {
    public ulong length(floatCell* cell)
    {
            ulong counter = 0;
            while (cell != null)
            {
                counter++;
                cell = (*cell).next;
            };
            return counter;
        }

        public floatCell* empty
        {
            get
            {
                return null;
            }
        }

		public float head(floatCell* forwardLinkedList)
        {
            return (*forwardLinkedList).element;
        }

		
        public floatArray toArray(floatCell* abc, IAllocator allocator)
        {
            var total = this.length(abc);
            var arr = (float*)allocator.Apply(total);
            var counter = 0;
            while (abc != null)
            {
                arr[counter] = (*abc).element;
                abc = (*abc).next;
                counter++;
            }

            return new floatArray { lenght = total, index = arr };
        }

        public floatCell* tail(floatCell* forwardLinkedList)
        {
            return (*forwardLinkedList).next;
        }

        public floatCell* last(floatCell* forwardLinkedList)
        {
            while (true)
            {
                if ((*forwardLinkedList).next == null) return forwardLinkedList;
                forwardLinkedList = (*forwardLinkedList).next;
            }
        }

        public void toCells(float[] array, floatCell* memory)
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
        public unsafe partial interface IdoubleCellAllocator
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            doubleCell* Apply(ulong count);
        }

		public unsafe partial interface IdoubleCellCellAllocator
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            doubleCellCell* Apply(ulong count);
        }

public unsafe partial class doubleCellExtensions
  {
    public ulong length(doubleCell* cell)
    {
            ulong counter = 0;
            while (cell != null)
            {
                counter++;
                cell = (*cell).next;
            };
            return counter;
        }

        public doubleCell* empty
        {
            get
            {
                return null;
            }
        }

		public double head(doubleCell* forwardLinkedList)
        {
            return (*forwardLinkedList).element;
        }

		
        public doubleArray toArray(doubleCell* abc, IAllocator allocator)
        {
            var total = this.length(abc);
            var arr = (double*)allocator.Apply(total);
            var counter = 0;
            while (abc != null)
            {
                arr[counter] = (*abc).element;
                abc = (*abc).next;
                counter++;
            }

            return new doubleArray { lenght = total, index = arr };
        }

        public doubleCell* tail(doubleCell* forwardLinkedList)
        {
            return (*forwardLinkedList).next;
        }

        public doubleCell* last(doubleCell* forwardLinkedList)
        {
            while (true)
            {
                if ((*forwardLinkedList).next == null) return forwardLinkedList;
                forwardLinkedList = (*forwardLinkedList).next;
            }
        }

        public void toCells(double[] array, doubleCell* memory)
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
        public unsafe partial interface IdecimalCellAllocator
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            decimalCell* Apply(ulong count);
        }

		public unsafe partial interface IdecimalCellCellAllocator
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            decimalCellCell* Apply(ulong count);
        }

public unsafe partial class decimalCellExtensions
  {
    public ulong length(decimalCell* cell)
    {
            ulong counter = 0;
            while (cell != null)
            {
                counter++;
                cell = (*cell).next;
            };
            return counter;
        }

        public decimalCell* empty
        {
            get
            {
                return null;
            }
        }

		public decimal head(decimalCell* forwardLinkedList)
        {
            return (*forwardLinkedList).element;
        }

		
        public decimalArray toArray(decimalCell* abc, IAllocator allocator)
        {
            var total = this.length(abc);
            var arr = (decimal*)allocator.Apply(total);
            var counter = 0;
            while (abc != null)
            {
                arr[counter] = (*abc).element;
                abc = (*abc).next;
                counter++;
            }

            return new decimalArray { lenght = total, index = arr };
        }

        public decimalCell* tail(decimalCell* forwardLinkedList)
        {
            return (*forwardLinkedList).next;
        }

        public decimalCell* last(decimalCell* forwardLinkedList)
        {
            while (true)
            {
                if ((*forwardLinkedList).next == null) return forwardLinkedList;
                forwardLinkedList = (*forwardLinkedList).next;
            }
        }

        public void toCells(decimal[] array, decimalCell* memory)
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
        public unsafe partial interface IBigIntegerCellAllocator
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            BigIntegerCell* Apply(ulong count);
        }

		public unsafe partial interface IBigIntegerCellCellAllocator
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            BigIntegerCellCell* Apply(ulong count);
        }

public unsafe partial class BigIntegerCellExtensions
  {
    public ulong length(BigIntegerCell* cell)
    {
            ulong counter = 0;
            while (cell != null)
            {
                counter++;
                cell = (*cell).next;
            };
            return counter;
        }

        public BigIntegerCell* empty
        {
            get
            {
                return null;
            }
        }

		public BigInteger head(BigIntegerCell* forwardLinkedList)
        {
            return (*forwardLinkedList).element;
        }

		
        public BigIntegerArray toArray(BigIntegerCell* abc, IAllocator allocator)
        {
            var total = this.length(abc);
            var arr = (BigInteger*)allocator.Apply(total);
            var counter = 0;
            while (abc != null)
            {
                arr[counter] = (*abc).element;
                abc = (*abc).next;
                counter++;
            }

            return new BigIntegerArray { lenght = total, index = arr };
        }

        public BigIntegerCell* tail(BigIntegerCell* forwardLinkedList)
        {
            return (*forwardLinkedList).next;
        }

        public BigIntegerCell* last(BigIntegerCell* forwardLinkedList)
        {
            while (true)
            {
                if ((*forwardLinkedList).next == null) return forwardLinkedList;
                forwardLinkedList = (*forwardLinkedList).next;
            }
        }

        public void toCells(BigInteger[] array, BigIntegerCell* memory)
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
        public unsafe partial interface IComplexCellAllocator
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            ComplexCell* Apply(ulong count);
        }

		public unsafe partial interface IComplexCellCellAllocator
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            ComplexCellCell* Apply(ulong count);
        }

public unsafe partial class ComplexCellExtensions
  {
    public ulong length(ComplexCell* cell)
    {
            ulong counter = 0;
            while (cell != null)
            {
                counter++;
                cell = (*cell).next;
            };
            return counter;
        }

        public ComplexCell* empty
        {
            get
            {
                return null;
            }
        }

		public Complex head(ComplexCell* forwardLinkedList)
        {
            return (*forwardLinkedList).element;
        }

		
        public ComplexArray toArray(ComplexCell* abc, IAllocator allocator)
        {
            var total = this.length(abc);
            var arr = (Complex*)allocator.Apply(total);
            var counter = 0;
            while (abc != null)
            {
                arr[counter] = (*abc).element;
                abc = (*abc).next;
                counter++;
            }

            return new ComplexArray { lenght = total, index = arr };
        }

        public ComplexCell* tail(ComplexCell* forwardLinkedList)
        {
            return (*forwardLinkedList).next;
        }

        public ComplexCell* last(ComplexCell* forwardLinkedList)
        {
            while (true)
            {
                if ((*forwardLinkedList).next == null) return forwardLinkedList;
                forwardLinkedList = (*forwardLinkedList).next;
            }
        }

        public void toCells(Complex[] array, ComplexCell* memory)
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
        public unsafe partial interface IcharCellAllocator
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            charCell* Apply(ulong count);
        }

		public unsafe partial interface IcharCellCellAllocator
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            charCellCell* Apply(ulong count);
        }

public unsafe partial class charCellExtensions
  {
    public ulong length(charCell* cell)
    {
            ulong counter = 0;
            while (cell != null)
            {
                counter++;
                cell = (*cell).next;
            };
            return counter;
        }

        public charCell* empty
        {
            get
            {
                return null;
            }
        }

		public char head(charCell* forwardLinkedList)
        {
            return (*forwardLinkedList).element;
        }

		
        public charArray toArray(charCell* abc, IAllocator allocator)
        {
            var total = this.length(abc);
            var arr = (char*)allocator.Apply(total);
            var counter = 0;
            while (abc != null)
            {
                arr[counter] = (*abc).element;
                abc = (*abc).next;
                counter++;
            }

            return new charArray { lenght = total, index = arr };
        }

        public charCell* tail(charCell* forwardLinkedList)
        {
            return (*forwardLinkedList).next;
        }

        public charCell* last(charCell* forwardLinkedList)
        {
            while (true)
            {
                if ((*forwardLinkedList).next == null) return forwardLinkedList;
                forwardLinkedList = (*forwardLinkedList).next;
            }
        }

        public void toCells(char[] array, charCell* memory)
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

