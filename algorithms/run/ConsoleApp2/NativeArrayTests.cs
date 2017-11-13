using System.Collections.Unsafe;
using System.Collections.Unsafe.bit32;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ConsoleApp2
{
    public unsafe class NativeArrayTests
    {
        private Allocator allocator;
        private Delete delete;

        public unsafe struct Allocator : IintNew
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            public int* Apply(uint count) => (int*)System.Runtime.InteropServices.Marshal.AllocHGlobal((int)count * sizeof(long));
        }

        public unsafe struct Delete : IintDelete
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            public void Apply(int* mem)
            {
                Console.WriteLine("BeforeDelete");
                System.Runtime.InteropServices.Marshal.FreeHGlobal((IntPtr)mem);
            }
        }

        /// <summary>
        /// O(n^2) time and O(1) memory
        /// </summary>
        /// <param name="start"></param>
        /// <param name="steps"></param>
        /// <param name="length"></param>
        /// <param name="array"></param>
        public void _rightRotation_(uint start, uint steps, uint length, int* array)
        {
            for (uint i = start; i < steps; i++)
            {
                int last = array[length-1];
                for (uint j = start; j < length - 1 ; j++) array[j] = array[j + 1];
                array[start] = last;
            }
        }


        public void Test()
        {
            this.allocator = new Allocator();
            this.delete = new Delete();
            fast();
            no_allocation();
        }

        private void fast()
        {
            var sut = new System.Collections.Unsafe.bit32.intArrayExtensions();          

            fixed (int* ptr = new[] { 1 })
            {
                sut._leftRotation_(1, 1, ptr, this.allocator, this.delete);
                Assert.Equal(1, ptr[0]);
            }

            fixed (int* ptr = new[] { 1, 2 })
            {
                sut._leftRotation_(1, 2, ptr, this.allocator, this.delete);
                Assert.Equal(2, ptr[0]);
                Assert.Equal(1, ptr[1]);
            }

            fixed (int* ptr = new[] { 1, 2, 3, 4, 5 })
            {
                sut._leftRotation_(2, 5, ptr, this.allocator, this.delete);
                Assert.Equal(3, ptr[0]);
                Assert.Equal(4, ptr[1]);
                Assert.Equal(5, ptr[2]);
                Assert.Equal(1, ptr[3]);
                Assert.Equal(2, ptr[4]);
            }

            fixed (int* ptr = new[] { 1, 2, 3, 4, 5 })
            {
                _rightRotation_(0, 2, 5, ptr);
                Assert.Equal(4, ptr[0]);
                Assert.Equal(5, ptr[1]);
                Assert.Equal(1, ptr[2]);
                Assert.Equal(2, ptr[3]);
                Assert.Equal(3, ptr[4]);
            }

            fixed (int* ptr = new[] { 1, 2, 3, 4, 5 })
            {
                sut._leftRotation_(4, 5, ptr, this.allocator, this.delete);
                Assert.Equal(5, ptr[0]);
                Assert.Equal(1, ptr[1]);
                Assert.Equal(2, ptr[2]);
                Assert.Equal(3, ptr[3]);
                Assert.Equal(4, ptr[4]);
            }
        }

        struct b111
            {
            int a;
            int a2;
            int a3;
            int a4;
            int a5;
        }


        private static void no_allocation()
        {
            //void* a = null;
            //var z = ((b111*)a)[1];

            var sut = new System.Collections.Unsafe.bit32.intArrayExtensions();
            var one = new[] { 1 };
            var two = new[] { 1, 2 };
            var five = new[] { 1, 2, 3, 4, 5 };
            var fiveFor4 = new[] { 1, 2, 3, 4, 5 };

            fixed (int* ptr = one)
            {
                sut._leftRotation_(1, 1, ptr);
                Assert.Equal(1, one[0]);
            }

            fixed (int* ptr = two)
            {
                sut._leftRotation_(1, 2, ptr);
                Assert.Equal(2, two[0]);
                Assert.Equal(1, two[1]);
            }

            fixed (int* ptr = five)
            {
                sut._leftRotation_(2, 5, ptr);
                Assert.Equal(3, five[0]);
                Assert.Equal(4, five[1]);
                Assert.Equal(5, five[2]);
                Assert.Equal(1, five[3]);
                Assert.Equal(2, five[4]);
            }

            fixed (int* ptr = fiveFor4)
            {
                sut.leftRotationZero_(4, 5, ptr).unwrap();
                Assert.Equal(5, fiveFor4[0]);
                Assert.Equal(1, fiveFor4[1]);
                Assert.Equal(2, fiveFor4[2]);
                Assert.Equal(3, fiveFor4[3]);
                Assert.Equal(4, fiveFor4[4]);
            }
        }
    }
}
