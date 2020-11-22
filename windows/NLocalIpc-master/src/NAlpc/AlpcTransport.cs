using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NAlpc
{
    ///ALPC for Windows
    public class AlpcTransport : System.ServiceModel.Channels.CommunicationObject
    {
        private string _portName;
        private AlpcPortHandle _handle;

        public AlpcTransport(string portName)
        {
            _portName = portName;
        }

        [StructLayout(LayoutKind.Sequential)]
        struct ChunkCarrierMessage
        {
            public PORT_MESSAGE Header;

            public uint Command;
            public byte[] Chunk;
        }

        protected override TimeSpan DefaultCloseTimeout
        {
            get
            {
                // local user will not consider this time as hang or freeze
                return TimeSpan.FromSeconds(3);
            }
        }

        protected override TimeSpan DefaultOpenTimeout
        {
            get
            {
                // local user will not consider this time as hang or freeze
                return TimeSpan.FromSeconds(3);
            }
        }

        protected override void OnAbort()
        {

        }

        protected override IAsyncResult OnBeginOpen(TimeSpan timeout, AsyncCallback callback, object state)
        {
            Task open = null;
            open  = Task.Factory.StartNew(x =>
                {
                _handle = new NAlpc.AlpcPortHandle();
                var attributes = new OBJECT_ATTRIBUTES();
                int status = NativeMethods.NtCreatePort(out _handle, ref attributes, 100, 100, 50);
                if (status != 0)
                    throw new Win32Exception(status);
                if (callback !=null)
                  callback(open);
            },state);
            open.Wait(timeout);
            return open;

        }

        protected override IAsyncResult OnBeginClose(TimeSpan timeout, AsyncCallback callback, object state)
        {
            Task close = null;
            close = Task.Factory.StartNew(x =>
            {
                _handle.Dispose();
                if (callback != null)
                    callback(close);
            }, state);
            close.Wait(timeout);
            return close;
        }

        protected override void OnClose(TimeSpan timeout)
        {
            OnBeginClose(timeout, null, null);
        }

        protected override void OnOpen(TimeSpan timeout)
        {
            OnBeginOpen(timeout, null, null);
        }

        protected override void OnEndClose(IAsyncResult result)
        {
        }

        protected override void OnEndOpen(IAsyncResult result)
        {
        }
    }
}
