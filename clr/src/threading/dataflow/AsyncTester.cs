using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace AcceptanceTests.Helpers
{
    /// <summary>
    /// Instance of this class provides facilities to make async and not deterministic time tests.
    /// </summary>
    /// <remarks>
    /// This class should
    /// - provide deterministic behavior as much as possible
    /// - not be misleading and work the same using the same setups
    /// - readable and understandable use code
    /// - usable in unit and acceptance tests
    /// </remarks>
    //TODO: investigate hardware threads
    //TODO: investigate AsyncTester.CanRobustlyRun and Assert.Inconclusive on single core machines
    public class AsyncTester<T>
    {
        private BufferBlock<T> waitForResult = new BufferBlock<T>();
        private int _threadId;
        private IList<TestTask> _testTasks = new List<TestTask>();

        public AsyncTester()
        {
            _threadId = Thread.CurrentThread.ManagedThreadId;
        }

        public TestTask Schedule(Action action)
        {
            Contract.Requires(isCallingFromSingleThread());
            var testTask = new TestTask(action);
            _testTasks.Add(testTask);
            return testTask;
        }


        public bool Post(T data)
        {
            return waitForResult.Post(data);
        }

        public void Run()
        {
            Contract.Requires(isCallingFromSingleThread());
            //TODO: Create TestTaskScheduler wich does not runs later registered tasks until first runned
            foreach (var testTask in _testTasks)
            {
                testTask.Start();
            }
        }

        public T Receive(TimeSpan timeout)
        {
            //TODO: begin wait only when all task runned
            return waitForResult.Receive(timeout);
        }

        public class TestTask
        {
            public TimeSpan InTime = TimeSpan.FromMilliseconds(0);
            public Action Action { get; set; }
            internal Task Task;

            public TestTask(Action action)
            {
                Action = action;
            }

            public void In(TimeSpan inTime)
            {
                InTime = inTime;
            }

            public void Start()
            {
                Task = new Task(() =>
                                     {
                                         Thread.Sleep(InTime);
                                         Action();
                                     },TaskCreationOptions.PreferFairness);
                
   
                Task.Start();
            }
        }

        private bool isCallingFromSingleThread()
        {
            return _threadId == Thread.CurrentThread.ManagedThreadId;
        }

        //public class TestTaskScheduler : TaskScheduler
        //{
        //    protected override void QueueTask(Task task)
        //    {
        //        throw new NotImplementedException();
        //    }

        //    protected override bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued)
        //    {
        //        throw new NotImplementedException();
        //    }

        //    protected override IEnumerable<Task> GetScheduledTasks()
        //    {
        //        throw new NotImplementedException();
        //    }
        //}

    }
}