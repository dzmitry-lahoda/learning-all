using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading;

namespace NDceRpc.ServiceModel.Sample
{
    [ServiceContract(SessionMode = SessionMode.Required, CallbackContract = typeof(ICallbackServiceCallback))]
    [Guid("FC20F4E4-E3F7-4D60-8FBC-94FF85FF835D")]
    public interface ICallbackService
    {
        [OperationContract(IsOneWay = false)]
        void Call();

        [OperationContract(IsOneWay = true)]
        void CallOneWay(CallData message);
    }

    [ServiceContract(SessionMode = SessionMode.Required, CallbackContract = typeof(ICallbackServiceCallback))]
    [Guid("94E81B2D-7E0C-431B-9CF2-EEBA7C15CF37")]
    public interface ICallbackService2:ICallbackService{}

    [DataContract]
    public class CallData
    {
        private DateTime _prop1 = DateTime.Now;
        private Guid _prop2 = Guid.NewGuid();

        [DataMember(Order = 1)]
        public DateTime Prop1
        {
            get { return _prop1; }
            set { _prop1 = value; }
        }

        [DataMember(Order = 2)]
        public Guid Prop2
        {
            get { return _prop2; }
            set { _prop2 = value; }
        }

        public override string ToString()
        {
            return string.Format("Prop1: {0}, Prop2: {1}", Prop1, Prop2);
        }
    }

    public class CallbackService : ICallbackService
    {
        private CallbackData _data;

        public CallbackService(CallbackData data)
        {
            _data = data;
        }

        public void Call()
        {
            var callback = OperationContext.Current.GetCallbackChannel<ICallbackServiceCallback>();
            callback.OnOneWayCallback();
        }

        public void CallOneWay(CallData message)
        {
            var callback = OperationContext.Current.GetCallbackChannel<ICallbackServiceCallback>();
            ConsoleEx.SrvWriteLine(message + " from " + OperationContext.Current.SessionId);
            callback.OnCallback(_data);
        }
    }

    [Guid("0C083E9E-AED0-4335-A446-AA40264B4484")]
    public interface ICallbackServiceCallback
    {
        [OperationContract(IsOneWay = false, IsInitiating = false, IsTerminating = false)]
        void OnCallback(CallbackData message);

        [OperationContract(IsOneWay = true, IsInitiating = false, IsTerminating = false)]
        void OnOneWayCallback();
    }

    public class CallbackServiceCallback : ICallbackServiceCallback
    {
        public ManualResetEvent Wait { get; set; }
        public CallbackData Called { get; set; }

        public CallbackServiceCallback()
        {
            Wait = new ManualResetEvent(false);
        }

        public void OnCallback(CallbackData message)
        {
            Called = message;
            Console.WriteLine(MethodBase.GetCurrentMethod().Name + " was called in " + Process.GetCurrentProcess().Id);
        }



        public void OnOneWayCallback()
        {
            Console.WriteLine(MethodBase.GetCurrentMethod().Name + " was called in " + Process.GetCurrentProcess().Id);
            Wait.Set();
        }
    }

    [DataContract]
    public class CallbackData
    {
        [DataMember(Order = 1)]
        public string Data { get; set; }
    }


    class Program
    {
        static Binding _binding = new LocalBinding();
        private static string _base = "ipc:///local/NDceRpc.ServiceModel.Sample";
        //static Binding _binding = new NetNamedPipeBinding();
        //private static string _base = "net.pipe://localhost/testnamedpipe/NDceRpc.ServiceModel.Sample";
       

