using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.ServiceModel;
using System.Text;
using System.Threading;
using NDceRpc;
using NDceRpc.ExplicitBytes;
using NDceRpc.Microsoft.Interop;
using managed_entities;

namespace ManagedServer
{
    class Program
    {
        public delegate void Host();

        private static NDceRpc.ExplicitBytes.ExplicitBytesServer _api;
        private static ServiceHost _wcf;

        private static void Main(string[] args)
        {
            Host host = hostRpc;
            for (int i = 0; i < args.Length; i++)
            {
                string o = args[i];
                if (o == "-m")
                {
                    string s = args[i + 1];

                    switch (s)
                    {
                        case "rpc":
                            host = hostRpc;
                            break;
                        case "wcf":
                            host = hostWcf;
                            break;
                        default:
                            break;
                    }

                }
            }
            host();
            Console.WriteLine("Server started");
            Console.ReadKey();
        }

        static byte[] api_OnExecute(IRpcCallInfo client, byte[] input)
        {
            byte[] resp = new byte[input.Length * 10];
            return resp;
        }

        private static void hostRpc()
        {

            var guid = Marshal.GenerateGuidForType(typeof(api));
            _api = new ExplicitBytesServer(guid);
            _api.AddProtocol(RpcProtseq.ncalrpc, "FastDataServer", 20);
            _api.OnExecute += api_OnExecute;
            _api.StartListening();
        }



        private static void hostWcf()
        {
            var t = new Thread(() =>
                {
                    var service = new WcfBytes();
                    _wcf = new ServiceHost(service, new []{api.Uri});
                    _wcf.AddServiceEndpoint(typeof (IWcfBytes), api.Binding, api.Endpoint.Uri);
                    _wcf.Open();
                });
            t.Start();
            

        }

    }
}