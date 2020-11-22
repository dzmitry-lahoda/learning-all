namespace NDceRpc.Microsoft.Interop
{
    /// <summary>
    /// Defines the type of protocol the client is connected with
    /// </summary>
    public enum RpcProtoseqType : uint
    {
        /// <summary> TCP, UDP, IPX over TCP, etc </summary>
        TCP = 0x1,
        /// <summary> Named Pipes </summary>
        NMP = (0x2),
        /// <summary> LPRC / Local RPC </summary>
        LRPC = (0x3),
        /// <summary> HTTP / IIS integrated </summary>
        HTTP = (0x4),
    }
}