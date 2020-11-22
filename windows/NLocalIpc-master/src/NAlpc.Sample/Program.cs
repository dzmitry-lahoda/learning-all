using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using G;
using Microsoft.Win32.SafeHandles;
using NAlpc;
using NAlpc.Tests;
using NRegFreeCom;
using Constants = NAlpc.Constants;
using PORT_MESSAGE = NAlpc.PORT_MESSAGE;

namespace NAlpc.Sample
{
    /// <summary>
    /// Demonstration program of using LPC facility      
    /// </summary>
    class Program
    {

        static void Main(string[] args)
        {
            Thread t1 = new Thread(NM.ServerThread1);
            t1.Start();

            Thread t2 = new Thread(NM.ClientThread1);
            t2.Start();
            
            Console.Read();
        }
    }
}

////-----------------------------------------------------------------------------
//// Server and client thread for testing small LPC messages





public static class NM
{
    public static unsafe void ClientThread1()
    {
          G.SECURITY_QUALITY_OF_SERVICE SecurityQos;

          UNICODE_STRING PortName;
           int Status = 0;
            IntPtr PortHandle = IntPtr.Zero;
            uint MaxMessageLength = 0;
            int MessageLength = sizeof(TRANSFERRED_MESSAGE);
            int ReplyLength = sizeof(TRANSFERRED_MESSAGE);
            uint PassCount;


        //        //
        //        // Allocate space for message to be transferred through LPC
        //        //

             TRANSFERRED_MESSAGE   LpcMessage = TRANSFERRED_MESSAGE.Create();
             TRANSFERRED_MESSAGE LpcReply = TRANSFERRED_MESSAGE.Create();



                for(PassCount = 0; PassCount < 3; PassCount++)
                {

                    PortName.Length = (ushort)(LpcPortName.Length * 2);
                    PortName.MaximumLength = (ushort)(PortName.Length + 2);
                    PortName.buffer = Marshal.StringToHGlobalUni(LpcPortName);

                    SecurityQos = new G.SECURITY_QUALITY_OF_SERVICE();
                    SecurityQos.Length = (uint)sizeof(G.SECURITY_QUALITY_OF_SERVICE);
                    SecurityQos.ImpersonationLevel = SECURITY_IMPERSONATION_LEVEL.SecurityImpersonation;
                    SecurityQos.EffectiveOnly = 0;
                    SecurityQos.ContextTrackingMode = 1;// SECURITY_DYNAMIC_TRACKING;

        //            _tprintf(_T("Client: Test sending LPC data of size less than 0x%lX bytes ...\n"), MAX_LPC_DATA);
        //            _tprintf(_T("Client: Connecting to port \"%s\" (NtConnectPort) ...\n"), LpcPortName);
                    uint sss = 0;
                    Status = G.NativeMethods.NtConnectPort_NoMarshal(ref PortHandle,
                                           ref PortName,
                                           ref SecurityQos,
                                            IntPtr.Zero,
                                            IntPtr.Zero,
                                           ref MaxMessageLength,
                                             IntPtr.Zero,
                                           ref  sss);
                    AlpcErrorHandler.Check(Status);
        //            _tprintf(_T("Client: NtConnectPort result 0x%08lX\n"), Status);

        //            //
        //            // Initialize the request header, reply header and fill request text
        //            //

                G.PORT_MESSAGE.InitializeMessageHeader(ref LpcMessage.Header, (ushort)MessageLength, 0);
                G.PORT_MESSAGE.InitializeMessageHeader(ref LpcReply.Header, (ushort)ReplyLength, 0);

                    LpcMessage.MessageText = 123.456;
                    

                                if(PassCount == 0)
                                {
                    //                _tprintf(_T("Client: Sending request, reply not required (NtRequestPort)\n"));
                                    LpcMessage.Command = 123;//LPC_COMMAND_REQUEST_NOREPLY;
                                    IntPtr asd = Marshal.AllocHGlobal(sizeof (TRANSFERRED_MESSAGE));
                                    Marshal.StructureToPtr(LpcMessage,asd,false);
                                    Status = G.NativeMethods.NtRequestPort_NoMarshal(PortHandle, asd);
                                    AlpcErrorHandler.Check(Status);
                    //                _tprintf(_T("Client: NtRequestPort result 0x%08lX\n"), Status);
                                   Thread.Sleep(500);
                                }

                    //            //
                    //            // SECOND PASS: Send the request and wait for reply
                    //            //

                    //            if(PassCount == 1)
                    //            {
                    //                _tprintf(_T("Client: Sending request, waiting for reply (NtRequestWaitReplyPort)\n"));
                    //                LpcMessage->Command = LPC_COMMAND_REQUEST_REPLY;
                    //                Status = NtRequestWaitReplyPort(PortHandle, &LpcMessage->Header, &LpcReply->Header);
                    //                _tprintf(_T("Client: NtRequestWaitReplyPort result 0x%08lX\n"), Status);
                    //                Sleep(500);
                    //            }

                    //            //
                    //            // THIRD PASS: Send the Stop command
                    //            //

                    //            if(PassCount == 2)
                    //            {
                    //                _tprintf(_T("Client: Sending STOP request, reply not required (NtRequestPort)\n"));
                    //                LpcMessage->Command = LPC_COMMAND_STOP;
                    //                Status = NtRequestPort(PortHandle, &LpcMessage->Header);
                    //                _tprintf(_T("Client: NtRequestPort result 0x%08lX\n"), Status);
                    //                Sleep(500);
                    //            }

                    //            //
                    //            // Close the connected port
                    //            //

                    //            if(PortHandle != NULL)
                    //            {
                    //                _tprintf(_T("Client: Closing the port (NtClose) \n"));
                    //                Status = NtClose(PortHandle);
                    //                _tprintf(_T("Client: NtClose result 0x%08lX\n"), Status);
                    //            }
                }
           

    }


