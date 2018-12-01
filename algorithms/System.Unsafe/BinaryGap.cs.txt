using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace run
{
    class BinaryGap
    {
        public void test()
        {
            Console.WriteLine(Int32.MaxValue);
            Console.WriteLine(Math.Pow((long)2, 31));
            Console.WriteLine(Convert.ToString(0, 2));
            Console.WriteLine(Convert.ToString(1, 2));
            Console.WriteLine(Convert.ToString(2, 2));
            Console.WriteLine(Convert.ToString(9, 2));
            Console.WriteLine(Convert.ToString(15, 2));
            Console.WriteLine(Convert.ToString(20, 2));
            Console.WriteLine(Convert.ToString(529, 2));
            Console.WriteLine(Convert.ToString(1041, 2));
            Console.WriteLine(Convert.ToString(Int32.MaxValue, 2));
            Console.WriteLine(Convert.ToInt32("1000000000000000000000000000001", 2));
            var p = new BinaryGap();
            Assert.Equal(0, p.solution(0));
            Assert.Equal(0, p.solution(1));
            Assert.Equal(2, p.solution(9));
            Assert.Equal(4, p.solution(529));
            Assert.Equal(1, p.solution(20));
            Assert.Equal(0, p.solution(15));
            Assert.Equal(5, p.solution(1041));
            Assert.Equal(0, p.solution(Int32.MaxValue));
            Assert.Equal(29, p.solution(1073741825));
        }

        public int solution(int n)
        {
            int max = 0, count = 0;
            n |= n - 1;
            while (n != n >> 1)
            {
                n >>= 1;
                if ((n & 1) == 1)
                {
                    if (count > max)
                        max = count;
                    count = 0;
                }
                else
                    count++;
            }
            return max;
        }

        public int solution2(int N)
        {
            if (N < 0) throw new ArgumentException("Should non negative", nameof(N));
            if (N == 0 || N == 1) return 0;
            // not optimal solution (should use binary operators of shifs)
            // but at as it uses visual representaion I am exatly sure that will solve with limited time
            var value = Convert.ToString(N, 2);
            var counter = 0;
            var maxCounter = 0;
            for (var i = 0; i < value.Length; i++)
            {
                if (value[i] == '1')
                {
                    maxCounter = Math.Max(maxCounter, counter);
                    counter = 0;
                }
                else
                {
                    counter++;
                }
            }

            return maxCounter;
        }
    }
}
