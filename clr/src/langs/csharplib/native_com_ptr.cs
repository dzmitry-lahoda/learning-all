using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace unsafe_com
{
    public struct MyDevice { }

    public static class Extensions
    {
        ////Error CS1103  The first parameter of an extension method cannot be of type 'MyDevice*'	
        //public static unsafe void Release(this MyDevice* com)
        //{
        //    Debug.WriteLine((IntPtr)com);
        //}
        
        //public static unsafe void Render(this MyDevice* com)
        //{
        //    Debug.WriteLine((IntPtr)com);
        //}

        public static unsafe void Release(this ref MyDevice com)
        {
            Marshal.Release((IntPtr)Unsafe.AsPointer(ref com));
        }

        private static void Render(IntPtr com)
        {
            Console.WriteLine(com);
        }

        public static unsafe void Render(this ref MyDevice com)
        {
            Render((IntPtr)Unsafe.AsPointer(ref com));
        }

        public unsafe static ref MyDevice InitDevice()
        {
            // just a hack to allocated "com pointer"
            void* someCom = (void*)Marshal.AllocHGlobal(0);
            ref MyDevice x = ref Unsafe.AsRef<MyDevice>(someCom);
            return ref x;
        }
    }

    public static class TestClass
    {
        public static void Test()
        {
            ref var x = ref Extensions.InitDevice();
            x.Render();
            x.Release();
        }
    }
}