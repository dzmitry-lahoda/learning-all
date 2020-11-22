using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;

using System.Threading;
using System.Threading.Tasks;

using NDceRpc.ExplicitBytes;
using NDceRpc.Serialization;
using NDceRpc.ServiceModel.Channels;
using NDceRpc.ServiceModel.Dispatcher;
using Message = NDceRpc.ServiceModel.Channels.Message;
using MessageEncoder = NDceRpc.ServiceModel.Channels.MessageEncoder;


namespace NDceRpc.ServiceModel
{
    public class ClientRuntime : IDisposable
    {
        int _disposeSignaled;
        object _service;
        IExplicitBytesClient _client;
        object _remote;
        DispatchTable _operations = new DispatchTable();
        string _session;
        BinaryObjectSerializer _serializer;
        Type _typeOfService;
        InstanceContext _context;
        readonly Type _generatedProxyType;
        bool _disposed;
        string _address;
        ManualResetEvent _operationPending = new ManualResetEvent(true);
        Binding _binding;
        Guid _uuid;
        SynchronizationContext _syncContext;
        ServiceEndpoint _endpoint;
        IList<IClientMessageInspector> _messageInspectors = new List<IClientMessageInspector>();
        NDceRpc.ServiceModel.Channels.MessageEncoder _encoder = new ProtobufMessageEncodingBindingElement();

        //TODO: split RpcProxyRouter and RpcCallbackProxy
        public ClientRuntime(string address, ServiceEndpoint endpoint, bool callback = false, InstanceContext context = null, Guid customUuid = default(Guid), Type generatedProxyType = null)
        {
            _endpoint = endpoint;
            _binding = endpoint._binding;
            _serializer = _binding.Serializer;
            _typeOfService = endpoint._contractType;
            _context = context;
            if (_context != null && _context._useSynchronizationContext)
            {
                _syncContext = SynchronizationContext.Current;
            }
            _generatedProxyType = generatedProxyType;

            _address = address;

            _operations = DispatchTableFactory.GetOperations(_typeOfService);


            _uuid = EndpointMapper.CreateUuid(_address, _typeOfService);
            if (customUuid != Guid.Empty) // callback proxy
            {
                _uuid = customUuid;
            }

            var serviceContract = AttributesReader.GetServiceContract(_typeOfService);
            //TODO: null check only for duplex callback, really should always be here
            if (serviceContract != null && serviceContract.SessionMode == SessionMode.Required)
            {
                _session = Guid.NewGuid().ToString();
            }
            //TODO: allow to be initialized with pregenerated proxy
            var realProxy = new RpcRealProxy(_typeOfService, this, _endpoint, _encoder);
            _remote = realProxy.GetTransparentProxy();
            _client = RpcRequestReplyChannelFactory.CreateClient(_binding, _uuid, _address);
            foreach (var behavior in _endpoint.Behaviors)
            {
                behavior.ApplyClientBehavior(_endpoint, this);
            }

        }


        public ClientRuntime(Uri address, ServiceEndpoint endpoint, bool callback = false, InstanceContext context = null)
            : this(address.ToString(), endpoint, callback, context)
        {

        }


        internal class RpcRealProxy : RealProxy, System.Runtime.Remoting.IRemotingTypeInfo, IContextChannel
        {


            private readonly Type _service;
            private readonly ClientRuntime _router;
            private readonly ServiceEndpoint _endpoint;
            private readonly MessageEncoder _encoder;
            private string _typeName;
            private CommunicationState _state = CommunicationState.Created;
            private TimeSpan _operationTimeout = TimeSpan.FromSeconds(60);

            public RpcRealProxy(Type service, ClientRuntime router, ServiceEndpoint endpoint, NDceRpc.ServiceModel.Channels.MessageEncoder encoder)
                : base(service)
            {
                _service = service;
                _router = router;
                _endpoint = endpoint;
                _encoder = encoder;
                _typeName = string.Format("{0}`[{1}]", typeof(RpcRealProxy).FullName, service.FullName);
            }

