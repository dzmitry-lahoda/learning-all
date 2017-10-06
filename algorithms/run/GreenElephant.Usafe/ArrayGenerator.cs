  
  
  
  
  
  
  
  
  
using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;

namespace GreenElephant.Usafe
{

namespace bit16
{
    public static class bits
	{
	
			        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	 		public static ushort leftShiftRing(ushort current, ushort steps, ushort length)
			 // https://stackoverflow.com/questions/2726920/c-sharp-xor-on-two-byte-variables-will-not-compile-without-a-cast
			 => current < steps ? (ushort)(length - steps + current) : (ushort)(current - steps);
			 
	}



    public static class arrayErrorExtensions
    {
        public static void unwrap(this ArrayError err)
        {
            if (err != ArrayError.ok) System.Environment.Exit((int)err);
        }
    }

    public enum ArrayError : byte
    {
        ok,
        shit_is_longer_than_lenght,
        emptyOrNullArray
    }



        public unsafe interface IintNew
        {
		        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
             int* Apply( ushort count);
        }

		    public unsafe interface IintDelete
    {
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        void Apply(int* root);
    }

  	public unsafe struct intArray
    {
        public ushort lenght;
        public int* index;
    }

	public unsafe partial class intArrayExtensions
  {
  public int* empty { get { return null; } }


        public ArrayError leftRotationZero_(ushort steps, ushort length, int* array)
        {
            if (steps > length) return ArrayError.shit_is_longer_than_lenght;
            if (array == this.empty || length <= 0) return ArrayError.emptyOrNullArray;
            _leftRotation_(steps, length, array);
            return ArrayError.ok;
        }

        /// <summary>
        /// Time - O(2 * length).
        /// Memory - O(length)
        /// </summary>
        /// <param name="steps"></param>
        /// <param name="length"></param>
        /// <param name="array"></param>
        /// <param name="allocator"></param>
        /// <param name="delete"></param>
        public void _leftRotation_(ushort steps, ushort length, int* array, IintNew allocator, IintDelete delete)
        {
            if (steps == 0)
            {
                return;
            }
            else if (steps == 1)
            {
                _leftRotation_(steps, length, array);
                return;
            }

            var memArr = allocator.Apply(length);
            for (ushort j = 0; j < length; j++)
            {
                var shift = bits.leftShiftRing(j, steps, length);
                memArr[shift] = array[j];
            }

            for (ushort j = 0; j < length; j++)
            {
                array[j] = memArr[j];
            }

            delete.Apply(memArr);
        }



        /// <summary>
        /// Time - O(length^2).
        /// Memory - O(1)
        /// </summary>
        public void _leftRotation_(ushort steps, ushort length, int* array)
        {
            for (ushort i = 0; i < steps; i++)
            {
                int first = array[0];
                for (ushort j = 1; j < length; j++) array[j - 1] = array[j];
                array[length - 1] = first;
            }
        }

        public bool equals(ushort length1, int* array1, ushort length2, int* array2)
        {
            if (length1 != length2) return false;
            if (array1 == array2) return true;
            for (ushort i = 0; i < length1; i++)
            {
                if (array1[i] != array2[i]) return false;
            }

            return true;
        }
		}


        public unsafe interface IlongNew
        {
		        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
             long* Apply( ushort count);
        }

		    public unsafe interface IlongDelete
    {
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        void Apply(long* root);
    }

  	public unsafe struct longArray
    {
        public ushort lenght;
        public long* index;
    }

	public unsafe partial class longArrayExtensions
  {
  public long* empty { get { return null; } }


        public ArrayError leftRotationZero_(ushort steps, ushort length, long* array)
        {
            if (steps > length) return ArrayError.shit_is_longer_than_lenght;
            if (array == this.empty || length <= 0) return ArrayError.emptyOrNullArray;
            _leftRotation_(steps, length, array);
            return ArrayError.ok;
        }

        /// <summary>
        /// Time - O(2 * length).
        /// Memory - O(length)
        /// </summary>
        /// <param name="steps"></param>
        /// <param name="length"></param>
        /// <param name="array"></param>
        /// <param name="allocator"></param>
        /// <param name="delete"></param>
        public void _leftRotation_(ushort steps, ushort length, long* array, IlongNew allocator, IlongDelete delete)
        {
            if (steps == 0)
            {
                return;
            }
            else if (steps == 1)
            {
                _leftRotation_(steps, length, array);
                return;
            }

            var memArr = allocator.Apply(length);
            for (ushort j = 0; j < length; j++)
            {
                var shift = bits.leftShiftRing(j, steps, length);
                memArr[shift] = array[j];
            }

            for (ushort j = 0; j < length; j++)
            {
                array[j] = memArr[j];
            }

            delete.Apply(memArr);
        }



        /// <summary>
        /// Time - O(length^2).
        /// Memory - O(1)
        /// </summary>
        public void _leftRotation_(ushort steps, ushort length, long* array)
        {
            for (ushort i = 0; i < steps; i++)
            {
                long first = array[0];
                for (ushort j = 1; j < length; j++) array[j - 1] = array[j];
                array[length - 1] = first;
            }
        }

        public bool equals(ushort length1, long* array1, ushort length2, long* array2)
        {
            if (length1 != length2) return false;
            if (array1 == array2) return true;
            for (ushort i = 0; i < length1; i++)
            {
                if (array1[i] != array2[i]) return false;
            }

            return true;
        }
		}


        public unsafe interface IfloatNew
        {
		        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
             float* Apply( ushort count);
        }

		    public unsafe interface IfloatDelete
    {
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        void Apply(float* root);
    }

  	public unsafe struct floatArray
    {
        public ushort lenght;
        public float* index;
    }

	public unsafe partial class floatArrayExtensions
  {
  public float* empty { get { return null; } }


        public ArrayError leftRotationZero_(ushort steps, ushort length, float* array)
        {
            if (steps > length) return ArrayError.shit_is_longer_than_lenght;
            if (array == this.empty || length <= 0) return ArrayError.emptyOrNullArray;
            _leftRotation_(steps, length, array);
            return ArrayError.ok;
        }

        /// <summary>
        /// Time - O(2 * length).
        /// Memory - O(length)
        /// </summary>
        /// <param name="steps"></param>
        /// <param name="length"></param>
        /// <param name="array"></param>
        /// <param name="allocator"></param>
        /// <param name="delete"></param>
        public void _leftRotation_(ushort steps, ushort length, float* array, IfloatNew allocator, IfloatDelete delete)
        {
            if (steps == 0)
            {
                return;
            }
            else if (steps == 1)
            {
                _leftRotation_(steps, length, array);
                return;
            }

            var memArr = allocator.Apply(length);
            for (ushort j = 0; j < length; j++)
            {
                var shift = bits.leftShiftRing(j, steps, length);
                memArr[shift] = array[j];
            }

            for (ushort j = 0; j < length; j++)
            {
                array[j] = memArr[j];
            }

            delete.Apply(memArr);
        }



