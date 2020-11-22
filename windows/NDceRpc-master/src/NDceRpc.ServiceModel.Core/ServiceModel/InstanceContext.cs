using System;
using System.Diagnostics;
using System.Linq;

using System.Threading;

namespace NDceRpc.ServiceModel
{
    //RpcTwoChannelInstanseContext
    public class InstanceContext : ICommunicationObject, IDisposable
    {
        internal readonly object _contextObject;
        private CallbackServiceHost _callbackServer;
        private Type _typeOfService;
        private string _serverAddress;

        private string _session;
        private bool _started;
        private Binding _binding;


        public InstanceContext(object contextObject)
        {
            _contextObject = contextObject;
            _behaviour = AttributesReader.GetCallbackBehavior(contextObject.GetType()) ?? new CallbackBehaviorAttribute();
            _useSynchronizationContext = _behaviour.UseSynchronizationContext;
        }

        public SynchronizationContext SynchronizationContext { get; private set; }
        internal bool _useSynchronizationContext;
        private CallbackBehaviorAttribute _behaviour;

        public void Abort()
        {
            throw new NotImplementedException();
        }

        public void Close()
        {

        }

        public void Close(TimeSpan timeout)
        {
            throw new NotImplementedException();
        }

        public IAsyncResult BeginClose(AsyncCallback callback, object state)
        {
            throw new NotImplementedException();
        }

        public IAsyncResult BeginClose(TimeSpan timeout, AsyncCallback callback, object state)
        {
            throw new NotImplementedException();
        }

        public void EndClose(IAsyncResult result)
        {
            throw new NotImplementedException();
        }

        public void Open()
        {
            throw new NotImplementedException();
        }

        public void Open(TimeSpan timeout)
        {
            throw new NotImplementedException();
        }

        public IAsyncResult BeginOpen(AsyncCallback callback, object state)
        {
            throw new NotImplementedException();
        }

        public IAsyncResult BeginOpen(TimeSpan timeout, AsyncCallback callback, object state)
        {
            throw new NotImplementedException();
        }

        public void EndOpen(IAsyncResult result)
        {
            throw new NotImplementedException();
        }

        public CommunicationState State { get; private set; }
        public event EventHandler Closed;
        public event EventHandler Closing;
        public event EventHandler Faulted;
        public event EventHandler Opened;
        public event EventHandler Opening;


        //TODO: implement polling duplex
        //TODO: implement single channell duplex
        //context.CreateClientStub(_typeOfService);
        // Tasks.Factory.StartNew((x) =>
        //    {
        //        while (!_disposed)
        //        {
        //            var request = new MessageRequest {Callback = true};
        //            var stream = new MemoryStream();
        //            _serializer.WriteObject(stream, request);
        //            var result = _client.Execute(stream.ToArray());
        //            var response = (MessageResponse)_serializer.ReadObject(new MemoryStream(result),
        //                                                                 typeof (MessageResponse));
        //            context.Invoke(response);
        //        }
        //    });
        internal void Initialize(Type typeOfService, string serverAddress, Binding binding, string session, SynchronizationContext syncContext)
        {
            if (!_started)
            {
                Open(typeOfService, serverAddress, binding, session, syncContext);
            }
            _started = true;
        }

        private void Open(Type typeOfService, string serverAddress, Binding binding, string session,
                          SynchronizationContext syncContext)
        {
            this.SynchronizationContext = syncContext;
            _typeOfService = typeOfService;
            _serverAddress = serverAddress;
            _binding = binding;
            _session = session;
            var contract = AttributesReader.GetServiceContract(_typeOfService);

            Debug.Assert(contract.CallbackContract != null);
            //BUG: should be used only for non polling HTTM, all others should go via provided single channel
            var address = _serverAddress + _session.Replace("-", "");

            _callbackServer = new CallbackServiceHost(this, address, _behaviour);
            _callbackServer.AddServiceEndpoint(contract.CallbackContract, new Guid(_session), _binding, address);
            _callbackServer.Open();
        }

        public void Dispose()
        {
            //_callbackServer.Dispose();
        }
    }
}