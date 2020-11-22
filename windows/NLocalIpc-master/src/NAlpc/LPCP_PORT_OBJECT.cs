using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace NAlpc
{
    [StructLayout(LayoutKind.Sequential)]
    public struct LPCP_PORT_OBJECT
    {
        public IntPtr ConnectionPort;
        public IntPtr ConnectedPort;
        public LPCP_PORT_QUEUE MsgQueue;
        public CLIENT_ID Creator;
        public IntPtr ClientSectionBase;
        public IntPtr ServerSectionBase;
        public IntPtr PortContext;
        public IntPtr ClientThread;
        public SECURITY_QUALITY_OF_SERVICE SecurityQos;
        public SECURITY_CLIENT_CONTEXT StaticSecurity;
        public LIST_ENTRY LpcReplyChainHead;
        public LIST_ENTRY LpcDataInfoChainHead;

        //      union
        //{
        //     PEPROCESS ServerProcess;
        //     PEPROCESS MappingProcess;
        //};
        public IntPtr MappingProcess;

        public ushort MaxMessageLength;
        public ushort MaxConnectionInfoLength;
        public uint Flags;
        public KEVENT WaitEvent;

        //TEMP: buffer
        public byte b1;
        public byte b2;
        public byte b3;
        public byte b4;
        public byte b5;
        public byte b6;
        public byte b7;
        public byte b8;
        public byte b9;
        public byte b10;
        public byte b11;
        public byte b12;
        public byte b13;
        public byte b14;
        public byte b15;
        public byte b16;
        public byte b17;
        public byte b18;
        public byte b19;
        public byte b20;
        public byte b21;
        public byte b22;
        public byte b23;
        public byte b24;
        public byte b25;
        public byte b26;
        public byte b27;
        public byte b28;
        public byte b29;
        public byte b30;
        public byte b31;


    }
}
