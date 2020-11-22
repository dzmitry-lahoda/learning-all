using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using NUnit.Framework;

namespace NDceRpc.ServiceModel.Core.Tests
{
    [TestFixture]
    public class AttributesReaderTests
    {
        [Test]
        public void WorksWithSystemServiceModelAttributes()
        {
            var address = @"ipc:///test" + MethodBase.GetCurrentMethod().Name;
            var serv = new WcfSimpleService();
            using (var host = new ServiceHost(serv, new Uri(address)))
            {
                var b = new LocalBinding();
                host.AddServiceEndpoint(typeof(IWcfSimpleService), b, address);
                host.Open();
                using ( var f = new ChannelFactory<IWcfSimpleService>(b))
                {
                    var c = f.CreateChannel(new EndpointAddress(address));
                   c.Do();
                }
            }
        }

        [Test]
        public void GetServiceContract_wcf_custom()
        {
            var attr = AttributesReader.GetServiceContract(typeof (IWcfSimpleService));

            Assert.IsNotNull(attr);
        }

        [Test]
        public void GetOperationContract_wcf_custom()
        {
            var attr = AttributesReader.GetOperationContract(typeof(IWcfSimpleService).GetMethods().First());

            Assert.IsNotNull(attr);
        }

        [Test]
        public void GetOperationContract_custom_custom()
        {
            var attr = AttributesReader.GetOperationContract(typeof(ISimpleService).GetMethods().First());

            Assert.IsNotNull(attr);
        }

        [Test]
        public void GetServiceContract_custom_custom()
        {
            var attr = AttributesReader.GetServiceContract(typeof(ISimpleService));

            Assert.IsNotNull(attr);
        }


        [Test]
        public void GetOperationContract_no_null()
        {
            var attr = AttributesReader.GetOperationContract(typeof(INoOperationsService).GetMethods().First());

            Assert.IsNull(attr);
        }

        [Test]
        public void IsOperationContract_no_false()
        {
            var isOp = AttributesReader.IsOperationContract(typeof(INoOperationsService).GetMethods().First());

            Assert.IsFalse(isOp);
        }

        [Test]
        public void IsOperationContract_yes_true()
        {
            var isOp = AttributesReader.IsOperationContract(typeof(ISimpleService).GetMethods().First());

            Assert.IsTrue(isOp);
        }

        [Test]
        public void IsOperationContract_wcfYes_true()
        {
            var isOp = AttributesReader.IsOperationContract(typeof(IWcfSimpleService).GetMethods().First());

            Assert.IsTrue(isOp);
        }

        [Test]
        public void GetServiceContract_no_null()
        {
            var attr = AttributesReader.GetServiceContract(typeof(INotService));

            Assert.IsNull(attr);
        }
    }
}
