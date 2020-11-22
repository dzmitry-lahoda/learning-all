using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.ServiceModel;
using System.Text;
using Microsoft.Win32.SafeHandles;
using NDceRpc;
using NDceRpc.ExplicitBytes;
using NDceRpc.Microsoft.Interop;
using NStopwatch;
using managed_entities;

namespace ManagedClient
{



    delegate void req_builder(uint size, out byte[] req_data, out uint sizeP);

    delegate void resp_builder(uint size, byte[] resp_data);
    delegate void makeRequest(uint req_size, req_builder b, resp_builder br, out byte[] result, out uint resultSize);

    internal class Program
    {
        static bool trace = true;

        static bool oneway = false;
        static bool reuse = true;


        const int kb = 1024;
        static uint req_size100 = 100 * kb;
        static uint req_size50 = 50 * kb;
        static uint req_size10 = 10 * kb;
        static uint req_size1 = 1 * kb;
        static uint req_size0_1 = (int)(0.1 * kb);



        static void resp_bytes(uint size, byte[] resp_data)
        {
            //do nothing
            //if (trace)
            //{
            //    unsafe
            //    {
            //        char* rd = (char*)GCHandle.Alloc(resp_data,GCHandleType.Pinned).AddrOfPinnedObject();
            //    }
            //}
            //Console.WriteLine(Encoding.ASCII.GetString(resp_data));

        }



        static void req_bytes(uint size, out byte[] req_data, out uint sizeP)
        {

            req_data = new byte[size];
            var msg = Encoding.ASCII.GetBytes("Hello from");
            Array.Copy(msg, req_data, msg.Length);
            sizeP = size;
        }

        static void req_obj(uint size, out byte[] req_data, out uint sizeP)
        {
            var rs = rss[size];
            sizeP = (uint)rs.getSize();
            rs.toArray(out  req_data, (int)sizeP);

        }

        static void resp_obj(uint size, byte[] resp_data)
        {
            custom_response cr = new custom_response();
            cr.fromArray(resp_data);
        }


