using IPCLib;

namespace WCFServer
{
    class RemoteObject : IRemoteObject
    {
        public byte[] GetRBytes(int numBytes)
        {
            return new byte[numBytes];
        }

        public bool ReceiveRBytes(byte[] bytes)
        {
            return true;
        }
    }
}


