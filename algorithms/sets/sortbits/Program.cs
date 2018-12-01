using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    class Solution
    {
        static void Main(string[] args)
        {
            var path = ("C:/windows/assembly");

            Console.WriteLine(args.Count());
           string[] arg = args[0].Split(' ');
            int n = int.Parse(arg[0]);
            int k = int.Parse(arg[1]);
            //int n = 7;
            //int k = 12;
            int nk = 0;
            string mask = "1";
            for (int i = 0; i < n-1; i++)
            {
                mask += "0";
            }
            int intMask = Convert.ToInt32(mask, 2);

            Dictionary<int,int> previous = new Dictionary<int, int>();
            for (int i = 0; i < k; i++)
            {
                if (i == 0)
                {
                    previous[0] = 0;
                    continue;
                }
                else if (previous[i - 1] == 0 || ConvertX(previous[i - 1],n).Substring(0,1) == "1")
                {
                    nk = (nk << 1) + 1 ;
                    previous[i] = nk;
                    continue;
                }
                else
                {
                    int prev = previous[i - 1];
                    
                    previous[i] = prev << 1;
                    continue;
                }
            }
            //foreach (var previou in previous)
            //{
            //    Console.WriteLine(ConvertX(previou.Value, n));
            //}
            Console.WriteLine(ConvertX(previous[k - 1],n));
        }

        public static string ConvertX(int x,int n)
        {
            char[] bits = new char[32];
            int i = 0;

            while (x != 0)
            {
                bits[i++] = (x & 1) == 1 ? '1' : '0';
                x >>= 1;
            }

            Array.Reverse(bits, 0, i);
           var res = (new  string(bits)).Trim().Replace("\0","");
            var prefix = "";
            for (int j = 0; j < (n-res.Length); j++)
            {
                prefix += "0";
            }
            return prefix + res;
        }
    }
}
