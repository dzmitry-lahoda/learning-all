using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace NDceRpc.ServiceModel.Core.Tests
{
    class CompletedAsyncResult<T> : IAsyncResult
    {
        T data;

        public CompletedAsyncResult(T data)
        { this.data = data; }

        public T Data
        { get { return data; } }

        public object AsyncState
        { get { return (object)data; } }

        public WaitHandle AsyncWaitHandle
        { get { throw new Exception("The method or operation is not implemented."); } }

        public bool CompletedSynchronously
        { get { return true; } }

        public bool IsCompleted
        { get { return true; } }

    }

    [ServiceContract()]
    [Guid("FF58B22B-DE08-4874-9891-9A1144EDE7B9")]
    public interface IAsyncTaskService
    {
        [OperationContract]
        [DispId(1)]
        Task<string> GetMessages(string msg);
    }

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    class AsyncTaskService : IAsyncTaskService
    {
        public Task<string> GetMessages(string msg)
        {

            return System.Threading.Tasks.Task<string>.Factory.StartNew(() =>
            {
                return new String(msg.Reverse().ToArray());
            });
        }
    }

    [ServiceContract(SessionMode = SessionMode.Required, CallbackContract = typeof(IAsyncServiceCallback))]
    [Guid("101380E2-1C11-403E-8CFC-3BD2815E6BAB")]
    public interface IAsyncService
    {
        [OperationContract(IsOneWay = false)]
        [DispId(1)]
        void DoSyncCall();

        [DispId(2)]
        [OperationContract(AsyncPattern = true)]
        IAsyncResult BeginServiceAsyncMethod(AsyncCallback callback, object asyncState);

        string EndServiceAsyncMethod(IAsyncResult result);
    }

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    class AsyncService : IAsyncService
    {
        private object _data;
        private ManualResetEvent _done;

        public AsyncService(ManualResetEvent done)
        {
            _done = done;
        }

        public void DoSyncCall()
        {
            var callback = OperationContext.Current.GetCallbackChannel<IAsyncServiceCallback>();
            var wait = callback.BeginCallback(null, null);
            wait.AsyncWaitHandle.WaitOne();
            callback.EndCallback(wait);

        }

        public IAsyncResult BeginServiceAsyncMethod(AsyncCallback callback, object data)
        {
            return new CompletedAsyncResult<string>("Hello");
        }

        public string EndServiceAsyncMethod(IAsyncResult asyncResult)
        {
            _done.Set();
            return ":)";
        }
    }

    [Guid("56F95F2A-C070-4482-80CD-6E8C025C9FB5")]
    public interface IAsyncServiceCallback
    {
        [OperationContract(IsOneWay = true, AsyncPattern = true)]
        IAsyncResult BeginCallback(AsyncCallback callback, object data);


        void EndCallback(IAsyncResult asyncResult);
    }

    class AsyncServiceCallback : IAsyncServiceCallback
    {
        public IAsyncResult BeginCallback(AsyncCallback callback, object data)
        {
            return new CompletedAsyncResult<string>("Hello");
        }

        public void EndCallback(IAsyncResult asyncResult)
        {

        }
    }
}
