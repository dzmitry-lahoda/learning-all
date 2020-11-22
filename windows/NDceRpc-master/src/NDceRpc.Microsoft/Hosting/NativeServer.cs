using System;

namespace NDceRpc.Native
{
    /// <summary>
    /// Hosts native server interfaces provided by unmanaged code as pointer.
    /// </summary>
    public class NativeServer:Server
    {
        public NativeServer(IntPtr sIfHandle) :this(sIfHandle, IntPtr.Zero, IntPtr.Zero)
        {
        }

        public NativeServer(IntPtr sIfHandle, IntPtr mgrTypeUuid, IntPtr mgrEpv)
        {
            base.ServerRegisterInterface(sIfHandle, _handle, mgrTypeUuid, mgrEpv);
        }
    }
}