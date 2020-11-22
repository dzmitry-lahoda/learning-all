using System;

namespace NDceRpc
{
	[System.Runtime.InteropServices.UnmanagedFunctionPointerAttribute(System.Runtime.InteropServices.CallingConvention.StdCall)]
	public delegate void RPC_C_SECURITY_CALLBACK(System.IntPtr Binding);
}