        /// <summary>
        /// Time - O(length^2).
        /// Memory - O(1)
        /// </summary>
        public void _leftRotation_(ushort steps, ushort length, float* array)
        {
            for (ushort i = 0; i < steps; i++)
            {
                float first = array[0];
                for (ushort j = 1; j < length; j++) array[j - 1] = array[j];
                array[length - 1] = first;
            }
        }

        public bool equals(ushort length1, float* array1, ushort length2, float* array2)
        {
            if (length1 != length2) return false;
            if (array1 == array2) return true;
            for (ushort i = 0; i < length1; i++)
            {
                if (array1[i] != array2[i]) return false;
            }

            return true;
        }
		}


        public unsafe interface IdoubleNew
        {
		        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
             double* Apply( ushort count);
        }

		    public unsafe interface IdoubleDelete
    {
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        void Apply(double* root);
    }

  	public unsafe struct doubleArray
    {
        public ushort lenght;
        public double* index;
    }

	public unsafe partial class doubleArrayExtensions
  {
  public double* empty { get { return null; } }


        public ArrayError leftRotationZero_(ushort steps, ushort length, double* array)
        {
            if (steps > length) return ArrayError.shit_is_longer_than_lenght;
            if (array == this.empty || length <= 0) return ArrayError.emptyOrNullArray;
            _leftRotation_(steps, length, array);
            return ArrayError.ok;
        }

        /// <summary>
        /// Time - O(2 * length).
        /// Memory - O(length)
        /// </summary>
        /// <param name="steps"></param>
        /// <param name="length"></param>
        /// <param name="array"></param>
        /// <param name="allocator"></param>
        /// <param name="delete"></param>
        public void _leftRotation_(ushort steps, ushort length, double* array, IdoubleNew allocator, IdoubleDelete delete)
        {
            if (steps == 0)
            {
                return;
            }
            else if (steps == 1)
            {
                _leftRotation_(steps, length, array);
                return;
            }

            var memArr = allocator.Apply(length);
            for (ushort j = 0; j < length; j++)
            {
                var shift = bits.leftShiftRing(j, steps, length);
                memArr[shift] = array[j];
            }

            for (ushort j = 0; j < length; j++)
            {
                array[j] = memArr[j];
            }

            delete.Apply(memArr);
        }



        /// <summary>
        /// Time - O(length^2).
        /// Memory - O(1)
        /// </summary>
        public void _leftRotation_(ushort steps, ushort length, double* array)
        {
            for (ushort i = 0; i < steps; i++)
            {
                double first = array[0];
                for (ushort j = 1; j < length; j++) array[j - 1] = array[j];
                array[length - 1] = first;
            }
        }

        public bool equals(ushort length1, double* array1, ushort length2, double* array2)
        {
            if (length1 != length2) return false;
            if (array1 == array2) return true;
            for (ushort i = 0; i < length1; i++)
            {
                if (array1[i] != array2[i]) return false;
            }

            return true;
        }
		}


        public unsafe interface IdecimalNew
        {
		        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
             decimal* Apply( ushort count);
        }

		    public unsafe interface IdecimalDelete
    {
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        void Apply(decimal* root);
    }

  	public unsafe struct decimalArray
    {
        public ushort lenght;
        public decimal* index;
    }

	public unsafe partial class decimalArrayExtensions
  {
  public decimal* empty { get { return null; } }


        public ArrayError leftRotationZero_(ushort steps, ushort length, decimal* array)
        {
            if (steps > length) return ArrayError.shit_is_longer_than_lenght;
            if (array == this.empty || length <= 0) return ArrayError.emptyOrNullArray;
            _leftRotation_(steps, length, array);
            return ArrayError.ok;
        }

        /// <summary>
        /// Time - O(2 * length).
        /// Memory - O(length)
        /// </summary>
        /// <param name="steps"></param>
        /// <param name="length"></param>
        /// <param name="array"></param>
        /// <param name="allocator"></param>
        /// <param name="delete"></param>
        public void _leftRotation_(ushort steps, ushort length, decimal* array, IdecimalNew allocator, IdecimalDelete delete)
        {
            if (steps == 0)
            {
                return;
            }
            else if (steps == 1)
            {
                _leftRotation_(steps, length, array);
                return;
            }

            var memArr = allocator.Apply(length);
            for (ushort j = 0; j < length; j++)
            {
                var shift = bits.leftShiftRing(j, steps, length);
                memArr[shift] = array[j];
            }

            for (ushort j = 0; j < length; j++)
            {
                array[j] = memArr[j];
            }

            delete.Apply(memArr);
        }



        /// <summary>
        /// Time - O(length^2).
        /// Memory - O(1)
        /// </summary>
        public void _leftRotation_(ushort steps, ushort length, decimal* array)
        {
            for (ushort i = 0; i < steps; i++)
            {
                decimal first = array[0];
                for (ushort j = 1; j < length; j++) array[j - 1] = array[j];
                array[length - 1] = first;
            }
        }

        public bool equals(ushort length1, decimal* array1, ushort length2, decimal* array2)
        {
            if (length1 != length2) return false;
            if (array1 == array2) return true;
            for (ushort i = 0; i < length1; i++)
            {
                if (array1[i] != array2[i]) return false;
            }

            return true;
        }
		}


        public unsafe interface IBigIntegerNew
        {
		        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
             BigInteger* Apply( ushort count);
        }

		    public unsafe interface IBigIntegerDelete
    {
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        void Apply(BigInteger* root);
    }

  	public unsafe struct BigIntegerArray
    {
        public ushort lenght;
        public BigInteger* index;
    }

	public unsafe partial class BigIntegerArrayExtensions
  {
  public BigInteger* empty { get { return null; } }


        public ArrayError leftRotationZero_(ushort steps, ushort length, BigInteger* array)
        {
            if (steps > length) return ArrayError.shit_is_longer_than_lenght;
            if (array == this.empty || length <= 0) return ArrayError.emptyOrNullArray;
            _leftRotation_(steps, length, array);
            return ArrayError.ok;
        }

        /// <summary>
        /// Time - O(2 * length).
        /// Memory - O(length)
        /// </summary>
        /// <param name="steps"></param>
        /// <param name="length"></param>
        /// <param name="array"></param>
        /// <param name="allocator"></param>
        /// <param name="delete"></param>
        public void _leftRotation_(ushort steps, ushort length, BigInteger* array, IBigIntegerNew allocator, IBigIntegerDelete delete)
        {
            if (steps == 0)
            {
                return;
            }
            else if (steps == 1)
            {
                _leftRotation_(steps, length, array);
                return;
            }

            var memArr = allocator.Apply(length);
            for (ushort j = 0; j < length; j++)
            {
                var shift = bits.leftShiftRing(j, steps, length);
                memArr[shift] = array[j];
            }

            for (ushort j = 0; j < length; j++)
            {
                array[j] = memArr[j];
            }

            delete.Apply(memArr);
        }



        /// <summary>
        /// Time - O(length^2).
        /// Memory - O(1)
        /// </summary>
        public void _leftRotation_(ushort steps, ushort length, BigInteger* array)
        {
            for (ushort i = 0; i < steps; i++)
            {
                BigInteger first = array[0];
                for (ushort j = 1; j < length; j++) array[j - 1] = array[j];
                array[length - 1] = first;
            }
        }