        private static string pipe_name = "\\\\.\\pipe\\FastDataServer";
        private static void Main(string[] args)
        {
            req_builder builder = req_bytes;
            resp_builder br = resp_bytes;
            makeRequest call = makePipeRequest;

            for (int i = 0; i < args.Length; i++)
            {
                string o = args[i];
                if (o == "-m")
                {
                    string s = args[i + 1];
                    makeRequest makeWindowsMessageRequest = null;
                    makeRequest makeSharedMemoryRequest = null;
                    switch (s)
                    {
                        case "pipes":
                            call = makePipeRequest;
                            break;
                        case "messaging":
                            call = makeWindowsMessageRequest;
                            break;
                        case "rpc":
                            call = makeRpcRequest;
                            break;
                        case "wcf":
                            call = makeWcfRequest;
                            break;
                        default:
                            call = makeSharedMemoryRequest;
                            break;
                    }

                }
                if (o == "-d")
                {
                    string s = args[i + 1];
                    req_builder req_msg = null;
                    builder = s == "bytes" ? req_bytes : (s == "object" ? req_obj : req_msg);
                    resp_builder resp_msg = null;
                    br = s == "bytes" ? resp_bytes : (s == "object" ? resp_obj : resp_msg);
                }
                if (o == "-r")
                {
                    reuse = true;
                }
                if (o == "-o")
                {
                    oneway = true;
                }
            }

            if (reuse)
            {
                if (call == makePipeRequest)
                {
                    while (!WaitNamedPipe(pipe_name, 1)) { }
                }
                if (call == makeRpcRequest)
                {
                    init_rpc();
                }
                if (call == makeWcfRequest)
                {
                    init_wcf();
                }
            }
            byte[] req;
            uint real_size = 0;
            byte[] response;
            uint resp_size = 0;

            if (!oneway)
            {
                init_reqs(req_size100, 5000, 10);
                init_reqs(req_size50, 2500, 10);
                init_reqs(req_size10, 500, 10);
                init_reqs(req_size1, 50, 10);
                init_reqs(req_size0_1, 5, 10);
            }
            builder(req_size100, out req, out real_size);
            for (int i = 0; i < 5; i++)
            {
                call(req_size100, builder, br, out response, out resp_size);
            }

            var sw = new Reportwatch();
            sw.Lang = ReportwatchLangs.Markdown;
            sw.SetMode("REAL_TIME");



            Console.WriteLine("Client started");


            var msr100 = "Client process requests ~100kb and gets ~1000kb response of server process";
            var msr50 = "Client process requests ~50kb and gets ~500kb response of server process";
            var msr10 = "Client process requests ~10kb and gets ~100kb response  of server process";
            var msr1 = "Client process requests ~1kb and gets ~10kb response  of server process";
            var msr0_1 = "Client process requests ~0.1kb and gets ~1kb response  of server process";

            builder(req_size100, out req, out real_size);
            Console.WriteLine("Request size :" + real_size / kb + " kb");
            for (int i = 0; i < 5; i++)
            {

                sw.Start(msr100);
                call(req_size100, builder, br, out response, out resp_size);
                sw.Stop(msr100);
            }
            Console.WriteLine("Response size was :" + resp_size / kb + " kb");


            builder(req_size50, out req, out real_size);
            Console.WriteLine("Request size :" + real_size / kb + " kb");
            for (int i = 0; i < 10; i++)
            {

                sw.Start(msr50);
                call(req_size50, builder, br, out response, out resp_size);
                sw.Stop(msr50);
            }
            Console.WriteLine("Response size was :" + resp_size / kb + " kb");

            builder(req_size10, out req, out real_size);
            Console.WriteLine("Message size :" + real_size / kb + " kb");
            for (int i = 0; i < 50; i++)
            {

                sw.Start(msr10);
                call(req_size10, builder, br, out response, out resp_size);
                sw.Stop(msr10);
            }
            Console.WriteLine("Response size was :" + resp_size / kb + " kb");

            builder(req_size1, out req, out real_size);
            Console.WriteLine("Message size :" + real_size + " bytes");
            for (int i = 0; i < 500; i++)
            {
                //Sleep(1);
                //BUG: sometimes hangs here when pipes used....
                sw.Start(msr1);
                call(req_size1, builder, br, out response, out resp_size);
                sw.Stop(msr1);
            }
            Console.WriteLine("Response size was :" + resp_size / kb + " kb");


            builder(req_size0_1, out req, out real_size);
            Console.WriteLine("Message size :" + real_size + " bytes");
            for (int i = 0; i < 1000; i++)
            {//BUG: server of shared memory crashes if make number of 10000
                //Sleep(1);
                //BUG: sometimes hangs here when pipes used....
                sw.Start(msr0_1);
                call(req_size0_1, builder, br, out response, out resp_size);
                sw.Stop(msr0_1);
            }
            Console.WriteLine("Response size was :" + resp_size + " bytes");


            builder(req_size100, out req, out real_size);
            Console.WriteLine("Request size :" + real_size / kb + " kb");
            for (int i = 0; i < 5; i++)
            {

                sw.Start(msr100);
                call(req_size100, builder, br, out response, out resp_size);
                sw.Stop(msr100);
            }
            Console.WriteLine("Response size was :" + resp_size / kb + " kb");


            builder(req_size50, out req, out real_size);
            Console.WriteLine("Request size :" + real_size / kb + " kb");
            for (int i = 0; i < 10; i++)
            {

                sw.Start(msr50);
                call(req_size50, builder, br, out response, out resp_size);
                sw.Stop(msr50);
            }
            Console.WriteLine("Response size was :" + resp_size / kb + " kb");

            builder(req_size10, out req, out real_size);
            Console.WriteLine("Message size :" + real_size / kb + " kb");
            for (int i = 0; i < 50; i++)
            {

                sw.Start(msr10);
                call(req_size10, builder, br, out response, out resp_size);
                sw.Stop(msr10);
            }
            Console.WriteLine("Response size was :" + resp_size / kb + " kb");

            builder(req_size1, out req, out real_size);
            Console.WriteLine("Message size :" + real_size + " bytes");
            for (int i = 0; i < 500; i++)
            {
                //Sleep(1);
                //BUG: sometimes hangs here when pipes used....
                sw.Start(msr1);
                call(req_size1, builder, br, out response, out resp_size);
                sw.Stop(msr1);
            }
            Console.WriteLine("Response size was :" + resp_size / kb + " kb");


            builder(req_size0_1, out req, out real_size);
            Console.WriteLine("Message size :" + real_size + " bytes");
            for (int i = 0; i < 1000; i++)
            {//BUG: server of shared memory crashes if make number of 10000
                //Sleep(1);
                //BUG: sometimes hangs here when pipes used....
                sw.Start(msr0_1);
                call(req_size0_1, builder, br, out response, out resp_size);
                sw.Stop(msr0_1);
            }
            Console.WriteLine("Response size was :" + resp_size + " bytes");


            //TODO: make assertion of response

            sw.Report(msr100);
            sw.Report(msr50);
            sw.Report(msr10);
            sw.Report(msr1);
            sw.Report(msr0_1);

            Console.ReadKey();
        }

