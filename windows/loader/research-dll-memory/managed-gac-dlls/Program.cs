using System;

using System.Reflection;
using System.Runtime.CompilerServices;


namespace managed_gac_dlls
{
    class Program
    {
        static void Main(string[] args)
        {



            Console.WriteLine("Load System.Core");
            Console.ReadKey();
            LoadSystemXml("System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");

            Console.WriteLine("Load System.Xml");
            Console.ReadKey();
            LoadSystemXml("System.Xml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");

            Console.WriteLine("Init types System.Xml (does not influences cause GAC assemblies are compiled)");
            Console.ReadKey();
            LoadSystemXmlTypes();

            Console.WriteLine("Load System.Configuration");
            Console.ReadKey();
            LoadSystemXml("System.Configuration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");

            Console.WriteLine("Load System.Xaml");
            Console.ReadKey();
            LoadSystemXml("System.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");

            Console.WriteLine("Load System.Xml.Linq ");
            Console.ReadKey();
            LoadSystemXml("System.Xml.Linq, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");


            Console.WriteLine("Load WindowsBase (WPF layer 1)");
            Console.ReadKey();
            LoadSystemXml("WindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35");


            Console.WriteLine("Init types WindowsBase (does not influences cause GAC assemblies are compiled)");
            Console.ReadKey();
            LoadWindowsBaseTypes();

            Console.WriteLine("Load PresentationCore (WPF layer 2)");
            Console.ReadKey();
            LoadSystemXml("PresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35");

            Console.WriteLine("Load PresentationFramework (WPF layer 3)");
            Console.ReadKey();
            LoadSystemXml("PresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35");

            

            Console.WriteLine("Load System.ServiceModel ");
            Console.ReadKey();
            LoadSystemXml("System.ServiceModel, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");

            Console.ReadKey();
        }

        [System.Runtime.CompilerServices.MethodImpl(MethodImplOptions.NoInlining)]
        private static void LoadWindowsBaseTypes()
        {
            var xmlType1 = typeof(System.Windows.Threading.Dispatcher);
            Console.WriteLine(xmlType1.Name);

            var xmlType2 = typeof(System.Windows.Interop.ComponentDispatcher);
            Console.WriteLine(xmlType2.Name);
        }



        [System.Runtime.CompilerServices.MethodImpl(MethodImplOptions.NoInlining)]
        private static void LoadSystemXmlTypes()
        {
            var xmlType1 = typeof(System.Xml.XmlDocument);
            new System.Xml.XmlDocument();
            Console.WriteLine(xmlType1.Name);
            var xmlType2 = typeof(System.Xml.XmlReader);
            Console.WriteLine(xmlType2.Name);
        }

        [System.Runtime.CompilerServices.MethodImpl(MethodImplOptions.NoInlining)]
        private static Assembly LoadSystemXml(string name)
        {
            var asm = Assembly.Load(name);
            Console.WriteLine(asm.FullName);
            GCCleanUp();
            return asm;
        }
        private static void GCCleanUp()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }
    }
}
