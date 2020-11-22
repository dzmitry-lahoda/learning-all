using System;
using System.IO;

namespace NDceRpc.ServiceModel.Channels
{
    public abstract class MessageEncoder
    {
        public abstract void WriteObject(Stream stream, object graph);
        public abstract object ReadObject(Stream data, Type type);
    }
}