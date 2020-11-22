using System;
using System.Runtime.InteropServices;

namespace NAlpc
{
    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public struct CLIENT_ID
    {

        /// HANDLE->void*
        public System.IntPtr UniqueProcess;

        /// HANDLE->void*
        public System.IntPtr UniqueThread;
    }
}