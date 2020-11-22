using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO.MemoryMappedFiles;
using System.IO;
using System.Security.AccessControl;
using System.Threading.Tasks;



namespace NLocalIpc
{
    /// <summary>
    /// Instance of this class does asynchronous event propagation on single machine in Peer to Peer(process to proccess) manner.
    /// </summary>
    /// <typeparam name="T">Serializable object</typeparam>
    /// <remarks>
    /// Each event of unique name with <typeparamref name="T"/> type registers itself in Shared memory.
    /// When event raised its data its written into Shared memory.
    /// Then all registered listeners are notified via wait handles.
    /// Each listener reads event data out of Shared memory and notifies to raiser that reading was done. 
    /// Next event raising is blocked until all subscribers confirm previous message read.
    /// All in-process handles invoked asynchronously (without blocking of inter process communication).
    /// </remarks>
    //TODO: split into 2 classes LocalPeersNetwork (which does work with peer registy) and this 
    public class LocalInterProcessEvent<T> : IDisposable
        where T : EventArgs
    {
        private string _name;


        private TimeSpan _peerResponce = TimeSpan.FromSeconds(10);
        const int MAX_BYTES = 32767;

        private int _disposeSignaled;
        private bool Disposed
        {
            get { return _disposeSignaled != 0; }
        }

        private EventHandler<T> _handler = delegate { };
        private Mutex _readerWriterLock;
        private MemoryMappedFileChannel _eventData;
        private MemoryMappedFileChannel _eventWaitersRegistry;
        private bool _initialized;
        private LocalPeer _me;

        public static class MutextHelper
        {

            public static Mutex AttachToMutex(string mutexName)
            {
                bool createdNew;

                // Create a string representing the current user. 
                string user = Environment.UserDomainName + "\\" + Environment.UserName;

                // Create a security object that grants no access.
                MutexSecurity mSec = new MutexSecurity();

                // Add a rule that grants the current user the  
                // right to wait on or signal the event.
                MutexAccessRule rule = new MutexAccessRule(user,
                    MutexRights.FullControl,
                    AccessControlType.Allow);
                mSec.AddAccessRule(rule);

                return new Mutex(false, mutexName, out createdNew, mSec);
            }

            public static EventWaitHandle AttachToEventWaitHandle(string name, EventResetMode mode = EventResetMode.ManualReset)
            {
                bool createdNew;
                // Create a string representing the current user. 
                string user = Environment.UserDomainName + "\\" + Environment.UserName;

                // Create a security object that grants no access.
                EventWaitHandleSecurity mSec = new EventWaitHandleSecurity();

                // Add a rule that grants the current user the  
                // right to wait on or signal the event.
                EventWaitHandleAccessRule rule = new EventWaitHandleAccessRule(user,
                    EventWaitHandleRights.FullControl,
                    AccessControlType.Allow);
                mSec.AddAccessRule(rule);
             
                return new EventWaitHandle(false, mode, name, out createdNew, mSec);
            }


            // make debugger to step through exceptional driven code
            [DebuggerStepperBoundary]
            [DebuggerStepThrough]
            public static bool TryOpenExistingEventWaitHandle(string name, out EventWaitHandle waitHandle)
            {
                try
                {
                    waitHandle = EventWaitHandle.OpenExisting(name, EventWaitHandleRights.FullControl);
                    return true;
                }
                catch (WaitHandleCannotBeOpenedException)
                {
                    waitHandle = null;
                    return false;
                }
            }
        }

        /// <summary>
        /// Physical mechanism to share data between peers.
        /// </summary>
        internal class MemoryMappedFileChannel : IDisposable
        {
            private int _disposeSignaled;
            private bool Disposed
            {
                get { return _disposeSignaled != 0; }
            }
            private MemoryMappedFile _file;
            private int _bufferSize;
            private BinaryFormatterObjectWriter _serializator = new BinaryFormatterObjectWriter();

            private static MemoryMappedFile attachToMemoryMapedFile(int maxBytes, string name)
            {
                return MemoryMappedFile.CreateOrOpen(name, maxBytes, MemoryMappedFileAccess.ReadWrite);
            }

            public MemoryMappedFileChannel(string name, int bufferSize)
            {
                _file = attachToMemoryMapedFile(bufferSize, name);
                _bufferSize = bufferSize;
            }

            public void Write<TSerializable>(TSerializable @object)
            {
                throwIfDisposed();

                using (var stream = new MemoryStream())
                {
                    _serializator.WriteObject(@object, stream);
                    stream.Seek(0, SeekOrigin.Begin);
                    var data = stream.ToArray();

                    using (Stream view = _file.CreateViewStream(0, _bufferSize, MemoryMappedFileAccess.Write))
                    {
                        
                        view.Write(BitConverter.GetBytes(data.Length), 0, sizeof(int));
                        view.Write(data, 0, data.Length);
                        view.Flush();
                    }
                }

            }
            
            //TODO: clean data after read if needed
            //private void clean(Stream stream, int count)
            //{
            //    for (int i = 0; i < count; i++)
            //        stream.WriteByte(0);
            //    stream.Seek(0, SeekOrigin.Begin);
            //}

            public TSerializable Read<TSerializable>()
            {
                throwIfDisposed();
                using (Stream view = _file.CreateViewStream(0, _bufferSize, MemoryMappedFileAccess.Read))
                {

                    byte[] lengthPrefixData = new byte[sizeof(int)];


                    view.Read(lengthPrefixData, 0, sizeof(int));
                    //TODO: ensure that if lengthPrefix  != 0 then it is really != 0, e.g. when mmf created is it zeroed?
                    int lengthPrefix = BitConverter.ToInt32(lengthPrefixData, 0);

                    if (lengthPrefix == 0)
                        return default(TSerializable);
                    byte[] data = new byte[lengthPrefix];
                    view.Read(data, 0, lengthPrefix);

                    if (data.Length == 0)
                      return default(TSerializable);
                    TSerializable @object = (TSerializable)_serializator.ReadObject(data);
                    return @object;
                }
            }



            public void Dispose()
            {
                if (Interlocked.Exchange(ref _disposeSignaled, 1) != 0)
                    return;
                _file.Dispose();
                _file = null;
            }

            private void throwIfDisposed()
            {
                if (Disposed)
                {
                    throw new ObjectDisposedException(this.ToString());
                }
            }

            ~MemoryMappedFileChannel()
            {
                try
                {
                    Dispose();
                }
                catch { }                
            }
        }

        /// <summary>
        /// Traces/logs execution if turned on. 
        /// </summary>
        internal static class Tracer
        {
            static Tracer()
            {
                Initialize();
            }

            private static void Initialize()
            {
                Source = new TraceSource(typeof(Tracer).Namespace);
                Debug.Assert(Source != null);
            }

            public static TraceSource Source { get; private set; }
        }

        /// <summary>
        /// Reads and writes CLR objects into stream of bytes.
        /// </summary>
        //TODO: use other serializer or modify this to be version independent
        internal class BinaryFormatterObjectWriter
        {
            public object ReadObject(byte[] bytes)
            {
                using (MemoryStream stream = new MemoryStream(bytes))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    return formatter.Deserialize(stream);
                }
            }

            public void WriteObject(object graph, Stream buffer)
            {

                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(buffer, graph);

            }
        }

