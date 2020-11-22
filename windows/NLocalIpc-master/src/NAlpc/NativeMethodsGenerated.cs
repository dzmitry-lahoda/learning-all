using System;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;
using NAlpc;

namespace G
{



    public class Constants
    {

        /// MAX_LPC_DATA -> 0x130
        public const int MAX_LPC_DATA = 304;

        /// LPC_REQUEST -> 1
        public const int LPC_REQUEST = 1;

        /// LPC_REPLY -> 2
        public const int LPC_REPLY = 2;

        /// LPC_DATAGRAM -> 3
        public const int LPC_DATAGRAM = 3;

        /// LPC_LOST_REPLY -> 4
        public const int LPC_LOST_REPLY = 4;

        /// LPC_PORT_CLOSED -> 5
        public const int LPC_PORT_CLOSED = 5;

        /// LPC_CLIENT_DIED -> 6
        public const int LPC_CLIENT_DIED = 6;

        /// LPC_EXCEPTION -> 7
        public const int LPC_EXCEPTION = 7;

        /// LPC_DEBUG_EVENT -> 8
        public const int LPC_DEBUG_EVENT = 8;

        /// LPC_ERROR_EVENT -> 9
        public const int LPC_ERROR_EVENT = 9;

        /// LPC_CONNECTION_REQUEST -> 10
        public const int LPC_CONNECTION_REQUEST = 10;

        /// ALPC_REQUEST -> 0x2000 | LPC_REQUEST
        public const int ALPC_REQUEST = (8192 | Constants.LPC_REQUEST);

        /// ALPC_CONNECTION_REQUEST -> 0x2000 | LPC_CONNECTION_REQUEST
        public const int ALPC_CONNECTION_REQUEST = (8192 | Constants.LPC_CONNECTION_REQUEST);

