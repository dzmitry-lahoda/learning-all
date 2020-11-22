namespace NDceRpc.Microsoft.Interop
{
    /// Return Type: RPC_STATUS->int  
    ///InterfaceUuid: RPC_IF_HANDLE->void*  
    ///Context: void*  
    [System.Runtime.InteropServices.UnmanagedFunctionPointerAttribute(System.Runtime.InteropServices.CallingConvention.StdCall)]
    public delegate RPC_STATUS RPC_IF_CALLBACK_FN(System.IntPtr Interface, System.IntPtr Context);


}