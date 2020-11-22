using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Threading;

namespace NDceRpc.ServiceModel.Core.Tests
{
    [ServiceContract]
    [Guid("641BA85E-DC0C-4AAB-AC03-4F5C1AAAD595")]
    public interface IABC
    {
        [OperationContract]
        void B();
        [OperationContract]
        void C();
        [OperationContract]
        void A();
    }

    [ServiceContract]
    [Guid("CD9A1BA0-621A-4D2A-A6CC-7B16EB79C3D3")]
    public interface IABCD
    {
        [OperationContract]
        void B();
        [DispId(42)]
        [OperationContract]
        void D();
        [OperationContract]
        void C();
        [OperationContract]
        void A();
    }



    [System.ServiceModel.ServiceContract]
    public interface IWcfSimpleService
    {
        [System.ServiceModel.OperationContract(IsOneWay = false)]
        void Do();
    }
    [System.ServiceModel.ServiceBehavior(InstanceContextMode = System.ServiceModel.InstanceContextMode.Single)]
    public class WcfSimpleService : IWcfSimpleService
    {
        public void Do()
        {

        }
    }

    [ServiceContract]
    public interface INoOperationsService
    {
        void Do();
    }


    public interface INotService
    {
        void Do();
    }


    public class NoAttrSimpleService : ISimpleService
    {
        public void Do()
        {

        }
    }

    [ServiceContract]
    public interface ISimpleService
    {
        [OperationContract(IsOneWay = false)]
        void Do();
    }
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class SimpleService : ISimpleService
    {
        public void Do()
        {

        }
    }

    [ServiceContract]
    [Guid("7916102D-903A-4E2E-B8ED-4C0DEFEEDF15")]
    public interface IOtherService
    {

        [OperationContract(IsOneWay = true)]
        void Do();
    }

    public class OtherService : IOtherService
    {
        private ManualResetEvent _wait;

        public OtherService(ManualResetEvent wait)
        {
            _wait = wait;
        }

        public void Do()
        {
            _wait.Set();
        }
    }

    public class Service : IService
    {
        private EventWaitHandle _wait;

        public Service(EventWaitHandle wait)
        {
            _wait = wait;
        }

        public ServiceResult DoWithParamsAndResult(string p1, Guid p2)
        {
            return new ServiceResult { d1 = 2 };
        }

        public void DoOneWay()
        {
            _wait.Set();
        }

        public void CallOtherService(string address)
        {
            var factory = new ChannelFactory<IOtherService>(new NetNamedPipeBinding());
            var channell = factory.CreateChannel<IOtherService>(new EndpointAddress(address));
            channell.Do();
        }

        public void DoException(string message)
        {
            throw new InvalidOperationException(message);
        }

        public void Dispose()
        {

        }
    }




    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class ExceptionService : IExceptionService
    {
        public void DoException(string message)
        {
            throw new InvalidOperationException(message);
        }
    }


    [Guid("B8003DB5-D743-49DE-98D3-C64852D25083")]
    [ServiceContract]
    public interface IExceptionService
    {
        [OperationContract(IsOneWay = false)]
        void DoException(string message);
    }

    [ServiceContract]
    [Guid("C059B8B0-9318-4467-9BB7-4FBB9979C3C5")]
    public interface IService : IDisposable
    {
        [OperationContract(IsOneWay = false)]
        ServiceResult DoWithParamsAndResult(string p1, Guid p2);

        [OperationContract(IsOneWay = true)]
        void DoOneWay();

        [OperationContract(IsOneWay = true)]
        void CallOtherService(string address);

    }

    [DataContract]
    public class ServiceResult
    {
        [DataMember(Order = 1)]
        public int d1 { get; set; }
        [DataMember(Order = 2)]
        public double d2 { get; set; }

    }

    [ServiceContract]

    public interface IInheritanceService : IBaseService,IDisposable
    {

        [OperationContract(IsOneWay = false)]
        void Do();



    }

    [ServiceContract]

    public interface IBaseService : IDisposable
    {

        [OperationContract(IsOneWay = false)]
        void DoBase();



    }

    public class InheritanceService : IInheritanceService
    {
        


        public void Do()
        {
           
        }

        public void Dispose()
        {
            
        }

        public void DoBase()
        {
                
        }
    }
}
