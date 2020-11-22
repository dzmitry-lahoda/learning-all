
using System;

using System.Runtime.ConstrainedExecution;
using System.Security;
using System.Security.Permissions;


namespace NDceRpc.ServiceModel
{
    /// <summary>
    /// Extend the <see cref="T:NDceRpc.ServiceModel.ExceptionHandler"/> class to create an exception handler for unhandled exceptions that occur within the NDceRpc runtime.
    /// </summary>
    /// <seealso cref="System.ServiceModel.Dispatcher.ExceptionHandler"/>
    public abstract class ExceptionHandler
    {
        private static readonly ExceptionHandler alwaysHandle = (ExceptionHandler)new ExceptionHandler.AlwaysHandleExceptionHandler();
        private static ExceptionHandler transportExceptionHandler = ExceptionHandler.alwaysHandle;
       
        [SecurityCritical]
        private static ExceptionHandler asynchronousThreadExceptionHandler = ExceptionHandler.alwaysHandle;

        /// <summary>
        /// Gets an instance of <see cref="T:NDceRpc.ServiceModel.ExceptionHandler"/> that handles all exceptions.
        /// </summary>
        /// 
        /// <returns>
        /// An <see cref="T:NDceRpc.ServiceModel.ExceptionHandler"/> object that returns true for all exceptions.
        /// </returns>
        public static ExceptionHandler AlwaysHandle
        {
            get
            {
                return ExceptionHandler.alwaysHandle;
            }
        }

        /// <summary>
        /// Gets or sets the current <see cref="T:NDceRpc.ServiceModel.ExceptionHandler"/> implementation for the application domain.
        /// </summary>
        /// 
        /// <returns>
        /// Assign a custom <see cref="T:NDceRpc.ServiceModel.ExceptionHandler"/> object that receives unhandled exceptions that occur on asynchronous NDceRpc threads.
        /// </returns>
        public static ExceptionHandler AsynchronousThreadExceptionHandler
        {
            [SecurityCritical, ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success), SecurityTreatAsSafe]
            get
            {
                return ExceptionHandler.asynchronousThreadExceptionHandler;
            }
            [SecurityCritical, SecurityTreatAsSafe, SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
            set
            {
                ExceptionHandler.asynchronousThreadExceptionHandler = value;
            }
        }

        /// <summary>
        /// Gets or sets the current transport <see cref="T:NDceRpc.ServiceModel.ExceptionHandler"/> implementation for the application domain.
        /// </summary>
        /// 
        /// <returns>
        /// A custom <see cref="T:NDceRpc.ServiceModel.ExceptionHandler"/> object that receives unhandled exceptions that occur within the global NDceRpc transports.
        /// </returns>
        public static ExceptionHandler TransportExceptionHandler
        {
            get
            {
                return ExceptionHandler.transportExceptionHandler;
            }
            set
            {
                ExceptionHandler.transportExceptionHandler = value;
            }
        }

        static ExceptionHandler()
        {
        }

        /// <summary>
        /// When overridden in a derived class, returns true if the exception has been handled, or false if the exception should be rethrown and the application terminated.
        /// </summary>
        /// 
        /// <returns>
        /// true if the exception has been handled; otherwise, false.
        /// </returns>
        /// <param name="exception">The exception the occurred within the NDceRpc runtime and which may terminate the application.</param>
        public abstract bool HandleException(Exception exception);

        internal static bool HandleTransportExceptionHelper(Exception exception)
        {
            //if (exception == null)
               // throw
            ExceptionHandler exceptionHandler = ExceptionHandler.TransportExceptionHandler;
            if (exceptionHandler == null)
                return false;
            try
            {
                if (!exceptionHandler.HandleException(exception))
                    return false;
            }
            catch (Exception ex)
            {
                //if (IsFatal(ex))
                {
                  //  throw;
                }
                //else
                {
                    //if (ShouldTraceError)
                       RpcTrace.Error(ex);
                    return false;
                }
            }
           // if (ShouldTraceError)
           //     TraceHandledException(exception, TraceEventType.Error);
            return true;
        }

        private class AlwaysHandleExceptionHandler : ExceptionHandler
        {
            public override bool HandleException(Exception exception)
            {
                return true;
            }
        }
    }
}
