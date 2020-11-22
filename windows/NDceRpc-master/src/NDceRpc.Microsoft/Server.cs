using System;
using NDceRpc.Microsoft.Interop;

namespace NDceRpc
{
    public class Server : IDisposable
    {


        /// <summary> The max limit of in-flight calls </summary>
        public const int MAX_CALL_LIMIT = 255;


        public const int DEFAULT_REQUEST_LIMIT = -1;

        protected readonly RpcHandle _handle;
        protected static readonly UsageCounter _listenerCount = new UsageCounter("RpcRuntime.Listener.{0}", System.Diagnostics.Process.GetCurrentProcess().Id);
        protected bool _isListening;
        protected int _maxCalls;

        public Server()
        {
            _maxCalls = MAX_CALL_LIMIT;
            _handle = new RpcServerHandle();
        }

        private class RpcServerHandle : RpcHandle
        {
            protected override void DisposeHandle(ref IntPtr handle)
            {
                if (handle != IntPtr.Zero)
                {
                    NativeMethods.RpcServerUnregisterIf(handle, IntPtr.Zero, 1);
                    handle = IntPtr.Zero;
                }
            }
        }
        /// <summary>
        /// Disposes of the server and stops listening if the server is currently listening
        /// </summary>
        public virtual void Dispose()
        {
            StopListening();
            _handle.Dispose();
        }

        protected  void ServerRegisterInterface(IntPtr sIfHandle, RpcHandle handle)
        {
            ServerRegisterInterface(sIfHandle, handle, IntPtr.Zero, IntPtr.Zero);
        }

        protected void ServerRegisterInterface(IntPtr sIfHandle, RpcHandle handle, IntPtr mgrTypeUuid, IntPtr mgrEpv)
        {
            //TODO: make server isolated of other process services, make it not static as possible
            Guard.Assert(NativeMethods.RpcServerRegisterIf(sIfHandle, mgrTypeUuid, mgrEpv));
            //RPC_IF_CALLBACK_FN security = null;
            // Guard.Assert(NativeMethods.RpcServerRegisterIfEx(sIf.Handle, IntPtr.Zero, IntPtr.Zero,  InterfacRegistrationFlags.RPC_IF_AUTOLISTEN, MAX_CALL_LIMIT, ref security));
            handle.Handle = sIfHandle;
        }

        protected readonly FunctionPtr<RPC_IF_CALLBACK_FN> _authCallback = new FunctionPtr<RPC_IF_CALLBACK_FN>(AuthCallback);
        protected void serverRegisterInterface(Ptr<RPC_SERVER_INTERFACE> sIf,  int maxCalls, int maxRequestBytes, bool allowAnonymousCallback)
        {

            var flags = InterfacRegistrationFlags.Standard_interface_semantics;
            IntPtr fnAuth = IntPtr.Zero;
            if (allowAnonymousCallback)
            {
                flags = InterfacRegistrationFlags.RPC_IF_ALLOW_CALLBACKS_WITH_NO_AUTH;
                fnAuth = _authCallback.Handle;
            }

            if (!allowAnonymousCallback && maxRequestBytes < 0)
                Guard.Assert(NativeMethods.RpcServerRegisterIf(sIf.Handle, IntPtr.Zero, IntPtr.Zero));
            else
            {
                Guard.Assert(NativeMethods.RpcServerRegisterIf2(sIf.Handle, IntPtr.Zero, IntPtr.Zero, flags,
                 maxCalls <= 0 ? MAX_CALL_LIMIT : maxCalls,
                 maxRequestBytes <= 0 ? 80 * 1024 : maxRequestBytes, _authCallback.Handle));
            }
            _handle.Handle = sIf.Handle; 
        }
        
        /// <summary>
        /// Used to ensure that the server is listening with a specific protocol type.  
        /// </summary>
        public bool AddProtocol(RpcProtseq protocol, string endpoint, int maxCalls)
        {
            _maxCalls = Math.Max(_maxCalls, maxCalls);
            return serverUseProtseqEp(protocol, maxCalls, endpoint);
       
        }

