using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace NDceRpc.Microsoft.Interop
{
    /// <summary>
    /// Is thread safe.
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("{Handle}")]
    public abstract class RpcHandle : IDisposable
    {
        internal IntPtr Handle;
        private readonly List<IDisposable> _pinnedAddresses = new List<IDisposable>();


        internal IntPtr PinFunction<T>(T data)
            where T : class, ICloneable, ISerializable
        {
            var instance = new FunctionPtr<T>(data);
            lock (_pinnedAddresses)
            {
                _pinnedAddresses.Add(instance);
            }
          
            return instance.Handle;
        }

        internal IntPtr Pin<T>(T data)
        {
            return CreatePtr(data).Handle;
        }

        internal bool GetPtr<T>(out T value)
        {
            lock (_pinnedAddresses)
            {
                foreach (object o in _pinnedAddresses)
                {
                    if (o is T)
                    {
                        value = (T) o;
                        return true;
                    }
                }
                value = default(T);
                return false;
            }
        }

        internal Ptr<T> CreatePtr<T>(T data)
        {
            Ptr<T> ptr = new Ptr<T>(data);
            lock (_pinnedAddresses)
            {
                _pinnedAddresses.Add(ptr);
            }
            return ptr;
        }

        ~RpcHandle()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
        }

        public void Dispose(bool disposing)
        {
            try
            {
                RpcTrace.Verbose("RpcHandle.Dispose on {0}", Handle);

                if (Handle != IntPtr.Zero)
                {
                    DisposeHandle(ref Handle);
                }
                lock (_pinnedAddresses)
                {
                    for (int i = _pinnedAddresses.Count - 1; i >= 0; i--)
                    {
                        _pinnedAddresses[i].Dispose();
                    }
                    _pinnedAddresses.Clear();
                }
            }
            finally
            {
                Handle = IntPtr.Zero;
            }
            if (disposing)
            {
                GC.SuppressFinalize(this);
            }
        }

        protected abstract void DisposeHandle(ref IntPtr handle);

        protected bool Equals(RpcHandle other)
        {
            return Handle.Equals(other.Handle);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            if (Handle == IntPtr.Zero) return false;// if handle not initialized we cannot state equality, better state unequlity
            return Equals((RpcHandle) obj);
        }

        public override int GetHashCode()
        {
            return Handle.GetHashCode();
        }
    }
}