using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Specialized;

namespace EPAM.Trainings.Zadanie1_7
{
    static class Program
    {
        static void Main(string[] args)
        {
            
            StringDictionary sd = CommandLine.Parse(args);
            int n;
            if (Int32.TryParse(sd["n"], out n))
            {
                List<String> stringList = new List<string>();
                while (true)
                {
                    String str = Console.ReadLine();
                    if (str.Length > 0)
                    {
                        stringList.Add(str);
                        int index = 0;
                        int length =Int32.MaxValue;
                        foreach (String var in stringList)
                        {
                            if (var.Length < length)
                            {
                                length = var.Length;
                                index = stringList.IndexOf(var);
                                str = var;
                            }
                        }
                        if (str.Length > n)
                        {
                            str = str.Substring(0, n);
                        }
                        Console.WriteLine(String.Format("{0} elements of the shortest string= ", n) + str);
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }
    }
}
