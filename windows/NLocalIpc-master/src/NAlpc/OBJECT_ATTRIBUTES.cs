using System;
using System.Runtime.InteropServices;

namespace NAlpc
{

    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public struct OBJECT_ATTRIBUTES
    {

        public int Length;
        public IntPtr RootDirectory;
        private IntPtr ObjectName;
        public uint Attributes;
        public IntPtr SecurityDescriptor;
        public IntPtr SecurityQualityOfService;

        ///// ULONG->unsigned int
        //public uint Length;

        ///// HANDLE->void*
        //public System.IntPtr RootDirectory;

        ///// PUNICODE_STRING->_UNICODE_STRING*
        //public System.IntPtr ObjectName;

        ///// ULONG->unsigned int
        //public uint Attributes;

        ///// PVOID->void*
        //public System.IntPtr SecurityDescriptor;

        ///// PVOID->void*
        //public System.IntPtr SecurityQualityOfService;

        public static unsafe void InitializeObjectAttributes_Ptr(ref OBJECT_ATTRIBUTES p, string n, uint a, IntPtr r, IntPtr s)
        {
            p.Length = sizeof(OBJECT_ATTRIBUTES);
            p.RootDirectory = r;
            p.Attributes = a;

            p.ObjectName = Marshal.AllocHGlobal(Marshal.SizeOf(n));
            Marshal.StructureToPtr(n, p.ObjectName, false);

            p.ObjectName = Marshal.StringToHGlobalUni(n);
            p.SecurityDescriptor = s;
            p.SecurityQualityOfService = IntPtr.Zero;
        }

        public static unsafe void InitializeObjectAttributes_Ptr(ref OBJECT_ATTRIBUTES p, ref UNICODE_STRING n, uint a, IntPtr r, IntPtr s)
        {
            p.Length = sizeof(OBJECT_ATTRIBUTES);
            p.RootDirectory = r;
            p.Attributes = a;

            p.ObjectName = Marshal.AllocHGlobal(Marshal.SizeOf(n));
            Marshal.StructureToPtr(n, p.ObjectName, false);

            p.SecurityDescriptor = s;
            p.SecurityQualityOfService = IntPtr.Zero;
        }

        public static unsafe void InitializeObjectAttributes(ref OBJECT_ATTRIBUTES p, ref UNICODE_STRING n, uint a, IntPtr r, ref SECURITY_DESCRIPTOR s)
        {
            p.Length = sizeof (OBJECT_ATTRIBUTES);
            p.RootDirectory = r;
            p.Attributes = a;
            var pn = IntPtr.Zero;
            Marshal.StructureToPtr(n, pn, false);
            p.ObjectName = pn;
            var ps = IntPtr.Zero;
            Marshal.StructureToPtr(s, ps, false);
            p.SecurityDescriptor = ps;
            p.SecurityQualityOfService = IntPtr.Zero;
        }

   


    }
}