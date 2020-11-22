using System;
using System.Runtime.InteropServices;

namespace NDceRpc.Microsoft.Interop.Async
{
    /// <summary>
    /// The structure holds the state of an asynchronous remote procedure call. Used to wait for, query, reply to, or cancel asynchronous calls.
    /// </summary>
    /// <seealso href="http://msdn.microsoft.com/en-us/library/windows/desktop/aa378490.aspx"/>
    [StructLayout(LayoutKind.Sequential)]

    public struct RPC_ASYNC_STATE
    {
        /// <summary>
        /// Size of this structure, in bytes. The environment sets this member when <see cref="NativeMethods.RpcAsyncInitializeHandle"/> is called. Do not modify this member.
        /// </summary>
        ushort Size;
        /// <summary>
        /// The run-time environment sets this member when <see cref="NDceRpc.NativeMethodscInitializeHandle"/> is called. Do not modify this member.
        /// </summary>
        uint Signature;
        /// <summary>
        /// The run-time environment sets this member when <see cref="NDceRpc.NativeMethodscInitializeHandle"/> is called. Do not modify this member.
        /// </summary>
        int Lock;

        /// <summary>
        /// <seealso cref="RpcAsync.RPC_C_NOTIFY_ON_SEND_COMPLETE"/>
        /// </summary>
        uint Flags;

        /// <summary>
        /// Reserved for use by the stubs. Do not use this member.
        /// </summary>
        IntPtr StubInfo;

        /// <summary>
        /// Use this member for any application-specific information that you want to keep track of in this structure.
        /// </summary>
        IntPtr UserInfo;

        /// <summary>
        /// Reserved for use by the RPC run-time environment. Do not use this member.
        /// </summary>
        IntPtr RuntimeInfo;
        RPC_ASYNC_EVENT Event;
        RPC_NOTIFICATION_TYPES NotificationType;
        RPC_ASYNC_NOTIFICATION_INFO u;
        /// <summary>
        /// Reserved for compatibility with future versions, if any. Do not use this member.
        /// </summary>
        IntPtr[] Reserved;

    }
}
