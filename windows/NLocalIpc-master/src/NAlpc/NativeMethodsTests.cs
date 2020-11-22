using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace NAlpc.Tests
{
    [TestFixture]
    public class NativeMethodsTests
    {
        [Test]
        public void NtCreatePort()
        {
            var name = "\\" + this.GetType().Name + MethodBase.GetCurrentMethod().Name;
            NAlpc.AlpcPortHandle handle = null;
            var attributes = new OBJECT_ATTRIBUTES();
            int status = NativeMethods.NtCreatePort(out handle, ref attributes, 100, 100, 50);
            AlpcErrorHandler.Check(status);
        
            Assert.AreEqual(Constants.S_OK, status);
            IntPtr realPointer = handle.DangerousGetHandle();
            Assert.AreNotEqual(IntPtr.Zero, realPointer);
            Assert.IsFalse(handle.IsInvalid);
            Assert.IsFalse(handle.IsClosed);
            handle.Dispose();
            Assert.IsTrue(handle.IsClosed);
        }

        [Test]
        public void NtCreatePort_DebugView()
        {
            var name = "\\" + this.GetType().Name + MethodBase.GetCurrentMethod().Name;
            NAlpc.AlpcPortHandle handle = null;
            var attributes = new OBJECT_ATTRIBUTES();
            int status = NativeMethods.NtCreatePort(out handle, ref attributes, 100, 100, 50);
            AlpcErrorHandler.Check(status);
            var underlyingObject = handle.DebugView;
            Assert.IsNotNull(underlyingObject);
            Assert.AreEqual(Constants.S_OK, status);
            IntPtr realPointer = handle.DangerousGetHandle();
            Assert.AreNotEqual(IntPtr.Zero, realPointer);
            Assert.IsFalse(handle.IsInvalid);
            Assert.IsFalse(handle.IsClosed);
            handle.Dispose();
            Assert.IsTrue(handle.IsClosed);
        }

   

        [Test]
        public void NtCreatePort_NtListenPort()
        {
            var name = "\\" + this.GetType().Name + MethodBase.GetCurrentMethod().Name;
            NAlpc.AlpcPortHandle handle = null;
            var attributes = new OBJECT_ATTRIBUTES();
            NativeMethods.NtCreatePort(out handle, ref attributes, 100, 100, 50);
            var msg = new MY_TRANSFERRED_MESSAGE();
            var wait = Task.Factory.StartNew(() =>
                {
                    var status = NativeMethods.NtListenPort(handle, ref msg.Header);
                    Assert.AreNotEqual(0, status);
                });
            var listenBlocksThread = wait.Wait(100);
            Assert.IsFalse(listenBlocksThread);
            handle.Dispose();
        }


        [Test]
        [ExpectedException(typeof(Win32Exception))]
        public void NtCreatePort_NtAcceptConnectPort()
        {
            var name = "\\" + this.GetType().Name + MethodBase.GetCurrentMethod().Name;
            NAlpc.AlpcPortHandle handle = null;
            var attributes = new OBJECT_ATTRIBUTES();
            NativeMethods.NtCreatePort(out handle, ref attributes, 100, 100, 50);
            var msg = new MY_TRANSFERRED_MESSAGE();

            NAlpc.AlpcPortHandle serverCommunicationPort = null;

            IntPtr optional = IntPtr.Zero;
            int status = NativeMethods.NtAcceptConnectPort(out serverCommunicationPort, optional, ref msg.Header, true, optional,
                                              optional);
            AlpcErrorHandler.Check(status);
            handle.Dispose();
        }



        [Test]
        [ExpectedException(typeof(Win32Exception))]
        public void NtConnectPort()
        {
            var handle = new AlpcPortHandle();
            var name = "\\" + this.GetType().Name + MethodBase.GetCurrentMethod().Name;
            IntPtr optional = IntPtr.Zero;
            uint maxMessageLenght = 100;
            var SecurityQos = new SECURITY_QUALITY_OF_SERVICE();
            int status = NativeMethods.NtConnectPort(out handle, name, ref SecurityQos, out optional, out optional, ref maxMessageLenght, out optional, out optional);
            AlpcErrorHandler.Check(status);

        }


        [Test]
        public void NtCreate_Listen_AcceptConnect_Connect()
        {
            var name = "\\" + this.GetType().Name + MethodBase.GetCurrentMethod().Name;
            NAlpc.AlpcPortHandle serverConnectionPort = null;
            var attributes = new OBJECT_ATTRIBUTES();
            uint maxMessageLenght = 30;
            int serverCreatePortStatus = NativeMethods.NtCreatePort(out serverConnectionPort, ref attributes, 50, maxMessageLenght, 10);
            AlpcErrorHandler.Check(serverCreatePortStatus);
            var msg = new MY_TRANSFERRED_MESSAGE();
            var wait = Task.Factory.StartNew(() =>
            {
                var serverListenPortStatus = NativeMethods.NtListenPort(serverConnectionPort, ref msg.Header);
                AlpcErrorHandler.Check(serverListenPortStatus);
                NAlpc.AlpcPortHandle serverComuicationPort = null;
                IntPtr optional1 = IntPtr.Zero;
                int serverAcceptConnectPort = NativeMethods.NtAcceptConnectPort(out serverComuicationPort, optional1, ref msg.Header, true, optional1,
                                                  optional1);
                AlpcErrorHandler.Check(serverAcceptConnectPort);
            });
            var listenBlocksThread = wait.Wait(150);
            
   
            IntPtr optional = IntPtr.Zero;
           
            var SecurityQos = SECURITY_QUALITY_OF_SERVICE.Create(SECURITY_IMPERSONATION_LEVEL.SecurityImpersonation, false, true);
            AlpcPortHandle clientHandle;

            int status = NativeMethods.NtConnectPort(out clientHandle, name, ref SecurityQos, out optional, out optional, ref maxMessageLenght, out optional, out optional);
            var ex = new Win32Exception(status);
            var err= Marshal.GetLastWin32Error();
            var ex2 = new Win32Exception(err);
            Assert.IsFalse(listenBlocksThread);
            serverConnectionPort.Dispose();
        }

  


        [Test]
        public void NtListenPort()
        {
            var handle = new AlpcPortHandle();
            var msg = new MY_TRANSFERRED_MESSAGE();
            var wait = Task.Factory.StartNew(() =>
            {
                var status = NativeMethods.NtListenPort(handle, ref msg.Header);
                Assert.AreNotEqual(0, status);
            });
            var listenBlocksThread = wait.Wait(100);
            Assert.IsTrue(listenBlocksThread);

        }



        [StructLayout(LayoutKind.Sequential)]
        struct MY_TRANSFERRED_MESSAGE
        {
            public PORT_MESSAGE Header;

            public uint Command;
            //public char[] MessageText; //48
        }
    }
}
