using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading;
using NStopwatch;
using NUnit.Framework;

namespace WCF.IntegrationTests
{
    [TestFixture]
    public class StartupTimeTests
    {

        [System.ServiceModel.ServiceContract]
        [Guid("130B1570-30F3-498D-9C10-19B037A746D0")]
        public interface IService2
        {
            [System.ServiceModel.OperationContract(IsOneWay = false)]
            byte[] Execute2(byte[] arg);
        }

        [System.ServiceModel.ServiceBehavior(InstanceContextMode = System.ServiceModel.InstanceContextMode.Single)]
        private class Service2 : IService2
        {
            public byte[] Execute2(byte[] arg)
            {
                return arg;
            }
        }


        [System.ServiceModel.ServiceContract]
        [Guid("FB055ED1-8090-4A66-9EFB-1469A5336420")]
        public interface IService
        {
            [System.ServiceModel.OperationContract(IsOneWay = false)]
            byte[] Execute(byte[] arg);
        }

        [System.ServiceModel.ServiceBehavior(InstanceContextMode = System.ServiceModel.InstanceContextMode.Single)]
        private class Service : IService
        {
            public byte[] Execute(byte[] arg)
            {
                return arg;
            }
        }


        [Test]
        public void NamedPipeCallbck()
        {
            var binding = new NetNamedPipeBinding { MaxConnections = 5 };
            var path = "net.pipe://127.0.0.1/testpipename";
            DoHostWithCallback(binding, path);
            DoHostWithCallback(binding, path);
        }

        private static void DoHostWithCallback(Binding binding, string path)
        {
            var reportWatch = new NStopwatch.Reportwatch();

            DoHostWithCallbackInternal(reportWatch, binding, path);
            reportWatch.Report("ServiceHost ctor");
            reportWatch.Report("AddServiceEndpoint");
            reportWatch.Report("Open");
            reportWatch.Report("ChannelFactory ctor");
            reportWatch.Report("CreateChannel");
            reportWatch.Report("Execute");
            reportWatch.ReportAll();
        }

        private static void DoHostWithCallbackInternal(Reportwatch reportWatch, Binding binding, string path)
        {
            reportWatch.Start("ServiceHost ctor");
            using (var server = new ServiceHost(new CallbackService(), new Uri(path)))
            {
                reportWatch.Stop("ServiceHost ctor");

                reportWatch.Start("AddServiceEndpoint");
                server.AddServiceEndpoint(typeof(ICallbackService), binding, path);
                reportWatch.Stop("AddServiceEndpoint");

                reportWatch.Start("Open");
                server.Open();
                reportWatch.Stop("Open");

                reportWatch.Start("ChannelFactory ctor");
                var context = new InstanceContext(new CallbackServiceCallback());
                using (var channelFactory = new DuplexChannelFactory<ICallbackService>(context, binding))
                {
                    reportWatch.Stop("ChannelFactory ctor");

                    reportWatch.Start("CreateChannel");
                    var client = channelFactory.CreateChannel(new EndpointAddress(path));
                    reportWatch.Stop("CreateChannel");

                    reportWatch.Start("Execute");
                    client.Call();
                    reportWatch.Stop("Execute");
                }
            }
        }

        [Test]
        public void NamedPipeCallbackLoop()
        {
            var binding = new NetNamedPipeBinding() { MaxConnections = 5 };
            var path = "net.pipe://127.0.0.1/" + this.GetType().Name + "_" + MethodBase.GetCurrentMethod().Name;
            var reportWatch = new NStopwatch.Reportwatch();
            for (int i = 0; i < 1000; i++)
            {
                DoHostWithCallbackInternal(reportWatch, binding, path);
            }
            reportWatch.ReportAll();
        }

        [Test]
        public void NamedPipe_byteArray()
        {
            var tester = new FirstCallTester(Console.Out);
            tester.Start("1 Service");
            DoWcfHost();
            tester.Stop();
            tester.Start("2 Service2");
            DoWcfHost2();
            tester.Stop();
            tester.Start("3 Service2");
            DoWcfHost2();
            tester.Report();
        }

        private static void DoWcfHost()
        {
            var reportWatch = new NStopwatch.Reportwatch();

            reportWatch.Start("ServiceHost ctor");
            using (var server = new ServiceHost(new Service(), new Uri("net.pipe://127.0.0.1/testpipename")))
            {
                reportWatch.Stop("ServiceHost ctor");

                reportWatch.Start("AddServiceEndpoint");
                var binding = new NetNamedPipeBinding { MaxConnections = 5 };
                server.AddServiceEndpoint(typeof(IService), binding, "net.pipe://127.0.0.1/testpipename");
                reportWatch.Stop("AddServiceEndpoint");

                reportWatch.Start("Open");
                server.Open();
                reportWatch.Stop("Open");

                reportWatch.Start("ChannelFactory ctor");
                using (var channelFactory = new ChannelFactory<IService>(binding))
                {
                    reportWatch.Stop("ChannelFactory ctor");

                    reportWatch.Start("CreateChannel");
                    var client = channelFactory.CreateChannel(new EndpointAddress("net.pipe://127.0.0.1/testpipename"));
                    reportWatch.Stop("CreateChannel");

                    reportWatch.Start("Execute");
                    client.Execute(new byte[0]);
                    reportWatch.Stop("Execute");
                }
            }
            reportWatch.Report("ServiceHost ctor");
            reportWatch.Report("AddServiceEndpoint");
            reportWatch.Report("Open");
            reportWatch.Report("ChannelFactory ctor");
            reportWatch.Report("CreateChannel");
            reportWatch.Report("Execute");
        }

        private static void DoWcfHost2()
        {
            var reportWatch = new NStopwatch.Reportwatch();

            DoWcfHost2Internal(reportWatch);
            reportWatch.Report("ServiceHost ctor");
            reportWatch.Report("AddServiceEndpoint");
            reportWatch.Report("Open");
            reportWatch.Report("ChannelFactory ctor");
            reportWatch.Report("CreateChannel");
            reportWatch.Report("Execute");
        }

        private static void DoWcfHost2Internal(Reportwatch reportWatch)
        {
            reportWatch.Start("ServiceHost ctor");
            using (var server = new ServiceHost(new Service2(), new Uri("net.pipe://127.0.0.1/testpipename")))
            {
                reportWatch.Stop("ServiceHost ctor");

                reportWatch.Start("AddServiceEndpoint");
                var binding = new NetNamedPipeBinding { MaxConnections = 5 };
                server.AddServiceEndpoint(typeof(IService2), binding, "net.pipe://127.0.0.1/testpipename");
                reportWatch.Stop("AddServiceEndpoint");

                reportWatch.Start("Open");
                server.Open();
                reportWatch.Stop("Open");

                reportWatch.Start("ChannelFactory ctor");
                using (var channelFactory = new ChannelFactory<IService2>(binding))
                {
                    reportWatch.Stop("ChannelFactory ctor");

                    reportWatch.Start("CreateChannel");
                    var client = channelFactory.CreateChannel(new EndpointAddress("net.pipe://127.0.0.1/testpipename"));
                    reportWatch.Stop("CreateChannel");

                    reportWatch.Start("Execute");
                    client.Execute2(new byte[0]);
                    reportWatch.Stop("Execute");
                }
            }
        }
    }
}