        [Serializable]
        internal class PeerInfo
        {
            public PeerInfo(string waiter, string responder)
            {
                WaiterName = waiter;
                ResponderName = responder;
            }

            public PeerInfo() { }

            public string WaiterName { get; set; }

  
            public string ResponderName { get; set; }
        }

        internal class LocalPeer:IDisposable
        {
            private int _disposeSignaled;
            private bool Disposed
            {
                get { return _disposeSignaled != 0; }
            }

            public LocalPeer(string eventName)
            {
                PeerId = string.Format("event:{0},class:{1},pid:{2},appdomain:{3}",eventName,this.GetType().Name, Process.GetCurrentProcess().Id , AppDomain.CurrentDomain.Id);
                WaiterName = PeerId +" Waiter";
                Waiter = MutextHelper.AttachToEventWaitHandle(WaiterName, EventResetMode.AutoReset);
                ResponderName = PeerId +" Responder";
                Responder = MutextHelper.AttachToEventWaitHandle(ResponderName, EventResetMode.AutoReset);
          
            }

            public string PeerId { get; private set; }

            public string WaiterName { get; private set; }

            /// <summary>
            /// Is used to wait until somebody raises event.
            /// </summary>
            public EventWaitHandle Waiter { get; private set; }

            public string ResponderName { get; private set; }

            /// <summary>
            /// Is used to notify raiser that  that receiver read out event data
            /// </summary>
            public EventWaitHandle Responder { get; private set; }

            public void Dispose()
            {
                if (Interlocked.Exchange(ref _disposeSignaled, 1) != 0)
                    return;
                Waiter.Dispose();
                Waiter = null;
                Responder.Dispose();
                Responder = null;
            }

            private void throwIfDisposed()
            {
                if (Disposed)
                    throw new ObjectDisposedException(this.ToString());
            }

            ~LocalPeer()
            {
                try
                {
                    Dispose();
                }
                catch { }                
            }
        }

        public LocalInterProcessEvent(string name)
        {
            _name = name;
            _readerWriterLock = MutextHelper.AttachToMutex(_name + "Peer2Peer.ReaderWriter");
            _me = new LocalPeer(name);
            _eventWaitersRegistry = new MemoryMappedFileChannel(_name + "Peer2Peer.Registry", MAX_BYTES);
            _eventData = new MemoryMappedFileChannel(_name + "Peer2Peer.Data", MAX_BYTES);
        }

