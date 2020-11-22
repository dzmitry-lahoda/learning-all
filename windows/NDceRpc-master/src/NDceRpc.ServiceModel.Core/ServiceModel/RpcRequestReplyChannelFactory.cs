using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using NDceRpc.ExplicitBytes;
using NDceRpc.Microsoft.Interop;

namespace NDceRpc.ServiceModel
{
    public static class RpcRequestReplyChannelFactory
    {
        public static IExplicitBytesClient CreateClient(Binding binding, Guid uuid, string address)
        {

            var bindingInfo = EndpointMapper.WcfToRpc(address);
            bindingInfo.EndPoint = CanonizeEndpoint(bindingInfo);
            var client = new ExplicitBytesClient(uuid, bindingInfo);
            
            //NOTE: applying any authentication on local IPC greatly slows down start up of many simulatanious service
            bool skipAuthentication = binding.Authentication == RPC_C_AUTHN.RPC_C_AUTHN_NONE && bindingInfo.Protseq == RpcProtseq.ncalrpc;
            if (skipAuthentication)
            {
                client.AuthenticateAsNone();
            }
            else
            {
                client.AuthenticateAs(null, binding.Authentication == RPC_C_AUTHN.RPC_C_AUTHN_NONE
                                                                  ? ExplicitBytesClient.Anonymous
                                                                  : ExplicitBytesClient.Self,
                                                              binding.Authentication == RPC_C_AUTHN.RPC_C_AUTHN_NONE
                                                                  ? RPC_C_AUTHN_LEVEL.RPC_C_AUTHN_LEVEL_NONE
                                                                  : RPC_C_AUTHN_LEVEL.RPC_C_AUTHN_LEVEL_PKT_PRIVACY,
                                                              binding.Authentication);
            }
         
            return client;
        }

        public static IExplicitBytesServer CreateHost(Binding binding, string address, Guid uuid)
        {

            var host = new ExplicitBytesServer(uuid);
            var endpointBinding = EndpointMapper.WcfToRpc(address);
            string endPoint = CanonizeEndpoint(endpointBinding);
            host.AddProtocol(binding.ProtocolTransport, endPoint, binding.MaxConnections);
            host.AddAuthentication(binding.Authentication);
            return host;
        }

        
        private static string CanonizeEndpoint(EndpointBindingInfo endpointBinding)
        {
           var endpoint = endpointBinding.EndPoint;
            // Windows XP has restiction on names of local transport
           //  RPC_S_STRING_TOO_LONG is thrown on XP, but not 7
		   // http://msdn.microsoft.com/en-us/library/windows/desktop/aa367115.aspx
           //so generating GUID for each 
            if (endpointBinding.Protseq == RpcProtseq.ncalrpc 
                && Environment.OSVersion.Platform == PlatformID.Win32NT
                && Environment.OSVersion.Version.Major < 6
                )
            {
                //hope will not clash on Windows, can leave part of old one, but need to find out limits
                endpoint = "NET"+GuidUtility.Create(EndpointMapper.RpcNamespace, endpoint).ToString("N");
            }
            return endpoint;
        }
  
    }






}
