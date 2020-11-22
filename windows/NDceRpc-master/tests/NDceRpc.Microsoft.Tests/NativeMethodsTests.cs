using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using NDceRpc.ExplicitBytes;
using NDceRpc.Microsoft.Interop;
using NUnit.Framework;

namespace NDceRpc.Test
{
    [TestFixture]
    public class NativeMethodsTests
    {
        [Test]
        public void RpcBindingSetAuthInfo_null_notOk()
        {
            var identity = new SEC_WINNT_AUTH_IDENTITY();
            var status = NativeMethods.RpcBindingSetAuthInfo(IntPtr.Zero, "", RPC_C_AUTHN_LEVEL.RPC_C_AUTHN_LEVEL_NONE,
                                                            RPC_C_AUTHN.RPC_C_AUTHN_NONE, ref identity, 0);
            Assert.AreNotEqual(RPC_STATUS.RPC_S_OK, status);
        }


        private class ServerRpcHandle : RpcHandle
        {
            protected override void DisposeHandle(ref IntPtr handle)
            {

            }
        }

        [Test]
        public void RpcObjectSetType_serverInterfacesRegistered_OK()
        {
            ExplicitBytesExecute dummy = Dummy;
            var handle = new ServerRpcHandle();
            var iid = Guid.NewGuid();
            Ptr<RPC_SERVER_INTERFACE> sIf = ServerInterfaceFactory.Create(handle, iid, RpcRuntime.TYPE_FORMAT, RpcRuntime.FUNC_FORMAT,
                                                                   dummy);
            var mgr = Guid.NewGuid();
            unsafe
            {
                var unmanaged = Marshal.AllocCoTaskMem(sizeof(Guid));
                Marshal.StructureToPtr(mgr, unmanaged, false);
                var registerResult = NativeMethods.RpcServerRegisterIf(sIf.Handle, unmanaged, IntPtr.Zero);
                Assert.AreEqual(RPC_STATUS.RPC_S_OK, registerResult);

                var setTypeResult = NativeMethods.RpcObjectSetType(ref iid, ref mgr);
                Assert.AreEqual(RPC_STATUS.RPC_S_OK, setTypeResult);
            }



        }

        private uint Dummy(IntPtr clienthandle, uint szinput, IntPtr input, out uint szoutput, out IntPtr output)
        {
            szoutput = 0;
            output = IntPtr.Zero;
            return (uint)RPC_STATUS.RPC_S_OK;
        }
    }
}