        /// <summary>
        /// Adds a type of authentication sequence that will be allowed for RPC connections to this process.
        /// </summary>
        public bool AddAuthentication(RPC_C_AUTHN type)
        {
            return AddAuthentication(type, null);
        }
        /// <summary>
        /// Adds a type of authentication sequence that will be allowed for RPC connections to this process.
        /// </summary>
        public bool AddAuthentication(RPC_C_AUTHN type, string serverPrincipalName)
        {
            return serverRegisterAuthInfo(type, serverPrincipalName);
        }
        /// <summary>
        /// Starts the RPC listener for this instance,
        /// </summary>
        public void StartListening()
        {
            if (_isListening)
                return;

            _listenerCount.Increment(serverListen, _maxCalls);
            _isListening = true;
        }
        /// <summary>
        /// Stops listening for this instance.
        /// </summary>
        public void StopListening()
        {
            if (!_isListening)
                return;

            _isListening = false;
            _listenerCount.Decrement(serverStopListening);
        }


        private static bool serverUseProtseqEp(RpcProtseq protocol, int maxCalls, String endpoint)
        {
            RpcTrace.Verbose("serverUseProtseqEp({0})", protocol);
            // all RPC servers within the process will be available on that protocol
            // once invoked this can not be undone
            //TODO: make server isolated of other process services, make it not static as possible
            RPC_STATUS err = NativeMethods.RpcServerUseProtseqEp(protocol.ToString(), maxCalls, endpoint, IntPtr.Zero);
            if (err != RPC_STATUS.RPC_S_DUPLICATE_ENDPOINT)
                Guard.Assert(err);
            return err == RPC_STATUS.RPC_S_OK;
        }

        protected static RPC_STATUS AuthCallback(IntPtr Interface, IntPtr Context)
        {
            return RPC_STATUS.RPC_S_OK;
        }

        private static bool serverRegisterAuthInfo(RPC_C_AUTHN auth, string serverPrincName)
        {
            RpcTrace.Verbose("serverRegisterAuthInfo({0})", auth);
            RPC_STATUS response = NativeMethods.RpcServerRegisterAuthInfo(serverPrincName, (uint)auth, IntPtr.Zero, IntPtr.Zero);
            if (response != RPC_STATUS.RPC_S_OK)
            {
                RpcTrace.Warning("serverRegisterAuthInfo - unable to register authentication type {0}", auth);
                return false;
            }
            return true;
        }

 
        private static void serverListen(int maxCalls)
        {
            RpcTrace.Verbose("Begin Server Listening");
            // starts listening all server in process on registered protocols
            //TODO: make server isolated of other process services, make it not static as possible
            RPC_STATUS result = NativeMethods.RpcServerListen(1, maxCalls, 1);
            if (result == RPC_STATUS.RPC_S_ALREADY_LISTENING)
            {
                result = RPC_STATUS.RPC_S_OK;
            }
            Guard.Assert(result);
            RpcTrace.Verbose("Server Ready");
        }

        private static void serverStopListening()
        {
            RpcTrace.Verbose("Stop Server Listening");
            // stops listening on all registered protocols.
            //TODO: make server isolated of other process services, make it not static as possible
            RPC_STATUS result = NativeMethods.RpcMgmtStopServerListening(IntPtr.Zero);
            if (result != RPC_STATUS.RPC_S_OK)
            {
                RpcTrace.Warning("RpcMgmtStopServerListening result = {0}", result);
            }
            //TODO: make server isolated of other process services, make it not static as possible
            result = NativeMethods.RpcMgmtWaitServerListen();
            if (result != RPC_STATUS.RPC_S_OK)
            {
                RpcTrace.Warning("RpcMgmtWaitServerListen result = {0}", result);
            }
        }

        protected bool Equals(Server other)
        {
            return Equals(_handle, other._handle);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Server) obj);
        }

        public override int GetHashCode()
        {
            return (_handle != null ? _handle.GetHashCode() : 0);
        }
    }
}