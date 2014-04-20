using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PreparingToInterview
{
    //Implement an algorithm to determine if a string has all unique characters  What if you
    //can not use additional data structures?
    class Program1
    {
        public static void Main1()
        {
            var strToCheck = "sdflk.;";
            var uniqueChars = new HashSet<char>(strToCheck.ToCharArray());
            //
            bool unique = true;
            for (int i = 0; i < strToCheck.Length; i++)
            {
                for (int j = i; j < strToCheck.Length; j++)
                {
                    if (i == j) continue;
                    //check
                }
            }

            // ASCII - bool[256]
            //  sort
            // bitwise ops for a-z

            //
            Console.WriteLine(strToCheck.Distinct().Count() == strToCheck.Count());
            
            //
            Console.WriteLine(strToCheck.Length == uniqueChars.Count);
        }
    }
}