        public bool equals(ushort length1, BigInteger* array1, ushort length2, BigInteger* array2)
        {
            if (length1 != length2) return false;
            if (array1 == array2) return true;
            for (ushort i = 0; i < length1; i++)
            {
                if (array1[i] != array2[i]) return false;
            }

            return true;
        }
		}


        public unsafe interface IComplexNew
        {
		        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
             Complex* Apply( ushort count);
        }

		    public unsafe interface IComplexDelete
    {
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        void Apply(Complex* root);
    }

  	public unsafe struct ComplexArray
    {
        public ushort lenght;
        public Complex* index;
    }

	public unsafe partial class ComplexArrayExtensions
  {
  public Complex* empty { get { return null; } }


        public ArrayError leftRotationZero_(ushort steps, ushort length, Complex* array)
        {
            if (steps > length) return ArrayError.shit_is_longer_than_lenght;
            if (array == this.empty || length <= 0) return ArrayError.emptyOrNullArray;
            _leftRotation_(steps, length, array);
            return ArrayError.ok;
        }

        /// <summary>
        /// Time - O(2 * length).
        /// Memory - O(length)
        /// </summary>
        /// <param name="steps"></param>
        /// <param name="length"></param>
        /// <param name="array"></param>
        /// <param name="allocator"></param>
        /// <param name="delete"></param>
        public void _leftRotation_(ushort steps, ushort length, Complex* array, IComplexNew allocator, IComplexDelete delete)
        {
            if (steps == 0)
            {
                return;
            }
            else if (steps == 1)
            {
                _leftRotation_(steps, length, array);
                return;
            }

            var memArr = allocator.Apply(length);
            for (ushort j = 0; j < length; j++)
            {
                var shift = bits.leftShiftRing(j, steps, length);
                memArr[shift] = array[j];
            }

            for (ushort j = 0; j < length; j++)
            {
                array[j] = memArr[j];
            }

            delete.Apply(memArr);
        }



        /// <summary>
        /// Time - O(length^2).
        /// Memory - O(1)
        /// </summary>
        public void _leftRotation_(ushort steps, ushort length, Complex* array)
        {
            for (ushort i = 0; i < steps; i++)
            {
                Complex first = array[0];
                for (ushort j = 1; j < length; j++) array[j - 1] = array[j];
                array[length - 1] = first;
            }
        }

        public bool equals(ushort length1, Complex* array1, ushort length2, Complex* array2)
        {
            if (length1 != length2) return false;
            if (array1 == array2) return true;
            for (ushort i = 0; i < length1; i++)
            {
                if (array1[i] != array2[i]) return false;
            }

            return true;
        }
		}


        public unsafe interface IcharNew
        {
		        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
             char* Apply( ushort count);
        }

		    public unsafe interface IcharDelete
    {
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        void Apply(char* root);
    }

  	public unsafe struct charArray
    {
        public ushort lenght;
        public char* index;
    }

	public unsafe partial class charArrayExtensions
  {
  public char* empty { get { return null; } }


        public ArrayError leftRotationZero_(ushort steps, ushort length, char* array)
        {
            if (steps > length) return ArrayError.shit_is_longer_than_lenght;
            if (array == this.empty || length <= 0) return ArrayError.emptyOrNullArray;
            _leftRotation_(steps, length, array);
            return ArrayError.ok;
        }

        /// <summary>
        /// Time - O(2 * length).
        /// Memory - O(length)
        /// </summary>
        /// <param name="steps"></param>
        /// <param name="length"></param>
        /// <param name="array"></param>
        /// <param name="allocator"></param>
        /// <param name="delete"></param>
        public void _leftRotation_(ushort steps, ushort length, char* array, IcharNew allocator, IcharDelete delete)
        {
            if (steps == 0)
            {
                return;
            }
            else if (steps == 1)
            {
                _leftRotation_(steps, length, array);
                return;
            }

            var memArr = allocator.Apply(length);
            for (ushort j = 0; j < length; j++)
            {
                var shift = bits.leftShiftRing(j, steps, length);
                memArr[shift] = array[j];
            }

            for (ushort j = 0; j < length; j++)
            {
                array[j] = memArr[j];
            }

            delete.Apply(memArr);
        }



        /// <summary>
        /// Time - O(length^2).
        /// Memory - O(1)
        /// </summary>
        public void _leftRotation_(ushort steps, ushort length, char* array)
        {
            for (ushort i = 0; i < steps; i++)
            {
                char first = array[0];
                for (ushort j = 1; j < length; j++) array[j - 1] = array[j];
                array[length - 1] = first;
            }
        }

        public bool equals(ushort length1, char* array1, ushort length2, char* array2)
        {
            if (length1 != length2) return false;
            if (array1 == array2) return true;
            for (ushort i = 0; i < length1; i++)
            {
                if (array1[i] != array2[i]) return false;
            }

            return true;
        }
		}

}
namespace bit32
{
    public static class bits
	{
	
			        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	 		public static uint leftShiftRing(uint current, uint steps, uint length)
			 // https://stackoverflow.com/questions/2726920/c-sharp-xor-on-two-byte-variables-will-not-compile-without-a-cast
			 => current < steps ? (uint)(length - steps + current) : (uint)(current - steps);
			 
	}



    public static class arrayErrorExtensions
    {
        public static void unwrap(this ArrayError err)
        {
            if (err != ArrayError.ok) System.Environment.Exit((int)err);
        }
    }

    public enum ArrayError : byte
    {
        ok,
        shit_is_longer_than_lenght,
        emptyOrNullArray
    }



        public unsafe interface IintNew
        {
		        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
             int* Apply( uint count);
        }

		    public unsafe interface IintDelete
    {
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        void Apply(int* root);
    }

  	public unsafe struct intArray
    {
        public uint lenght;
        public int* index;
    }

	public unsafe partial class intArrayExtensions
  {
  public int* empty { get { return null; } }


        public ArrayError leftRotationZero_(uint steps, uint length, int* array)
        {
            if (steps > length) return ArrayError.shit_is_longer_than_lenght;
            if (array == this.empty || length <= 0) return ArrayError.emptyOrNullArray;
            _leftRotation_(steps, length, array);
            return ArrayError.ok;
        }

        /// <summary>
        /// Time - O(2 * length).
        /// Memory - O(length)
        /// </summary>
        /// <param name="steps"></param>
        /// <param name="length"></param>
        /// <param name="array"></param>
        /// <param name="allocator"></param>
        /// <param name="delete"></param>
        public void _leftRotation_(uint steps, uint length, int* array, IintNew allocator, IintDelete delete)
        {
            if (steps == 0)
            {
                return;
            }
            else if (steps == 1)
            {
                _leftRotation_(steps, length, array);
                return;
            }

            var memArr = allocator.Apply(length);
            for (uint j = 0; j < length; j++)
            {
                var shift = bits.leftShiftRing(j, steps, length);
                memArr[shift] = array[j];
            }

            for (uint j = 0; j < length; j++)
            {
                array[j] = memArr[j];
            }

            delete.Apply(memArr);
        }