        static Dictionary<uint, custom_request> rss = new Dictionary<uint, custom_request>();
        static string name = "cool data";
        static string id = "very very cool id";
        static string type = "very very cool type";
        private static void init_reqs(uint reqSize100, int p1, int p2)
        {
            var rs = new custom_request();
            for (int i = 0; i < p1; i++)
            {
                rs.m_ids.Add(id);
            }
            for (int i = 0; i < p2; i++)
            {
                rs.m_types.Add(type);
            }
            rs.m_name = name;
            rss[reqSize100] = rs;

        }

        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern SafeFileHandle CreateFile(
            string lpFileName,
            [MarshalAs(UnmanagedType.U4)] FileAccess dwDesiredAccess,
            [MarshalAs(UnmanagedType.U4)] FileShare dwShareMode,
            IntPtr lpSecurityAttributes,
            [MarshalAs(UnmanagedType.U4)] FileMode dwCreationDisposition,
            [MarshalAs(UnmanagedType.U4)] FileAttributes dwFlagsAndAttributes,
            IntPtr hTemplateFile);

        [DllImport("kernel32.dll")]
        static extern bool WaitNamedPipe(string lpNamedPipeName, uint nTimeOut);
        static SafeFileHandle hPipe = null;

        private static NDceRpc.ExplicitBytes.ExplicitBytesClient _api;
        private static IWcfBytes _wcf;

        [DllImport("kernel32.dll")]
        static extern bool WriteFile(SafeFileHandle hFile, System.IntPtr lpBuffer,
           uint nNumberOfBytesToWrite, out uint lpNumberOfBytesWritten,
            IntPtr lpOverlapped);

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool CloseHandle(SafeHandle hObject);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool ReadFile(SafeHandle hFile, [Out] byte[] lpBuffer,
           uint nNumberOfBytesToRead, out uint lpNumberOfBytesRead, IntPtr lpOverlapped);


        static void makeWcfRequest(uint req_size, req_builder b, resp_builder br, out byte[] result, out uint resultSize)
        {
            if (_wcf == null || !reuse) init_wcf();
            byte[] req_data;
            uint real_size;
            b(req_size, out req_data, out real_size);
            result = _wcf.Execute(req_data);
            resultSize = (uint)result.Length;
        }

        private static void init_wcf()
        {
            var channelFatory = new ChannelFactory<IWcfBytes>(api.Binding);
            _wcf = channelFatory.CreateChannel(new EndpointAddress(api.Uri));
            //_wcf.Execute(new byte[] {});//WCF uses much of runtime upon start, so warup first call
        }


