using System;
using System.IO;
      
namespace Itransition.Training.Task04
{
    /// <summary>
    /// This type provides possibility to dump direcory tree to console
    /// </summary>
    public static class DirectoryTree
    {
        /// <summary>
        /// Dumps tree to console
        /// </summary>
        /// <param name="StartDirectory">Directory from wich dumping should be started</param>
        public static void DumpTree(String startDirectory)
        {
            String Spaces = "";
            Recursion(startDirectory, Spaces);
        }

        private static void Recursion(String startDirectory, String spaces)
        {
            try
            {
                String[] Directories = Directory.GetDirectories(startDirectory);
                if (startDirectory.Length != 0)
                {
                    spaces += " ";
                    foreach (String Dir in Directories)
                    {
                        String DirToShow = Dir.Substring(Dir.LastIndexOf(@"\") + 1, Dir.Length - Dir.LastIndexOf(@"\") - 1);
                        Console.WriteLine(spaces + DirToShow);
                        Recursion(Dir, spaces);
                    }

                }
            }
            catch (Exception e)
            {
                if ((e is UnauthorizedAccessException) && (spaces.Length > 0))
                {
                    Console.WriteLine(spaces + e.Message);
                }
                else
                {
                    throw;
                }
            }
        }

    }

}

