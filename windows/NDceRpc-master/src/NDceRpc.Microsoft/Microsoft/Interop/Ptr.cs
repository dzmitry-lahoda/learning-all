using System;
using System.Runtime.InteropServices;

namespace NDceRpc.Microsoft.Interop
{
    public class Ptr<T> : IDisposable
    {
        private readonly GCHandle _handle;

        public Ptr(T data)
        {
            _handle = GCHandle.Alloc(data, GCHandleType.Pinned);
        }

        public T Data
        {
            get { return (T) _handle.Target; }
        }

        public IntPtr Handle
        {
            get { return _handle.AddrOfPinnedObject(); }
        }

        public void Dispose()
        {
            _handle.Free();
        }
    }
}