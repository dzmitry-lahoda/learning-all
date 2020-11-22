namespace NDceRpc.Microsoft.Interop
{
    public enum RpcCallStatus : uint
    {
        Invalid = 0,
        InProgress = 1,
        Cancelled = 2,
        Disconnected = 3
    }
}