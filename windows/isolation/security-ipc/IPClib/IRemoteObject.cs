using System.ServiceModel;

namespace IPCLib
{
    [ServiceContract]
    public interface IRemoteObject
    {
        [OperationContract(IsOneWay = false)]
        byte[] GetRBytes(int numBytes);

        [OperationContract(IsOneWay = false)]
        bool ReceiveRBytes(byte[] bytes);
    }
}