    [DllImport("kernel32.dll", SetLastError = true, CallingConvention = CallingConvention.Winapi)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool IsWow64Process(
         [In] Microsoft.Win32.SafeHandles.SafeHandleZeroOrMinusOneIsInvalid hProcess,
         [Out, MarshalAs(UnmanagedType.Bool)] out bool wow64Process
         );

    [DllImport("kernel32.dll")]
    static extern Microsoft.Win32.SafeHandles.SafeHandleZeroOrMinusOneIsInvalid GetCurrentProcess();

    static Boolean Is32BitProcessUnderWOW64()
    {

        bool bIsWow64Process = false;
        var p = GetCurrentProcess();
        IsWow64Process(p, out bIsWow64Process);
        p.Dispose();
        return bIsWow64Process;
    }
    private const uint LPC_COMMAND_REQUEST_NOREPLY = 0x00000000;
    private const uint LPC_COMMAND_REQUEST_REPLY = 0x00000001;
    private const uint LPC_COMMAND_STOP = 0x00000002;



    [StructLayout(LayoutKind.Sequential)]
    public struct TRANSFERRED_MESSAGE
    {
        public static TRANSFERRED_MESSAGE Create()
        {
            var msg = new TRANSFERRED_MESSAGE();
            msg.MessageText = 0;
            msg.Header = new G.PORT_MESSAGE();
            return msg;
        }

        public G.PORT_MESSAGE Header;
        public uint Command;
        public double MessageText; 

    }



    [DllImport("ntdll.dll")]
    public static extern void RtlInitUnicodeString(
      ref UNICODE_STRING DestinationString,
      [MarshalAs(UnmanagedType.LPWStr)] string SourceString);
    static string LpcPortName = "\\TestLpcPortName";      // Name of the LPC port. Must be in the form of "\\name"


    private const int SECURITY_DESCRIPTOR_REVISION = 1;

    [DllImport("advapi32.dll", SetLastError = true)]

    public static extern bool InitializeSecurityDescriptor(out SECURITY_DESCRIPTOR pSecurityDescriptor, uint dwRevision);


    [DllImport("advapi32.dll", SetLastError = true, EntryPoint = "InitializeSecurityDescriptor")]
    public static extern bool InitializeSecurityDescriptor_Ptr(out IntPtr pSecurityDescriptor, uint dwRevision);

    [DllImport("advapi32.dll", SetLastError = true, EntryPoint = "SetSecurityDescriptorDacl")]
    static extern bool SetSecurityDescriptorDacl_Ptr(ref IntPtr sd, bool daclPresent, IntPtr dacl, bool daclDefaulted);

    [DllImport("advapi32.dll", EntryPoint = "ConvertStringSecurityDescriptorToSecurityDescriptorW")]
    [return: MarshalAs(UnmanagedType.Bool)]
       public static extern Boolean ConvertStringSecurityDescriptorToSecurityDescriptor(
            [MarshalAs(UnmanagedType.LPWStr)] String strSecurityDescriptor,
            UInt32 sDRevision,
            ref IntPtr securityDescriptor,
            ref UInt32 securityDescriptorSize);

   

    [DllImport("advapi32.dll", SetLastError = true)]
    static extern bool SetSecurityDescriptorDacl(ref SECURITY_DESCRIPTOR sd, bool daclPresent, IntPtr dacl, bool daclDefaulted);
    public static unsafe void ServerThread1()
    {
//        SECURITY_DESCRIPTOR sd = new SECURITY_DESCRIPTOR();
        IntPtr sd = Marshal.AllocHGlobal(sizeof(SECURITY_DESCRIPTOR));
        OBJECT_ATTRIBUTES ObjAttr = new OBJECT_ATTRIBUTES();              // Object attributes for the name
        UNICODE_STRING PortName = new UNICODE_STRING();
        int Status;
        IntPtr LpcPortHandle ;
        IntPtr RequestBuffer = Marshal.AllocHGlobal(sizeof(G.PORT_MESSAGE) + Constants.MAX_LPC_DATA);
        bool WeHaveToStop = false;
        int nError;
        TRANSFERRED_MESSAGE tm = TRANSFERRED_MESSAGE.Create();
     
            //
            // Initialize security descriptor that will be set to
            // "Everyone has the full access"
            //


        if (!InitializeSecurityDescriptor_Ptr(out sd, SECURITY_DESCRIPTOR_REVISION))
            {
                nError = Marshal.GetLastWin32Error();
                if (nError != Constants.S_OK) throw new Win32Exception(nError);
            }

            //
            // Set the empty DACL to the security descriptor
            //

        if (!SetSecurityDescriptorDacl_Ptr(ref sd, true, IntPtr.Zero, false))
            {
                nError = Marshal.GetLastWin32Error();
                if (nError != Constants.S_OK) throw new Win32Exception(nError);
            }

        uint securityDescriptorSize = 0;
        ConvertStringSecurityDescriptorToSecurityDescriptor("S:(ML;;NW;;;LW)", 1, ref sd, ref securityDescriptorSize);

            //
            // Initialize attributes for the port and create it
            //

            //RtlInitUnicodeString(ref PortName, LpcPortName);
        PortName.Length = (ushort)(LpcPortName.Length * 2);
        PortName.MaximumLength = (ushort)(PortName.Length + 2);
        PortName.buffer = Marshal.StringToHGlobalUni(LpcPortName);

        NRegFreeCom.AssemblySystem a = new AssemblySystem();
        //var qwe = LoadLibrary(@"I:\src\NLocalIpc\build\Debug\rpcrtex.dll");
       // var aaaaa = NRegFreeCom.Interop.NativeMethods.GetProcAddress(qwe, "secddd");
        var aa = a.LoadFrom(@"I:\src\NLocalIpc\build\Debug\rpcrtex.dll");
        var aaa = aa.GetDelegate<secddd>();
        sd = aaa();

        OBJECT_ATTRIBUTES.InitializeObjectAttributes_Ptr(ref ObjAttr, ref PortName, 0, IntPtr.Zero, sd);

            Console.WriteLine("Server: Creating LPC port \"{0}\" (NtCreatePort) ...\n", LpcPortName);
       var msg = (uint) (sizeof (PORT_MESSAGE) + G.Constants.MAX_LPC_DATA);
         
            Status = G.NativeMethods.NtCreatePort(out LpcPortHandle,
                ref ObjAttr,
                100,
               msg,
                0);
            Console.WriteLine("Server: NtCreatePort result {0:x8}", Status);


            AlpcErrorHandler.Check(Status);

            //
            // Process all incoming LPC messages
            //
        uint qwe = 0;
            while (WeHaveToStop == false)
            {
                //PTRANSFERRED_MESSAGE
                IntPtr LpcMessage = IntPtr.Zero;
                IntPtr ServerHandle = IntPtr.Zero;

                //
                // Create the data buffer for the request
                //

                LpcMessage = RequestBuffer;
                Console.WriteLine("Server: ------------- Begin loop ----------------------\n {0}", Status);

                //
                // Listen to the port. This call is blocking, and cannot be interrupted,
                // even if the handle is closed. The only way how to stop the block is to send
                // an LPC request which will be recognized by server thread as "Stop" command
                //
                AlpcErrorHandler.Check(Status);

                Console.WriteLine("Server: Listening to LPC port (NtListenPort) ... {0} \n", LpcPortName);


                Status = G.NativeMethods.NtListenPort(LpcPortHandle,
                    ref tm.Header);
                Console.WriteLine("Server: NtListenPort result {0:x8}\n", Status);

                //
                // Accept the port connection
                //

                AlpcErrorHandler.Check(Status);

                Console.WriteLine("Server: Accepting LPC connection (NtAcceptConnectPort) ...{0:}\n", LpcPortName);
                IntPtr NULL = IntPtr.Zero;
                G.PORT_VIEW pv = new G.PORT_VIEW();
                G.REMOTE_PORT_VIEW cv = new G.REMOTE_PORT_VIEW();
              
                AlpcPortHandle sh;
                Status = G.NativeMethods.NtAcceptConnectPort_Ptr(out ServerHandle,
                    IntPtr.Zero,
                    ref tm.Header,
                    1,
                    NULL,
                    NULL);
                Console.WriteLine("Server: NtAcceptConnectPort result {0:x8}\n", Status);



                //
                // Complete the connection
                //

                AlpcErrorHandler.Check(Status);

                Console.WriteLine("Server: Completing LPC connection (NtCompleteConnectPort) ...{0:}\n", LpcPortName);

                Status = G.NativeMethods.NtCompleteConnectPort(ServerHandle);
                Console.WriteLine("Server: NtCompleteConnectPort result {0:x8}\n", Status);
                


                //
                // Now accept the data request coming from the port.
                //

                AlpcErrorHandler.Check(Status);
                 // lpcMsg = (TRANSFERRED_MESSAGE)Marshal.PtrToStructure(LpcMessage, typeof(TRANSFERRED_MESSAGE));
              //  mh = lpcMsg.Header;
                Console.WriteLine("Server: Receiving LPC data (NtReplyWaitReceivePort) ...{0:}\n", LpcPortName);


                IntPtr asd = Marshal.AllocHGlobal(sizeof (TRANSFERRED_MESSAGE));
                Marshal.StructureToPtr(tm, asd,false);

                Status = G.NativeMethods.NtReplyWaitReceivePort_NoMarshal(ServerHandle,
                        ref NULL,
                        IntPtr.Zero,
                       asd);
                    Console.WriteLine("Server: NtReplyWaitReceivePort result {0:x8}\n", Status);
                var wqe = Marshal.PtrToStructure(asd, typeof (TRANSFERRED_MESSAGE));
              //  var qwe = Marshal.PtrToStringUni(tm.MessageText);
                //
                // Get the data sent by the client 
                //
   
                AlpcErrorHandler.Check(Status);



                    // If a request has been received, answer to it.
                switch (tm.Command)
                {
                    case LPC_COMMAND_REQUEST_NOREPLY:

                        Console.WriteLine("Server: Received request {0}\n", tm.MessageText);

                        break; // Nothing more to do

                    case LPC_COMMAND_REQUEST_REPLY:
                        //_tprintf(_T("Server: Received request \"%s\"\n"), LpcMessage->MessageText);
                        //_tprintf(_T("Server: Sending reply (NtReplyPort) ...\n"), LpcPortName);
                        Status = G.NativeMethods.NtReplyPort(LpcPortHandle, ref tm.Header);
                        //_tprintf(_T("Server: NtReplyPort result 0x%08lX\n"), Status);
                        break;

                    case LPC_COMMAND_STOP: // Stop the work
                        //_tprintf(_T("Server: Stopping ...\n"));
                        WeHaveToStop = false;
                        break;
                }

          
                //
                // Close the server connection handle
                //

                if (ServerHandle != NULL)
                {
                   // _tprintf(_T("Server: Closing the request handle (NtClose) ...\n"), LpcPortName);
                   Status = NAlpc.NativeMethods.NtClose(ServerHandle);
                    //_tprintf(_T("Server: NtClose result 0x%08lX\n"), Status);
                }

                //_tprintf(_T("Server: ------------- End loop ----------------------\n"), Status);
            }



        return ;
    }


    [DllImport("kernel32.dll", EntryPoint = "LoadLibrary", SetLastError = true)]
    public static extern IntPtr LoadLibrary([MarshalAs(UnmanagedType.LPStr)] string lpLibFileName);
}




[UnmanagedFunctionPointer(CallingConvention.StdCall)]
public delegate IntPtr secddd();



////-----------------------------------------------------------------------------
//// main

//int _tmain(void)
//{
//    HANDLE hThread[2];
//    DWORD dwThreadId;

//    //
//    // Save the handle of the main heap
//    //

//    g_hHeap = GetProcessHeap();

//    //
//    // Note for 64-bit Windows: Functions that use PORT_MESSAGE structure
//    // as one of the parameters DO NOT WORK under WoW64. The reason is that
//    // the layer between 32-bit NTDLL and 64-bit NTDLL does not translate
//    // the structure to its 64-bit layout and most of the functions
//    // fail with STATUS_INVALID_PARAMETER (0xC000000D)
//    //

//    if(Is32BitProcessUnderWOW64())
//    {
//        _tprintf(_T("WARNING: You are running 32-bit version of the example under 64-bit Windows.\n")
//                 _T("This is not supported and will not work.\n"));
//        _getch();
//    }

//    //
//    // Run both threads testing small LPC messages
//    //

//    hThread[0] = CreateThread(NULL, 0, ServerThread1, NULL, 0, &dwThreadId);
//    hThread[1] = CreateThread(NULL, 0, ClientThread1, NULL, 0, &dwThreadId);
//    WaitForMultipleObjects(2, hThread, TRUE, INFINITE);
//    CloseHandle(hThread[0]);
//    CloseHandle(hThread[1]);

//    //
//    // Run both threads testing large LPC messages
//    //
///*
//    hThread[0] = CreateThread(NULL, 0, ServerThread2, NULL, 0, &dwThreadId);
//    hThread[1] = CreateThread(NULL, 0, ClientThread2, NULL, 0, &dwThreadId);
//    WaitForMultipleObjects(2, hThread, TRUE, INFINITE);
//    CloseHandle(hThread[0]);
//    CloseHandle(hThread[1]);
//*/
//    _getch();
//    return 0;
//}

