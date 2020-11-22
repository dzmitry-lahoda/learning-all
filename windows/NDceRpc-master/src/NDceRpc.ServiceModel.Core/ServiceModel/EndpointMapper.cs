using System;
using System.Diagnostics;
using NDceRpc.Microsoft.Interop;

namespace NDceRpc.ServiceModel
{
    public static class EndpointMapper
    {
        /// <summary>
        /// for ALPC on Windows and Domain sockets on Linux use ipc:///
        /// </summary>
        public const string UriSchemeIpc = "ipc";

        
        /// <summary>
        /// Makes WCF adress to be RPC
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public static EndpointBindingInfo WcfToRpc(string address)
        {
            Debug.Assert(Uri.IsWellFormedUriString(address, UriKind.Absolute));
            
            var uri = new Uri(address);
            
            if (uri.Scheme == Uri.UriSchemeNetTcp)
            {
                Debug.Assert(String.IsNullOrEmpty(uri.PathAndQuery) || uri.PathAndQuery == "/");
                return new EndpointBindingInfo(RpcProtseq.ncacn_ip_tcp,uri.Host,uri.Port.ToString());
            }
            if (uri.Scheme == Uri.UriSchemeNetPipe)
            {
                Debug.Assert(uri.IsDefaultPort);
                Debug.Assert(String.IsNullOrEmpty(uri.Query));
                return new EndpointBindingInfo(RpcProtseq.ncacn_np,uri.Host,@"\pipe"+uri.PathAndQuery.Replace('/', '\\'));
            }
            if (string.Equals(uri.Scheme, UriSchemeIpc, StringComparison.InvariantCultureIgnoreCase))
            {
                Debug.Assert(uri.IsDefaultPort);
                Debug.Assert(String.IsNullOrEmpty(uri.Query));
                Debug.Assert(String.IsNullOrEmpty(uri.Host));
                return new EndpointBindingInfo(RpcProtseq.ncalrpc, null, uri.ToString().Replace(UriSchemeIpc+":///",string.Empty).Replace('/', '/'));
            }
            //TODO: HTTP transport
            throw new NotImplementedException(uri.Scheme + " not implemented");
        }

        public static readonly Guid RpcNamespace = new Guid("6C5FC266-BD34-4E46-BA29-3536B5CB14DE");

        public static Guid CreateUuid(string address, Type contractType)
        {
            return GuidUtility.Create(RpcNamespace, address + contractType.GUID);
        }

        public static string RpcToWcf(EndpointBindingInfo bindingInfo)
        {
            if (bindingInfo.Protseq == RpcProtseq.ncacn_ip_tcp)
            {
                return new UriBuilder(Uri.UriSchemeNetTcp, bindingInfo.NetworkAddr, Int32.Parse(bindingInfo.EndPoint)).Uri.ToString();
            }
            if (bindingInfo.Protseq == RpcProtseq.ncalrpc)
            {
              

                var path = bindingInfo.EndPoint;
                
                var builder = new UriBuilder(UriSchemeIpc,null);
                builder.Path = path;
                //TODO: use builder to build with empty host ///
                //builder.Host = string.Empty;
                //return builder.Uri.ToString();
                string local = ":///";
                return builder.Scheme + local + builder.Path;
            }
            if (bindingInfo.Protseq == RpcProtseq.ncacn_np)
            {
                var path = bindingInfo.EndPoint.Replace(@"\pipe", string.Empty).Replace("\\", "/");
                var builder = new UriBuilder(Uri.UriSchemeNetPipe, bindingInfo.NetworkAddr);
                builder.Path = path;
                return builder.Uri.ToString();
            }
            throw new NotImplementedException(bindingInfo.Protseq + " not implemented");
        }
    }
}
