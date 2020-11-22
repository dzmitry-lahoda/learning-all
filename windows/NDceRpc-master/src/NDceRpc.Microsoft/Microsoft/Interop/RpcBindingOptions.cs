using System;

namespace NDceRpc
{
	/// <summary>
	/// Rpc binding options.
	/// </summary>
	/// <seealso href="https://msdn.microsoft.com/en-us/library/windows/desktop/aa373568.aspx"/>
	public enum RpcBindingOptions:uint
	{
		RPC_C_OPT_BINDING_NONCAUSAL = 9,
		RPC_C_OPT_MAX_OPTIONS = 17,
		RPC_C_DONT_FAIL = 4,
		RPC_C_OPT_SESSION_ID = 6,
		RPC_C_OPT_COOKIE_AUTH = 7,
		RPC_C_OPT_RESOURCE_TYPE_UUID = 8,
		RPC_C_OPT_DONT_LINGER = 8,
		RPC_C_OPT_UNIQUE_BINDING = 11,
		/// <summary>
		/// Undocumented
		/// </summary>
		RPC_C_OPT_SECURITY_CALLBACK=10,
	}
}

