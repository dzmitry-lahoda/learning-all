using System;
using System.Runtime.InteropServices;
using System.Security.Principal;

namespace NDceRpc.Microsoft.Interop
{
    internal class RpcCallInfo : IRpcCallInfo, IDisposable
    {
        private readonly IntPtr _clientHandle;
        private WindowsIdentity _user;
        private bool _impersonating;
        private RPC_CALL_ATTRIBUTES_V2 _callAttrs;
        private byte[] _clientAddress;
        private string _clientPrincipalName;
        private bool _isAuthenticated;

        public RpcCallInfo(IntPtr clientHandle)
        {
            _user = null;
            _impersonating = false;
            _clientHandle = clientHandle;
        }

        public bool IsImpersonating
        {
            get { return _impersonating; }
        }

        public IDisposable Impersonate()
        {
            if (_impersonating)
            {
                return new IgnoreOnDispose();
            }

            if (!IsAuthenticated)
                throw new UnauthorizedAccessException();

            Guard.Assert(NativeMethods.RpcImpersonateClient(_clientHandle));
            _impersonating = true;
            return new RpcImpersonationContext(this);
        }

        private void RevertToSelf()
        {
            if (_impersonating)
            {
                Guard.Assert(NativeMethods.RpcRevertToSelfEx(_clientHandle));
            }
            _impersonating = false;
        }

        public WindowsIdentity ClientUser
        {
            get
            {
                if (_user == null)
                {
                    if (IsAuthenticated)
                    {
                        using (Impersonate())
                            _user = WindowsIdentity.GetCurrent(true);
                    }
                    else
                    {
                        _user = WindowsIdentity.GetAnonymous();
                    }
                }

                return _user;
            }
        }

        public string ClientPrincipalName
        {
            get
            {
                GetCallInfo();
                return _clientPrincipalName;
            }
        }

        public byte[] ClientAddress
        {
            get
            {
                GetCallInfo();
                return _clientAddress == null ? new byte[0] : (byte[])_clientAddress.Clone();
            }
        }

        public void Dispose()
        {
            RevertToSelf();
            if (_user != null)
            {
                _user.Dispose();
            }
            _user = null;
        }

        public bool IsAuthenticated
        {
            get { return GetCallInfo().AuthenticationService != RPC_C_AUTHN.RPC_C_AUTHN_NONE || _isAuthenticated; }
        }

        public bool IsClientLocal
        {
            get
            {
                return GetCallInfo().ProtocolSequence == RpcProtoseqType.LRPC ||
                       GetCallInfo().IsClientLocal == RpcCallClientLocality.Local;
            }
        }

        public RpcProtoseqType ProtocolType
        {
            get { return GetCallInfo().ProtocolSequence; }
        }

        public RPC_C_AUTHN_LEVEL ProtectionLevel
        {
            get { return GetCallInfo().AuthenticationLevel; }
        }

        public RPC_C_AUTHN AuthenticationLevel
        {
            get { return GetCallInfo().AuthenticationService; }
        }

        public IntPtr ClientPid
        {
            get { return GetCallInfo().ClientPID; }
        }

