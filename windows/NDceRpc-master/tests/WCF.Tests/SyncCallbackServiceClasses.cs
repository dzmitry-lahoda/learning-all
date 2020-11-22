using System.Runtime.Serialization;
using System.ServiceModel;
using System.Threading;

namespace WCF.Tests
{


    [ServiceContract(SessionMode = SessionMode.Required, CallbackContract = typeof(ISyncCallbackServiceCallback))]

    public interface ISyncCallbackService
    {
        [OperationContract(IsOneWay = false)]
        int Call();
    }

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class SyncCallbackService : ISyncCallbackService
    {

        public int Call()
        {
            return OperationContext.Current.GetCallbackChannel<ISyncCallbackServiceCallback>().OnCallback();
        }
    }




    public interface ISyncCallbackServiceCallback
    {
        [OperationContract(IsOneWay = false, IsInitiating = false, IsTerminating = false)]
        int OnCallback();


    }
    
    //[CallbackBehavior(UseSynchronizationContext = true)] - default
    public class SyncCallbackServiceCallback : ISyncCallbackServiceCallback
    {
        public int OnCallback()
        {
            return Thread.CurrentThread.ManagedThreadId;
        }


    }


    [CallbackBehavior(UseSynchronizationContext = false)]
    public class NotSyncCallbackServiceCallback : ISyncCallbackServiceCallback
    {
        public int OnCallback()
        {
            return Thread.CurrentThread.ManagedThreadId;
        }


    }

}
