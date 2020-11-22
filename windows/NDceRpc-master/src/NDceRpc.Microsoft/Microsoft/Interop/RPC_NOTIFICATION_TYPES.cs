using System;

namespace NDceRpc.Microsoft.Interop.Async
{
    public enum RPC_NOTIFICATION_TYPES:uint
    {
        /// <summary>
        /// No notification is specified; <see cref="RPC_ASYNC_NOTIFICATION_INFO"/> is not initialized.
        /// </summary>
        RpcNotificationTypeNone,

        //The notification mechanism is a Windows event.
        RpcNotificationTypeEvent,
     

        //The notification mechanism is a Windows asynchronous procedure call.
        RpcNotificationTypeApc,
       
        //The notification mechanism is an I/O completion port.
        RpcNotificationTypeIoc,
        

        /// <summary>
        /// The notification mechanism is a Windows system message.
        /// </summary>
        [Obsolete("Windows Server 2003 or later:  Notification via the HWND is deprecated. Do not use this value.")]

        RpcNotificationTypeHwnd,


        /// <summary>
        /// The notification mechanism is a function callback.
        /// </summary>
        RpcNotificationTypeCallback,

    }
}