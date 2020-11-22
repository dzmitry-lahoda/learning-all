using System;
using System.Threading;

namespace NDceRpc
{
    //TODO: make this process wide via shared memory
    public class UsageCounter
    {
        private const int MaxCount = int.MaxValue;
        private const int Timeout = 120000;

        readonly Mutex _lock;
        readonly Semaphore _count;

        /// <summary> Creates a composite name with the format and arguments specified </summary>
        public UsageCounter(string nameFormat, params object[] arguments)
        {
            string name = String.Format(nameFormat, arguments);
            _lock = new Mutex(false, name + ".Lock");
            _count = new Semaphore(MaxCount, MaxCount, name + ".Count");
        }

        /// <summary> Delegate fired inside lock if this is the first Increment() call on the name provided </summary>
        public void Increment<T>(Action<T> beginUsage, T arg)
        {
            if (!_lock.WaitOne(Timeout, false))
                throw new TimeoutException();
            try
            {
                if (!_count.WaitOne(Timeout, false))
                    throw new TimeoutException();

                if (!_count.WaitOne(Timeout, false))
                {
                    _count.Release();
                    throw new TimeoutException();
                }

                int counter = 1 + _count.Release();

                //if this is the first call
                if (beginUsage != null && counter == (MaxCount - 1))
                    beginUsage(arg);
            }
            finally
            {
                _lock.ReleaseMutex();
            }
        }

        /// <summary> Delegate fired inside lock if the Decrement() count reaches zero </summary>
        public void Decrement(ThreadStart endUsage)
        {
            if (!_lock.WaitOne(Timeout, false))
                throw new TimeoutException();
            try
            {
                int counter = 1 + _count.Release();

                //if this is the last decrement expected
                if (endUsage != null && counter == MaxCount)
                    endUsage();
            }
            finally
            {
                _lock.ReleaseMutex();
            }
        }
    }
}
