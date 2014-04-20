using System;
using System.Collections.Generic;
using System.Reflection;

namespace Itransition.Training.Task08
{
    public class Program
    {
        public static String HelpMessage= @"Type: Itransition.Training.Task08.exe ""asmFileName""
Where ""asmFileName"" is the fullpath to .NET assembly
Example: Itransition.Training.Task08.exe D:\ES\Source\Training\asd.dll
";
        public static List<String> GetListOfNamespaces(Assembly asm)
        {
            List<String> Namespaces = new List<String>(); 
            Type[] types = asm.GetTypes();
            foreach (Type t in types)
            {
                if (!Namespaces.Contains(t.Namespace))
                {
                    Namespaces.Add(t.Namespace);
                }
            }
            return Namespaces;
        }

        public static List<String> GetGroupedListOfNamespaces(Assembly asm)
        {
            List<String> Namespaces = GetListOfNamespaces(asm);
            List<String> GroupedList = new List<String>();
            Type[] types = asm.GetTypes();
            foreach (String s in Namespaces)
            {
                GroupedList.Add("");
                GroupedList.Add("Namespace: " + s);
                foreach (Type t in types)
                {
                    if (s == t.Namespace)
                    {
                        GroupedList.Add(" Type: " + t.Name);
                    }
                }
            }
            return GroupedList;
        }

        public static void ShowGroupedListOfNamespaces(List<String> GroupedList)
        {
            foreach (String s in GroupedList)
            {
                Console.WriteLine(s);
            }
        }

        static void Main(string[] args)
        {
            String asmFileName = "";
            if (args.Length > 0)
            {
                foreach (String s in args)
                {
                    asmFileName += s + " ";
                }
                try
                {
                    Assembly asm = Assembly.LoadFrom(asmFileName);
                    ShowGroupedListOfNamespaces(GetGroupedListOfNamespaces(asm));
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message + '\n' + '\n' + HelpMessage);
                }
            }
            else
            {
                Console.WriteLine('\n'+HelpMessage);
            }
            Console.ReadLine();
        }
    }
}
