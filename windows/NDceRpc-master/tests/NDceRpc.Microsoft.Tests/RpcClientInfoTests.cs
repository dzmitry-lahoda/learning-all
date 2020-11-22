

using System;
using NDceRpc.ExplicitBytes;
using NDceRpc.Microsoft.Interop;
using NUnit.Framework;

namespace NDceRpc.Test
{
    [TestFixture]
    public class RpcClientInfoTests
    {
  
        [Test]
        public void TestClientOnLocalRpc()
        {
            Guid iid = Guid.NewGuid();
            using (ExplicitBytesServer server = new ExplicitBytesServer(iid))
            {
                server.AddProtocol(RpcProtseq.ncalrpc, "lrpctest", 5);
                server.AddAuthentication(RPC_C_AUTHN.RPC_C_AUTHN_WINNT);
                server.StartListening();
                server.OnExecute +=
                    delegate(IRpcCallInfo client, byte[] arg)
                        {
                            Assert.AreEqual(0, arg.Length);
                            Assert.AreEqual(RPC_C_AUTHN.RPC_C_AUTHN_WINNT, client.AuthenticationLevel);
                            Assert.AreEqual(RPC_C_AUTHN_LEVEL.RPC_C_AUTHN_LEVEL_PKT_PRIVACY, client.ProtectionLevel);
                            Assert.AreEqual(RpcProtoseqType.LRPC, client.ProtocolType);
                            Assert.AreEqual(new byte[0], client.ClientAddress);
                            Assert.AreEqual(System.Diagnostics.Process.GetCurrentProcess().Id, client.ClientPid.ToInt32());
                            Assert.AreEqual(System.Security.Principal.WindowsIdentity.GetCurrent().Name, client.ClientPrincipalName);
                            Assert.AreEqual(System.Security.Principal.WindowsIdentity.GetCurrent().Name, client.ClientUser.Name);
                            Assert.AreEqual(true, client.IsClientLocal);
                            Assert.AreEqual(true, client.IsAuthenticated);
                            Assert.AreEqual(false, client.IsImpersonating);
                            using(client.Impersonate())
                                Assert.AreEqual(true, client.IsImpersonating);
                            Assert.AreEqual(false, client.IsImpersonating);
                            return arg;
                        };
                var endpointBinding = new EndpointBindingInfo(RpcProtseq.ncalrpc, null, "lrpctest");
                using (ExplicitBytesClient client = new ExplicitBytesClient(iid, endpointBinding))
                {
                    client.AuthenticateAs(ExplicitBytesClient.Self);
                    client.Execute(new byte[0]);
                }
            }
        }

        [Test]
        public void TestClientOnNamedPipe()
        {
            Guid iid = Guid.NewGuid();
            using (ExplicitBytesServer server = new ExplicitBytesServer(iid))
            {
                server.AddProtocol(RpcProtseq.ncacn_np, @"\pipe\testpipename", 5);
                server.AddAuthentication(RPC_C_AUTHN.RPC_C_AUTHN_WINNT);
                server.StartListening();
                server.OnExecute +=
                    delegate(IRpcCallInfo client, byte[] arg)
                    {
                        Assert.AreEqual(0, arg.Length);
                        Assert.AreEqual(RPC_C_AUTHN.RPC_C_AUTHN_WINNT, client.AuthenticationLevel);
                        Assert.AreEqual(RPC_C_AUTHN_LEVEL.RPC_C_AUTHN_LEVEL_PKT_PRIVACY, client.ProtectionLevel);
                        Assert.AreEqual(RpcProtoseqType.NMP, client.ProtocolType);
                        Assert.AreEqual(new byte[0], client.ClientAddress);
                        Assert.AreEqual(0, client.ClientPid.ToInt32());
                        Assert.AreEqual(String.Empty, client.ClientPrincipalName);
                        Assert.AreEqual(System.Security.Principal.WindowsIdentity.GetCurrent().Name, client.ClientUser.Name);
                        Assert.AreEqual(true, client.IsClientLocal);
                        Assert.AreEqual(true, client.IsAuthenticated);
                        Assert.AreEqual(false, client.IsImpersonating);
                        using (client.Impersonate())
                            Assert.AreEqual(true, client.IsImpersonating);
                        Assert.AreEqual(false, client.IsImpersonating);
                        return arg;
                    };

                var endpointBinding = new EndpointBindingInfo(RpcProtseq.ncacn_np, null, @"\pipe\testpipename");
                using (ExplicitBytesClient client = new ExplicitBytesClient(iid, endpointBinding))
                {
                    client.AuthenticateAs(ExplicitBytesClient.Self);
                    client.Execute(new byte[0]);
                }
            }
        }

