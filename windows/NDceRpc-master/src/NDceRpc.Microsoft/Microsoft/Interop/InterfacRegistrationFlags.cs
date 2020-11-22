using System;

namespace NDceRpc.Microsoft.Interop
{
    /// <summary>
    /// http://msdn.microsoft.com/en-us/library/windows/desktop/aa373954.aspx
    /// </summary>
    [Flags]
    public enum InterfacRegistrationFlags : uint
    {
        /// <summary>
        /// 
        /// </summary>
        Standard_interface_semantics = (uint)0,
        RPC_IF_AUTOLISTEN = 0x0001,
        RPC_IF_OLE = 0x0002,

        RPC_IF_ALLOW_UNKNOWN_AUTHORITY = 0x0004,
        RPC_IF_ALLOW_SECURE_ONLY = 0x0008,
        /// <summary>
        /// When this interface flag is registered, the RPC runtime invokes the registered security callback for all calls, regardless of identity, protocol sequence, or authentication level of the client. This flag is allowed only when a security callback is registered.
        /// Note  This flag is available starting with Windows XP with SP2 and Windows Server 2003 with SP1. When this flag is not set, RPC automatically filters all unauthenticated calls before they reach the security callback.
        /// </summary>
        RPC_IF_ALLOW_CALLBACKS_WITH_NO_AUTH = 0x0010,
        RPC_IF_ALLOW_LOCAL_ONLY = 0x0020,
        RPC_IF_SEC_NO_CACHE = 0x0040,
        //#if (NTDDI_VERSION >= NTDDI_VISTA)
        RPC_IF_SEC_CACHE_PER_PROC = 0x0080,
        RPC_IF_ASYNC_CALLBACK = 0x0100,
        //#endif // (NTDDI_VERSION >= NTDDI_VISTA)
    }
}