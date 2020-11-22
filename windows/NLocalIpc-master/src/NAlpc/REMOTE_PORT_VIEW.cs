using System;
using System.Runtime.InteropServices;

namespace NAlpc
{
    /// <summary>
    /// Define structure for shared memory coming from remote side of the port
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct REMOTE_PORT_VIEW
    {

        ulong Length;                      // Size of this structure
        UIntPtr ViewSize;                    // The size of the view (bytes)
        IntPtr ViewBase;                    // Base address of the view

    }
}