            public override IMessage Invoke(IMessage msg)
            {

                var input = (IMethodCallMessage)msg;


                //TODO: move to RpcCallbackProxy
                if (_router._context != null)
                    _router._context.Initialize(_router._typeOfService, _router._address, _router._binding, _router._session, _router._syncContext);

                Debug.Assert(input.MethodBase != null);
                Debug.Assert(input.MethodBase.DeclaringType != null);
                var iid = input.MethodBase.DeclaringType;
                if (iid == typeof(ICommunicationObject))
                {
                    //TODO: use something faster than string comparison
                    if (input.MethodName == "get_State")
                    {
                        return new ReturnMessage(State, null, 0, input.LogicalCallContext, input);
                    }
                }
                else if (iid == typeof(IContextChannel))
                {
                    if (input.MethodName == "set_OperationTimeout")
                    {
                        OperationTimeout = (TimeSpan)input.InArgs[0];
                        return new ReturnMessage(null, null, 0, input.LogicalCallContext, input);
                    }
                    if (input.MethodName == "get_OperationTimeout")
                    {
                        return new ReturnMessage(OperationTimeout, null, 0, input.LogicalCallContext, input);
                    }
                }

                MethodResponse methodReturn;

                OperationDispatchBase op;
                if (_router._operations.TokenToOperation.TryGetValue(input.MethodBase.MetadataToken, out op))
                {
                    var r = new MessageRequest();
                    if (_router._session != null)
                    {
                        r.Session = _router._session;
                    }
                    r.Operation = op.Identifier;
                    var ps = new List<RpcParamData>();
                    for (int i = 0; i < op.Params.Count; i++)
                    {
                        var paramIdentifier = i;//TODO: try to make this connection with more inderect way
                        var stream = new MemoryStream();
                        _router._serializer.WriteObject(stream, input.GetInArg(i));
                        ps.Add(new RpcParamData { Identifier = paramIdentifier, Data = stream.ToArray() });
                    }
                    r.Data.AddRange(ps.ToArray());
                    var rData = new MemoryStream();
                    _router._serializer.WriteObject(rData, r);
                    if (op is AsyncOperationDispatch)
                    {
                        object asyncState = input.GetInArg(op.Params.Count + 1);
                        var asyncCallback = (AsyncCallback)input.GetInArg(op.Params.Count);
                        Task task = Tasks.Factory.StartNew((x) =>
                        {
                            try
                            {
                                _router._operationPending.Reset();

                                byte[] result = null;


                                result = ExecuteRequest(rData);

                                var reply =
                                    (Message)
                                    _encoder.ReadObject(new MemoryStream(result),
                                                                                     typeof(Message));

                                applyReplyProcessing(reply);
                            }
                            catch (ExternalException ex)
                            {
                                throw HandleCommunicationError(ex);
                            }
                            catch (Exception ex)
                            {
                                throw;
                            }
                            finally
                            {
                                _router._operationPending.Set();
                            }
                            return new ReturnMessage(null, null, 0, null, input);
                        }, asyncState);

                        task.ContinueWith(x =>
                            {
                                //TODO: do exception handling like in WCF
                                RpcTrace.Error(x.Exception);

                                if (asyncCallback != null)
                                {
                                    asyncCallback(x);
                                }
                            }, TaskContinuationOptions.OnlyOnFaulted);

                        task.ContinueWith(x =>
                        {
                            if (asyncCallback != null)
                            {
                                asyncCallback(x);
                            }
                        }, TaskContinuationOptions.OnlyOnRanToCompletion);
                        return new ReturnMessage(task, null, 0, null, input);

                    }
                    else if (op.Operation.IsOneWay)
                    {
                        Debug.Assert(op.MethodInfo.ReturnType == typeof(void));
                        try
                        {
                            _router._operationPending.Reset();

                            byte[] result = null;


                            result = ExecuteRequest(rData);

                            var reply = (Message)_encoder.ReadObject(new MemoryStream(result), typeof(Message));

                            try
                            {
                                applyReplyProcessing(reply);
                            }
                            catch (Exception ex)
                            {
                                return new ReturnMessage(ex, input);
                            }
                        }
                        catch (ExternalException ex)
                        {
                            throw HandleCommunicationError(ex);
                        }
                        catch (Exception ex)
                        {
                            throw;
                        }
                        finally
                        {
                            _router._operationPending.Set();
                        }
                        return new ReturnMessage(null, null, 0, null, input);
                    }

                    try
                    {
                        _router._operationPending.Reset();

                        byte[] result = null;

                        //BUG: using tasks adds  30% to simple local calls with bytes, and 10% longer then WCF...
                        //TODO: use native MS-RPC timeouts
                        //Task operation = Task.Factory.StartNew(() =>
                        //    {
                        result = ExecuteRequest(rData);
                        //    });
                        //var ended = operation.Wait(_operationTimeout);
                        //if (!ended)
                        //{
                        //    var timeourError =
                        //        new TimeoutException(
                        //            string.Format("The request channel timed out attempting to send after {0}",
                        //                          _operationTimeout));
                        //    return new ReturnMessage(timeourError,input);
                        //}

                        var reply = (Message)_encoder.ReadObject(new MemoryStream(result), typeof(Message));
                        applyReplyProcessing(reply);
                        if (op.MethodInfo.ReturnType != typeof(void))
                        {
                            var retVal = _router._serializer.ReadObject(new MemoryStream(reply.Data), op.MethodInfo.ReturnType);
                            var ret = new ReturnMessage(retVal, null, 0, null, input);
                            return ret;
                        }
                    }
                    catch (ExternalException ex)
                    {
                        var wrappedException = HandleCommunicationError(ex);
                        return new ReturnMessage(wrappedException, input);
                    }
                    catch (Exception ex)
                    {
                        return new ReturnMessage(ex, input);
                    }
                    finally
                    {
                        _router._operationPending.Set();
                    }
                    return new ReturnMessage(null, null, 0, null, input);
                }
                //var error = new ActionNotSupportedException();
                //RpcTrace.Error();
                throw new InvalidOperationException(string.Format("Cannot find operation {0} on service {1}", input.MethodName, _service));

            }

