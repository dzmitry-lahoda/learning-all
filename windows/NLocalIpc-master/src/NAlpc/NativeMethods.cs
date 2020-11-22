using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace NAlpc
{
    public static class NativeMethods
    {
        [DllImport("advapi32.dll", SetLastError = true)]
        public static extern bool InitializeSecurityDescriptor(SECURITY_DESCRIPTOR pSecurityDescriptor, uint dwRevision);

        ///<summary>
        ///    Creates a LPC port object. The creator of the LPC port becomes a server
        ///    of LPC communication

        ///    PortHandle - Points to a variable that will receive the
        ///        port object handle if the call is successful.

        ///    ObjectAttributes - Points to a structure that specifies the object’s
        ///        attributes. OBJ_KERNEL_HANDLE, OBJ_OPENLINK, OBJ_OPENIF, OBJ_EXCLUSIVE,
        ///        OBJ_PERMANENT, and OBJ_INHERIT are not valid attributes for a port object.

        ///    MaxConnectionInfoLength - The maximum size, in bytes, of data that can
        ///        be sent through the port.

        ///    MaxMessageLength - The maximum size, in bytes, of a message
        ///        that can be sent through the port.

        ///    MaxPoolUsage - Specifies the maximum amount of NonPaged pool that can be used for
        ///        message storage. Zero means default value.

        ///    ZwCreatePort verifies that (MaxDataSize &lt;= 0x104) and (MaxMessageSize &lt;= 0x148).
        ///</summary>
        [DllImport("ntdll.dll", EntryPoint = "NtCreatePort", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern int NtCreatePort(out NAlpc.AlpcPortHandle PortHandle, ref OBJECT_ATTRIBUTES ObjectAttributes, uint MaxConnectionInfoLength, uint MaxMessageLength, uint MaxPoolUsage);

        [DllImport("ntdll.dll", EntryPoint = "NtClose", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern int NtClose(IntPtr Handle);



        ///<summary>
        ///    Listens on a port for a connection request message on the server side.
        ///    PortHandle - A handle to a port object. The handle doesn't need to grant any specific access.
        ///    ConnectionRequest - Points to a caller-allocated buffer or variable that receives the connect message sent to the port.
        /// </summary>
        [DllImport("ntdll.dll", EntryPoint = "NtListenPort", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern int NtListenPort(AlpcPortHandle PortHandle, ref PORT_MESSAGE RequestMessage);



        ///<summary>
        ///    Accepts or rejects a connection request on the server side.

        ///    PortHandle - Points to a variable that will receive the port object
        ///        handle if the call is successful.

        ///   PortContext - A numeric identifier to be associated with the port.

        ///    ConnectionRequest - Points to a caller-allocated buffer or variable
        ///        that identifies the connection request and contains any connect
        ///        data that should be returned to requestor of the connection

        ///    AcceptConnection - Specifies whether the connection should
        ///        be accepted or not

        ///    ServerView - Optionally points to a structure describing
        ///        the shared memory region used to send large amounts of data to the
        ///        requestor; if the call is successful, this will be updated

        ///    ClientView - Optionally points to a caller-allocated buffer
        ///        or variable that receives information on the shared memory
        ///        region used by the requestor to send large amounts of data to the
        ///        caller

        /// </summary>
        [DllImport("ntdll.dll", EntryPoint = "NtAcceptConnectPort", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern int NtAcceptConnectPort(out AlpcPortHandle PortHandle, IntPtr PortContext, ref PORT_MESSAGE ConnectionRequest, bool AcceptConnection, IntPtr /*out  PORT_VIEW*/ ServerView, IntPtr /*out   REMOTE_PORT_VIEW*/ ClientView);


        ///<summary>
        /// Creates a port connected to a named port (cliend side).

        ///    PortHandle - A pointer to a variable that will receive the client
        ///        communication port object handle value.

        ///    PortName - Points to a structure that specifies the name
        ///        of the port to connect to.

        ///    SecurityQos - Points to a structure that specifies the level
        ///        of impersonation available to the port listener.

        ///    ClientView - Optionally points to a structure describing
        ///        the shared memory region used to send large amounts of data
        ///        to the listener; if the call is successful, this will be updated.

        ///    ServerView - Optionally points to a caller-allocated buffer
        ///        or variable that receives information on the shared memory region
        ///        used by the listener to send large amounts of data to the
        ///        caller.

        ///    MaxMessageLength - Optionally points to a variable that receives the size,
        ///        in bytes, of the largest message that can be sent through the port.

        ///    ConnectionInformation - Optionally points to a caller-allocated
        ///        buffer or variable that specifies connect data to send to the listener,
        ///        and receives connect data sent by the listener.

        ///    ConnectionInformationLength - Optionally points to a variable that
        ///        specifies the size, in bytes, of the connect data to send
        ///        to the listener, and receives the size of the connect data
        ///        sent by the listener.
        /// </summary>
        [DllImport("ntdll.dll", EntryPoint = "NtConnectPort", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern int NtConnectPort(
                out NAlpc.AlpcPortHandle PortHandle,
                string PortName,
               ref SECURITY_QUALITY_OF_SERVICE SecurityQos,
               out IntPtr /*PPORT_VIEW */ ClientView,
               out IntPtr /* PREMOTE_PORT_VIEW */  ServerView,
              ref UInt32 MaxMessageLength,
                  out IntPtr ConnectionInformation,
             out IntPtr /*  UInt32*/ ConnectionInformationLength
              );

        ///<summary>
        ///    Completes the port connection process on the server side.
        ///    PortHandle - A handle to a port object. The handle doesn't need 
        ///        to grant any specific access.
        /// </summary>
        [DllImport("ntdll.dll", EntryPoint = "NtCompleteConnectPort", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern int NtCompleteConnectPort(AlpcPortHandle PortHandle);


        ///<summary>

        ///    NtRequestPort
        ///    =============

        ///    Sends a request message to a port (client side)

        ///    PortHandle - A handle to a port object. The handle doesn't need 
        ///        to grant any specific access.

        ///    RequestMessage - Points to a caller-allocated buffer or variable
        ///        that specifies the request message to send to the port.

        ///</summary>
        [DllImport("ntdll.dll", EntryPoint = "NtRequestPort", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern int
        NtRequestPort(
            AlpcPortHandle PortHandle,
            ref PORT_MESSAGE RequestMessage
            );

        ///<summary>

        ///    NtRequestWaitReplyPort
        ///    ======================

        ///    Sends a request message to a port and waits for a reply (client side)

        ///    PortHandle - A handle to a port object. The handle doesn't need 
        ///        to grant any specific access.

        ///    RequestMessage - Points to a caller-allocated buffer or variable
        ///        that specifies the request message to send to the port.

        ///    ReplyMessage - Points to a caller-allocated buffer or variable
        ///        that receives the reply message sent to the port.

        ///</summary>

        [DllImport("ntdll.dll", EntryPoint = "NtRequestWaitReplyPort", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern int
        NtRequestWaitReplyPort(
                 AlpcPortHandle PortHandle,
        ref PORT_MESSAGE RequestMessage,
        out PORT_MESSAGE ReplyMessage
            );




        ///<summary>

        ///    NtReplyPort
        ///    ===========

        ///    Sends a reply message to a port (Server side)

        ///    PortHandle - A handle to a port object. The handle doesn't need 
        ///        to grant any specific access.

        ///    ReplyMessage - Points to a caller-allocated buffer or variable
        ///        that specifies the reply message to send to the port.

        ///</summary>


        [DllImport("ntdll.dll", EntryPoint = "NtReplyPort", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern int
        NtReplyPort(
AlpcPortHandle PortHandle,
       ref PORT_MESSAGE ReplyMessage
            );



        ///<summary>

        ///    NtReplyWaitReplyPort
        ///    ====================

        ///    Sends a reply message to a port and waits for a reply message

        ///    PortHandle - A handle to a port object. The handle doesn't need 
        ///        to grant any specific access.

        ///    ReplyMessage - Points to a caller-allocated buffer or variable
        ///        that specifies the reply message to send to the port.

        ///</summary>

        [DllImport("ntdll.dll", EntryPoint = "NtReplyWaitReplyPort", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern int
        NtReplyWaitReplyPort(
    AlpcPortHandle PortHandle,
        out PORT_MESSAGE ReplyMessage
            );



        ///<summary>

        ///    NtReplyWaitReceivePort
        ///    ======================

        ///    Optionally sends a reply message to a port and waits for a
        ///    message

        ///    PortHandle - A handle to a port object. The handle doesn't need 
        ///        to grant any specific access.

        ///    PortContext - Optionally points to a variable that receives
        ///        a numeric identifier associated with the port.

        ///    ReplyMessage - Optionally points to a caller-allocated buffer
        ///        or variable that specifies the reply message to send to the port.

        ///    ReceiveMessage - Points to a caller-allocated buffer or variable
        ///        that receives the message sent to the port.

        ///</summary>

        [DllImport("ntdll.dll", EntryPoint = "NtReplyWaitReceivePort", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern int
        NtReplyWaitReceivePort(
        AlpcPortHandle PortHandle,
        out IntPtr PortContext,
        ref PORT_MESSAGE ReplyMessage,
        out PORT_MESSAGE ReceiveMessage
            );


    }
}
