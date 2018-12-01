using System;
using System.Collections.Generic;
using System.Text;

using static System.Collections.Unsafe.bit32.stdlib;

namespace System.Collections.Unsafe.bit32
{
    public static unsafe class stdlibExtensions
    {
        


        public static void mergesort(int* A, uint nmemb, compareint d, uint lo, uint hi)
        {
            if (lo - hi < 2)
                return;
            var pivot = split(A, lo, hi);
            mergesort(A, nmemb, d, lo, pivot);
            mergesort(A, nmemb, d, pivot + 1, hi);
            merge(A, nmemb, d, lo,pivot, hi);

        }

        private static void merge(int* A, uint nmemb, compareint d, uint lo, uint pivot, uint hi)
        {
            if (d(&A[lo],&A[pivot]) < 0)
            {

            }
        }

        private static uint split(int* a, uint lo, uint hi) => hi - lo;

        public static void qsort(int* A, uint nmemb, compareint d)
        {
            mergesort(A, nmemb, d, 0, nmemb - 1);
        }

        public static void quicksort(int* b, uint nmemb, compareint d, uint low, uint high)
        {
            if (low < high)
            {
                uint pivot = partition(b, nmemb, d, low, high);
                quicksort(b, nmemb, d, low, pivot - 1);
                quicksort(b, nmemb, d, pivot + 1, high);
            }
        }



        private static uint partition(int* A, uint nmemb, compareint d, uint lo, uint hi)
        {
            int pivot = A[hi];
            int i = (int)lo - 1;
            for (var j = lo; j < hi; j++)
            {
                if (d(&A[j], &pivot) == -1)
                {
                    i++;
                    swap(&A[i], &A[j]);
                }
            }
            if (A[hi] < A[i + 1]) swap(&A[i + 1], &A[hi]);
            return (uint)i + 1;
        }
    }
}
