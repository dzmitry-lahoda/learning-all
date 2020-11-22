namespace NDceRpc.ExplicitBytes
{
    /// <summary>
    /// Server side transport interface.
    /// </summary>
    public interface IExplicitBytesServer
    {
        /// <summary>
        /// 
        /// </summary>
        void StartListening();

        /// <summary>
        /// 
        /// </summary>
        void Dispose();

        /// <summary>
        /// 
        /// </summary>
        event  RpcExecuteHandler OnExecute;
    }
}