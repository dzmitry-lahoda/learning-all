using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using NDceRpc.ExplicitBytes;
using NDceRpc.ServiceModel.Channels;

namespace NDceRpc.ServiceModel
{


    public class RpcEndpointDispatcher : EndpointDispatcher
    {
        private  MessageEncoder _encoder = new ProtobufMessageEncodingBindingElement();
        //private readonly EndpointBindingInfo _address;
        private readonly bool _duplex;
        private readonly SynchronizationContext _syncContext;

        private Dictionary<string, RpcCallbackChannelFactory> _clients = new Dictionary<string, RpcCallbackChannelFactory>();
        private OperationContext _noOp = new OperationContext();

        //private ManualResetEvent _opened = new ManualResetEvent(false);
        protected IExplicitBytesServer _host;
        private ManualResetEvent _operationPending = new ManualResetEvent(true);
        private ConcurrencyMode _concurrency;


        public RpcEndpointDispatcher(object singletonService, ServiceEndpoint endpoint, bool duplex = false, SynchronizationContext syncContext = null)
            : base(singletonService, endpoint)
        {
            _duplex = duplex;
            _syncContext = syncContext;
        }

        private Message invokeContract(IRpcCallInfo call, MessageRequest request, Type contractType)
        {
            OperationDispatchBase operation;
            bool operationExists = _operations.IdToOperation.TryGetValue(request.Operation, out operation);
            if (!operationExists)
            {
                var error = new ActionNotSupportedException(string.Format("Server endpoint {0} with contract {1} does not supports operation with id = {2}. Check that client and server use the same version of contract and binding. ", _endpoint._address, contractType, request.Operation));
                var faultMessage = new Message();
                faultMessage = makeFault(error, faultMessage);
                return faultMessage;
            }
            OperationContext operationContext = SetupOperationConext(call, request, contractType);
            Func<Message> invokeAction = () =>
                {
                    OperationContext.Current = operationContext;
                    if (_concurrency == ConcurrencyMode.Single)
                    {
                        lock (this)
                        {
                            return invokeContract(operation, request);
                        }
                    }
                    return invokeContract(operation, request);
                };
            if (operation.Operation.IsOneWay)
            {
                Task task = Tasks.Factory.StartNew(invokeAction);
                task.ContinueWith(x => RpcTrace.Error(x.Exception), TaskContinuationOptions.OnlyOnFaulted);
                return new Message();
            }
            else
            {
                return invokeAction();
            }
        }

        private Message invokeContract(OperationDispatchBase operation, MessageRequest request)
        {
            var args = deserializeMessageArguments(request, operation);
            if (operation is AsyncOperationDispatch)
            {
                args.Add(null);//AsyncCallback
                args.Add(null);//object asyncState
            }
            var response = new Message();
            try
            {
                var result = invokeServerMethod(operation, args);
                enrichResponseWithReturn(operation, result, response);
            }
            catch (Exception ex)
            {
                response = makeFault(ex, response);
            }
            finally
            {
                OperationContext.Current = _noOp;
            }

            return response;
        }

        private Message makeFault(Exception ex, Message response)
        {
            var error = ex.ToString();
            var detail = new MemoryStream();
            _encoder.WriteObject(detail, error);
            //TODO: make fault with details only in case of IncludeExceptionDetailsInFaults
            response.Fault = new FaultData()
                {
                    Code = ex.GetType().GUID.ToString(),
                    Reason = ex.Message,
                    Detail = detail.ToArray(),
                    Name = ex.GetType().FullName,
                    Node = _endpoint._address
                };

            foreach (var errorHandler in _channelDispatcher.ErrorHandlers)
            {
                if (errorHandler.HandleError(ex))
                {
                    errorHandler.ProvideFault(ex, MessageVersion.WcfProtoRpc1, ref response);
                }
            }
            return response;
        }


