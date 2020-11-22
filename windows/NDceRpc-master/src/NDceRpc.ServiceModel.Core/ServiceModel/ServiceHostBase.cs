using System;
using System.Collections;
using System.Collections.Generic;


namespace NDceRpc.ServiceModel
{
    /// <summary>
    /// 
    /// </summary>
    public class ServiceHostBase : ICommunicationObject, IDisposable
    {
        private TimeSpan closeTimeout = RpcServiceDefaults.ServiceHostCloseTimeout;

        /// <summary>
        /// Gets or sets the interval of time allowed for the service host to close.
        /// </summary>
        /// 
        /// <returns>
        /// The <see cref="T:System.Timespan"/> that specifies the interval of time allowed for the service host to close.
        /// </returns>
        /// <exception cref="T:System.ArgumentOutOfRangeException">The value, in milliseconds, is less than zero or is larger than <see cref="F:System.Int32.MaxValue"/> (2,147,483,647 or, in hexadecimal notation, 0X7FFFFFFF).</exception><exception cref="T:System.InvalidOperationException">The host is in an <see cref="F:System.ServiceModel.CommunicationState.Opening"/> or <see cref="F:System.ServiceModel.CommunicationState.Closing"/> state and cannot be modified.</exception><exception cref="T:System.ObjectDisposedException">The host is already in a <see cref="F:System.ServiceModel.CommunicationState.Closed"/> state and cannot be modified.</exception><exception cref="T:System.ServiceModel.CommunicationObjectFaultedException">The host is in a <see cref="F:System.ServiceModel.CommunicationState.Faulted"/> state and cannot be modified.</exception>
        public TimeSpan CloseTimeout
        {
            get
            {
                return this.closeTimeout;
            }
            set
            {
                lock (this.ThisLock)
                {
                    this.closeTimeout = value;
                }
            }
        }

        protected object ThisLock
        {
            get { return _thisLock; }

        }

        protected Uri _baseAddress;
    
        protected object _service;
    
        protected bool _disposed;
        protected List<RpcEndpointDispatcher> _endpointDispatchers = new List<RpcEndpointDispatcher>();
        protected ConcurrencyMode _concurrency = ConcurrencyMode.Single;
        private object _thisLock = new object();
      
   
        private static System.Collections.ArrayList _gcRoot = new ArrayList();

        protected ServiceEndpoint CreateEndpoint(Type contractType, Binding binding, string address, Guid uuid)
        {
            var uri = new Uri(address, UriKind.RelativeOrAbsolute);
            if (!uri.IsAbsoluteUri)
            {
                address = _baseAddress + address;
            }
            var endpoint = new ServiceEndpoint(binding, contractType, address, uuid);
            return endpoint;
        }



  

        public void EndClose(IAsyncResult result)
        {
            throw new NotImplementedException();
        }

        public void Open()
        {
            if (State == CommunicationState.Opened)
                throw new InvalidOperationException(
                    "The communication object, System.ServiceModel.ServiceHost, cannot be modified while it is in the Opened state.");
            foreach (var stub in _endpointDispatchers)
            {
                stub.Open(_concurrency);
                //TODO: make GC root disposable
                lock (_gcRoot.SyncRoot)
                {
                    _gcRoot.Add(stub);
                }
                foreach (var behaviour in stub._endpoint.Behaviors)
                {
                    behaviour.ApplyDispatchBehavior(stub._endpoint,stub);
                }
            }
            State = CommunicationState.Opened;
     
           
        }

        public void Open(TimeSpan timeout)
        {
            throw new NotImplementedException();
        }

        public IAsyncResult BeginOpen(AsyncCallback callback, object state)
        {
            throw new NotImplementedException();
        }

        public IAsyncResult BeginOpen(TimeSpan timeout, AsyncCallback callback, object state)
        {
            throw new NotImplementedException();
        }

        public void EndOpen(IAsyncResult result)
        {
            throw new NotImplementedException();
        }

        public CommunicationState State { get; private set; }
        public event EventHandler Closed;
        public event EventHandler Closing;
        public event EventHandler Faulted;
        public event EventHandler Opened;
        public event EventHandler Opening;

        public void Dispose()
        {
            if (_disposed)
            {
                return;
            }
            foreach (var stub in _endpointDispatchers)
            {
                stub.Dispose(CloseTimeout);
            }
            _endpointDispatchers.Clear();
   
            _disposed = true;
        }

        public void Stop()
        {
            Dispose();
        }

        public void Abort()
        {
            Dispose();
        }

        public void Close()
        {
            this.Dispose();
        }

        public void Close(TimeSpan timeout)
        {
            throw new NotImplementedException();
        }

        public IAsyncResult BeginClose(AsyncCallback callback, object state)
        {
            throw new NotImplementedException();
        }

        public IAsyncResult BeginClose(TimeSpan timeout, AsyncCallback callback, object state)
        {
            throw new NotImplementedException();
        }
    }
}