        [Test]
        public void TestClientOnAnonymousPipe()
        {
            Guid iid = Guid.NewGuid();
            using (ExplicitBytesServer server = new ExplicitBytesServer(iid))
            {
                server.AddProtocol(RpcProtseq.ncacn_np, @"\pipe\testpipename", 5);
                server.AddAuthentication(RPC_C_AUTHN.RPC_C_AUTHN_NONE);
                server.StartListening();
                server.OnExecute +=
                    delegate(IRpcCallInfo client, byte[] arg)
                    {
                        Assert.AreEqual(0, arg.Length);
                        Assert.AreEqual(RPC_C_AUTHN.RPC_C_AUTHN_NONE, client.AuthenticationLevel);
                        Assert.AreEqual(RPC_C_AUTHN_LEVEL.RPC_C_AUTHN_LEVEL_NONE, client.ProtectionLevel);
                        Assert.AreEqual(RpcProtoseqType.NMP, client.ProtocolType);
                        Assert.AreEqual(new byte[0], client.ClientAddress);
                        Assert.AreEqual(0, client.ClientPid.ToInt32());
                        Assert.AreEqual(null, client.ClientPrincipalName);
                        Assert.AreEqual(System.Security.Principal.WindowsIdentity.GetAnonymous().Name, client.ClientUser.Name);
                        Assert.AreEqual(true, client.IsClientLocal);
                        Assert.AreEqual(false, client.IsAuthenticated);
                        Assert.AreEqual(false, client.IsImpersonating);

                        bool failed = false;
                        try { client.Impersonate().Dispose(); }
                        catch (UnauthorizedAccessException) { failed = true; }
                        Assert.AreEqual(true, failed);
                        return arg;
                    };

                var endpointBinding = new EndpointBindingInfo(RpcProtseq.ncacn_np, null, @"\pipe\testpipename");
                using (ExplicitBytesClient client = new ExplicitBytesClient(iid, endpointBinding))
                {
                    client.AuthenticateAs(ExplicitBytesClient.Anonymous);
                    client.Execute(new byte[0]);
                }
            }
        }

        [Test]
        public void TestClientOnTcpip()
        {
            Guid iid = Guid.NewGuid();
            using (ExplicitBytesServer server = new ExplicitBytesServer(iid))
            {
                server.AddProtocol(RpcProtseq.ncacn_ip_tcp, @"18081", 5);
                server.AddAuthentication(RPC_C_AUTHN.RPC_C_AUTHN_WINNT);
                server.StartListening();
                server.OnExecute +=
                    delegate(IRpcCallInfo client, byte[] arg)
                    {
                        Assert.AreEqual(0, arg.Length);
                        Assert.AreEqual(RPC_C_AUTHN.RPC_C_AUTHN_WINNT, client.AuthenticationLevel);
                        Assert.AreEqual(RPC_C_AUTHN_LEVEL.RPC_C_AUTHN_LEVEL_PKT_PRIVACY, client.ProtectionLevel);
                        Assert.AreEqual(RpcProtoseqType.TCP, client.ProtocolType);
                        Assert.AreEqual(16, client.ClientAddress.Length);
                        Assert.AreEqual(0, client.ClientPid.ToInt32());
                        Assert.AreEqual(String.Empty, client.ClientPrincipalName);
                        Assert.AreEqual(System.Security.Principal.WindowsIdentity.GetCurrent().Name, client.ClientUser.Name);
                        Assert.AreEqual(false, client.IsClientLocal);
                        Assert.AreEqual(true, client.IsAuthenticated);
                        Assert.AreEqual(false, client.IsImpersonating);
                        using (client.Impersonate())
                            Assert.AreEqual(true, client.IsImpersonating);
                        Assert.AreEqual(false, client.IsImpersonating);
                        return arg;
                    };
                var endpointBinding = new EndpointBindingInfo(RpcProtseq.ncacn_ip_tcp, null, @"18081");
                using (ExplicitBytesClient client = new ExplicitBytesClient(iid, endpointBinding))
                {
                    client.AuthenticateAs(ExplicitBytesClient.Self);
                    client.Execute(new byte[0]);
                }
            }
        }

        [Test]
        public void TestNestedClientImpersonate()
        {
            Guid iid = Guid.NewGuid();
            using (ExplicitBytesServer server = new ExplicitBytesServer(iid))
            {
                server.AddProtocol(RpcProtseq.ncacn_np, @"\pipe\testpipename", 5);
                server.AddAuthentication(RPC_C_AUTHN.RPC_C_AUTHN_WINNT);
                server.StartListening();
                server.OnExecute +=
                    delegate(IRpcCallInfo client, byte[] arg)
                    {
                        Assert.AreEqual(false, client.IsImpersonating);
                        using (client.Impersonate())
                        {
                            Assert.AreEqual(true, client.IsImpersonating);
                            using (client.Impersonate())
                                Assert.AreEqual(true, client.IsImpersonating); 
                            //does not dispose, we are still impersonating
                            Assert.AreEqual(true, client.IsImpersonating);
                        }
                        Assert.AreEqual(false, client.IsImpersonating);
                        return arg;
                    };
                var endpointBinding = new EndpointBindingInfo(RpcProtseq.ncacn_np, null, @"\pipe\testpipename");
                using (ExplicitBytesClient client = new ExplicitBytesClient(iid, endpointBinding))
                {
                    client.AuthenticateAs(ExplicitBytesClient.Self);
                    client.Execute(new byte[0]);
                }
            }
        }
    }
}
