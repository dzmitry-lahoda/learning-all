using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.ServiceModel;
using System.Threading;
using IPCLib;

namespace WCFClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Client");

            Console.WriteLine("Press a key to start WCF sending");
            Console.ReadKey();
            WCFSending(128);
            WCFSending(4 * 1024);
            WCFSending(256 * 1024);
            WCFSending(128);
            WCFSending(4 * 1024);
            WCFSending(256 * 1024);
            Console.ReadKey();
            return;
        }

        public static bool SetLowIntegrityLevel(IntPtr hObject)
        {
            bool bResult = false;
            IntPtr pSD = IntPtr.Zero;
            IntPtr pSacl = IntPtr.Zero;
            IntPtr lpbSaclPresent = IntPtr.Zero;
            IntPtr lpbSaclDefaulted = IntPtr.Zero;
            uint securityDescriptorSize = 0;

            if ( NativeMethods.ConvertStringSecurityDescriptorToSecurityDescriptor("S:(ML;;NW;;;LW)", 1, ref pSD, ref securityDescriptorSize))
            {
                if (NativeMethods.GetSecurityDescriptorSacl(pSD, out lpbSaclPresent, out pSacl, out lpbSaclDefaulted))
                {
                    int result = NativeMethods.SetSecurityInfoPtr(hObject,
                                                  NativeMethods.SE_OBJECT_TYPE.SE_KERNEL_OBJECT,
                                                  NativeMethods.LABEL_SECURITY_INFORMATION,
                                                  IntPtr.Zero,
                                                  IntPtr.Zero,
                                                  IntPtr.Zero,
                                                  pSacl);
                    bResult = (result == 0);
                }
                NativeMethods.LocalFree(pSD);
            }

            return bResult;
        }

    




        private static void WCFSending(int bufferLength)
        {
            try
            {
                var bind = new NetNamedPipeBinding(NetNamedPipeSecurityMode.Transport);
                bind.MaxReceivedMessageSize = 2147483647;

                bind.ReaderQuotas.MaxArrayLength = 2147483647;
                bind.ReaderQuotas.MaxBytesPerRead = 2147483647;
                //var bind = new AclSecuredNamedPipeBinding();
                ////b.AddUserOrGroup(new SecurityIdentifier("S-1-16-1024"));
                //IntPtr pSD = IntPtr.Zero;
                //IntPtr pSacl = IntPtr.Zero;
                //IntPtr lpbSaclPresent = IntPtr.Zero;
                //IntPtr lpbSaclDefaulted = IntPtr.Zero;
                //uint securityDescriptorSize = 0;

                //var R = ConvertStringSecurityDescriptorToSecurityDescriptorW("S:(ML;;NW;;;LW)", 1, ref pSD, ref securityDescriptorSize)
                // ;
                //byte[] bb = new byte[securityDescriptorSize];
                //unsafe
                //{
                //    byte* b = (byte*)pSD;
                //    for (int i = 0; i < securityDescriptorSize; i++)
                //    {
                //        bb[i] = *b;
                //        b++;
                //    }
                //}
                //bind.AddUserOrGroup(new SecurityIdentifier(pSD));
                // bind.AddUserOrGroup(new SecurityIdentifier("S-1-16-1024"));
                //bind.AddUserOrGroup(new SecurityIdentifier("S-1-16-2048"));
                //bind.AddUserOrGroup(new SecurityIdentifier("S-1-16-4096"));
                //bind.AddUserOrGroup(new SecurityIdentifier("S-1-16-8192"));

                var f = new ChannelFactory<IRemoteObject>(bind);
                var c = f.CreateChannel(new EndpointAddress("net.pipe://localhost/Demo/DoStuff"));

                //WCFDemo.RemoteObjectClient client = new RemoteObjectClient("NetNamedPipeBinding_IRemoteObject");
                byte[] buffer = new byte[bufferLength];
                c.ReceiveRBytes(buffer);

                var stopWatch = Stopwatch.StartNew();
                for (int i = 0; i < 50000; i++)
                {
                    c.ReceiveRBytes(buffer);
                }
                stopWatch.Stop();
                Console.WriteLine("Time used: {0}\tPer sec: {1} - {2}", stopWatch.Elapsed, (50000 * 1000) / stopWatch.ElapsedMilliseconds, bufferLength);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

        }
    }
}
