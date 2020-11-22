using System;
using System.Reflection;
using System.ServiceModel;
using NUnit.Framework;

namespace WCF.Tests
{
    [TestFixture]
    public class SecurityTests
    {
        [Test]
 
        public void Open_Open_error()
        {
            var tcp= new NetTcpBinding();
            var pipe = new NetNamedPipeBinding(NetNamedPipeSecurityMode.None);
          
            Assert.IsTrue(tcp.Security.GetType() != pipe.Security.GetType());
        }
    }
}