        static void Main(string[] args)
        {
            try
            {
                Start(args);
            }
            catch (Exception ex)
            {
                Console.WriteLine("FAILED:");
                Console.WriteLine(ex);
            }
           
            GC.Collect();
            GC.WaitForFullGCComplete();
            GC.Collect();
            Console.ReadKey();

        }
         [System.Runtime.CompilerServices.MethodImpl(MethodImplOptions.NoInlining)]
        private static void Start(string[] args)
        {
             //TODO: enable logging hook
            //ObsoleteErrorHandler.Handler = new RedConsoleHandler();
            var isServer = args.Length < 1;
            if (isServer)
            {
                var typeOfService = typeof (ICallbackService);
                var address = StartServer(typeOfService);
                ThreadPool.QueueUserWorkItem((x) =>
                    {
                        try
                        {
                            Thread.Sleep(5000);
                            StartClient(address);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("FAILED:");
                            Console.WriteLine(ex);
                        }
                       
                    });
                Thread.Sleep(new Random((int)DateTime.Now.Ticks).Next(2000, 3000));
                Process.Start(Assembly.GetExecutingAssembly().CodeBase, address);
                var typeOfService2 = typeof(ICallbackService2);
                var address2 = StartServer(typeOfService2);
                var t = new Thread(() =>
                    {
                        try
                        {
                            StartClient2(address2);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("FAILED:");
                            Console.WriteLine(ex);
                        }

                    });
                t.SetApartmentState(ApartmentState.STA);
                t.Start();
                Process.Start(Assembly.GetExecutingAssembly().CodeBase, address2);
            }
            else
            {
              

                var address = args[0];
                if (address.Contains(typeof(ICallbackService2).Name))
                {
                    StartClient2(address);
                }
                else
                {
                   // Debugger.Launch();
                    StartClient(address);
                }
            }
        }

        [System.Runtime.CompilerServices.MethodImpl(MethodImplOptions.NoInlining)]
        private static string StartServer(Type typeOfService)
        {
            string address = _base + typeOfService.Name;
            var srv = new CallbackService(new CallbackData {Data = "Hello from Server"});
            var server = new ServiceHost(srv, new Uri(address));
            server.AddServiceEndpoint(typeOfService, _binding, address);
            server.Open();
            ConsoleEx.SrvWriteLine("Server started :" + address);
           
            return address;
        }
        [System.Runtime.CompilerServices.MethodImpl(MethodImplOptions.NoInlining)]
        private static void StartClient(string address)
        {

            Console.WriteLine("Server address = " + address);
            var clb = new CallbackServiceCallback();

            Console.WriteLine("Client started");
            var factory = new DuplexChannelFactory<ICallbackService>(new InstanceContext(clb),
                                      _binding);
            var client = factory.CreateChannel(new EndpointAddress(address));
            for (int i = 0; i < 6; i++)
            {

                client.CallOneWay(new CallData());
                client.Call();
                Thread.Sleep(new Random((int)DateTime.Now.Ticks).Next(2000, 3000));
            }
        }
        [System.Runtime.CompilerServices.MethodImpl(MethodImplOptions.NoInlining)]
        private static void StartClient2(string address)
        {

            Console.WriteLine("Server address = " + address);
            var clb = new CallbackServiceCallback();

            Console.WriteLine("Client started");
            var factory = new DuplexChannelFactory<ICallbackService2>(new InstanceContext(clb),
                                       _binding);
            var client = factory.CreateChannel(new EndpointAddress(address));
            for (int i = 0; i < 6; i++)
            {

                client.CallOneWay(new CallData());
                client.Call();
                Thread.Sleep(new Random((int)DateTime.Now.Ticks).Next(2000, 3000));
            }
        }


    }

    public static class ConsoleEx
    {
        public static void SrvWriteLine(string value)
        {

            Console.WriteLine("SERVER-> " +value);

        }
    }

    //internal class RedConsoleHandler : IErrorHander
    //{
    //    public bool Handle(Exception exception)
    //    {
    //        var prev = Console.ForegroundColor;
    //        Console.ForegroundColor = ConsoleColor.Red;
    //        Console.WriteLine(exception.Message);
    //        Console.ForegroundColor = prev;
    //        return true;
    //    }
    //}


}