        internal void Open(ConcurrencyMode concurrency)
        {
            _concurrency = concurrency;
            //Action open = delegate
            {
                try
                {
                    if (_host == null)
                    {
                        RpcExecuteHandler onExecute =
                            delegate(IRpcCallInfo client, byte[] arg)
                            {
                                if (_concurrency == ConcurrencyMode.Single)
                                {
                                    //lock (this)
                                    //{
                                    _operationPending.Reset();
                                    try
                                    {
                                        return Invoke(client, _endpoint._contractType, arg);
                                    }
                                    finally
                                    {
                                        _operationPending.Set();
                                    }
                                    //}
                                }
                                if (_concurrency == ConcurrencyMode.Multiple)
                                {
                                    //BUG: need have collection of operations because second operation rewrites state of first
                                    _operationPending.Reset();
                                    try
                                    {
                                        return Invoke(client, _endpoint._contractType, arg);
                                    }
                                    finally
                                    {
                                        _operationPending.Set();
                                    }
                                }

                                throw new NotImplementedException(
                                    string.Format("ConcurrencyMode {0} is note implemented", _concurrency));
                            };
                        _host = RpcRequestReplyChannelFactory.CreateHost(_endpoint._binding, _endpoint._address, _endpoint._uuid);


                        _host.OnExecute += onExecute;
                        _host.StartListening();
                    }
                    //_opened.Set();
                }
                catch (Exception ex)
                {
                    bool handled = ExceptionHandler.AlwaysHandle.HandleException(ex);
                    if (!handled) throw;
                }
            };
            //Tasks.Factory.StartNew(open);
            //_opened.WaitOne();
        }

        public byte[] Invoke(IRpcCallInfo call, Type contractType, byte[] arg)
        {
            var messageRequest = (MessageRequest)_encoder.ReadObject(new MemoryStream(arg), typeof(MessageRequest));
            Message response = invokeContract(call, messageRequest, contractType);
            var stream = new MemoryStream();
            _encoder.WriteObject(stream, response);
            return stream.ToArray();
        }

        private void enrichResponseWithReturn(OperationDispatchBase operation, object result, Message response)
        {
            if (operation.MethodInfo.ReturnType != typeof(void) && operation.GetType() != typeof(AsyncOperationDispatch))
            {
                var stream = new MemoryStream();
                _endpoint._binding.Serializer.WriteObject(stream, result);
                response.Data = stream.ToArray();
            }
        }

        private object invokeServerMethod(OperationDispatchBase operation, List<object> args)
        {
            object result = null;
            if (_syncContext != null)
            {
                _syncContext.Send(
                    _ =>
                   result = invokeViaReflection(operation, args),
                    null);
            }
            else
            {
                result = invokeViaReflection(operation, args);
            }
            return result;
        }

        private object invokeViaReflection(OperationDispatchBase operation, List<object> args)
        {
            try
            {
                return operation.MethodInfo.Invoke(_singletonService, BindingFlags.Public, null, args.ToArray(), null);
            }
            catch (TargetInvocationException ex)
            {
                if (ex.InnerException != null)
                {
                    throw ex.InnerException; //invoked method throw error which we rethrow
                }
                throw; // usually when failed to find method on CLR object
            }
        }

        private List<object> deserializeMessageArguments(MessageRequest request, OperationDispatchBase operation)
        {
            var args = new List<object>(operation.Params.Count);
            for (int i = 0; i < operation.Params.Count; i++)
            {
                RpcParamData pData = request.Data[i];
                var map = operation.Params[pData.Identifier];
                var type = map.Info.ParameterType;
                var obj = _endpoint._binding.Serializer.ReadObject(new MemoryStream(pData.Data), type);
                args.Add(obj);
            }
            return args;
        }

        private OperationContext SetupOperationConext(IRpcCallInfo call, MessageRequest request, Type contractType)
        {
            var operationContext = new OperationContext { SessionId = request.Session };

            if (request.Session != null)
            {
                if (_duplex)
                {
                    lock (_clients)
                    {
                        RpcCallbackChannelFactory channelFactory = null;
                        bool contains = _clients.TryGetValue(request.Session, out channelFactory);
                        if (!contains)
                        {
                            var contract = AttributesReader.GetServiceContract(contractType);
                            channelFactory = new RpcCallbackChannelFactory(_endpoint._binding,
                                                                           contract.CallbackContract, new Guid(request.Session),
                                                                           true);
                            _clients[request.Session] = channelFactory;
                        }
                        var callbackBindingInfo = EndpointMapper.WcfToRpc(_endpoint._address);
                        if (!call.IsClientLocal)
                        {
                            //BUG: callbacks accross network does not work
                            //callbackAddress.NetworkAddr =  call.ClientAddress    
                        }

                        callbackBindingInfo.EndPoint += request.Session.Replace("-", "");
                        operationContext.SetGetter(_clients[request.Session], EndpointMapper.RpcToWcf(callbackBindingInfo));
                    }
                }
            }
            return operationContext;
        }

        internal void Dispose(TimeSpan CloseTimeout)
        {
            if (_host != null)
            {
                _operationPending.WaitOne(CloseTimeout);
                _host.Dispose();
                _host = null;
            }
        }
    }


}
