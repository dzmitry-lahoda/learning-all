using System.Collections.Unsafe.bit32;
using System;
using System.Collections.Generic;
using System.Text;
using static System.Collections.Unsafe.bit32.stdlib;
namespace System.Collections.Unsafe
{
    //public unsafe interface IFatAllocator
    //{
    //    memory Apply(uint length);
    //}

    //public unsafe struct memory : IDisposable
    //{
    //    public memory(IDelete delete, void** root)
    //    {
    //        this.root = root;
    //        this.delete = delete;
    //    }

    //    private void** root;
    //    private IDelete delete;

    //    public void Dispose()
    //    {
    //        delete.Apply(this.root);
    //    }
    //}

    public  unsafe partial class intArrayExtensions
    {
        public int* counts(int nums_size, int* nums, int maxes_size, int* maxes, int* result_size)
        {
            return null;
        }

        int* oddNumbers(int l, int r, int* result_size)
        {
            *result_size = r - l;
            if (l % 2 != 0)
            {
                (*result_size)++;
            }
            else
            {
                l += 1;
            }

            if (r % 2 != 0)
            {
                (*result_size)++;
            }
            else
            {
                r -= 1;
            }

            *result_size /= 2;
            int* res = (int*)malloc(*result_size * sizeof(int));
            int counter = 0;
            while (l <= r)
            {
                res[counter] = l;
                l += 2;
                counter += 1;
            }

            return res;
        }

        static char* NO;
        static char* YES;
        
        static intArrayExtensions()
        {
            NO = (char*)malloc(2 * sizeof(char));
            NO[0] = 'N';
            NO[1] = 'O';

            YES = (char*)malloc(3 * sizeof(char));
            YES[0] = 'Y';
            YES[1] = 'E';
            YES[2] = 'S';
        }
    }
}
