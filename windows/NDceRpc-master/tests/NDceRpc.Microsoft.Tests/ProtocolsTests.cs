

using System;
using System.Text;
using NDceRpc.ExplicitBytes;
using NDceRpc.Microsoft.Interop;
using NUnit.Framework;

namespace NDceRpc.Test
{
    [TestFixture]
    public class ProtocolsTests
    {
        string[] LocalNames = new string[] { null, "localhost", "127.0.0.1", "::1", Environment.MachineName };

  

        [Test]
        public void TcpIpTest()
        {
            ReversePingTest(RpcProtseq.ncacn_ip_tcp, LocalNames, "18080", 
                RPC_C_AUTHN.RPC_C_AUTHN_WINNT, RPC_C_AUTHN.RPC_C_AUTHN_GSS_NEGOTIATE);
        }

        [Test]
        public void NamedPipeTest()
        {
            ReversePingTest(RpcProtseq.ncacn_np, LocalNames, @"\pipe\testpipename", 
                RPC_C_AUTHN.RPC_C_AUTHN_NONE, RPC_C_AUTHN.RPC_C_AUTHN_WINNT, RPC_C_AUTHN.RPC_C_AUTHN_GSS_NEGOTIATE);
        }

        [Test]
        public void LocalRpcTest()
        {
            ReversePingTest(RpcProtseq.ncalrpc, new string[] { null }, @"testsomename", 
                RPC_C_AUTHN.RPC_C_AUTHN_NONE, RPC_C_AUTHN.RPC_C_AUTHN_WINNT, RPC_C_AUTHN.RPC_C_AUTHN_GSS_NEGOTIATE);
        }


        static void ReversePingTest(RpcProtseq protocol, string[] hostNames, string endpoint, params RPC_C_AUTHN[] authTypes)
        {
            foreach (RPC_C_AUTHN auth in authTypes)
                ReversePingTest(protocol, hostNames, endpoint, auth);
        }

        static void ReversePingTest(RpcProtseq protocol, string[] hostNames, string endpoint, RPC_C_AUTHN auth)
        {
            Guid iid = Guid.NewGuid();
            using (ExplicitBytesServer server = new ExplicitBytesServer(iid))
            {
                server.OnExecute += 
                    delegate(IRpcCallInfo client, byte[] arg)
                    {
                        Array.Reverse(arg);
                        return arg;
                    };

                server.AddProtocol(protocol, endpoint, 5);
                server.AddAuthentication(auth);
                server.StartListening();

                byte[] input = Encoding.ASCII.GetBytes("abc");
                byte[] expect = Encoding.ASCII.GetBytes("cba");

                foreach (string hostName in hostNames)
                {
                    using (ExplicitBytesClient client = new ExplicitBytesClient(iid, new EndpointBindingInfo(protocol, hostName, endpoint)))
                    {
                        client.AuthenticateAs(null, auth == RPC_C_AUTHN.RPC_C_AUTHN_NONE
                                                      ? ExplicitBytesClient.Anonymous
                                                      : ExplicitBytesClient.Self, 
                                                  auth == RPC_C_AUTHN.RPC_C_AUTHN_NONE
                                                      ? RPC_C_AUTHN_LEVEL.RPC_C_AUTHN_LEVEL_NONE
                                                      : RPC_C_AUTHN_LEVEL.RPC_C_AUTHN_LEVEL_PKT_PRIVACY,
                                                  auth);

                        Assert.AreEqual(expect, client.Execute(input));
                    }
                }
            }
        }
    }
}
