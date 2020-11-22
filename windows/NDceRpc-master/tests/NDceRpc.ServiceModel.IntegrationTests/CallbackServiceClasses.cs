using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace NDceRpc.ServiceModel.IntegrationTests
{


    [ServiceContract(SessionMode = SessionMode.Required, CallbackContract = typeof(ICallbackServiceCallback))]
    [Guid("8D14E4D2-1D11-4745-8621-5DBB0D1836CA")]
    public interface ICallbackService
    {
        [OperationContract(IsOneWay = false)]
        void Call();
    }
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single,ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class CallbackService : ICallbackService
    {

        public void Call()
        {
            NDceRpc.ServiceModel.OperationContext.Current.GetCallbackChannel<ICallbackServiceCallback>().OnCallback(new CallbackData());
        }
    }


    [DataContract]
    public class CallbackData
    {
        [DataMember(Order = 1)]
        public string Data { get; set; }
    }

        [ServiceContract]
    [Guid("556A23A5-06BB-4F6C-9DC3-380E18F7416E")]
    public interface ICallbackServiceCallback
    {
        [OperationContract(IsOneWay = false, IsInitiating = false, IsTerminating = false)]
        void OnCallback(CallbackData message);

        [OperationContract(IsOneWay = true, IsInitiating = false, IsTerminating = false)]
        void OnOneWayCallback();
    }

    [CallbackBehavior]
    internal class CallbackServiceCallback : ICallbackServiceCallback
    {
        public void OnCallback(CallbackData message)
        {

        }

        public void OnOneWayCallback()
        {

        }
    }

}
