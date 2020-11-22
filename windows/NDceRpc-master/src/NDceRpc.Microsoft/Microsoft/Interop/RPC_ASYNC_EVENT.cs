namespace NDceRpc.Microsoft.Interop.Async
{
    /// <summary>
    /// The enumerated type describes the asynchronous notification events that an RPC application can receive.
    /// </summary>
    /// <seealso href="http://msdn.microsoft.com/en-us/library/windows/desktop/aa378488.aspx"/>
    public enum RPC_ASYNC_EVENT:uint
    {
        /// <summary>
        /// The remote procedure call has completely executed.
        /// </summary>
        RpcCallComplete,
        RpcSendComplete,
        RpcReceiveComplete,
        RpcClientDisconnect,
        RpcClientCancel
    }
}