        /// <summary>
        /// Time - O(length^2).
        /// Memory - O(1)
        /// </summary>
        public void _leftRotation_(uint steps, uint length, int* array)
        {
            for (uint i = 0; i < steps; i++)
            {
                int first = array[0];
                for (uint j = 1; j < length; j++) array[j - 1] = array[j];
                array[length - 1] = first;
            }
        }

        public bool equals(uint length1, int* array1, uint length2, int* array2)
        {
            if (length1 != length2) return false;
            if (array1 == array2) return true;
            for (uint i = 0; i < length1; i++)
            {
                if (array1[i] != array2[i]) return false;
            }

            return true;
        }
		}


        public unsafe interface IlongNew
        {
		        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
             long* Apply( uint count);
        }

		    public unsafe interface IlongDelete
    {
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        void Apply(long* root);
    }

  	public unsafe struct longArray
    {
        public uint lenght;
        public long* index;
    }

	public unsafe partial class longArrayExtensions
  {
  public long* empty { get { return null; } }


        public ArrayError leftRotationZero_(uint steps, uint length, long* array)
        {
            if (steps > length) return ArrayError.shit_is_longer_than_lenght;
            if (array == this.empty || length <= 0) return ArrayError.emptyOrNullArray;
            _leftRotation_(steps, length, array);
            return ArrayError.ok;
        }

        /// <summary>
        /// Time - O(2 * length).
        /// Memory - O(length)
        /// </summary>
        /// <param name="steps"></param>
        /// <param name="length"></param>
        /// <param name="array"></param>
        /// <param name="allocator"></param>
        /// <param name="delete"></param>
        public void _leftRotation_(uint steps, uint length, long* array, IlongNew allocator, IlongDelete delete)
        {
            if (steps == 0)
            {
                return;
            }
            else if (steps == 1)
            {
                _leftRotation_(steps, length, array);
                return;
            }

            var memArr = allocator.Apply(length);
            for (uint j = 0; j < length; j++)
            {
                var shift = bits.leftShiftRing(j, steps, length);
                memArr[shift] = array[j];
            }

            for (uint j = 0; j < length; j++)
            {
                array[j] = memArr[j];
            }

            delete.Apply(memArr);
        }



        /// <summary>
        /// Time - O(length^2).
        /// Memory - O(1)
        /// </summary>
        public void _leftRotation_(uint steps, uint length, long* array)
        {
            for (uint i = 0; i < steps; i++)
            {
                long first = array[0];
                for (uint j = 1; j < length; j++) array[j - 1] = array[j];
                array[length - 1] = first;
            }
        }

        public bool equals(uint length1, long* array1, uint length2, long* array2)
        {
            if (length1 != length2) return false;
            if (array1 == array2) return true;
            for (uint i = 0; i < length1; i++)
            {
                if (array1[i] != array2[i]) return false;
            }

            return true;
        }
		}


        public unsafe interface IfloatNew
        {
		        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
             float* Apply( uint count);
        }

		    public unsafe interface IfloatDelete
    {
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        void Apply(float* root);
    }

  	public unsafe struct floatArray
    {
        public uint lenght;
        public float* index;
    }

	public unsafe partial class floatArrayExtensions
  {
  public float* empty { get { return null; } }


        public ArrayError leftRotationZero_(uint steps, uint length, float* array)
        {
            if (steps > length) return ArrayError.shit_is_longer_than_lenght;
            if (array == this.empty || length <= 0) return ArrayError.emptyOrNullArray;
            _leftRotation_(steps, length, array);
            return ArrayError.ok;
        }

        /// <summary>
        /// Time - O(2 * length).
        /// Memory - O(length)
        /// </summary>
        /// <param name="steps"></param>
        /// <param name="length"></param>
        /// <param name="array"></param>
        /// <param name="allocator"></param>
        /// <param name="delete"></param>
        public void _leftRotation_(uint steps, uint length, float* array, IfloatNew allocator, IfloatDelete delete)
        {
            if (steps == 0)
            {
                return;
            }
            else if (steps == 1)
            {
                _leftRotation_(steps, length, array);
                return;
            }

            var memArr = allocator.Apply(length);
            for (uint j = 0; j < length; j++)
            {
                var shift = bits.leftShiftRing(j, steps, length);
                memArr[shift] = array[j];
            }

            for (uint j = 0; j < length; j++)
            {
                array[j] = memArr[j];
            }

            delete.Apply(memArr);
        }



        /// <summary>
        /// Time - O(length^2).
        /// Memory - O(1)
        /// </summary>
        public void _leftRotation_(uint steps, uint length, float* array)
        {
            for (uint i = 0; i < steps; i++)
            {
                float first = array[0];
                for (uint j = 1; j < length; j++) array[j - 1] = array[j];
                array[length - 1] = first;
            }
        }

        public bool equals(uint length1, float* array1, uint length2, float* array2)
        {
            if (length1 != length2) return false;
            if (array1 == array2) return true;
            for (uint i = 0; i < length1; i++)
            {
                if (array1[i] != array2[i]) return false;
            }

            return true;
        }
		}


        public unsafe interface IdoubleNew
        {
		        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
             double* Apply( uint count);
        }

		    public unsafe interface IdoubleDelete
    {
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        void Apply(double* root);
    }

  	public unsafe struct doubleArray
    {
        public uint lenght;
        public double* index;
    }

	public unsafe partial class doubleArrayExtensions
  {
  public double* empty { get { return null; } }


        public ArrayError leftRotationZero_(uint steps, uint length, double* array)
        {
            if (steps > length) return ArrayError.shit_is_longer_than_lenght;
            if (array == this.empty || length <= 0) return ArrayError.emptyOrNullArray;
            _leftRotation_(steps, length, array);
            return ArrayError.ok;
        }

        /// <summary>
        /// Time - O(2 * length).
        /// Memory - O(length)
        /// </summary>
        /// <param name="steps"></param>
        /// <param name="length"></param>
        /// <param name="array"></param>
        /// <param name="allocator"></param>
        /// <param name="delete"></param>
        public void _leftRotation_(uint steps, uint length, double* array, IdoubleNew allocator, IdoubleDelete delete)
        {
            if (steps == 0)
            {
                return;
            }
            else if (steps == 1)
            {
                _leftRotation_(steps, length, array);
                return;
            }

            var memArr = allocator.Apply(length);
            for (uint j = 0; j < length; j++)
            {
                var shift = bits.leftShiftRing(j, steps, length);
                memArr[shift] = array[j];
            }

            for (uint j = 0; j < length; j++)
            {
                array[j] = memArr[j];
            }

            delete.Apply(memArr);
        }



        /// <summary>
        /// Time - O(length^2).
        /// Memory - O(1)
        /// </summary>
        public void _leftRotation_(uint steps, uint length, double* array)
        {
            for (uint i = 0; i < steps; i++)
            {
                double first = array[0];
                for (uint j = 1; j < length; j++) array[j - 1] = array[j];
                array[length - 1] = first;
            }
        }

