using System.Runtime.InteropServices;
using NDceRpc.ExplicitBytes;

namespace NDceRpc.ServiceModel.RegFreeCom
{
    /// <summary>
    /// 
    /// </summary>
    [ComVisible(true)]
    [Guid(ComServerConstants.CLSID)]
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ComDefaultInterface(typeof(IComServer))]
    public class ComServer : IComServer
    {
        [ComVisible(false)]
        public RpcExecuteHandler OnExecute;

        public byte[] Execute(byte[] request)
        {
            RpcExecuteHandler onExecuted = OnExecute;
            return onExecuted(null, request);
        }
    }
}