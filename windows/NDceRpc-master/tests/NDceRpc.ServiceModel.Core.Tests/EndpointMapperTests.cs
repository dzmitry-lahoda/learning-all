using NDceRpc.Microsoft.Interop;
using NUnit.Framework;

namespace NDceRpc.ServiceModel.Core.Tests
{
    [TestFixture]
    public class EndpointMapperTests
    {
        [Test]
        public void WcfNamedPipe_RpcNamedPipe()
        {
            string wcf = "net.pipe://127.0.0.1/1/test.test/test";
            var rpc = EndpointMapper.WcfToRpc(wcf);
            Assert.AreEqual(@"127.0.0.1", rpc.NetworkAddr);
            Assert.AreEqual(@"\pipe\1\test.test\test", rpc.EndPoint);
        }

        [Test]
        public void RpcNamedPipe_WcfNamedPipe()
        {
            

            var endpointBinding = new EndpointBindingInfo(RpcProtseq.ncacn_np, "127.0.0.1", "test");
            var wcf = EndpointMapper.RpcToWcf(endpointBinding);
            Assert.AreEqual("net.pipe://127.0.0.1/test", wcf);
        }

        [Test]
        public void RpcLocalPipe_UriLocal()
        {
            var endpointBinding = new EndpointBindingInfo(RpcProtseq.ncalrpc, null, "test");
            var wcf = EndpointMapper.RpcToWcf(endpointBinding);
            Assert.AreEqual("ipc:///test", wcf);
        }

        [Test]
        public void WcfTcp_RpcTcp()
        {
            string wcf = "net.tcp://127.0.0.1:666";
            var rpc = EndpointMapper.WcfToRpc(wcf);
            Assert.AreEqual(@"127.0.0.1", rpc.NetworkAddr);
            Assert.AreEqual(@"666", rpc.EndPoint);
        }

        [Test]
        public void UriLocal_RpcLocal()
        {
            string wcf = "ipc:///1/test.test/test";
            var rpc = EndpointMapper.WcfToRpc(wcf);
            Assert.AreEqual(null, rpc.NetworkAddr);
            Assert.AreEqual(@"1/test.test/test", rpc.EndPoint);
        }

 
    }
}
