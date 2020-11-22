using System.Runtime.InteropServices;

namespace NDceRpc.ServiceModel.RegFreeCom
{
    /// <summary>
    /// 
    /// </summary>
    [Guid(ComServerConstants.IID)]
    [ComVisible(true)]
    [InterfaceType(ComInterfaceType.InterfaceIsDual)]
    public interface IComServer
    {
        byte[] Execute(byte[] request);
    }
}