        private RPC_CALL_ATTRIBUTES_V2 GetCallInfo()
        {
            if (_callAttrs.Version != 0)
                return _callAttrs;
            var attrs = new RPC_CALL_ATTRIBUTES_V2
                {
                    Version = 2,
                    Flags = RPC_CALL_ATTRIBUTES_FLAGS.RPC_QUERY_NO_AUTH_REQUIRED
                };
            RPC_STATUS err = NativeMethods.RpcServerInqCallAttributes(_clientHandle, ref attrs);

            if (err == RPC_STATUS.RPC_S_INVALID_ARG) //may not support v2 on early edditions of XP/SP3
            {
                attrs.Version = 1;
                err = NativeMethods.RpcServerInqCallAttributes(_clientHandle, ref attrs);
            }

            if (err == RPC_STATUS.RPC_S_OK)
            {
                _callAttrs = attrs;
                _isAuthenticated = false;
                attrs.Flags = RPC_CALL_ATTRIBUTES_FLAGS.RPC_QUERY_IS_CLIENT_LOCAL |
                              RPC_CALL_ATTRIBUTES_FLAGS.RPC_QUERY_NO_AUTH_REQUIRED;
                if ((err = NativeMethods.RpcServerInqCallAttributes(_clientHandle, ref attrs)) == RPC_STATUS.RPC_S_OK)
                {
                    _callAttrs.IsClientLocal = attrs.IsClientLocal;

                    if (_callAttrs.ProtocolSequence == RpcProtoseqType.LRPC)
                    {
                        attrs.Flags = RPC_CALL_ATTRIBUTES_FLAGS.RPC_QUERY_CLIENT_PID;
                        if ((err = NativeMethods.RpcServerInqCallAttributes(_clientHandle, ref attrs)) == RPC_STATUS.RPC_S_OK)
                            _callAttrs.ClientPID = attrs.ClientPID;
                    }
                }

                if (_callAttrs.ProtocolSequence != RpcProtoseqType.LRPC)
                {
                    using (Ptr<byte[]> callerAddress = new Ptr<byte[]>(new byte[1024]))
                    {
                        var localAddress = new RPC_CALL_LOCAL_ADDRESS_V1();
                        localAddress.Version = 1;
                        localAddress.Buffer = callerAddress.Handle;
                        localAddress.BufferSize = 1024;
                        localAddress.AddressFormat = RpcLocalAddressFormat.Invalid;
                        _callAttrs = attrs;

                        using (var callerAddressv1 = new Ptr<RPC_CALL_LOCAL_ADDRESS_V1>(localAddress))
                        {
                            attrs.CallLocalAddress = callerAddressv1.Handle;
                            attrs.Flags = RPC_CALL_ATTRIBUTES_FLAGS.RPC_QUERY_CALL_LOCAL_ADDRESS |
                                          RPC_CALL_ATTRIBUTES_FLAGS.RPC_QUERY_NO_AUTH_REQUIRED;
                            if ((err = NativeMethods. RpcServerInqCallAttributes(_clientHandle, ref attrs)) == RPC_STATUS.RPC_S_OK)
                            {
                                _clientAddress = new byte[callerAddressv1.Data.BufferSize];
                                Array.Copy(callerAddress.Data, _clientAddress, _clientAddress.Length);
                            }
                        }
                    }
                }

                using (Ptr<byte[]> clientPrincipal = new Ptr<byte[]>(new byte[1024]))
                {
                    attrs.ClientPrincipalName = clientPrincipal.Handle;
                    attrs.ClientPrincipalNameBufferLength = 1024;
                    attrs.Flags = RPC_CALL_ATTRIBUTES_FLAGS.RPC_QUERY_CLIENT_PRINCIPAL_NAME;
                    if ((err = NativeMethods.RpcServerInqCallAttributes(_clientHandle, ref attrs)) == RPC_STATUS.RPC_S_OK)
                    {
                        _clientPrincipalName = Marshal.PtrToStringUni(clientPrincipal.Handle);

                        if (!String.IsNullOrEmpty(_clientPrincipalName))
                        {
                            _isAuthenticated = true;
                            //On Windows XP this only returns a value on LRPC so we know they are local
                            if (attrs.Version == 1)
                                _callAttrs.IsClientLocal = RpcCallClientLocality.Local;
                        }
                    }
                }
            }
            else
                RpcTrace.Warning("RpcServerInqCallAttributes error {0} = {1}", err, new RpcException(err).Message);


            return _callAttrs;
        }

        private class IgnoreOnDispose : IDisposable
        {
            void IDisposable.Dispose()
            {
            }
        }

        private class RpcImpersonationContext : IDisposable
        {
            private readonly RpcCallInfo _call;

            public RpcImpersonationContext(RpcCallInfo call)
            {
                _call = call;
            }

            void IDisposable.Dispose()
            {
                _call.RevertToSelf();
            }
        }


    }
}