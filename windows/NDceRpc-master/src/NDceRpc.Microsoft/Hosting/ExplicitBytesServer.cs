using System;
using System.Runtime.InteropServices;
using NDceRpc.Microsoft.Interop;

namespace NDceRpc.ExplicitBytes
{
    /// <summary>
    /// The delegate format for the OnExecute event
    /// </summary>
    public delegate byte[] RpcExecuteHandler(IRpcCallInfo call, byte[] input);


    /// <summary>
    /// Provides server-side services for RPC
    /// </summary>
    public class ExplicitBytesServer : Server, IExplicitBytesServer
    {


        /// <summary> The interface Id the service is using </summary>
        public readonly Guid IID;

        private RpcExecuteHandler _handler;
        public override void Dispose()
        {
            _handler = null;
            base.Dispose();
        }

        public ExplicitBytesServer(Guid iid, int maxCalls, int maxRequestBytes, bool allowAnonymousCallbacks)
        {
            IID = iid;
            RpcTrace.Verbose("ServerRegisterInterface({0})", iid);
            // Guid.Empty to avoid registration of any interface allowing access to AddProtocol/AddAuthentication
            if (Guid.Empty != iid)
            {
                Ptr<RPC_SERVER_INTERFACE> sIf = ServerInterfaceFactory.Create(_handle, iid, RpcRuntime.TYPE_FORMAT, RpcRuntime.FUNC_FORMAT,
                                                                        RpcEntryPoint);
                this.serverRegisterInterface(sIf, maxCalls, maxRequestBytes, allowAnonymousCallbacks);
                  
            }
        }


        /// <summary>
        /// Constructs an RPC server for the given interface guid, the guid is used to identify multiple rpc
        /// servers/services within a single process.
        /// </summary>
        public ExplicitBytesServer(Guid iid)
            : this(iid, MAX_CALL_LIMIT, DEFAULT_REQUEST_LIMIT, false)
        {
 
        }
        

 

       

        private uint RpcEntryPoint(IntPtr clientHandle, uint szInput, IntPtr input, out uint szOutput, out IntPtr output)
        {
            output = IntPtr.Zero;
            szOutput = 0;

            try
            {
                byte[] bytesIn = new byte[szInput];
                Marshal.Copy(input, bytesIn, 0, bytesIn.Length);

                byte[] bytesOut;
                using (RpcCallInfo call = new RpcCallInfo(clientHandle))
                {
                    bytesOut = Execute(call, bytesIn);
                }
                if (bytesOut == null)
                {
                    return (uint)RPC_STATUS.RPC_S_NOT_LISTENING;
                }

                szOutput = (uint)bytesOut.Length;
                output = RpcRuntime.Alloc(szOutput);
                Marshal.Copy(bytesOut, 0, output, bytesOut.Length);

                return (uint)RPC_STATUS.RPC_S_OK;
            }
            catch (Exception ex)
            {
                RpcRuntime.Free(output);
                output = IntPtr.Zero;
                szOutput = 0;

                RpcTrace.Error(ex);
                return (uint)RPC_STATUS.RPC_E_FAIL;
            }
        }
        /// <summary>
        /// Can be over-ridden in a derived class to handle the incomming RPC request, or you can
        /// subscribe to the OnExecute event.
        /// </summary>
        public virtual byte[] Execute(IRpcCallInfo call, byte[] input)
        {
            RpcExecuteHandler proc = _handler;
            if (proc != null)
            {
                return proc(call, input);
            }
            return null;
        }
        /// <summary>
        /// Allows a single subscription to this event to handle incomming requests rather than 
        /// deriving from and overriding the Execute call.
        /// </summary>
        public event RpcExecuteHandler OnExecute
        {
            add
            {
                lock (this)
                {
                    Guard.Assert<InvalidOperationException>(_handler == null, "The interface id is already registered.");
                    _handler = value;
                }
            }
            remove
            {
                lock (this)
                {
                    Guard.NotNull(value);
                    if (_handler != null)
                        Guard.Assert<InvalidOperationException>(
                            Object.ReferenceEquals(_handler.Target, value.Target)
                            && Object.ReferenceEquals(_handler.Method, value.Method)
                            );
                    _handler = null;
                }
            }
        }



    }
}