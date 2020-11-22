//
// IContextChannel.cs

using System;
using NDceRpc.ServiceModel.Channels;

namespace NDceRpc.ServiceModel
{
    public interface IContextChannel : IChannel, ICommunicationObject,
        IExtensibleObject<IContextChannel>
    {

        bool AllowOutputBatching { get; set; }

        IInputSession InputSession { get; }

        EndpointAddress LocalAddress { get; }

        TimeSpan OperationTimeout { get; set; }

        IOutputSession OutputSession { get; }

        EndpointAddress RemoteAddress { get; }

        string SessionId { get; }
    }
}