            private void applyReplyProcessing(Message reply)
            {
                foreach (IClientMessageInspector inspector in _router.MessageInspectors)
                {
                    inspector.AfterReceiveReply(ref reply, null);
                }
                if (reply.Fault != null)
                {
                    throw translateFault(reply.Fault);
                }
            }

            private Exception translateFault(FaultData fault)
            {

                var exceptionData = _encoder.ReadObject(new MemoryStream(fault.Detail), typeof(string));
                //TODO: identify how to handle any CommunicationException
                if (
                    //TODO: optimize this comparison
                    fault.Name == "System.ServiceModel.ActionNotSupportedException" ||
                    fault.Name == "NDceRpc.ServiceModel.ActionNotSupportedException"
                    )
                {
                    throw new NDceRpc.ServiceModel.ActionNotSupportedException(exceptionData.ToString());
                }
                return new FaultException(fault.Name + Environment.NewLine + exceptionData);
            }

            private Exception HandleCommunicationError(ExternalException ex)
            {
                //TODO: not all RPC errors means this - can fail in local memory and thread inside RPC - should interpret accordingly
                var oldState = State;
                setState(CommunicationState.Faulted);
                switch (oldState)
                {
                    case CommunicationState.Created:
                        return new EndpointNotFoundException(string.Format("Failed to connect to {0}", _router._address));
                    case CommunicationState.Opened:
                        return new CommunicationException(string.Format("Failed to request {0}", _router._address));
                }
                return ex;
            }

            private byte[] ExecuteRequest(MemoryStream rData)
            {
                _router.throwIfDisposed();
                var result = _router._client.Execute(rData.ToArray());
                setState(CommunicationState.Opened);
                return result;
            }

            private void setState(CommunicationState newState)
            {
                if (State == CommunicationState.Created && newState == CommunicationState.Opened)
                {
                    State = newState;
                }
                else if (newState == CommunicationState.Faulted)
                {
                    State = CommunicationState.Faulted;

                }
            }

            public bool CanCastTo(Type fromType, object o)
            {
                if (fromType == _service

                    )
                {
                    return true;
                }
                if (
                     fromType == typeof(NDceRpc.ServiceModel.IContextChannel) ||
                     fromType == typeof(NDceRpc.ServiceModel.Channels.IChannel) ||
                    fromType == typeof(NDceRpc.ServiceModel.ICommunicationObject))
                {
                    return true;
                }
                return false;
            }

            public string TypeName
            {
                get { return _typeName; }
                set { _typeName = value; }
            }

            public void Abort()
            {

            }

            public void Close()
            {

            }

            public void Close(TimeSpan timeout)
            {

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

            public CommunicationState State
            {
                get { return _state; }
                private set { _state = value; }
            }

            public event EventHandler Closed;
            public event EventHandler Closing;
            public event EventHandler Faulted;
            public event EventHandler Opened;
            public event EventHandler Opening;
            public T GetProperty<T>() where T : class
            {
                throw new NotImplementedException();
            }

            public IExtensionCollection<IContextChannel> Extensions { get; private set; }
            public bool AllowOutputBatching { get; set; }
            public NDceRpc.ServiceModel.Channels.IInputSession InputSession { get; private set; }
            public NDceRpc.ServiceModel.EndpointAddress LocalAddress { get; private set; }
            public TimeSpan OperationTimeout
            {
                get { return _operationTimeout; }
                set { _operationTimeout = value; }
            }

            public NDceRpc.ServiceModel.Channels.IOutputSession OutputSession { get; private set; }
            public NDceRpc.ServiceModel.EndpointAddress RemoteAddress { get; private set; }
            public string SessionId { get; private set; }
        }






        public IEnumerable<Type> GetRequiredInterfaces()
        {
            return Type.EmptyTypes;
        }

        public object Channell
        {
            get { return _remote; }
        }

        public IList<IClientMessageInspector> MessageInspectors
        {
            get { return _messageInspectors; }

        }

        private void throwIfDisposed()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(this.ToString());
            }
        }

        public void Close(TimeSpan closeTimeout)
        {
            if (Interlocked.Exchange(ref _disposeSignaled, 1) != 0)
            {
                return;
            }
            _disposed = true;
            _operationPending.WaitOne(closeTimeout);
            if (_context != null)
            {
                _context.Dispose();
                _context = null;
            }
            if (_client != null)
            {
                _client.Dispose();
                _client = null;
            }

        }

        public void Dispose()
        {
            Close(_binding.CloseTimeout);
        }
    }



}