        static void makeRpcRequest(uint req_size, req_builder b, resp_builder br, out byte[] result, out uint resultSize)
        {
            if (_api == null || !reuse) init_rpc();
            byte[] req_data;
            uint real_size;
            b(req_size, out req_data, out real_size);
            result = _api.Execute(req_data);
            resultSize = (uint)result.Length;
        }

        private static void init_rpc()
        {
            var guid = Marshal.GenerateGuidForType(typeof(api));
            _api = new ExplicitBytesClient(guid, new EndpointBindingInfo(RpcProtseq.ncalrpc, null, "FastDataServer"));
        }

        static void makePipeRequest(uint req_size, req_builder b, resp_builder br, out byte[] result, out uint resultSize)
        {
            result = null;
            resultSize = 0;
            if (!reuse)
                hPipe = null;

            if (hPipe == null || hPipe.IsInvalid)
                while (!WaitNamedPipe(pipe_name, 1)) { }

            while (hPipe == null || hPipe.IsInvalid)
            {

                hPipe = CreateFile(
                    pipe_name,
                    FileAccess.ReadWrite,
                    0,
                    IntPtr.Zero,
                    FileMode.Open,
                    FileAttributes.Normal,
                     IntPtr.Zero);

                if (hPipe.IsInvalid)
                    break;
            }


            byte[] request_data = null;
            uint rmessageSize = 0;
            b(req_size, out request_data, out rmessageSize);
            //unsafe
            //{
            //    byte* rd = (byte*)Marshal.AllocHGlobal((int)rmessageSize);
            //}

            byte[] msg = new byte[sizeof(int) + rmessageSize];

            Array.Copy(BitConverter.GetBytes((int)rmessageSize), msg, sizeof(int));
            Array.Copy(request_data, 0, msg, sizeof(int), request_data.Length);//NOTE: additional copy slows down pipes - can be avoided


            uint cbWritten = 0;
            IntPtr o = IntPtr.Zero;

            var write_marshall = GCHandle.Alloc(msg, GCHandleType.Pinned);
            WriteFile(hPipe, write_marshall.AddrOfPinnedObject(), sizeof(int) + rmessageSize, out cbWritten, o);
            write_marshall.Free();

            byte[] response = null;
            int responseSize = 0;
            if (!oneway)
            {

                uint cbRead = 0;
                var sb = new byte[sizeof(int)];

                bool size_was_read = ReadFile(hPipe, sb, sizeof(int), out cbRead, IntPtr.Zero);
                responseSize = (int)BitConverter.ToUInt32(sb, 0);
                response = new byte[responseSize];
                //if (!rSizeRead) Console.WriteLine( cbRead);
                //Console.WriteLine( cbRead);
                //if (GetLastError() == ERROR_MORE_DATA) Console.WriteLine( "MORE DATA");
                //var pin_resp = GCHandle.Alloc(response,GCHandleType.Pinned);
                //var pin_resp_addr = pin_resp.AddrOfPinnedObject();
                //bool was_read = Fast.ReadFile(hPipe,  pin_resp_addr, (uint)responseSize, out cbRead, IntPtr.Zero);
                //result = response;
                //pin_resp.Free();

                bool was_read = ReadFile(hPipe, response, (uint)responseSize, out cbRead, IntPtr.Zero);
                result = response;

                //Console.WriteLine( cbRead);
                //if (!rRead) Console.WriteLine( cbRead);
                //if (GetLastError() == ERROR_MORE_DATA) Console.WriteLine( "MORE DATA");
                //Console.WriteLine( rRead);
            }
            else
            {
                uint cbRead = 0;
                var got_data = new byte[1];
                bool was_read = ReadFile(hPipe, got_data, sizeof(bool), out cbRead, IntPtr.Zero);
            }
            if (!reuse)
                CloseHandle(hPipe);
            if (!oneway)
            {
                br((uint)responseSize, response);
                resultSize = (uint)responseSize;
            }
        }

    }


}












