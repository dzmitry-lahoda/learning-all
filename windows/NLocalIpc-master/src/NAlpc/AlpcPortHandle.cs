using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace NAlpc
{
    public class AlpcPortHandle : SafeHandleZeroOrMinusOneIsInvalid
    {
        public AlpcPortHandle() : base(true)
        {
        }

        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
        protected override bool ReleaseHandle()
        {
            return NativeMethods.NtClose(this.handle) == 0;
        }

        public unsafe LPCP_PORT_OBJECT? DebugView
        {
            get
            {
                if (!this.IsClosed && !this.IsInvalid && this.handle != IntPtr.Zero)
                {
                    return (LPCP_PORT_OBJECT)Marshal.PtrToStructure(this.handle, typeof (LPCP_PORT_OBJECT));
                }
                return null;
            }
        }
    }
}