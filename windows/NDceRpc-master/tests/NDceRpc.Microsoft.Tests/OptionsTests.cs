using System;
using NUnit.Framework;
using NDceRpc.ExplicitBytes;
using NDceRpc.Microsoft.Interop;

namespace NDceRpc.Microsoft.Tests
{
	[TestFixture]
	public class OptionsTests
	{





		[Test]
		public void ClientSecurityCallbackCalled()
		{
			Guid iid = Guid.NewGuid();
			using (ExplicitBytesServer server = new ExplicitBytesServer(iid))
			{
				server.AddProtocol(RpcProtseq.ncacn_ip_tcp, "18080", 5);
				server.AddAuthentication(RPC_C_AUTHN.RPC_C_AUTHN_WINNT);
				server.StartListening();
				server.OnExecute +=
					delegate(IRpcCallInfo clientInfo, byte[] arg)
				{ 
					return arg; 
				};

		
			    var endpoingBinding = new EndpointBindingInfo(RpcProtseq.ncacn_ip_tcp, "127.0.0.1", "18080");
				bool securityCalled = false;
				using (var client = new ExplicitBytesClient (iid, endpoingBinding)) {
					
					var authCallback = new FunctionPtr<RPC_C_SECURITY_CALLBACK>(x=>{
						securityCalled = true;
						IntPtr securityContext = IntPtr.Zero;
						var getSecurityStatus = NativeMethods.I_RpcBindingInqSecurityContext(x, out securityContext);
						Assert.AreEqual (RPC_STATUS.RPC_S_OK, getSecurityStatus);
						Assert.AreNotEqual(IntPtr.Zero,securityContext);
					});
					client.AuthenticateAs (null, ExplicitBytesClient.Self, RPC_C_AUTHN_LEVEL.RPC_C_AUTHN_LEVEL_PKT_PRIVACY, RPC_C_AUTHN.RPC_C_AUTHN_WINNT);
					var setStatus = NativeMethods.RpcBindingSetOption (client.Handle, (uint)RpcBindingOptions.RPC_C_OPT_SECURITY_CALLBACK, authCallback.Handle);
					Assert.AreEqual (RPC_STATUS.RPC_S_OK, setStatus);
					client.Execute (new byte[0]);

				}
				Assert.IsTrue (securityCalled);
			
			}
		}
	}
}