        public bool equals(uint length1, double* array1, uint length2, double* array2)
        {
            if (length1 != length2) return false;
            if (array1 == array2) return true;
            for (uint i = 0; i < length1; i++)
            {
                if (array1[i] != array2[i]) return false;
            }

            return true;
        }
		}


        public unsafe interface IdecimalNew
        {
		        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
             decimal* Apply( uint count);
        }

		    public unsafe interface IdecimalDelete
    {
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        void Apply(decimal* root);
    }

  	public unsafe struct decimalArray
    {
        public uint lenght;
        public decimal* index;
    }

	public unsafe partial class decimalArrayExtensions
  {
  public decimal* empty { get { return null; } }


        public ArrayError leftRotationZero_(uint steps, uint length, decimal* array)
        {
            if (steps > length) return ArrayError.shit_is_longer_than_lenght;
            if (array == this.empty || length <= 0) return ArrayError.emptyOrNullArray;
            _leftRotation_(steps, length, array);
            return ArrayError.ok;
        }

        /// <summary>
        /// Time - O(2 * length).
        /// Memory - O(length)
        /// </summary>
        /// <param name="steps"></param>
        /// <param name="length"></param>
        /// <param name="array"></param>
        /// <param name="allocator"></param>
        /// <param name="delete"></param>
        public void _leftRotation_(uint steps, uint length, decimal* array, IdecimalNew allocator, IdecimalDelete delete)
        {
            if (steps == 0)
            {
                return;
            }
            else if (steps == 1)
            {
                _leftRotation_(steps, length, array);
                return;
            }

            var memArr = allocator.Apply(length);
            for (uint j = 0; j < length; j++)
            {
                var shift = bits.leftShiftRing(j, steps, length);
                memArr[shift] = array[j];
            }

            for (uint j = 0; j < length; j++)
            {
                array[j] = memArr[j];
            }

            delete.Apply(memArr);
        }



        /// <summary>
        /// Time - O(length^2).
        /// Memory - O(1)
        /// </summary>
        public void _leftRotation_(uint steps, uint length, decimal* array)
        {
            for (uint i = 0; i < steps; i++)
            {
                decimal first = array[0];
                for (uint j = 1; j < length; j++) array[j - 1] = array[j];
                array[length - 1] = first;
            }
        }

        public bool equals(uint length1, decimal* array1, uint length2, decimal* array2)
        {
            if (length1 != length2) return false;
            if (array1 == array2) return true;
            for (uint i = 0; i < length1; i++)
            {
                if (array1[i] != array2[i]) return false;
            }

            return true;
        }
		}


        public unsafe interface IBigIntegerNew
        {
		        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
             BigInteger* Apply( uint count);
        }

		    public unsafe interface IBigIntegerDelete
    {
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        void Apply(BigInteger* root);
    }

  	public unsafe struct BigIntegerArray
    {
        public uint lenght;
        public BigInteger* index;
    }

	public unsafe partial class BigIntegerArrayExtensions
  {
  public BigInteger* empty { get { return null; } }


        public ArrayError leftRotationZero_(uint steps, uint length, BigInteger* array)
        {
            if (steps > length) return ArrayError.shit_is_longer_than_lenght;
            if (array == this.empty || length <= 0) return ArrayError.emptyOrNullArray;
            _leftRotation_(steps, length, array);
            return ArrayError.ok;
        }

        /// <summary>
        /// Time - O(2 * length).
        /// Memory - O(length)
        /// </summary>
        /// <param name="steps"></param>
        /// <param name="length"></param>
        /// <param name="array"></param>
        /// <param name="allocator"></param>
        /// <param name="delete"></param>
        public void _leftRotation_(uint steps, uint length, BigInteger* array, IBigIntegerNew allocator, IBigIntegerDelete delete)
        {
            if (steps == 0)
            {
                return;
            }
            else if (steps == 1)
            {
                _leftRotation_(steps, length, array);
                return;
            }

            var memArr = allocator.Apply(length);
            for (uint j = 0; j < length; j++)
            {
                var shift = bits.leftShiftRing(j, steps, length);
                memArr[shift] = array[j];
            }

            for (uint j = 0; j < length; j++)
            {
                array[j] = memArr[j];
            }

            delete.Apply(memArr);
        }



        /// <summary>
        /// Time - O(length^2).
        /// Memory - O(1)
        /// </summary>
        public void _leftRotation_(uint steps, uint length, BigInteger* array)
        {
            for (uint i = 0; i < steps; i++)
            {
                BigInteger first = array[0];
                for (uint j = 1; j < length; j++) array[j - 1] = array[j];
                array[length - 1] = first;
            }
        }

        public bool equals(uint length1, BigInteger* array1, uint length2, BigInteger* array2)
        {
            if (length1 != length2) return false;
            if (array1 == array2) return true;
            for (uint i = 0; i < length1; i++)
            {
                if (array1[i] != array2[i]) return false;
            }

            return true;
        }
		}


        public unsafe interface IComplexNew
        {
		        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
             Complex* Apply( uint count);
        }

		    public unsafe interface IComplexDelete
    {
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        void Apply(Complex* root);
    }

  	public unsafe struct ComplexArray
    {
        public uint lenght;
        public Complex* index;
    }

	public unsafe partial class ComplexArrayExtensions
  {
  public Complex* empty { get { return null; } }


        public ArrayError leftRotationZero_(uint steps, uint length, Complex* array)
        {
            if (steps > length) return ArrayError.shit_is_longer_than_lenght;
            if (array == this.empty || length <= 0) return ArrayError.emptyOrNullArray;
            _leftRotation_(steps, length, array);
            return ArrayError.ok;
        }

        /// <summary>
        /// Time - O(2 * length).
        /// Memory - O(length)
        /// </summary>
        /// <param name="steps"></param>
        /// <param name="length"></param>
        /// <param name="array"></param>
        /// <param name="allocator"></param>
        /// <param name="delete"></param>
        public void _leftRotation_(uint steps, uint length, Complex* array, IComplexNew allocator, IComplexDelete delete)
        {
            if (steps == 0)
            {
                return;
            }
            else if (steps == 1)
            {
                _leftRotation_(steps, length, array);
                return;
            }

            var memArr = allocator.Apply(length);
            for (uint j = 0; j < length; j++)
            {
                var shift = bits.leftShiftRing(j, steps, length);
                memArr[shift] = array[j];
            }

            for (uint j = 0; j < length; j++)
            {
                array[j] = memArr[j];
            }

            delete.Apply(memArr);
        }



        /// <summary>
        /// Time - O(length^2).
        /// Memory - O(1)
        /// </summary>
        public void _leftRotation_(uint steps, uint length, Complex* array)
        {
            for (uint i = 0; i < steps; i++)
            {
                Complex first = array[0];
                for (uint j = 1; j < length; j++) array[j - 1] = array[j];
                array[length - 1] = first;
            }
        }

        public bool equals(uint length1, Complex* array1, uint length2, Complex* array2)
        {
            if (length1 != length2) return false;
            if (array1 == array2) return true;
            for (uint i = 0; i < length1; i++)
            {
                if (array1[i] != array2[i]) return false;
            }

            return true;
        }
		}


