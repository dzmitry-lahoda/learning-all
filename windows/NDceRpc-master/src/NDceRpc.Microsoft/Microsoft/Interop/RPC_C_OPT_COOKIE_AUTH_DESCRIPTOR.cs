using System;

namespace NDceRpc
{
	/// <seealso href="https://msdn.microsoft.com/en-us/library/windows/desktop/dd877218.aspx"/>
	public struct RPC_C_OPT_COOKIE_AUTH_DESCRIPTOR
	{
		// looks like can just use String 
		uint BufferSize;
		//char*
		IntPtr Buffer;
	}
}

