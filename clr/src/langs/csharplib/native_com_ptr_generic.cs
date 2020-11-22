using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace unsafe_com_generic
{
    public unsafe struct Com2<T> where T : unmanaged
    {
        T* ptr;
    }

    public unsafe struct Com<T> where T:unmanaged, void*
    {

    }

    public struct MyDevice { }

    public static class Extensions
    {

    }

    public static class TestClass
    {
        public static void Test()
        {
            //Error CS0306  The type 'void*' may not be used as a type argument 
            //var t = new Com<void*>();
            //var t2 = new Com<MyDevice*>();
            var c2 = new Com2<MyDevice>();
        }
    }
}