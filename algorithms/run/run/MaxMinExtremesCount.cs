/*
 A non-empty zero-indexed array A consisting of N integers is given. A local minimum in the array is a pair of indices (P,Q) such that:
•	0 ≤ P ≤ Q ≤ N−1,
•	A[P] = A[P+1] = ... = A[Q−1] = A[Q],
•	if P > 0 then A[P−1] > A[P], and
•	if Q < N−1 then A[Q] < A[Q+1].
In other words, a local minimum is a one or contiguous fragment of the array containing equal elements that are smaller than any adjacent elements in the array.
Similarly, a local maximum in the array is a pair of integers (P,Q) such that:
•	0 ≤ P ≤ Q ≤ N−1,
•	A[P] = A[P+1] = ... = A[Q−1] = A[Q],
•	if P > 0 then A[P−1] < A[P], and
•	if Q < N−1 then A[Q] > A[Q+1].
In other words, a local maximum is a one or contiguous fragment of the array containing equal elements  that are larger than any adjacent elements in the array.
Local minima and local maxima are local extrema. 
If all the elements in the array are equal, then (0,N−1) is both local maximum and local minimum, counted as one local extremum.
Example 1:
  A[0]  = 2
  A[1]  = 2
  A[2]  = 3
  A[3]  = 4
  A[4]  = 3
  A[5]  = 3
  A[6]  = 2
  A[7]  = 2
  A[8]  = 1
  A[9]  = 1
  A[10] = 2
  A[11] = 5
 (3,3) and (11,11) - local maxima. 
 (0,1) and (8,9) -   local minima. 
 Number of local extemea - 4

Find Number of local extrema in A.

•	N is an integer within the range [1..2^17];
•	each element of array A is an integer within the range [−2^30..2^30].

Expected
worst-case time complexity is O(N);
worst-case space complexity is O(1), beyond input storage (not counting the storage required for input arguments).

Elements of input arrays can be modified.

*/

using Xunit;

namespace run
{
    class MaxMinExtremesCount
    {
        internal void Test()
        {
            Assert.Equal(solution(new int[] { }), 0);
            Assert.Equal(solution(new[] { 0 }), 1);
            Assert.Equal(solution(new[] { 1 }), 1);
            Assert.Equal(solution(new[] { 0, 1 }), 2);
            Assert.Equal(solution(new[] { 1, 0 }), 2);
            Assert.Equal(solution(new[] { 1, 0, 0, 1 }), 3);

            Assert.Equal(solution(new[] { 0, 1, 1, 0 }), 3);

            Assert.Equal(solution(new[] { 0, 1, 0, 1 }), 4);

            Assert.Equal(solution(new[] { 0, 1, 1, 2, 0 }), 3);

            Assert.Equal(solution(new[] { 0, 1, 1, 2, 3, 0 }), 3);

            Assert.Equal(solution(new[] { 0, 1, 3, 4, 5, 6 }), 2);

            Assert.Equal(solution(new[] { 2, 2, 3, 4, 3, 3, 2, 2, 1, 1, 2, 5 }), 4);
        }

        public int solution(int[] A)
        {
            if (A.Length == 0) return 0;

            bool? raising = null;
            var index = 0;
            var cursor = A[index];
            var localExtremes = 1;
            while (index < A.Length - 1)
            {
                var nextCursor = A[index + 1];
                if (nextCursor == cursor)
                {
                    cursor = nextCursor;
                }
                else
                {
                    if (!raising.HasValue)
                    {
                        localExtremes++;
                    }
                    else if (raising.Value)
                    {
                        if (nextCursor < cursor) localExtremes++;
                    }
                    else if (!raising.Value)
                    {
                        if (nextCursor > cursor) localExtremes++;
                    }

                    raising = cursor < nextCursor;
                    cursor = nextCursor;
                }

                index++;
            }


            return localExtremes;
        }
    }
}

