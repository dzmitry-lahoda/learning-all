using System;
using System.Reflection;
using System.ServiceModel;
using NUnit.Framework;

namespace WCF.Tests
{
    namespace ContractVersion1
    {
        [ServiceContract]
        public interface IService
        {
            [OperationContract(IsOneWay = false)]
            void Do();

            [OperationContract(IsOneWay = false)]
            void Do1();
        }
        [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
        public class Service : ContractVersion1.IService
        {
            public void Do()
            {

            }

            public void Do1()
            {

            }
        }
    }

    namespace ContractVersion2
    {
        [ServiceContract]
        public interface IService
        {
            [OperationContract(IsOneWay = false)]
            void Do();

            [OperationContract(IsOneWay = false)]
            void Do2();
        }

    }

    [TestFixture]
    public class OperationContractsTests
    {


        [Test]
        public void Services_DifferentNamespaces_sameOperation()
        {
            var baseAddress = @"net.pipe://127.0.0.1/" + this.GetType().Name + MethodBase.GetCurrentMethod().Name;
            var serv = new ContractVersion1.Service();
            using (var host = new ServiceHost(serv, new Uri[] { new Uri(baseAddress) }))
            {
                var binding = new NetNamedPipeBinding();
                host.AddServiceEndpoint(typeof(ContractVersion1.IService), binding, baseAddress);

                host.Open();
                using (var channelFatory = new ChannelFactory<ContractVersion2.IService>(binding))
                {
                    var c = channelFatory.CreateChannel(new EndpointAddress(baseAddress));
                    c.Do();
                }
            }
        }


        [Test]
        public void Services_DifferentNamespaces_differentOperations()
        {
            var baseAddress = @"net.pipe://127.0.0.1/" + this.GetType().Name + MethodBase.GetCurrentMethod().Name;
            var serv = new ContractVersion1.Service();
            using (var host = new ServiceHost(serv, new Uri[] { new Uri(baseAddress) }))
            {
                var binding = new NetNamedPipeBinding();
                host.AddServiceEndpoint(typeof(ContractVersion1.IService), binding, baseAddress);

                host.Open();
                using (var channelFatory = new ChannelFactory<ContractVersion2.IService>(binding))
                {
                    var c = channelFatory.CreateChannel(new EndpointAddress(baseAddress));
                    Assert.Throws<ActionNotSupportedException>(() => c.Do2());
                }
            }
        }
    }
}