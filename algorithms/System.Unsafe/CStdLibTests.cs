using System.Collections.Unsafe;
using System.Collections.Unsafe.bit32;
using System;
using System.Collections.Unsafe;
using Xunit;
using static System.Collections.Unsafe.bit32.stdlib;

namespace ConsoleApp2
{
    internal unsafe class CStdLibTests
    {

        // optimize, i.g. == probablu would be more seldom case
        // or use bit byse xor to get needed values
        private static int comparator(int* a, int* b) => a == b ? 0 : ((a > b) ? 1 : -1);

        public void Test()
        {
            var intArray = new System.Collections.Unsafe.bit32.intArrayExtensions();
            //fixed (int* nums = new int[] { 1, 2, 3 })
            //fixed (int* maxes = new int[] { 2, 4 })
            //{
            //    int result = 0;
            //    var r = qwe.counts(3, nums, 2, maxes, &result);
            //    Assert.Equal(2, result);
            //    Assert.True(r[0] == 2);
            //    Assert.True(r[1] == 3);
            //}

            fixed (int* array = new int[] { 3, 4, 5, 1, 2 })
            {
                //intArray.merge_(array, 0, 3, 4);
                Assert.Equal(1, array[0]);
                Assert.Equal(2, array[1]);
                Assert.Equal(3, array[2]);
                Assert.Equal(4, array[3]);
                Assert.Equal(5, array[4]);
            }


            compareint z = comparator;
            void* function = (void*)System.Runtime.InteropServices.Marshal.GetFunctionPointerForDelegate(z);




            fixed (int* a = new int[] { 3, 2, 1 })
            {
                stdlibExtensions.qsort(a, 3, z);
            }
            


            var stringLiteral = stdlib._("ab");
            Assert.True(stringLiteral[0] == 'a');
            Assert.True(stringLiteral[1] == 'b');
            Assert.True(stringLiteral[2] == 0);
        }




        int anagram(char* s)
        {
            return 0;
        }
    }
}