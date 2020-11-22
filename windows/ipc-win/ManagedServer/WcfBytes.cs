using System.ServiceModel;
using managed_entities;

namespace ManagedServer
{

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    internal class WcfBytes : IWcfBytes
    {
        public byte[] Execute(byte[] input)
        {
            byte[] resp = new byte[input.Length * 10];
            return resp;
        }
    }

    
}