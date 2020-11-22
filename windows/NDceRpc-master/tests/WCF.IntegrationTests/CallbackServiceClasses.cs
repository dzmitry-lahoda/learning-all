using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace WCF.IntegrationTests
{


    [ServiceContract(SessionMode = SessionMode.Required, CallbackContract = typeof(ICallbackServiceCallback))]

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
            OperationContext.Current.GetCallbackChannel<ICallbackServiceCallback>().OnCallback(new CallbackData());
        }
    }


    [DataContract]
    public class CallbackData
    {
        [DataMember(Order = 1)]
        public string Data { get; set; }
    }

    [ServiceContract]
    public interface ICallbackServiceCallback
    {
        [OperationContract(IsOneWay = false, IsInitiating = false, IsTerminating = false)]
        void OnCallback(CallbackData message);

        [OperationContract(IsOneWay = true, IsInitiating = false, IsTerminating = false)]
        void OnOneWayCallback();
    }

    [CallbackBehavior]
    public class CallbackServiceCallback : ICallbackServiceCallback
    {
        public void OnCallback(CallbackData message)
        {

        }

        public void OnOneWayCallback()
        {

        }
    }

}