        public unsafe interface IcharNew
        {
		        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
             char* Apply( uint count);
        }

		    public unsafe interface IcharDelete
    {
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        void Apply(char* root);
    }

  	public unsafe struct charArray
    {
        public uint lenght;
        public char* index;
    }

	public unsafe partial class charArrayExtensions
  {
  public char* empty { get { return null; } }


        public ArrayError leftRotationZero_(uint steps, uint length, char* array)
        {
            if (steps > length) return ArrayError.shit_is_longer_than_lenght;
            if (array == this.empty || length <= 0) return ArrayError.emptyOrNullArray;
            _leftRotation_(steps, length, array);
            return ArrayError.ok;
        }

        /// <summary>
        /// Time - O(2 * length).
        /// Memory - O(length)
        /// </summary>
        /// <param name="steps"></param>
        /// <param name="length"></param>
        /// <param name="array"></param>
        /// <param name="allocator"></param>
        /// <param name="delete"></param>
        public void _leftRotation_(uint steps, uint length, char* array, IcharNew allocator, IcharDelete delete)
        {
            if (steps == 0)
            {
                return;
            }
            else if (steps == 1)
            {
                _leftRotation_(steps, length, array);
                return;
            }

            var memArr = allocator.Apply(length);
            for (uint j = 0; j < length; j++)
            {
                var shift = bits.leftShiftRing(j, steps, length);
                memArr[shift] = array[j];
            }

            for (uint j = 0; j < length; j++)
            {
                array[j] = memArr[j];
            }

            delete.Apply(memArr);
        }



        /// <summary>
        /// Time - O(length^2).
        /// Memory - O(1)
        /// </summary>
        public void _leftRotation_(uint steps, uint length, char* array)
        {
            for (uint i = 0; i < steps; i++)
            {
                char first = array[0];
                for (uint j = 1; j < length; j++) array[j - 1] = array[j];
                array[length - 1] = first;
            }
        }

        public bool equals(uint length1, char* array1, uint length2, char* array2)
        {
            if (length1 != length2) return false;
            if (array1 == array2) return true;
            for (uint i = 0; i < length1; i++)
            {
                if (array1[i] != array2[i]) return false;
            }

            return true;
        }
		}

}
namespace bit64
{
    public static class bits
	{
	
			        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	 		public static ulong leftShiftRing(ulong current, ulong steps, ulong length)
			 // https://stackoverflow.com/questions/2726920/c-sharp-xor-on-two-byte-variables-will-not-compile-without-a-cast
			 => current < steps ? (ulong)(length - steps + current) : (ulong)(current - steps);
			 
	}



    public static class arrayErrorExtensions
    {
        public static void unwrap(this ArrayError err)
        {
            if (err != ArrayError.ok) System.Environment.Exit((int)err);
        }
    }

    public enum ArrayError : byte
    {
        ok,
        shit_is_longer_than_lenght,
        emptyOrNullArray
    }



        public unsafe interface IintNew
        {
		        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
             int* Apply( ulong count);
        }

		    public unsafe interface IintDelete
    {
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        void Apply(int* root);
    }

  	public unsafe struct intArray
    {
        public ulong lenght;
        public int* index;
    }

	public unsafe partial class intArrayExtensions
  {
  public int* empty { get { return null; } }


        public ArrayError leftRotationZero_(ulong steps, ulong length, int* array)
        {
            if (steps > length) return ArrayError.shit_is_longer_than_lenght;
            if (array == this.empty || length <= 0) return ArrayError.emptyOrNullArray;
            _leftRotation_(steps, length, array);
            return ArrayError.ok;
        }

        /// <summary>
        /// Time - O(2 * length).
        /// Memory - O(length)
        /// </summary>
        /// <param name="steps"></param>
        /// <param name="length"></param>
        /// <param name="array"></param>
        /// <param name="allocator"></param>
        /// <param name="delete"></param>
        public void _leftRotation_(ulong steps, ulong length, int* array, IintNew allocator, IintDelete delete)
        {
            if (steps == 0)
            {
                return;
            }
            else if (steps == 1)
            {
                _leftRotation_(steps, length, array);
                return;
            }

            var memArr = allocator.Apply(length);
            for (ulong j = 0; j < length; j++)
            {
                var shift = bits.leftShiftRing(j, steps, length);
                memArr[shift] = array[j];
            }

            for (ulong j = 0; j < length; j++)
            {
                array[j] = memArr[j];
            }

            delete.Apply(memArr);
        }



        /// <summary>
        /// Time - O(length^2).
        /// Memory - O(1)
        /// </summary>
        public void _leftRotation_(ulong steps, ulong length, int* array)
        {
            for (ulong i = 0; i < steps; i++)
            {
                int first = array[0];
                for (ulong j = 1; j < length; j++) array[j - 1] = array[j];
                array[length - 1] = first;
            }
        }

        public bool equals(ulong length1, int* array1, ulong length2, int* array2)
        {
            if (length1 != length2) return false;
            if (array1 == array2) return true;
            for (ulong i = 0; i < length1; i++)
            {
                if (array1[i] != array2[i]) return false;
            }

            return true;
        }
		}


        public unsafe interface IlongNew
        {
		        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
             long* Apply( ulong count);
        }

		    public unsafe interface IlongDelete
    {
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        void Apply(long* root);
    }

  	public unsafe struct longArray
    {
        public ulong lenght;
        public long* index;
    }

	public unsafe partial class longArrayExtensions
  {
  public long* empty { get { return null; } }


        public ArrayError leftRotationZero_(ulong steps, ulong length, long* array)
        {
            if (steps > length) return ArrayError.shit_is_longer_than_lenght;
            if (array == this.empty || length <= 0) return ArrayError.emptyOrNullArray;
            _leftRotation_(steps, length, array);
            return ArrayError.ok;
        }

        /// <summary>
        /// Time - O(2 * length).
        /// Memory - O(length)
        /// </summary>
        /// <param name="steps"></param>
        /// <param name="length"></param>
        /// <param name="array"></param>
        /// <param name="allocator"></param>
        /// <param name="delete"></param>
        public void _leftRotation_(ulong steps, ulong length, long* array, IlongNew allocator, IlongDelete delete)
        {
            if (steps == 0)
            {
                return;
            }
            else if (steps == 1)
            {
                _leftRotation_(steps, length, array);
                return;
            }

            var memArr = allocator.Apply(length);
            for (ulong j = 0; j < length; j++)
            {
                var shift = bits.leftShiftRing(j, steps, length);
                memArr[shift] = array[j];
            }

            for (ulong j = 0; j < length; j++)
            {
                array[j] = memArr[j];
            }

            delete.Apply(memArr);
        }



        /// <summary>
        /// Time - O(length^2).
        /// Memory - O(1)
        /// </summary>
        public void _leftRotation_(ulong steps, ulong length, long* array)
        {
            for (ulong i = 0; i < steps; i++)
            {
                long first = array[0];
                for (ulong j = 1; j < length; j++) array[j - 1] = array[j];
                array[length - 1] = first;
            }
        }