        /// Warning: Generation of Method Macros is not supported at this time
        /// InitializeMessageHeader -> "(ph,l,t) {                                                                      
        ///    (ph)->u1.s1.TotalLength      = (USHORT)(l);                        
        ///    (ph)->u1.s1.DataLength       = (USHORT)(l - sizeof(PORT_MESSAGE)); 
        ///    (ph)->u2.s2.Type             = (USHORT)(t);                        
        ///    (ph)->u2.s2.DataInfoOffset   = 0;                                  
        ///    (ph)->ClientId.UniqueProcess = NULL;                               
        ///    (ph)->ClientId.UniqueThread  = NULL;                               
        ///    (ph)->MessageId              = 0;                                  
        ///    (ph)->ClientViewSize         = 0;                                  
        ///}"
        public const string InitializeMessageHeader = @"(ph,l,t) {                                                                      
    (ph)->u1.s1.TotalLength      = (USHORT)(l);                        
    (ph)->u1.s1.DataLength       = (USHORT)(l - sizeof(PORT_MESSAGE)); 
    (ph)->u2.s2.Type             = (USHORT)(t);                        
    (ph)->u2.s2.DataInfoOffset   = 0;                                  
    (ph)->ClientId.UniqueProcess = NULL;                               
    (ph)->ClientId.UniqueThread  = NULL;                               
    (ph)->MessageId              = 0;                                  
    (ph)->ClientViewSize         = 0;                                  
}";
    }

 


    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public struct CLIENT_ID
    {

        /// HANDLE->void*
        public System.IntPtr UniqueProcess;

        /// HANDLE->void*
        public System.IntPtr UniqueThread;
    }

    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public struct Anonymous_75329b3c_d2b1_43f5_8a3b_7343e032056e
    {

        /// USHORT->unsigned short
        public ushort DataLength;

        /// USHORT->unsigned short
        public ushort TotalLength;
    }

    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Explicit)]
    public struct Anonymous_09726424_1cd5_4946_8a36_006f5960a44d
    {

        /// Anonymous_75329b3c_d2b1_43f5_8a3b_7343e032056e
        [System.Runtime.InteropServices.FieldOffsetAttribute(0)]
        public Anonymous_75329b3c_d2b1_43f5_8a3b_7343e032056e s1;

        /// ULONG->unsigned int
        [System.Runtime.InteropServices.FieldOffsetAttribute(0)]
        public uint Length;
    }

    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public struct Anonymous_d1bcb9b5_d820_4940_8507_634538410a70
    {

        /// USHORT->unsigned short
        public ushort Type;

        /// USHORT->unsigned short
        public ushort DataInfoOffset;
    }

    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Explicit)]
    public struct Anonymous_bef29820_2d56_4737_8b7e_13e28f15d6ba
    {

        /// Anonymous_d1bcb9b5_d820_4940_8507_634538410a70
        [System.Runtime.InteropServices.FieldOffsetAttribute(0)]
        public Anonymous_d1bcb9b5_d820_4940_8507_634538410a70 s2;

        /// ULONG->unsigned int
        [System.Runtime.InteropServices.FieldOffsetAttribute(0)]
        public uint ZeroInit;
    }

    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Explicit)]
    public struct Anonymous_a5008fdd_7b56_4347_91f7_6ec9ca421a6a
    {

        /// CLIENT_ID->_CLIENT_ID
        [System.Runtime.InteropServices.FieldOffsetAttribute(0)]
        public CLIENT_ID ClientId;

        /// double
        [System.Runtime.InteropServices.FieldOffsetAttribute(0)]
        public double DoNotUseThisField;
    }

    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Explicit)]
    public struct Anonymous_2b34ce8d_4bbc_4eae_b342_a6e119b0ae42
    {

        /// ULONG_PTR->unsigned int
        [System.Runtime.InteropServices.FieldOffsetAttribute(0)]
        public uint ClientViewSize;

        /// ULONG->unsigned int
        [System.Runtime.InteropServices.FieldOffsetAttribute(0)]
        public uint CallbackId;
    }

    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public struct PORT_MESSAGE
    {

        /// Anonymous_09726424_1cd5_4946_8a36_006f5960a44d
        public Anonymous_09726424_1cd5_4946_8a36_006f5960a44d u1;

        /// Anonymous_bef29820_2d56_4737_8b7e_13e28f15d6ba
        public Anonymous_bef29820_2d56_4737_8b7e_13e28f15d6ba u2;

        /// Anonymous_a5008fdd_7b56_4347_91f7_6ec9ca421a6a
        public Anonymous_a5008fdd_7b56_4347_91f7_6ec9ca421a6a Union1;

        /// ULONG->unsigned int
        public uint MessageId;

        /// Anonymous_2b34ce8d_4bbc_4eae_b342_a6e119b0ae42
        public Anonymous_2b34ce8d_4bbc_4eae_b342_a6e119b0ae42 Union2;

        /// <summary>
        /// Macro for initializing the message header
        /// </summary>
        /// <param name="ph"></param>
        /// <param name="l"></param>
        /// <param name="t"></param>
        public static void InitializeMessageHeader(ref PORT_MESSAGE ph, ushort l, PORT_MESSAGE_TYPES t)
        {
            (ph).u1.s1.TotalLength = (ushort)(l);
            unsafe
            {
                (ph).u1.s1.DataLength = (ushort)(l - sizeof(PORT_MESSAGE));
            }
            (ph).u2.s2.Type = (ushort)t;
            (ph).u2.s2.DataInfoOffset = 0;


        }
    }

    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public struct PORT_VIEW
    {

        /// ULONG->unsigned int
        public uint Length;

        /// HANDLE->void*
        public System.IntPtr SectionHandle;

        /// ULONG->unsigned int
        public uint SectionOffset;

        /// SIZE_T->ULONG_PTR->unsigned int
        public uint ViewSize;

        /// PVOID->void*
        public System.IntPtr ViewBase;

        /// PVOID->void*
        public System.IntPtr ViewRemoteBase;
    }

    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public struct REMOTE_PORT_VIEW
    {

        /// ULONG->unsigned int
        public uint Length;

        /// SIZE_T->ULONG_PTR->unsigned int
        public uint ViewSize;

        /// PVOID->void*
        public System.IntPtr ViewBase;
    }

    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public struct SECURITY_QUALITY_OF_SERVICE
    {

        /// DWORD->unsigned int
        public uint Length;

        /// SECURITY_IMPERSONATION_LEVEL->_SECURITY_IMPERSONATION_LEVEL
        public SECURITY_IMPERSONATION_LEVEL ImpersonationLevel;

        /// SECURITY_CONTEXT_TRACKING_MODE->BOOLEAN->BYTE->unsigned char
        public byte ContextTrackingMode;

        /// BOOLEAN->BYTE->unsigned char
        public byte EffectiveOnly;
    }

  

    public partial class NativeMethods
    {

        /// Return Type: NTSTATUS->int
        ///PortHandle: PHANDLE->HANDLE*
        ///ObjectAttributes: POBJECT_ATTRIBUTES->_OBJECT_ATTRIBUTES*
        ///MaxConnectionInfoLength: ULONG->unsigned int
        ///MaxMessageLength: ULONG->unsigned int
        ///MaxPoolUsage: ULONG->unsigned int
        [System.Runtime.InteropServices.DllImportAttribute("ntdll.dll", EntryPoint = "NtCreatePort", CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern int NtCreatePort(out System.IntPtr PortHandle,
            //[MarshalAs(UnmanagedType.LPStruct)]
            ref NAlpc.OBJECT_ATTRIBUTES ObjectAttributes, 
            uint MaxConnectionInfoLength, 
            uint MaxMessageLength,
            uint MaxPoolUsage);


        /// Return Type: NTSTATUS->int
        ///PortHandle: PHANDLE->HANDLE*
        ///ObjectAttributes: POBJECT_ATTRIBUTES->_OBJECT_ATTRIBUTES*
        ///MaxConnectionInfoLength: ULONG->unsigned int
        ///MaxMessageLength: ULONG->unsigned int
        ///MaxPoolUsage: ULONG->unsigned int
        [System.Runtime.InteropServices.DllImportAttribute("ntdll.dll", EntryPoint = "ZwCreatePort", CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern int ZwCreatePort(ref System.IntPtr PortHandle, ref NAlpc.OBJECT_ATTRIBUTES ObjectAttributes, uint MaxConnectionInfoLength, uint MaxMessageLength, uint MaxPoolUsage);


        /// Return Type: NTSTATUS->int
        ///PortHandle: PHANDLE->HANDLE*
        ///PortName: PUNICODE_STRING->_UNICODE_STRING*
        ///SecurityQos: PSECURITY_QUALITY_OF_SERVICE->_SECURITY_QUALITY_OF_SERVICE*
        ///ClientView: PPORT_VIEW->_PORT_VIEW*
        ///ServerView: PREMOTE_PORT_VIEW->_REMOTE_PORT_VIEW*
        ///MaxMessageLength: PULONG->ULONG*
        ///ConnectionInformation: PVOID->void*
        ///ConnectionInformationLength: PULONG->ULONG*
        [System.Runtime.InteropServices.DllImportAttribute("ntdll.dll", EntryPoint = "NtConnectPort", CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern int NtConnectPort(ref System.IntPtr PortHandle, ref NAlpc.UNICODE_STRING PortName, ref SECURITY_QUALITY_OF_SERVICE SecurityQos, ref PORT_VIEW ClientView, ref REMOTE_PORT_VIEW ServerView, ref uint MaxMessageLength, System.IntPtr ConnectionInformation, ref uint ConnectionInformationLength);

        [System.Runtime.InteropServices.DllImportAttribute("ntdll.dll", EntryPoint = "NtConnectPort", CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern int NtConnectPort_NoMarshal(ref System.IntPtr PortHandle, ref NAlpc.UNICODE_STRING PortName, ref SECURITY_QUALITY_OF_SERVICE SecurityQos, IntPtr ClientView, IntPtr ServerView, ref uint MaxMessageLength, System.IntPtr ConnectionInformation, ref uint ConnectionInformationLength);

        /// Return Type: NTSTATUS->int
        ///PortHandle: PHANDLE->HANDLE*
        ///PortName: PUNICODE_STRING->_UNICODE_STRING*
        ///SecurityQos: PSECURITY_QUALITY_OF_SERVICE->_SECURITY_QUALITY_OF_SERVICE*
        ///ClientView: PPORT_VIEW->_PORT_VIEW*
        ///ServerView: PREMOTE_PORT_VIEW->_REMOTE_PORT_VIEW*
        ///MaxMessageLength: PULONG->ULONG*
        ///ConnectionInformation: PVOID->void*
        ///ConnectionInformationLength: PULONG->ULONG*
        [System.Runtime.InteropServices.DllImportAttribute("ntdll.dll", EntryPoint = "ZwConnectPort", CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern int ZwConnectPort(ref System.IntPtr PortHandle, ref NAlpc.UNICODE_STRING PortName, ref SECURITY_QUALITY_OF_SERVICE SecurityQos, ref PORT_VIEW ClientView, ref REMOTE_PORT_VIEW ServerView, ref uint MaxMessageLength, System.IntPtr ConnectionInformation, ref uint ConnectionInformationLength);


        /// Return Type: NTSTATUS->int
        ///PortHandle: HANDLE->void*
        ///RequestMessage: PPORT_MESSAGE->_PORT_MESSAGE*
        [System.Runtime.InteropServices.DllImportAttribute("ntdll.dll", EntryPoint = "NtListenPort", CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern int NtListenPort(System.IntPtr PortHandle, ref PORT_MESSAGE RequestMessage);


        /// Return Type: NTSTATUS->int
        ///PortHandle: HANDLE->void*
        ///RequestMessage: PPORT_MESSAGE->_PORT_MESSAGE*
        [System.Runtime.InteropServices.DllImportAttribute("ntdll.dll", EntryPoint = "ZwListenPort", CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern int ZwListenPort(System.IntPtr PortHandle, ref PORT_MESSAGE RequestMessage);


        /// Return Type: NTSTATUS->int
        ///PortHandle: PHANDLE->HANDLE*
        ///PortContext: PVOID->void*
        ///ConnectionRequest: PPORT_MESSAGE->_PORT_MESSAGE*
        ///AcceptConnection: BOOLEAN->BYTE->unsigned char
        ///ServerView: PPORT_VIEW->_PORT_VIEW*
        ///ClientView: PREMOTE_PORT_VIEW->_REMOTE_PORT_VIEW*
        [System.Runtime.InteropServices.DllImportAttribute("ntdll.dll", EntryPoint = "NtAcceptConnectPort", CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern int NtAcceptConnectPort(ref System.IntPtr PortHandle, System.IntPtr PortContext, ref PORT_MESSAGE ConnectionRequest, byte AcceptConnection, ref PORT_VIEW ServerView, ref REMOTE_PORT_VIEW ClientView);

        [System.Runtime.InteropServices.DllImportAttribute("ntdll.dll", EntryPoint = "NtAcceptConnectPort", CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern int NtAcceptConnectPort_Ptr(out IntPtr PortHandle, System.IntPtr PortContext, ref PORT_MESSAGE ConnectionRequest, byte AcceptConnection, IntPtr ServerView, IntPtr ClientView);

        /// Return Type: NTSTATUS->int
        ///PortHandle: PHANDLE->HANDLE*
        ///PortContext: PVOID->void*
        ///ConnectionRequest: PPORT_MESSAGE->_PORT_MESSAGE*
        ///AcceptConnection: BOOLEAN->BYTE->unsigned char
        ///ServerView: PPORT_VIEW->_PORT_VIEW*
        ///ClientView: PREMOTE_PORT_VIEW->_REMOTE_PORT_VIEW*
        [System.Runtime.InteropServices.DllImportAttribute("ntdll.dll", EntryPoint = "ZwAcceptConnectPort", CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern int ZwAcceptConnectPort(ref System.IntPtr PortHandle, System.IntPtr PortContext, ref PORT_MESSAGE ConnectionRequest, byte AcceptConnection, ref PORT_VIEW ServerView, ref REMOTE_PORT_VIEW ClientView);


        /// Return Type: NTSTATUS->int
        ///PortHandle: HANDLE->void*
        [System.Runtime.InteropServices.DllImportAttribute("ntdll.dll", EntryPoint = "NtCompleteConnectPort", CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern int NtCompleteConnectPort(System.IntPtr PortHandle);


        /// Return Type: NTSTATUS->int
        ///PortHandle: HANDLE->void*
        [System.Runtime.InteropServices.DllImportAttribute("ntdll.dll", EntryPoint = "ZwCompleteConnectPort", CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern int ZwCompleteConnectPort(System.IntPtr PortHandle);


        /// Return Type: NTSTATUS->int
        ///PortHandle: HANDLE->void*
        ///RequestMessage: PPORT_MESSAGE->_PORT_MESSAGE*
        [System.Runtime.InteropServices.DllImportAttribute("ntdll.dll", EntryPoint = "NtRequestPort", CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern int NtRequestPort(System.IntPtr PortHandle, ref PORT_MESSAGE RequestMessage);

        [System.Runtime.InteropServices.DllImportAttribute("ntdll.dll", EntryPoint = "NtRequestPort", CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern int NtRequestPort_NoMarshal(System.IntPtr PortHandle, IntPtr RequestMessage);

        /// Return Type: NTSTATUS->int
        ///PortHandle: HANDLE->void*
        ///RequestMessage: PPORT_MESSAGE->_PORT_MESSAGE*
        [System.Runtime.InteropServices.DllImportAttribute("ntdll.dll", EntryPoint = "ZwRequestPort", CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern int ZwRequestPort(System.IntPtr PortHandle, ref PORT_MESSAGE RequestMessage);


        /// Return Type: NTSTATUS->int
        ///PortHandle: HANDLE->void*
        ///RequestMessage: PPORT_MESSAGE->_PORT_MESSAGE*
        ///ReplyMessage: PPORT_MESSAGE->_PORT_MESSAGE*
        [System.Runtime.InteropServices.DllImportAttribute("ntdll.dll", EntryPoint = "NtRequestWaitReplyPort", CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern int NtRequestWaitReplyPort(System.IntPtr PortHandle, ref PORT_MESSAGE RequestMessage, ref PORT_MESSAGE ReplyMessage);


        /// Return Type: NTSTATUS->int
        ///PortHandle: HANDLE->void*
        ///RequestMessage: PPORT_MESSAGE->_PORT_MESSAGE*
        ///ReplyMessage: PPORT_MESSAGE->_PORT_MESSAGE*
        [System.Runtime.InteropServices.DllImportAttribute("ntdll.dll", EntryPoint = "ZwRequestWaitReplyPort", CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern int ZwRequestWaitReplyPort(System.IntPtr PortHandle, ref PORT_MESSAGE RequestMessage, ref PORT_MESSAGE ReplyMessage);


        /// Return Type: NTSTATUS->int
        ///PortHandle: HANDLE->void*
        ///ReplyMessage: PPORT_MESSAGE->_PORT_MESSAGE*
        [System.Runtime.InteropServices.DllImportAttribute("ntdll.dll", EntryPoint = "NtReplyPort", CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern int NtReplyPort(System.IntPtr PortHandle, ref PORT_MESSAGE ReplyMessage);


        /// Return Type: NTSTATUS->int
        ///PortHandle: HANDLE->void*
        ///ReplyMessage: PPORT_MESSAGE->_PORT_MESSAGE*
        [System.Runtime.InteropServices.DllImportAttribute("ntdll.dll", EntryPoint = "ZwReplyPort", CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern int ZwReplyPort(System.IntPtr PortHandle, ref PORT_MESSAGE ReplyMessage);


        /// Return Type: NTSTATUS->int
        ///PortHandle: HANDLE->void*
        ///ReplyMessage: PPORT_MESSAGE->_PORT_MESSAGE*
        [System.Runtime.InteropServices.DllImportAttribute("ntdll.dll", EntryPoint = "NtReplyWaitReplyPort", CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern int NtReplyWaitReplyPort(System.IntPtr PortHandle, ref PORT_MESSAGE ReplyMessage);


        /// Return Type: NTSTATUS->int
        ///PortHandle: HANDLE->void*
        ///ReplyMessage: PPORT_MESSAGE->_PORT_MESSAGE*
        [System.Runtime.InteropServices.DllImportAttribute("ntdll.dll", EntryPoint = "ZwReplyWaitReplyPort", CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern int ZwReplyWaitReplyPort(System.IntPtr PortHandle, ref PORT_MESSAGE ReplyMessage);


        /// Return Type: NTSTATUS->int
        ///PortHandle: HANDLE->void*
        ///PortContext: PVOID*
        ///ReplyMessage: PPORT_MESSAGE->_PORT_MESSAGE*
        ///ReceiveMessage: PPORT_MESSAGE->_PORT_MESSAGE*
        [System.Runtime.InteropServices.DllImportAttribute("ntdll.dll", EntryPoint = "NtReplyWaitReceivePort", CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern int NtReplyWaitReceivePort(System.IntPtr PortHandle, ref System.IntPtr PortContext, ref PORT_MESSAGE ReplyMessage, ref PORT_MESSAGE ReceiveMessage);


        [System.Runtime.InteropServices.DllImportAttribute("ntdll.dll", EntryPoint = "NtReplyWaitReceivePort", CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern int NtReplyWaitReceivePort_Ptr(System.IntPtr PortHandle, ref System.IntPtr PortContext,  IntPtr ReplyMessage, ref PORT_MESSAGE ReceiveMessage);

        [System.Runtime.InteropServices.DllImportAttribute("ntdll.dll", EntryPoint = "NtReplyWaitReceivePort", CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern int NtReplyWaitReceivePort_NoMarshal(System.IntPtr PortHandle, ref System.IntPtr PortContext, IntPtr ReplyMessage, IntPtr ReceiveMessage);

        /// Return Type: NTSTATUS->int
        ///PortHandle: HANDLE->void*
        ///PortContext: PVOID*
        ///ReplyMessage: PPORT_MESSAGE->_PORT_MESSAGE*
        ///ReceiveMessage: PPORT_MESSAGE->_PORT_MESSAGE*
        [System.Runtime.InteropServices.DllImportAttribute("ntdll.dll", EntryPoint = "ZwReplyWaitReceivePort", CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern int ZwReplyWaitReceivePort(System.IntPtr PortHandle, ref System.IntPtr PortContext, ref PORT_MESSAGE ReplyMessage, ref PORT_MESSAGE ReceiveMessage);

    }

}
