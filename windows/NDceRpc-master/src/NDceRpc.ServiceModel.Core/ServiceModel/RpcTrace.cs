using System;
using System.Diagnostics;

namespace NDceRpc.ServiceModel
{
    internal static class RpcTrace
    {
        private static readonly string Name = typeof(RpcTrace).Namespace;
        public static TraceSource Source;


        static RpcTrace()
        {
            Source = new TraceSource(Name);
        }

        public static void TraceEvent(TraceEventType eventType, string format, params object[] args)
        {
            Source.TraceEvent(TraceEventType.Verbose, 0, format, args);

        }


        public static void Verbose(string message)
        {
            Source.TraceEvent(TraceEventType.Verbose, 0, message);

        }

        public static void Verbose(string message, params object[] arguments)
        {

            Verbose(String.Format(message, arguments));

        }

        public static void Warning(string message)
        {
            Source.TraceEvent(TraceEventType.Warning, 0, message);
        }

        public static void Warning(string message, params object[] arguments)
        {
            Warning(String.Format(message, arguments));
        }

        public static void Error(string message)
        {
            Source.TraceEvent(TraceEventType.Error, 0, message);
        }

        public static void Error(Exception error)
        {
            Error(error.ToString());
        }

        public static void Error(string message, params object[] arguments)
        {
            Error(String.Format(message, arguments));
        }
    }
}