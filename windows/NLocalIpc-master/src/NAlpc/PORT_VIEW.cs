using System;
using System.Runtime.InteropServices;

namespace NAlpc
{
    /// <summary>
    /// Define structure for initializing shared memory on the caller's side of the port
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct PORT_VIEW
    {

        ulong Length;                      // Size of this structure
        IntPtr SectionHandle;               // Handle to section object with
        // SECTION_MAP_WRITE and SECTION_MAP_READ
        SECTION SectionOffset;               // The offset in the section to map a view for
        // the port data area. The offset must be aligned 
        // with the allocation granularity of the system.
        UIntPtr ViewSize;                    // The size of the view (in bytes)
        IntPtr ViewBase;                    // The base address of the view in the creator 
        IntPtr ViewRemoteBase;              // The base address of the view in the process connected to the port.
    }
}