        public bool equals(ulong length1, long* array1, ulong length2, long* array2)
        {
            if (length1 != length2) return false;
            if (array1 == array2) return true;
            for (ulong i = 0; i < length1; i++)
            {
                if (array1[i] != array2[i]) return false;
            }

            return true;
        }
		}


        public unsafe interface IfloatNew
        {
		        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
             float* Apply( ulong count);
        }

		    public unsafe interface IfloatDelete
    {
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        void Apply(float* root);
    }

  	public unsafe struct floatArray
    {
        public ulong lenght;
        public float* index;
    }

	public unsafe partial class floatArrayExtensions
  {
  public float* empty { get { return null; } }


        public ArrayError leftRotationZero_(ulong steps, ulong length, float* array)
        {
            if (steps > length) return ArrayError.shit_is_longer_than_lenght;
            if (array == this.empty || length <= 0) return ArrayError.emptyOrNullArray;
            _leftRotation_(steps, length, array);
            return ArrayError.ok;
        }

        /// <summary>
        /// Time - O(2 * length).
        /// Memory - O(length)
        /// </summary>
        /// <param name="steps"></param>
        /// <param name="length"></param>
        /// <param name="array"></param>
        /// <param name="allocator"></param>
        /// <param name="delete"></param>
        public void _leftRotation_(ulong steps, ulong length, float* array, IfloatNew allocator, IfloatDelete delete)
        {
            if (steps == 0)
            {
                return;
            }
            else if (steps == 1)
            {
                _leftRotation_(steps, length, array);
                return;
            }

            var memArr = allocator.Apply(length);
            for (ulong j = 0; j < length; j++)
            {
                var shift = bits.leftShiftRing(j, steps, length);
                memArr[shift] = array[j];
            }

            for (ulong j = 0; j < length; j++)
            {
                array[j] = memArr[j];
            }

            delete.Apply(memArr);
        }



        /// <summary>
        /// Time - O(length^2).
        /// Memory - O(1)
        /// </summary>
        public void _leftRotation_(ulong steps, ulong length, float* array)
        {
            for (ulong i = 0; i < steps; i++)
            {
                float first = array[0];
                for (ulong j = 1; j < length; j++) array[j - 1] = array[j];
                array[length - 1] = first;
            }
        }

        public bool equals(ulong length1, float* array1, ulong length2, float* array2)
        {
            if (length1 != length2) return false;
            if (array1 == array2) return true;
            for (ulong i = 0; i < length1; i++)
            {
                if (array1[i] != array2[i]) return false;
            }

            return true;
        }
		}


        public unsafe interface IdoubleNew
        {
		        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
             double* Apply( ulong count);
        }

		    public unsafe interface IdoubleDelete
    {
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        void Apply(double* root);
    }

  	public unsafe struct doubleArray
    {
        public ulong lenght;
        public double* index;
    }

	public unsafe partial class doubleArrayExtensions
  {
  public double* empty { get { return null; } }


        public ArrayError leftRotationZero_(ulong steps, ulong length, double* array)
        {
            if (steps > length) return ArrayError.shit_is_longer_than_lenght;
            if (array == this.empty || length <= 0) return ArrayError.emptyOrNullArray;
            _leftRotation_(steps, length, array);
            return ArrayError.ok;
        }

        /// <summary>
        /// Time - O(2 * length).
        /// Memory - O(length)
        /// </summary>
        /// <param name="steps"></param>
        /// <param name="length"></param>
        /// <param name="array"></param>
        /// <param name="allocator"></param>
        /// <param name="delete"></param>
        public void _leftRotation_(ulong steps, ulong length, double* array, IdoubleNew allocator, IdoubleDelete delete)
        {
            if (steps == 0)
            {
                return;
            }
            else if (steps == 1)
            {
                _leftRotation_(steps, length, array);
                return;
            }

            var memArr = allocator.Apply(length);
            for (ulong j = 0; j < length; j++)
            {
                var shift = bits.leftShiftRing(j, steps, length);
                memArr[shift] = array[j];
            }

            for (ulong j = 0; j < length; j++)
            {
                array[j] = memArr[j];
            }

            delete.Apply(memArr);
        }



        /// <summary>
        /// Time - O(length^2).
        /// Memory - O(1)
        /// </summary>
        public void _leftRotation_(ulong steps, ulong length, double* array)
        {
            for (ulong i = 0; i < steps; i++)
            {
                double first = array[0];
                for (ulong j = 1; j < length; j++) array[j - 1] = array[j];
                array[length - 1] = first;
            }
        }

        public bool equals(ulong length1, double* array1, ulong length2, double* array2)
        {
            if (length1 != length2) return false;
            if (array1 == array2) return true;
            for (ulong i = 0; i < length1; i++)
            {
                if (array1[i] != array2[i]) return false;
            }

            return true;
        }
		}


        public unsafe interface IdecimalNew
        {
		        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
             decimal* Apply( ulong count);
        }

		    public unsafe interface IdecimalDelete
    {
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        void Apply(decimal* root);
    }

  	public unsafe struct decimalArray
    {
        public ulong lenght;
        public decimal* index;
    }

	public unsafe partial class decimalArrayExtensions
  {
  public decimal* empty { get { return null; } }


        public ArrayError leftRotationZero_(ulong steps, ulong length, decimal* array)
        {
            if (steps > length) return ArrayError.shit_is_longer_than_lenght;
            if (array == this.empty || length <= 0) return ArrayError.emptyOrNullArray;
            _leftRotation_(steps, length, array);
            return ArrayError.ok;
        }

        /// <summary>
        /// Time - O(2 * length).
        /// Memory - O(length)
        /// </summary>
        /// <param name="steps"></param>
        /// <param name="length"></param>
        /// <param name="array"></param>
        /// <param name="allocator"></param>
        /// <param name="delete"></param>
        public void _leftRotation_(ulong steps, ulong length, decimal* array, IdecimalNew allocator, IdecimalDelete delete)
        {
            if (steps == 0)
            {
                return;
            }
            else if (steps == 1)
            {
                _leftRotation_(steps, length, array);
                return;
            }

            var memArr = allocator.Apply(length);
            for (ulong j = 0; j < length; j++)
            {
                var shift = bits.leftShiftRing(j, steps, length);
                memArr[shift] = array[j];
            }

            for (ulong j = 0; j < length; j++)
            {
                array[j] = memArr[j];
            }

            delete.Apply(memArr);
        }



        /// <summary>
        /// Time - O(length^2).
        /// Memory - O(1)
        /// </summary>
        public void _leftRotation_(ulong steps, ulong length, decimal* array)
        {
            for (ulong i = 0; i < steps; i++)
            {
                decimal first = array[0];
                for (ulong j = 1; j < length; j++) array[j - 1] = array[j];
                array[length - 1] = first;
            }
        }

        public bool equals(ulong length1, decimal* array1, ulong length2, decimal* array2)
        {
            if (length1 != length2) return false;
            if (array1 == array2) return true;
            for (ulong i = 0; i < length1; i++)
            {
                if (array1[i] != array2[i]) return false;
            }

            return true;
        }
		}


