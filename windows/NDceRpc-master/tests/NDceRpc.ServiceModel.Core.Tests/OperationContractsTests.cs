using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.ServiceModel;
using NUnit.Framework;

namespace NDceRpc.ServiceModel.Core.Tests
{
    namespace ContractVersion1
    {
        [ServiceContract]
        [System.Runtime.InteropServices.Guid("2D2F51D8-54FB-4B4A-ABC7-F7AB5327D485")]
        public interface IService
        {
            [OperationContract(IsOneWay = false)]
            [DispId(42)]
            void Do();

            [OperationContract(IsOneWay = false)]
            [DispId(1)]
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
        [System.Runtime.InteropServices.Guid("2D2F51D8-54FB-4B4A-ABC7-F7AB5327D485")]
        public interface IService
        {
            [DispId(42)]
            [OperationContract(IsOneWay = false)]
            void Do();

            [DispId(2)]
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
            using (var host = new NDceRpc.ServiceModel.ServiceHost(serv, new Uri[] { new Uri(baseAddress) }))
            {
                var binding = new NDceRpc.ServiceModel.NetNamedPipeBinding();
                host.AddServiceEndpoint(typeof(ContractVersion1.IService), binding, baseAddress);

                host.Open();
                using (var channelFatory = new NDceRpc.ServiceModel.ChannelFactory<ContractVersion2.IService>(binding))
                {
                    var c = channelFatory.CreateChannel(new NDceRpc.ServiceModel.EndpointAddress(baseAddress));
                    c.Do();
                }
            }
        }

         [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
        internal class ABCD:IABCD 
        {
             public void B()
             {
                 
             }

             public void D()
             {
              
             }

             public void C()
             {
               
             }

             public void A()
             {
               
             }
        }

        [Test]
        public void Services_autoAndManualIds_Ok()
        {
            var baseAddress = @"net.pipe://127.0.0.1/" + this.GetType().Name + MethodBase.GetCurrentMethod().Name;
            var serv = new ABCD();
            using (var host = new NDceRpc.ServiceModel.ServiceHost(serv, new Uri[] { new Uri(baseAddress) }))
            {
                var binding = new NDceRpc.ServiceModel.NetNamedPipeBinding();
                host.AddServiceEndpoint(typeof(IABCD), binding, baseAddress);

                host.Open();
                using (var channelFatory = new NDceRpc.ServiceModel.ChannelFactory<IABCD>(binding))
                {
                    var c = channelFatory.CreateChannel(new NDceRpc.ServiceModel.EndpointAddress(baseAddress));
                    c.A();
                    c.D();
                }
            }
        }


        [Test]
        public void Services_DifferentNamespaces_differentOperations()
        {
            var baseAddress = @"net.pipe://127.0.0.1/" + this.GetType().Name + MethodBase.GetCurrentMethod().Name;
            var serv = new ContractVersion1.Service();
            using (var host = new NDceRpc.ServiceModel.ServiceHost(serv, new Uri[] { new Uri(baseAddress) }))
            {
                var binding = new NDceRpc.ServiceModel.NetNamedPipeBinding();
                host.AddServiceEndpoint(typeof(ContractVersion1.IService), binding, baseAddress);

                host.Open();
                using (var channelFatory = new NDceRpc.ServiceModel.ChannelFactory<ContractVersion2.IService>(binding))
                {
                    var c = channelFatory.CreateChannel(new NDceRpc.ServiceModel.EndpointAddress(baseAddress));
                    c.Do();
                    Assert.Throws<ActionNotSupportedException>(() => c.Do2());
                }
            }
        }
    }
}