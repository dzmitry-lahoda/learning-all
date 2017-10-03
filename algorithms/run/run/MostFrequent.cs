/*
Given an integer M in  [1..16,000] and a zero-indexed array A (a in [0..M]) consisting of N in [1..240,000] non-negative integers 
returns the value (or one of the values) that occurs most often in this array.
For example, given M = 3 and array A such that:
  A[0] = 1
  A[1] = 2
  A[2] = 3
  A[3] = 3
  A[4] = 1
  A[5] = 3
  A[6] = 1
the function may return 1 or 3.

worst-case  complexity 
time  is O(N+M);
space is O(M), beyond A storage (not counting the storage required for A arguments).
A modified.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
namespace run
{
    class MostFrequent
    {

        internal void Test()
        {
            Assert.Equal(solution(3, new[] { 1, 2, 3, 3, 1, 3, 1 }), 1);
            Assert.Equal(solution(7, new[] { 1, 2, 7, 7, 7, 3, 1 }), 7);
            Assert.Equal(solution(0, new[] { 0 }), 0);
            Assert.Equal(0, solution(1, new[] { 1, 0 }));

            Assert.Equal(0, solution(3, new[] { 1, 2, 3, 0, 0, 0 }));
        }

        public int solution(int M, int[] A)
        {
            var m = new int[M + 1];
            foreach (var i in A)
            {
                m[i]++;
            }

            var max = 0;
            var iter = 0;
            for (int i = 0; i < m.Length; i++)
            {
                if (m[i] > max)
                {
                    max = m[i];
                    iter = i;
                }
            }

            return iter;
        }
    }
}
