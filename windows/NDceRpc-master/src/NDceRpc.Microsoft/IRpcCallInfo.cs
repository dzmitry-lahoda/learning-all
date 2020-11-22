
using System;
using System.Security.Principal;
using NDceRpc.Microsoft.Interop;

namespace NDceRpc
{
    /// <summary>
    /// An interface that provide contextual information about the client within an Rpc server call
    /// </summary>
    public interface IRpcCallInfo
    {
        /// <summary>
        /// Returns true if the caller is using LRPC
        /// </summary>
        bool IsClientLocal { get; }
        /// <summary>
        /// Returns a most random set of bytes, undocumented Win32 value.
        /// </summary>
        byte[] ClientAddress { get; }

        /// <summary>
        /// Defines the type of the procol being used in the communication, unavailable on Windows XP
        /// </summary>
        RpcProtoseqType ProtocolType { get; }
        /// <summary>
        /// Returns the packet protection level of the communications
        /// </summary>
        RPC_C_AUTHN_LEVEL ProtectionLevel { get; }
        /// <summary>
        /// Returns the authentication level of the connection
        /// </summary>
        RPC_C_AUTHN AuthenticationLevel { get; }
        /// <summary>
        /// Returns the ProcessId of the LRPC caller, may not be valid on all platforms
        /// </summary>
        IntPtr ClientPid { get; }
        /// <summary>
        /// Returns true if the caller has authenticated as a user
        /// </summary>
        bool IsAuthenticated { get; }
        /// <summary>
        /// Returns the client user name if authenticated, not available on WinXP
        /// </summary>
        string ClientPrincipalName { get; }
        /// <summary>
        /// Returns the identity of the client user or Anonymous if unauthenticated
        /// </summary>
        WindowsIdentity ClientUser { get; }
        /// <summary>
        /// Returns true if already impersonating the caller
        /// </summary>
        bool IsImpersonating { get; }
        /// <summary>
        /// Returns a disposable context that is used to impersonate the calling user
        /// </summary>
        IDisposable Impersonate();
    }
}