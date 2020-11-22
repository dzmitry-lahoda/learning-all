using System;
using System.Collections.Generic;
using System.Text;

namespace EPAM.Trainings.Zadanie1_6
{
    public class Demo
    {
        public Demo(int val1, int val2)
        {
            Console.WriteLine("Constructor with the values {0}, {1}" +
                  " in domain {2} called", val1, val2,
                  AppDomain.CurrentDomain.FriendlyName);
        }
    }
    static class Program
    {
        static void Main()
        {
            AppDomain testDomain = AppDomain.CreateDomain("testDomain");
            testDomain.Load(@"Zadanie1.1");
            System.Reflection.Assembly[] assemblies = testDomain.GetAssemblies();
            foreach (System.Reflection.Assembly var in assemblies)
            {
                Console.WriteLine(var.FullName);
            }

            AppDomain.Unload(testDomain);
            Console.ReadLine();
        }
    }

}