        public void Subsribe(EventHandler<T> handler)
        {
            throwIfDisposed();
            _handler = handler;
            if (_handler == null)
            {
                _handler = (sender, e) => { };
            }
            if (!_initialized)
            {
                waitOneAndDoSafe(_readerWriterLock, registerPeer);
                Task.Factory.StartNew( startMonitoring);
                _initialized = true;
            }
        }

        ///<remarks>
        /// Handles cases when <see cref="Process"/> killed and <see cref="AbandonedMutexException"/> raised,
        /// Reading shared memory considered pure operation and system is left in valid state and no real crash handling is needed.
        /// </remarks>
        private static void waitOneAndDoSafe(Mutex mutex, Action action)
        {
            try
            {
                mutex.WaitOne();
            }
            catch (AbandonedMutexException ex)
            {
                Tracer.Source.TraceEvent(TraceEventType.Error, 0, ex.ToString());
            }
            try
            {
                action();
            }
            finally
            {
                mutex.ReleaseMutex();
            }
        }

        public void Raise(T eventArgs)
        {
            throwIfDisposed();
            if (!_initialized)
            {
                Subsribe(null);
            }
            Task.Factory.StartNew(x=> raiseInternal(x), eventArgs);
        }

        private void raiseInternal(object state)
        {
            T eventArgs = (T)state;
            waitOneAndDoSafe(_readerWriterLock,
                () =>
                {
                    _eventData.Write(eventArgs);
                    List<PeerInfo> waiters = readPeers();                    
                    var deadWaiters = signalHandlerThreads(waiters);
                    deleteDeadListeners(waiters, deadWaiters);
                });
        }

        private List<PeerInfo> signalHandlerThreads(List<PeerInfo> waiters)
        {
            var deadWaiters = new List<PeerInfo>();
            foreach (PeerInfo waiter in waiters)
            {
                // TODO: optimize in evening in case if waiter is current instanse 
                EventWaitHandle notifier;
                bool alive = MutextHelper.TryOpenExistingEventWaitHandle(waiter.WaiterName, out notifier);
                if (alive)
                {
                    EventWaitHandle responder;
                    MutextHelper.TryOpenExistingEventWaitHandle(waiter.ResponderName, out responder);
                    notifier.Set();
                    try
                    {
                        responder.WaitOne(_peerResponce);
                    }
                    catch (AbandonedMutexException)//peer crashed without Disposing
                    {
                        deadWaiters.Add(waiter);
                    }
                }
                else
                {
                    deadWaiters.Add(waiter); // peer was closed without Disposing
                }
            }
            return deadWaiters;
        }

        private void deleteDeadListeners(List<PeerInfo> waiters, List<PeerInfo> deadWaiters)
        {
            if (deadWaiters.Count != 0)
            {
                List<PeerInfo> liveWaiters = waiters.Except(deadWaiters).ToList();
                writePeers(liveWaiters);
            }
        }

        private void registerPeer()
        {
            List<PeerInfo> waiters = readPeers();
            if (waiters == null) waiters = new List<PeerInfo>();
            waiters.Add(new PeerInfo(_me.WaiterName, _me.ResponderName));
            writePeers(waiters);
        }

        private void writePeers(List<PeerInfo> waiters)
        {
            _eventWaitersRegistry.Write(waiters);
        }

        private List<PeerInfo> readPeers()
        {
            var peers = _eventWaitersRegistry.Read<List<PeerInfo>>();
            if (peers == null)
                peers = new List<PeerInfo>();
            return peers;
        }

        private void startMonitoring()
        {
            while (true)
            {
                _me.Waiter.WaitOne();
                if (Interlocked.CompareExchange(ref _disposeSignaled, 1,1) != 0)
                    return;
                T eventArgs = _eventData.Read<T>();
                _me.Responder.Set(); 
                if (eventArgs != null)
                {
                    Task.Factory.StartNew(() => _handler(this, eventArgs));
                }
                else
                {
                    Tracer.Source.TraceEvent(TraceEventType.Error, 0, "Inter process eventArgs in shared memory is null");
                }
                
            }
        }

        private void throwIfDisposed()
        {
            if (Disposed)
                throw new ObjectDisposedException(this.ToString());
        }


        public void Dispose()
            {
                if (Interlocked.Exchange(ref _disposeSignaled, 1) != 0)
                    return;
                _me.Waiter.Set();
                _me.Dispose();
                _eventWaitersRegistry.Dispose();
                _eventData.Dispose();
                _readerWriterLock.Dispose();
                _me = null;
                _eventWaitersRegistry = null;
                _eventData = null;
                _readerWriterLock = null;
            }

   

            ~LocalInterProcessEvent()
            {
                try
                {
                    Dispose();
                }
                catch { }                
            }


    
    }
}