using System;
using System.Threading;

//TODO:may be wrong because of my wrong understanding of "SynchronizingObject" essence
namespace Itransition.Training
{
    /// <summary>
    ///  SynchronizingObject gives access to shared resource
    ///  not more then to n threads simultaneously. 
    ///  Object provides capability to access the resource (with
    ///  specified timeout and without it) and to release the resource.
    ///  On effort to get access the resource in case of occupancy of the resource, 
    ///  thread is blocked until at least one of threads releases resource or timeout elapses
    /// </summary>
    /// <typeparam name="T">Shared resource type</typeparam>
    public class SynchronizingObject<T>:IDisposable
    {
        /// <summary>
        /// Current shared resource
        /// </summary>
        private T sharedResource;
        /// <summary>
        /// Maximal number of thread wich can an access shared resource
        /// </summary>
        private int n;
        /// <summary>
        /// Current timeout
        /// </summary>
        private TimeSpan currentTimeSpan;
        /// <summary>
        /// Сore semaphore
        /// </summary>
        private Semaphore semaphore;

        #region Properties
        /// <summary>
        /// Maximal number of thread wich can an access shared resource
        /// </summary>
        public int N
        {
            get { return n; }
        }

        /// <summary>
        /// Current shared resource
        /// </summary>
        public T SharedResource
        {
            get { return sharedResource; }
        }

        /// <summary>
        /// Current timeout
        /// </summary>
        public TimeSpan CurrentTimeSpan
        {
            get { return currentTimeSpan; }
        }
        #endregion

        /// <summary>
        /// Creates SynchronizingObject with infinity timeout
        /// </summary>
        /// <param name="sharedResource"></param>
        /// <param name="n"></param>
        public SynchronizingObject(T sharedResource,int n)
        {
            this.n = n;
            this.sharedResource = sharedResource;
            currentTimeSpan = new TimeSpan(0, 0, 0, 0, -1);
            semaphore = new Semaphore(n, n);
        }

        /// <summary>
        /// Creates SynchronizingObject
        /// </summary>
        /// <param name="sharedResource">Shared resource</param>
        /// <param name="n">Max number of threads</param>
        /// <param name="milliseccondTimeout">Timeout before exit</param>
        public SynchronizingObject(T sharedResource, int n, int milliseccondTimeout)
        {
            this.n = n;
            this.sharedResource = sharedResource;
            semaphore = new Semaphore(n, n);
            currentTimeSpan = new TimeSpan(0, 0, 0, 0, milliseccondTimeout);
        }

        /// <summary>
        /// Blocks the current thread until the current SynchronizingObject receives a signal or 
        /// timeout elapses
        /// </summary>
        public void WaitOne()
        {
            if (!semaphore.WaitOne(currentTimeSpan, false))
            {
                Thread.CurrentThread.Abort();
            }
        }

        /// <summary>
        /// Send signal to the SynchronizingObject about ending of the shared resource using
        /// </summary>
        public void Release()
        {
            semaphore.Release();
        }


        public void Dispose()
        {
            
        }

    }
}
