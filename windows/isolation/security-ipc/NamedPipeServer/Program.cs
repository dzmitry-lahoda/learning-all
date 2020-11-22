using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text;
using IPCLib;
using Microsoft.Win32.SafeHandles;
using System.IO.Pipes;

namespace NamedPipeServer
{
    class Program
    {
        [DllImport("advapi32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool OpenProcessToken(IntPtr ProcessHandle,
            UInt32 DesiredAccess, out IntPtr TokenHandle);
        public const UInt32 STANDARD_RIGHTS_REQUIRED = 0x000F0000;
        public const UInt32 STANDARD_RIGHTS_READ = 0x00020000;
        public const UInt32 TOKEN_ASSIGN_PRIMARY = 0x0001;
        public const UInt32 TOKEN_DUPLICATE = 0x0002;
        public const UInt32 TOKEN_IMPERSONATE = 0x0004;
        public const UInt32 TOKEN_QUERY = 0x0008;
        public const UInt32 TOKEN_QUERY_SOURCE = 0x0010;
        public const UInt32 TOKEN_ADJUST_PRIVILEGES = 0x0020;
        public const UInt32 TOKEN_ADJUST_GROUPS = 0x0040;
        public const UInt32 TOKEN_ADJUST_DEFAULT = 0x0080;
        public const UInt32 TOKEN_ADJUST_SESSIONID = 0x0100;
        public const UInt32 TOKEN_READ = (STANDARD_RIGHTS_READ | TOKEN_QUERY);
        public const UInt32 TOKEN_ALL_ACCESS = (STANDARD_RIGHTS_REQUIRED | TOKEN_ASSIGN_PRIMARY |
            TOKEN_DUPLICATE | TOKEN_IMPERSONATE | TOKEN_QUERY | TOKEN_QUERY_SOURCE |
            TOKEN_ADJUST_PRIVILEGES | TOKEN_ADJUST_GROUPS | TOKEN_ADJUST_DEFAULT |
            TOKEN_ADJUST_SESSIONID);
        [DllImport("advapi32.dll", SetLastError = true)]
        static extern bool ConvertStringSidToSid(
                    string StringSid,
                    out IntPtr ptrSid
                    );
        [StructLayout(LayoutKind.Sequential)]
        public struct TOKEN_MANDATORY_LABEL
        {

            public SID_AND_ATTRIBUTES Label;

        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SID_AND_ATTRIBUTES
        {
            public IntPtr Sid;
            public int Attributes;
        }
        [DllImport("advapi32.dll", SetLastError = true)]
        static extern Boolean SetTokenInformation(IntPtr TokenHandle, TOKEN_INFORMATION_CLASS TokenInformationClass,
            ref TOKEN_MANDATORY_LABEL TokenInformation, uint TokenInformationLength);
        const int SECURITY_MANDATORY_UNTRUSTED_RID = (0x00000000);
        const int SECURITY_MANDATORY_LOW_RID = (0x00001000);
        const int SECURITY_MANDATORY_MEDIUM_RID = (0x00002000);
        const int SECURITY_MANDATORY_HIGH_RID = (0x00003000);
        const int SECURITY_MANDATORY_SYSTEM_RID = (0x00004000);
        const int SECURITY_MANDATORY_PROTECTED_PROCESS_RID = (0x00005000);
        const int SE_GROUP_INTEGRITY = (0x00000020);
        [DllImport("advapi32.dll")]
        static extern uint GetLengthSid(IntPtr pSid);

        static void Main(string[] args)
        {
            //SafePipeHandle handle = LowIntegrityPipeFactory.CreateLowIntegrityNamedPipe("NamedPipe\\Test");
            //NamedPipeServerStream pipeServer = new NamedPipeServerStream(PipeDirection.InOut, true, false, handle);
            //pipeServer.BeginWaitForConnection(HandleConnection, pipeServer);


            //LowExecution();


            //IntPtr pSD = IntPtr.Zero;
            //IntPtr pSacl = IntPtr.Zero;
            //IntPtr lpbSaclPresent = IntPtr.Zero;
            //IntPtr lpbSaclDefaulted = IntPtr.Zero;
            //uint securityDescriptorSize = 0;

            //var R = ConvertStringSecurityDescriptorToSecurityDescriptorW("S:(ML;;NW;;;LW)", 1, ref pSD, ref securityDescriptorSize);
            //var R = ConvertStringSecurityDescriptorToSecurityDescriptorW("O:BAG:BAD:(A;;0xb;;;WD)S:(ML;;NX;;;LW)", 1, ref pSD, ref securityDescriptorSize);
            //var R = NativeMethods.ConvertStringSecurityDescriptorToSecurityDescriptor(CreateSddlForPipeSecurity(), 1, ref pSD, ref securityDescriptorSize);

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
            //var sids = new List<SecurityIdentifier>();
            //sids.Add(new SecurityIdentifier("S-1-16-1024"));
            //sids.Add(new SecurityIdentifier("S-1-16-2048"));
            //sids.Add(new SecurityIdentifier("S-1-16-4096"));

            //    sids.Add(new SecurityIdentifier("S-1-16-8192"));
            //sids.Add(new SecurityIdentifier("S-1-16-8192"));
            //sids.Add(new SecurityIdentifier(pSD));




            //        PipeSecurity ps = new PipeSecurity();
            //        PipeAccessRule aceClients = new PipeAccessRule(
            //            new SecurityIdentifier(WellKnownSidType.AuthenticatedUserSid, null),
            //            // or some other group defining the allowed clients
            //           PipeAccessRights.ReadWrite | PipeAccessRights.Synchronize,
            //            AccessControlType.Allow);
            //        PipeAccessRule aceOwner = new PipeAccessRule(
            //            WindowsIdentity.GetCurrent().Owner,
            //            PipeAccessRights.ReadWrite | PipeAccessRights.Synchronize,
            //            AccessControlType.Allow);
            //        var usersPipeAccessRule = new PipeAccessRule(
            //new SecurityIdentifier(WellKnownSidType.BuiltinUsersSid, null),
            //PipeAccessRights.ReadWrite | PipeAccessRights.Synchronize,
            //AccessControlType.Allow
            //);
            //        ps.AddAccessRule(usersPipeAccessRule);

            //        ps.AddAccessRule(aceClients);
            //        ps.AddAccessRule(aceOwner);

            //        // Allow Everyone read and write access to the pipe.
            //        ps.SetAccessRule(new PipeAccessRule("Authenticated Users",
            //            PipeAccessRights.ReadWrite, AccessControlType.Allow));

            //        // Allow the Administrators group full access to the pipe.
            //        ps.SetAccessRule(new PipeAccessRule("Administrators",
            //            PipeAccessRights.FullControl, AccessControlType.Allow));

            //        foreach (var securityIdentifier in sids)
            //        {
            //            PipeAccessRule lowCl = new PipeAccessRule(securityIdentifier, 
            //                PipeAccessRights.ReadWrite | PipeAccessRights.Synchronize,
            //                                                      AccessControlType.Allow);
            //            ps.AddAccessRule(lowCl);
            //        }
            // Fix up the DACL on the pipe before opening the listener instance
            // This won't disturb the SACL containing the mandatory integrity label
            NamedPipeServerStream pipeServer = null;
            try
            {
                pipeServer = new NamedPipeServerStream("NamedPipe/Test", PipeDirection.InOut, NamedPipeServerStream.MaxAllowedServerInstances, PipeTransmissionMode.Byte, PipeOptions.Asynchronous, 0, 0,
                    null, System.IO.HandleInheritability.None,
                    PipeAccessRights.TakeOwnership | PipeAccessRights.ChangePermissions);

                InterProcessSecurity.SetLowIntegrityLevel(pipeServer.SafePipeHandle);
               // InterProcessSecurity.SetLowIntegrityLevel("NamedPipe/Test"); //2
                   //  InterProcessSecurity.SetLowIntegrityLevel( @"\\.\pipe\NamedPipe/Test"); //161
               
                OutPipeSec(pipeServer);
                //LowPipe(pipeServer, sids);

                pipeServer.BeginWaitForConnection(HandleConnection, pipeServer);

                Console.WriteLine("Waiting...");
                Console.ReadLine();
            }
            finally
            {
                if (null != pipeServer) pipeServer.Close();
                pipeServer = null;
            }


        }



        private static void LowPipe(NamedPipeServerStream pipeServer, List<SecurityIdentifier> sids)
        {
            PipeSecurity ps = pipeServer.GetAccessControl();

            PipeAccessRule aceClients = new PipeAccessRule(
                new SecurityIdentifier(WellKnownSidType.AuthenticatedUserSid, null),
                // or some other group defining the allowed clients
                PipeAccessRights.ReadWrite,
                AccessControlType.Allow);
            PipeAccessRule aceOwner = new PipeAccessRule(
                WindowsIdentity.GetCurrent().Owner,
                PipeAccessRights.FullControl,
                AccessControlType.Allow);
            ps.AddAccessRule(aceClients);
            ps.AddAccessRule(aceOwner);


            foreach (var securityIdentifier in sids)
            {
                PipeAccessRule lowCl = new PipeAccessRule(securityIdentifier, PipeAccessRights.FullControl,
                                                          AccessControlType.Allow);
                ps.AddAccessRule(lowCl);
            }

            pipeServer.SetAccessControl(ps);


            OutPipeSec(pipeServer);
        }

        private static void OutPipeSec(NamedPipeServerStream pipeServer)
        {
            var rr =
                pipeServer.GetAccessControl()
                          .GetAccessRules(true, true, typeof(SecurityIdentifier))
                          .Cast<PipeAccessRule>()
                          .ToArray();
            Console.WriteLine();
            foreach (var pipeAccessRule in rr)
            {
                var asd = ToIntArray(pipeAccessRule.PipeAccessRights);
                Console.Write(pipeAccessRule.IdentityReference.Value + " ");
                foreach (var i in asd)
                {
                    Console.Write(Enum.GetName(typeof(PipeAccessRights), i) + " | ");
                }
                Console.WriteLine();
            }
        }

        private static string CreateSddlForPipeSecurity()
        {
            const string LOW_INTEGRITY_LABEL_SACL = "S:(ML;;NW;;;LW)";
            const string EVERYONE_CLIENT_ACE = "(A;;0x12019b;;;WD)";
            const string CALLER_ACE_TEMPLATE = "(A;;0x12019f;;;{0})";

            StringBuilder sb = new StringBuilder();
            sb.Append(LOW_INTEGRITY_LABEL_SACL);
            sb.Append("D:");
            sb.Append(EVERYONE_CLIENT_ACE);
            sb.AppendFormat(CALLER_ACE_TEMPLATE, WindowsIdentity.GetCurrent().Owner.Value);
            return sb.ToString();
        }

        [System.Runtime.InteropServices.DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr GetCurrentThread();

        [System.Runtime.InteropServices.DllImport("advapi32.dll", SetLastError = true)]
        private static extern bool OpenThreadToken(IntPtr ThreadHandle,
        uint DesiredAccess,
        bool OpenAsSelf,
        out IntPtr TokenHandle);

        private const int ERROR_NO_TOKEN = 1008; //From VC\PlatformSDK\Include\WinError.h

        //var ttt = new Thread(() =>
        //    {



        //Thread.CurrentPrincipal = new WindowsPrincipal(WindowsIdentity.Impersonate());

        //var ct = Thread.CurrentThread;


        //bool r1 = OpenThreadToken(ch, TOKEN_READ | TOKEN_IMPERSONATE, true, out pt);
        //if (!r1)
        //{
        //    var dw = System.Runtime.InteropServices.Marshal.GetLastWin32Error();
        //    if (ERROR_NO_TOKEN == dw) // 
        //    {
        //        Console.WriteLine("Not impersonating, cloak is a no-op!", "Impersonation");
        //    }
        //    else
        //    {
        //        Console.WriteLine("Unexpected error 0x" + dw.ToString("x4") + " on OpenThreadToken", "Impersonation");
        //    }
        //    Console.ReadKey();
        //}
        [DllImport("kernel32.dll")]
        static extern IntPtr OpenThread(ThreadAccess dwDesiredAccess, bool bInheritHandle,
           uint dwThreadId);
        [DllImport("kernel32.dll")]
        static extern uint GetCurrentThreadId();
        [Flags]
        public enum ThreadAccess : int
        {
            TERMINATE = (0x0001),
            SUSPEND_RESUME = (0x0002),
            GET_CONTEXT = (0x0008),
            SET_CONTEXT = (0x0010),
            SET_INFORMATION = (0x0020),
            QUERY_INFORMATION = (0x0040),
            SET_THREAD_TOKEN = (0x0080),
            IMPERSONATE = (0x0100),
            DIRECT_IMPERSONATION = (0x0200)
        }
        //private static unsafe void LowExecution()
        //{
        //    Console.WriteLine("Press  Enter to make Low...");
        //    Console.ReadLine();
        //    IntPtr pt;





        //    var cti = GetCurrentThreadId();
        //    var ct =
        //        OpenThread(
        //            ThreadAccess.GET_CONTEXT | ThreadAccess.SET_CONTEXT | ThreadAccess.SET_INFORMATION |
        //            ThreadAccess.QUERY_INFORMATION | ThreadAccess.SET_THREAD_TOKEN | ThreadAccess.IMPERSONATE |
        //            ThreadAccess.DIRECT_IMPERSONATION, true, cti);
        //    bool r1 = OpenThreadToken(ct, TOKEN_READ | TOKEN_IMPERSONATE, true, out pt);
        //    if (!r1)
        //    {
        //        var dw = System.Runtime.InteropServices.Marshal.GetLastWin32Error();
        //        if (ERROR_NO_TOKEN == dw) // 
        //        {
        //            Console.WriteLine("Not impersonating THREAD, cloak is a no-op!", "Impersonation");
        //        }
        //        else
        //        {
        //            Console.WriteLine("Unexpected error 0x" + dw.ToString("x4") + " on OpenThreadToken", "Impersonation");
        //        }
        //        Console.ReadKey();
        //    }

        //    Debug.Assert(OpenProcessToken(Process.GetCurrentProcess().Handle, TOKEN_ALL_ACCESS, out pt));


        //    IntPtr sdd;

        //    Debug.Assert(ConvertStringSidToSid("S-1-16-4096", out sdd));
        //    TOKEN_MANDATORY_LABEL tl = new TOKEN_MANDATORY_LABEL();
        //    tl.Label.Sid = sdd;
        //    tl.Label.Attributes = SE_GROUP_INTEGRITY;
        //    unsafe
        //    {
        //        Debug.Assert(
        //            SetTokenInformation(pt, TOKEN_INFORMATION_CLASS.TokenIntegrityLevel,
        //                                ref tl,
        //                                (uint)(sizeof(TOKEN_MANDATORY_LABEL) + GetLengthSid(sdd))
        //                )
        //            );
        //    }
        //}

        public static int[] ToIntArray(System.Enum o)
        {
            return o.ToString()
                .Split(new string[] { ", " }, StringSplitOptions.None)
                .Select(i => (int)Enum.Parse(o.GetType(), i))
                .ToArray();
        }

        private static void HandleConnection(IAsyncResult ar)
        {
            Console.WriteLine("Received connection");
        }





        [DllImport("Advapi32.dll", EntryPoint = "SetSecurityInfo")]
        public static extern int SetSecurityInfoPtr(IntPtr hFileMappingObject,
                                                    NativeMethods.SE_OBJECT_TYPE objectType,
                                                    Int32 securityInfo,
                                                    IntPtr psidOwner,
                                                    IntPtr psidGroup,
                                                    IntPtr pDacl,
                                                    IntPtr pSacl);






    }

    enum TOKEN_INFORMATION_CLASS
    {
        /// <summary>
        /// The buffer receives a TOKEN_USER structure that contains the user account of the token.
        /// </summary>
        TokenUser = 1,

        /// <summary>
        /// The buffer receives a TOKEN_GROUPS structure that contains the group accounts associated with the token.
        /// </summary>
        TokenGroups,

        /// <summary>
        /// The buffer receives a TOKEN_PRIVILEGES structure that contains the privileges of the token.
        /// </summary>
        TokenPrivileges,

        /// <summary>
        /// The buffer receives a TOKEN_OWNER structure that contains the default owner security identifier (SID) for newly created objects.
        /// </summary>
        TokenOwner,

        /// <summary>
        /// The buffer receives a TOKEN_PRIMARY_GROUP structure that contains the default primary group SID for newly created objects.
        /// </summary>
        TokenPrimaryGroup,

        /// <summary>
        /// The buffer receives a TOKEN_DEFAULT_DACL structure that contains the default DACL for newly created objects.
        /// </summary>
        TokenDefaultDacl,

        /// <summary>
        /// The buffer receives a TOKEN_SOURCE structure that contains the source of the token. TOKEN_QUERY_SOURCE access is needed to retrieve this information.
        /// </summary>
        TokenSource,

        /// <summary>
        /// The buffer receives a TOKEN_TYPE value that indicates whether the token is a primary or impersonation token.
        /// </summary>
        TokenType,

        /// <summary>
        /// The buffer receives a SECURITY_IMPERSONATION_LEVEL value that indicates the impersonation level of the token. If the access token is not an impersonation token, the function fails.
        /// </summary>
        TokenImpersonationLevel,

        /// <summary>
        /// The buffer receives a TOKEN_STATISTICS structure that contains various token statistics.
        /// </summary>
        TokenStatistics,

        /// <summary>
        /// The buffer receives a TOKEN_GROUPS structure that contains the list of restricting SIDs in a restricted token.
        /// </summary>
        TokenRestrictedSids,

        /// <summary>
        /// The buffer receives a DWORD value that indicates the Terminal Services session identifier that is associated with the token. 
        /// </summary>
        TokenSessionId,

        /// <summary>
        /// The buffer receives a TOKEN_GROUPS_AND_PRIVILEGES structure that contains the user SID, the group accounts, the restricted SIDs, and the authentication ID associated with the token.
        /// </summary>
        TokenGroupsAndPrivileges,

        /// <summary>
        /// Reserved.
        /// </summary>
        TokenSessionReference,

        /// <summary>
        /// The buffer receives a DWORD value that is nonzero if the token includes the SANDBOX_INERT flag.
        /// </summary>
        TokenSandBoxInert,

        /// <summary>
        /// Reserved.
        /// </summary>
        TokenAuditPolicy,

        /// <summary>
        /// The buffer receives a TOKEN_ORIGIN value. 
        /// </summary>
        TokenOrigin,

        /// <summary>
        /// The buffer receives a TOKEN_ELEVATION_TYPE value that specifies the elevation level of the token.
        /// </summary>
        TokenElevationType,

        /// <summary>
        /// The buffer receives a TOKEN_LINKED_TOKEN structure that contains a handle to another token that is linked to this token.
        /// </summary>
        TokenLinkedToken,

        /// <summary>
        /// The buffer receives a TOKEN_ELEVATION structure that specifies whether the token is elevated.
        /// </summary>
        TokenElevation,

        /// <summary>
        /// The buffer receives a DWORD value that is nonzero if the token has ever been filtered.
        /// </summary>
        TokenHasRestrictions,

        /// <summary>
        /// The buffer receives a TOKEN_ACCESS_INFORMATION structure that specifies security information contained in the token.
        /// </summary>
        TokenAccessInformation,

        /// <summary>
        /// The buffer receives a DWORD value that is nonzero if virtualization is allowed for the token.
        /// </summary>
        TokenVirtualizationAllowed,

        /// <summary>
        /// The buffer receives a DWORD value that is nonzero if virtualization is enabled for the token.
        /// </summary>
        TokenVirtualizationEnabled,

        /// <summary>
        /// The buffer receives a TOKEN_MANDATORY_LABEL structure that specifies the token's integrity level. 
        /// </summary>
        TokenIntegrityLevel,

        /// <summary>
        /// The buffer receives a DWORD value that is nonzero if the token has the UIAccess flag set.
        /// </summary>
        TokenUIAccess,

        /// <summary>
        /// The buffer receives a TOKEN_MANDATORY_POLICY structure that specifies the token's mandatory integrity policy.
        /// </summary>
        TokenMandatoryPolicy,

        /// <summary>
        /// The buffer receives the token's logon security identifier (SID).
        /// </summary>
        TokenLogonSid,

        /// <summary>
        /// The maximum value for this enumeration
        /// </summary>
        MaxTokenInfoClass
    }
}