        public unsafe interface IBigIntegerNew
        {
		        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
             BigInteger* Apply( ulong count);
        }

		    public unsafe interface IBigIntegerDelete
    {
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        void Apply(BigInteger* root);
    }

  	public unsafe struct BigIntegerArray
    {
        public ulong lenght;
        public BigInteger* index;
    }

	public unsafe partial class BigIntegerArrayExtensions
  {
  public BigInteger* empty { get { return null; } }


        public ArrayError leftRotationZero_(ulong steps, ulong length, BigInteger* array)
        {
            if (steps > length) return ArrayError.shit_is_longer_than_lenght;
            if (array == this.empty || length <= 0) return ArrayError.emptyOrNullArray;
            _leftRotation_(steps, length, array);
            return ArrayError.ok;
        }

        /// <summary>
        /// Time - O(2 * length).
        /// Memory - O(length)
        /// </summary>
        /// <param name="steps"></param>
        /// <param name="length"></param>
        /// <param name="array"></param>
        /// <param name="allocator"></param>
        /// <param name="delete"></param>
        public void _leftRotation_(ulong steps, ulong length, BigInteger* array, IBigIntegerNew allocator, IBigIntegerDelete delete)
        {
            if (steps == 0)
            {
                return;
            }
            else if (steps == 1)
            {
                _leftRotation_(steps, length, array);
                return;
            }

            var memArr = allocator.Apply(length);
            for (ulong j = 0; j < length; j++)
            {
                var shift = bits.leftShiftRing(j, steps, length);
                memArr[shift] = array[j];
            }

            for (ulong j = 0; j < length; j++)
            {
                array[j] = memArr[j];
            }

            delete.Apply(memArr);
        }



        /// <summary>
        /// Time - O(length^2).
        /// Memory - O(1)
        /// </summary>
        public void _leftRotation_(ulong steps, ulong length, BigInteger* array)
        {
            for (ulong i = 0; i < steps; i++)
            {
                BigInteger first = array[0];
                for (ulong j = 1; j < length; j++) array[j - 1] = array[j];
                array[length - 1] = first;
            }
        }

        public bool equals(ulong length1, BigInteger* array1, ulong length2, BigInteger* array2)
        {
            if (length1 != length2) return false;
            if (array1 == array2) return true;
            for (ulong i = 0; i < length1; i++)
            {
                if (array1[i] != array2[i]) return false;
            }

            return true;
        }
		}


        public unsafe interface IComplexNew
        {
		        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
             Complex* Apply( ulong count);
        }

		    public unsafe interface IComplexDelete
    {
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        void Apply(Complex* root);
    }

  	public unsafe struct ComplexArray
    {
        public ulong lenght;
        public Complex* index;
    }

	public unsafe partial class ComplexArrayExtensions
  {
  public Complex* empty { get { return null; } }


        public ArrayError leftRotationZero_(ulong steps, ulong length, Complex* array)
        {
            if (steps > length) return ArrayError.shit_is_longer_than_lenght;
            if (array == this.empty || length <= 0) return ArrayError.emptyOrNullArray;
            _leftRotation_(steps, length, array);
            return ArrayError.ok;
        }

        /// <summary>
        /// Time - O(2 * length).
        /// Memory - O(length)
        /// </summary>
        /// <param name="steps"></param>
        /// <param name="length"></param>
        /// <param name="array"></param>
        /// <param name="allocator"></param>
        /// <param name="delete"></param>
        public void _leftRotation_(ulong steps, ulong length, Complex* array, IComplexNew allocator, IComplexDelete delete)
        {
            if (steps == 0)
            {
                return;
            }
            else if (steps == 1)
            {
                _leftRotation_(steps, length, array);
                return;
            }

            var memArr = allocator.Apply(length);
            for (ulong j = 0; j < length; j++)
            {
                var shift = bits.leftShiftRing(j, steps, length);
                memArr[shift] = array[j];
            }

            for (ulong j = 0; j < length; j++)
            {
                array[j] = memArr[j];
            }

            delete.Apply(memArr);
        }



        /// <summary>
        /// Time - O(length^2).
        /// Memory - O(1)
        /// </summary>
        public void _leftRotation_(ulong steps, ulong length, Complex* array)
        {
            for (ulong i = 0; i < steps; i++)
            {
                Complex first = array[0];
                for (ulong j = 1; j < length; j++) array[j - 1] = array[j];
                array[length - 1] = first;
            }
        }

        public bool equals(ulong length1, Complex* array1, ulong length2, Complex* array2)
        {
            if (length1 != length2) return false;
            if (array1 == array2) return true;
            for (ulong i = 0; i < length1; i++)
            {
                if (array1[i] != array2[i]) return false;
            }

            return true;
        }
		}


        public unsafe interface IcharNew
        {
		        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
             char* Apply( ulong count);
        }

		    public unsafe interface IcharDelete
    {
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        void Apply(char* root);
    }

  	public unsafe struct charArray
    {
        public ulong lenght;
        public char* index;
    }

	public unsafe partial class charArrayExtensions
  {
  public char* empty { get { return null; } }


        public ArrayError leftRotationZero_(ulong steps, ulong length, char* array)
        {
            if (steps > length) return ArrayError.shit_is_longer_than_lenght;
            if (array == this.empty || length <= 0) return ArrayError.emptyOrNullArray;
            _leftRotation_(steps, length, array);
            return ArrayError.ok;
        }

        /// <summary>
        /// Time - O(2 * length).
        /// Memory - O(length)
        /// </summary>
        /// <param name="steps"></param>
        /// <param name="length"></param>
        /// <param name="array"></param>
        /// <param name="allocator"></param>
        /// <param name="delete"></param>
        public void _leftRotation_(ulong steps, ulong length, char* array, IcharNew allocator, IcharDelete delete)
        {
            if (steps == 0)
            {
                return;
            }
            else if (steps == 1)
            {
                _leftRotation_(steps, length, array);
                return;
            }

            var memArr = allocator.Apply(length);
            for (ulong j = 0; j < length; j++)
            {
                var shift = bits.leftShiftRing(j, steps, length);
                memArr[shift] = array[j];
            }

            for (ulong j = 0; j < length; j++)
            {
                array[j] = memArr[j];
            }

            delete.Apply(memArr);
        }



        /// <summary>
        /// Time - O(length^2).
        /// Memory - O(1)
        /// </summary>
        public void _leftRotation_(ulong steps, ulong length, char* array)
        {
            for (ulong i = 0; i < steps; i++)
            {
                char first = array[0];
                for (ulong j = 1; j < length; j++) array[j - 1] = array[j];
                array[length - 1] = first;
            }
        }

        public bool equals(ulong length1, char* array1, ulong length2, char* array2)
        {
            if (length1 != length2) return false;
            if (array1 == array2) return true;
            for (ulong i = 0; i < length1; i++)
            {
                if (array1[i] != array2[i]) return false;
            }

            return true;
        }
		}

}
}

