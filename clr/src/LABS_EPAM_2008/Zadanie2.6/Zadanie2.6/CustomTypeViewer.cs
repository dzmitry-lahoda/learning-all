using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace EPAM.Trainings.Zadanie2_6
{
    public static class CustomTypeViewer
    {

        public static void Display(String typeName, String assemblyNAme)
        {
            Console.WriteLine("***** Welcome to CustomTypeViewer *****");
            Console.WriteLine("***** Type Name {0}*****", typeName);
            try
            {
                Type[] types = Assembly.Load(assemblyNAme).GetTypes();
                Type type=null;
                foreach (Type t in types)
                {
                    if (t.ToString()==typeName.ToString())
                    {
                        type = t;
                        break;
                    }
                }
                Console.WriteLine("");
                if (!type.IsGenericType)
                {
                    ListVariousStats(type);
                    ListFields(type);
                    ListProps(type);
                    ListMethods(type);
                    ListInterfaces(type);
                }
                else
                {
                    type = type.GetGenericTypeDefinition();
                    foreach (Type t in types)
                    {
                        Console.WriteLine(t.Name);
                        ListVariousStats(t);
                        ListFields(t);
                        ListProps(t);
                        ListMethods(t);
                        ListInterfaces(t);
                    }
                }
            }
            catch
            {
                Console.WriteLine("Sorry, can't find type");
            }
        }

        /// <summary>
        /// Display method names of type
        /// </summary>
        /// <param name="t"></param>
        public static void ListMethods(Type t)
        {
            Console.WriteLine("***** Methods *****");
            MethodInfo[] mi = t.GetMethods();
            foreach (MethodInfo m in mi)
                Console.WriteLine("->{0}", m.Name);
            Console.WriteLine("");
        }

        /// <summary>
        /// Display field names of type
        /// </summary>
        /// <param name="t"></param>
        public static void ListFields(Type t)
        {
            Console.WriteLine("***** Fields *****");
            FieldInfo[] fi = t.GetFields();
            foreach (FieldInfo field in fi)
                Console.WriteLine("->{0}", field.Name);
            Console.WriteLine("");
        }

       /// <summary>
        /// Display property names of type
       /// </summary>
       /// <param name="t"></param>
        public static void ListProps(Type t)
        {
            Console.WriteLine("***** Properties *****");
            PropertyInfo[] pi = t.GetProperties();
            foreach (PropertyInfo prop in pi)
                Console.WriteLine("->{0}", prop.Name);
            Console.WriteLine("");
        }

        /// <summary>
        /// Display implemented interfaces
        /// </summary>
        /// <param name="t"></param>
        public static void ListInterfaces(Type t)
        {
            Console.WriteLine("***** Interfaces *****");
            Type[] ifaces = t.GetInterfaces();
            foreach (Type i in ifaces)
                Console.WriteLine("->{0}", i.Name);
        }

        /// <summary>
        /// Displaying Various Odds and Ends
        /// </summary>
        /// <param name="t"></param>
        public static void ListVariousStats(Type t)
        {
            Console.WriteLine("***** Various Statistics *****");
            Console.WriteLine("Base class is: {0}", t.BaseType);
            Console.WriteLine("Is type abstract? {0}", t.IsAbstract);
            Console.WriteLine("Is type sealed? {0}", t.IsSealed);
            Console.WriteLine("Is type generic? {0}", t.IsGenericTypeDefinition);
            Console.WriteLine("Is type aclass type? {0}", t.IsClass);
            Console.WriteLine("");
        }
    }
}
