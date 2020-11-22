namespace NDceRpc.ExplicitBytes
{
    /// <summary>
    /// Client side transport interfaces
    /// </summary>
    public interface IExplicitBytesClient
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        byte[] Execute(byte[] arg);

        /// <summary>
        /// 
        /// </summary>
        void Dispose();
    }
}