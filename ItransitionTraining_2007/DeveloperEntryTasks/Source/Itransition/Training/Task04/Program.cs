using System;
using System.IO;

namespace Itransition.Training.Task04
{

    class Program
    {
        static void Main(string[] args)
        {
            String StartDirectory="";
            if (args.Length != 0)
            {
                foreach (String s in args)
                {
                    StartDirectory+=s+" ";
                }
            }
            else
            {
                try
                {
                    StartDirectory = Directory.GetCurrentDirectory();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

            }
            try
            {
                DirectoryTree.DumpTree(StartDirectory);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
