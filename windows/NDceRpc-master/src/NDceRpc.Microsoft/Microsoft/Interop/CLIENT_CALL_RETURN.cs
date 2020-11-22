using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace NDceRpc.Microsoft.Interop
{
    [StructLayout(LayoutKind.Explicit)] // union
    public struct CLIENT_CALL_RETURN
    {
        [FieldOffset(0)]
        IntPtr Pointer;

        [FieldOffset(0)]
        int Simple;
    }
}
