using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Pipes;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Security.AccessControl;
using System.Security.Principal;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.Threading;
using IPCLib;
using Microsoft.Win32.SafeHandles;
using WCFServer;


namespace mAdcOW.Server
{
    internal class Program
    {
        private static List<SecurityIdentifier> _aa;
        private static SecurityIdentifier sid_all;
        private static string _uri;

        private static void Main(string[] args)
        {
            Console.WriteLine("Server");



            Thread wcfThread = new Thread(WCFServer);
            wcfThread.Start();

       

            Console.ReadLine();
            Console.WriteLine("done");
    
            wcfThread.Abort();
        }



        static void WCFServer()
        {
            try
            {
                ServiceHost host = new ServiceHost(typeof(RemoteObject), new Uri("net.pipe://localhost/Demo/DoStuff"));

                var pipe = new NetNamedPipeBinding(NetNamedPipeSecurityMode.Transport);
                pipe.MaxReceivedMessageSize = 2147483647;
                pipe.ReaderQuotas.MaxArrayLength = 2147483647;
                pipe.ReaderQuotas.MaxBytesPerRead = 2147483647;
                //var pipe = new AclSecuredNamedPipeBinding();
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

                //sid_all = new SecurityIdentifier(pSD);
                //pipe.AddUserOrGroup(new SecurityIdentifier("S-1-16-1024"));
                //pipe.AddUserOrGroup(new SecurityIdentifier("S-1-16-2048"));
                //pipe.AddUserOrGroup(new SecurityIdentifier("S-1-16-4096"));
                //pipe.AddUserOrGroup(new SecurityIdentifier("S-1-16-8192"));
                //pipe.AddUserOrGroup(sid_all);
                _uri = "net.pipe://localhost/Demo/DoStuff";
                host.AddServiceEndpoint(typeof(IRemoteObject).FullName, pipe, _uri);
               // _aa = pipe._allowedUsers;
                host.Opened += lowerIntegrityLevel;
                Stopwatch sw = new Stopwatch();
                sw.Start();
                host.Open();
                sw.Stop();
                Console.WriteLine("Opening hist time = " + sw.Elapsed.Milliseconds);//~29 milliseconds
                Console.WriteLine("Server Started");
                Console.ReadLine();
                host.Close();

     

            }
            catch (Exception ex)
            {
                
                Console.WriteLine(ex);
            }

        }

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern SafePipeHandle CreateFile(string lpFileName, int dwDesiredAccess, int dwShareMode, IntPtr lpSECURITY_ATTRIBUTES, int dwCreationDisposition, int dwFlagsAndAttributes, IntPtr hTemplateFile);
        public static void PatchPipeSecurity(Uri ﻿serviceUrl)
        {
            string pipeName = ResolveServiceUrlToPipeName﻿﻿﻿﻿﻿﻿(serviceUrl);
            // disassembly from WCF
            int dwFlagsAndAttributes = 1073741824;
            //if (this.includeSecurityIdentity)
                dwFlagsAndAttributes |= 1114112;
            var file = CreateFile(@"\\.\pipe\"+pipeName, -1073741824, 0, IntPtr.Zero, 3, dwFlagsAndAttributes, IntPtr.Zero);
            InterProcessSecurity.SetLowIntegrityLevel(@"\\.\pipe\" + pipeName);
           /// InterProcessSecurity.SetLowIntegrityLevel(file);

            using (var ss﻿tm = new NamedPipeServerStream(@"\\.\pipe\" + pipeName + "a", PipeDirection.InOut, -1,
                      PipeTransmissionMode.Byte, PipeOptions.None, 0, 0, null, System.IO.HandleInheritability.None,
                         PipeAccessRights.ChangePermissions | PipeAccessRights.TakeOwnership | PipeAccessRights.AccessSystemSecurity))
            {
               InterProcessSecurity.SetLowIntegrityLevel(ss﻿tm.SafePipeHandle);
                //PipeSecurity pipeSecurity = ss﻿tm.GetAccessControl();
                ////pipeSecurity.SetSecurityDescriptorSddlForm("S:(ML;;NW;;;LW)");
                ////pipeSecurity.SetAccessRule(new PipeAccessRule());




                //    var permissions_all = PipeAccessRights.ReadPermissions | PipeAccessRights.ReadWrite | PipeAccessRights.CreateNewInstance | PipeAccessRights.FullControl;
                //    PipeAccessRule newAccessRule_s = new PipeAccessRule(new SecurityIdentifier("S-1-16-12288"), permissions_all, AccessControlType.Allow);
                //pipeSecurity.SetAccessRule(newAccessRule_s);


                //PipeAccessRule newAccessRule_all = new PipeAccessRule(sid_all, permissions_all, AccessControlType.Allow);
                //pipeSecurity.SetAccessRule(newAccessRule_all);


                //var rr = pipeSecurity.GetAccessRules(true, true, typeof(SecurityIdentifier)).Cast<PipeAccessRule>().ToArray();
                //Console.WriteLine();
                //foreach (var pipeAccessRule in rr)
                //{
                //    var asd = ToIntArray(pipeAccessRule.PipeAccessRights);
                //    Console.Write(pipeAccessRule.IdentityReference.Value + " ");
                //    foreach (var i in asd)
                //    {

                //        Console.Write(Enum.GetName(typeof(PipeAccessRights), i) + " | ");

                //    }
                //    Console.WriteLine();
                //}





                //foreach (var pipeAccessRule in rr)
                //{

                //    if (pipeAccessRule.AccessControlType == AccessControlType.Deny)
                //    {

                //    }

                //    if (new SecurityIdentifier("S-1-5-2").Equals(pipeAccessRule.IdentityReference))
                //    {
                //        PipeAccessRights permissions2 = pipeAccessRule.PipeAccessRights;
                //        permissions2 |= PipeAccessRights.ReadPermissions | PipeAccessRights.ReadWrite | PipeAccessRights.CreateNewInstance | PipeAccessRights.FullControl;
                //        PipeAccessRule newAccessRule2 = new PipeAccessRule(pipeAccessRule.IdentityReference, permissions2, AccessControlType.Allow);
                //        pipeSecurity.RemoveAccessRuleSpecific(pipeAccessRule);
                //        pipeSecurity.SetAccessRule(newAccessRule2);
                //        continue;

                //    }
                //    //if (new SecurityIdentifier("S-1-18141941858304").Equals(pipeAccessRule.IdentityReference))
                //    {
                //        //PipeAccessRights permissions2 = pipeAccessRule.PipeAccessRights;
                //        //permissions2 |= PipeAccessRights.ReadPermissions | PipeAccessRights.ReadWrite | PipeAccessRights.CreateNewInstance;
                //        //PipeAccessRule newAccessRule2 = new PipeAccessRule(pipeAccessRule.IdentityReference, permissions2, AccessControlType.Allow);
                //        //pipeSecurity.RemoveAccessRuleSpecific(pipeAccessRule);
                //        //pipeSecurity.SetAccessRule(newAccessRule2);
                //        //continue;

                //    }
                //    if (new SecurityIdentifier("S-1-16-4096").Equals(pipeAccessRule.IdentityReference))
                //    {
                //        //pipeSecurity.RemoveAccessRuleSpecific(pipeAccessRule);
                //        //continue;

                //        PipeAccessRights permissions2 = pipeAccessRule.PipeAccessRights;
                //        permissions2 |= PipeAccessRights.ReadPermissions | PipeAccessRights.ReadWrite | PipeAccessRights.CreateNewInstance | PipeAccessRights.FullControl;
                //        PipeAccessRule newAccessRule2 = new PipeAccessRule(pipeAccessRule.IdentityReference, permissions2, AccessControlType.Allow);
                //        pipeSecurity.RemoveAccessRuleSpecific(pipeAccessRule);
                //        pipeSecurity.SetAccessRule(newAccessRule2);
                //        continue;

                //    }
                //    if (new SecurityIdentifier("S-1-16-1024").Equals(pipeAccessRule.IdentityReference))
                //    {
                //        PipeAccessRights permissions2 = pipeAccessRule.PipeAccessRights;
                //        permissions2 |= PipeAccessRights.ReadPermissions | PipeAccessRights.ReadWrite | PipeAccessRights.CreateNewInstance | PipeAccessRights.FullControl;
                //        PipeAccessRule newAccessRule2 = new PipeAccessRule(pipeAccessRule.IdentityReference, permissions2, AccessControlType.Allow);
                //        pipeSecurity.RemoveAccessRuleSpecific(pipeAccessRule);
                //        pipeSecurity.SetAccessRule(newAccessRule2);
                //        continue;

                //    }
                //    if (new SecurityIdentifier("S-1-16-2048").Equals(pipeAccessRule.IdentityReference))
                //    {

                //        PipeAccessRights permissions2 = pipeAccessRule.PipeAccessRights;
                //        permissions2 |= PipeAccessRights.ReadPermissions | PipeAccessRights.ReadWrite | PipeAccessRights.CreateNewInstance | PipeAccessRights.FullControl;
                //        PipeAccessRule newAccessRule2 = new PipeAccessRule(pipeAccessRule.IdentityReference, permissions2, AccessControlType.Allow);
                //        pipeSecurity.RemoveAccessRuleSpecific(pipeAccessRule);
                //        pipeSecurity.SetAccessRule(newAccessRule2);
                //        continue;

                //    }
                //    var u = WindowsIdentity.GetCurrent().User;
                //    if (u.Equals(pipeAccessRule.IdentityReference))
                //    {
                //        PipeAccessRights permissions2 = pipeAccessRule.PipeAccessRights;
                //        permissions2 |= PipeAccessRights.ReadPermissions | PipeAccessRights.ReadWrite | PipeAccessRights.CreateNewInstance | PipeAccessRights.FullControl;
                //        PipeAccessRule newAccessRule2 = new PipeAccessRule(pipeAccessRule.IdentityReference, permissions2, AccessControlType.Allow);
                //        pipeSecurity.RemoveAccessRuleSpecific(pipeAccessRule);
                //        pipeSecurity.SetAccessRule(newAccessRule2);
                //        continue;

                //    }
                //    if (new SecurityIdentifier("S-1-16-12288").Equals(pipeAccessRule.IdentityReference))
                //    {
                //        PipeAccessRights permissions2 = pipeAccessRule.PipeAccessRights;
                //        permissions2 |= PipeAccessRights.ReadPermissions | PipeAccessRights.ReadWrite | PipeAccessRights.CreateNewInstance | PipeAccessRights.FullControl;
                //        PipeAccessRule newAccessRule2 = new PipeAccessRule(pipeAccessRule.IdentityReference, permissions2, AccessControlType.Allow);
                //        pipeSecurity.RemoveAccessRuleSpecific(pipeAccessRule);
                //        pipeSecurity.SetAccessRule(newAccessRule2);
                //        continue;
                //    }
                //    if (new SecurityIdentifier("S-1-16-12288").Equals(pipeAccessRule.IdentityReference))

                //    {
                //        PipeAccessRights permissions2 = pipeAccessRule.PipeAccessRights;
                //        permissions2 |= PipeAccessRights.ReadPermissions | PipeAccessRights.ReadWrite | PipeAccessRights.CreateNewInstance | PipeAccessRights.FullControl;
                //        PipeAccessRule newAccessRule2 = new PipeAccessRule(pipeAccessRule.IdentityReference, permissions2, AccessControlType.Allow);
                //        pipeSecurity.RemoveAccessRuleSpecific(pipeAccessRule);
                //        pipeSecurity.SetAccessRule(newAccessRule2);
                //        continue;
                //    }
                //               if (new SecurityIdentifier("S-1-16-8192").Equals(pipeAccessRule.IdentityReference))

                //    {
                //        PipeAccessRights permissions2 = pipeAccessRule.PipeAccessRights;
                //        permissions2 |= PipeAccessRights.ReadPermissions | PipeAccessRights.ReadWrite | PipeAccessRights.CreateNewInstance | PipeAccessRights.FullControl;
                //        PipeAccessRule newAccessRule2 = new PipeAccessRule(pipeAccessRule.IdentityReference, permissions2, AccessControlType.Allow);
                //        pipeSecurity.RemoveAccessRuleSpecific(pipeAccessRule);
                //        pipeSecurity.SetAccessRule(newAccessRule2);
                //        continue;
                //    }
                //    var asd = Thread.CurrentPrincipal.Identity.Name;
                //    if (new SecurityIdentifier("S-1-5-5-0-417582").Equals(pipeAccessRule.IdentityReference))
                //    {
                //        PipeAccessRights permissions2 = pipeAccessRule.PipeAccessRights;
                //        permissions2 |= PipeAccessRights.ReadPermissions | PipeAccessRights.ReadWrite | PipeAccessRights.CreateNewInstance | PipeAccessRights.FullControl;
                //        PipeAccessRule newAccessRule2 = new PipeAccessRule(pipeAccessRule.IdentityReference, permissions2, AccessControlType.Allow);
                //        pipeSecurity.RemoveAccessRuleSpecific(pipeAccessRule);
                //        pipeSecurity.SetAccessRule(newAccessRule2);
                //        continue;

                //    }

                //               if (!u.Equals(pipeAccessRule.IdentityReference)

                //                   && !(new SecurityIdentifier("S-1-5-5-0-417582").Equals(pipeAccessRule.IdentityReference))
                //                   )
                //               {
                //                   PipeAccessRights permissions2 = pipeAccessRule.PipeAccessRights;
                //                   permissions2 |= PipeAccessRights.ReadPermissions | PipeAccessRights.ReadWrite | PipeAccessRights.CreateNewInstance | PipeAccessRights.FullControl;
                //                   PipeAccessRule newAccessRule2 = new PipeAccessRule(pipeAccessRule.IdentityReference, permissions2, AccessControlType.Allow);
                //                   pipeSecurity.RemoveAccessRuleSpecific(pipeAccessRule);
                //                   pipeSecurity.SetAccessRule(newAccessRule2);
                //                   continue;

                //               }
                //    //PipeAccessRights permissions = pipeAccessRule.PipeAccessRights;
                //    //permissions &= ~PipeAccessRights.FullControl;
                //    //PipeAccessRule newAccessRule = new PipeAccessRule(pipeAccessRule.IdentityReference, permissions, AccessControlType.Allow);
                //    //pipeSecurity.RemoveAccessRuleSpecific(pipeAccessRule);
                //    //pipeSecurity.SetAccessRule(newAccessRule);
                //}


                ////PipeAccessRights permissions = PipeAccessRights.FullControl;
                ////PipeAccessRule newAccessRule = new PipeAccessRule(sid_all, permissions, AccessControlType.Allow);
                ////pipeSecurity.SetAccessRule(newAccessRule);

                //var rr2 = pipeSecurity.GetAccessRules(true, true, typeof(SecurityIdentifier)).Cast<PipeAccessRule>().ToArray();
                //foreach (var securityIdentifier in _aa.Skip(1))
                //{
                //    //pipeSecurity.SetAccessRule(new PipeAccessRule(securityIdentifier.ToString(),PipeAccessRights.FullControl,AccessControlType.Allow));
                //}

                //foreach (PipeAccessRule pipeAccessRule in pipeSecurity.GetAccessRules(true, true, typeof(SecurityIdentifier)))
                //{

                //    //if (AccessControlType.Allow.Equals(pipeAccessRule.AccessControlType))
                //    //{
                //    //    // Remove the CreatorOwner ACE - it is redundant 
                //    //    if (new SecurityIdentifier(WellKnownSidType.CreatorOwnerSid, null).Equals(pipeAccessRule.IdentityReference))
                //    //    {
                //    //        pipeSecurity.RemoveAccessRuleSpecific(pipeAccessRule);
                //    //        continue;
                //    //    }

                //    //    // Remove the FILE_CREATE_PIPE_INSTANCE permission from each ACE except the one 
                //    //    // for the service account itself, which will be the identity executing this code  
                //    //    // Update: Because of the way the permissions defect has been fixed, this code is not sufficient  
                //    //    // in .NET4, where we would need also to grant FILE_CREATE_PIPE_INSTANCE permission to the  
                //    //    // service owner account. Without this the service will not work, and will hang in a busy loop.  
                //    //    if (!WindowsIdentity.GetCurrent().User.Equals(pipeAccessRule.IdentityReference))
                //    //    {
                //    //        PipeAccessRights permissions = pipeAccessRule.PipeAccessRights;
                //    //        permissions &= ~PipeAccessRights.CreateNewInstance;
                //    //        PipeAccessRule newAccessRule
                //    //            = new PipeAccessRule(pipeAccessRule.IdentityReference, permissions, AccessControlType.Allow);
                //    //        pipeSecurity.RemoveAccessRuleSpecific(pipeAccessRule);
                //    //        pipeSecurity.SetAccessRule(newAccessRule);

                //    //    }
                //    //}
                //}
                //ss﻿tm.SetAccessControl(pipeSecurity);
                //PipeSecurity pipeSecurity2 = ss﻿tm.GetAccessControl();
                ////pipeSecurity.SetSecurityDescriptorSddlForm("S:(ML;;NW;;;LW)");
                ////pipeSecurity.SetAccessRule(new PipeAccessRule());
                //var rr3 = pipeSecurity2.GetAccessRules(true, true, typeof(SecurityIdentifier)).Cast<PipeAccessRule>().ToArray();
            }

          //  using (NamedPipeServerStream ss﻿tm = new NamedPipeServerStream(pipeName, PipeDirection.InOut, -1,
          //PipeTransmissionMode.Byte, PipeOptions.None, 0, 0, null, System.IO.HandleInheritability.None,
          //PipeAccessRights.ChangePermissions))
          //  {
          //      PipeSecurity pipeSecurity = ss﻿tm.GetAccessControl();
          // var rr = pipeSecurity.GetAccessRules(true, true, typeof(SecurityIdentifier)).Cast<PipeAccessRule>().ToArray();

          //      foreach (var pipeAccessRule in rr)
          //      {
          //          if (pipeAccessRule.AccessControlType == AccessControlType.Deny)
          //          {

          //          }
          //          if (new SecurityIdentifier("S-1-16-4096").Equals(pipeAccessRule.IdentityReference))
          //          {
          
          //          }
         


          //      }


          //      Console.WriteLine();
          //      var rr3 = pipeSecurity.GetAccessRules(true, true, typeof(SecurityIdentifier)).Cast<PipeAccessRule>().ToArray();
          //      foreach (var pipeAccessRule in rr3)
          //      {
          //          var asd = ToIntArray(pipeAccessRule.PipeAccessRights);
          //          Console.Write(pipeAccessRule.IdentityReference.Value + " ");
          //          foreach (var i in asd)
          //          {

          //              Console.Write(Enum.GetName(typeof(PipeAccessRights), i) + " | ");

          //          }
          //          Console.WriteLine();
          //      }
          //  }
        }

        public static int[] ToIntArray( System.Enum o)
        {
            return o.ToString()
                .Split(new string[] { ", " }, StringSplitOptions.None)
                .Select(i => (int)Enum.Parse(o.GetType(), i))
                .ToArray();
        }
		
        /// <summary>
        /// Make Low Integrity pipes for communication with Low Integrity Processes (e.g. Internet Explorer in Protected Mode with custom addin)
        /// </summary>
        /// <param name="address"></param>
        //TODO: fix 3.5 by reflection or better fix by using dissassembly for several internal WCF classes
        //TODO: fix reflection perfromance - cache meta info
        static void lowerIntegrityLevel(object sender, EventArgs e)
        {           
            //Stopwatch overhead = new Stopwatch();
            //overhead.Start();
            //PatchPipeSecurity(new Uri(_uri));
            if (_uri.Contains("net.pipe://localhost/"))
            {
                var addressParts = _uri.Replace("net.pipe://localhost/", string.Empty).Split('/');
                var segments = (new[] { "net.pipe", "+" }).Concat(addressParts).ToArray();


                var staticEntryName = "System.ServiceModel.Channels.NamedPipeChannelListener, " + typeof(NamedPipeTransportBindingElement).Assembly.FullName;
                var tNamedPipeChannelListener = Type.GetType(staticEntryName, true);
                var ftransportManagerTable = tNamedPipeChannelListener.GetField("transportManagerTable", BindingFlags.NonPublic | BindingFlags.Static);
                var transportManagerTable = ftransportManagerTable.GetValue(tNamedPipeChannelListener);
                var froot = transportManagerTable.GetType().GetField("root", BindingFlags.NonPublic | BindingFlags.Instance);
                var root = froot.GetValue(transportManagerTable);

                var fchildren = root.GetType().GetField("children", BindingFlags.NonPublic | BindingFlags.Instance);


                var segmentsDepth = 0;
                var segmentHierarchyNode = fchildren.GetValue(root);

                while (true)
                {
                    var tSegmentHierarchyNode = segmentHierarchyNode.GetType();

                    var children = tSegmentHierarchyNode.GetProperties().Where(x => x.GetIndexParameters().Any()).Single();
                    var child = children.GetValue(segmentHierarchyNode, new object[] { segments[segmentsDepth] });
                    if (segmentsDepth == segments.Length - 1)
                    {

                        var pData = child.GetType().GetMember("Data", MemberTypes.Property, BindingFlags.Instance | BindingFlags.Public)[0] as PropertyInfo;
                        var data = pData.GetValue(child, null);
                        var tData = data.GetType();

                        var connectionListener = tData.GetField("connectionListener", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(data);

                        var pipeConnectionListener = connectionListener.GetType().GetField("connectionListener", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(connectionListener);
                        var pendingAccepts = pipeConnectionListener.GetType().GetField("pendingAccepts", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(pipeConnectionListener);
                        var firstPendingAccept = pendingAccepts.GetType().GetProperties().Where(x => x.GetIndexParameters().Any()).Single();
                        var pendingAccept = firstPendingAccept.GetValue(pendingAccepts, new object[] { 0 });
                        var pipeHandle = pendingAccept.GetType()
                                                   .GetField("pipeHandle", BindingFlags.NonPublic | BindingFlags.Instance)
                                                   .GetValue(pendingAccept) as SafeHandle;
                        InterProcessSecurity.SetLowIntegrityLevel(pipeHandle); // 2 milliseconds
                        break;

                    }
                    var up = child.GetType().GetField("children", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(child);
                    segmentHierarchyNode = up;
                    segmentsDepth++;
                }
                //overhead.Stop();
                //Console.WriteLine("Host security hack reflection overhead = " + overhead.Elapsed.Milliseconds);
                // 7 milliseconds
            }
        }

        public static string ResolveServiceUrlToPipeName﻿﻿﻿﻿﻿﻿(Uri serviceUrl)
        {
            Type pipeConnectionInitiatorType =
   Type.GetType("System.ServiceModel.Channels.PipeConnectionInitiator, " + typeof(NetNamedPipeBinding).Assembly.FullName, true);
            MethodInfo getPipeNameMethodInfo =
                pipeConnectionInitiatorType.GetMethod("GetPipeName", BindingFlags.NonPublic | BindingFlags.Static);
            string pipename = (string)getPipeNameMethodInfo.Invoke(null, new object[] { serviceUrl });
            return pipename.Replace(@"\\.\pipe\", String.Empty);
        }

        public static T GetProperty1<T>(Type t, object o, string property)
        {
            var p = t.GetMember(property, MemberTypes.Property, BindingFlags.Instance | BindingFlags.NonPublic)[0] as PropertyInfo;
            Debug.Assert(p != null);
            return (T)(p.GetValue(o, null));
        }

         ﻿ public const int LABEL_SECURITY_INFORMATION = 0x00000010;

           public enum SE_OBJECT_TYPE
           {
               SE_UNKNOWN_OBJECT_TYPE = 0,
               SE_FILE_OBJECT,
               SE_SERVICE,
               SE_PRINTER,
               SE_REGISTRY_KEY,
               SE_LMSHARE,
               SE_KERNEL_OBJECT,
               SE_WINDOW_OBJECT,
               SE_DS_OBJECT,
               SE_DS_OBJECT_ALL,
               SE_PROVIDER_DEFINED_OBJECT,
               SE_WMIGUID_OBJECT,
               SE_REGISTRY_WOW64_32KEY
           }

           public static bool SetLowIntegrityLevel(IntPtr hObject)
           {
               bool bResult = false;
               IntPtr pSD = IntPtr.Zero;
               IntPtr pSacl = IntPtr.Zero;
               IntPtr lpbSaclPresent = IntPtr.Zero;
               IntPtr lpbSaclDefaulted = IntPtr.Zero;
               uint securityDescriptorSize = 0;

               if (ConvertStringSecurityDescriptorToSecurityDescriptorW("S:(ML;;NW;;;LW)", 1, ref pSD, ref securityDescriptorSize))
               {
                   if (GetSecurityDescriptorSacl(pSD, out lpbSaclPresent, out pSacl, out lpbSaclDefaulted))
                   {
                       int result = SetSecurityInfo(hObject,
                                                     SE_OBJECT_TYPE.SE_KERNEL_OBJECT,
                                                     LABEL_SECURITY_INFORMATION,
                                                     IntPtr.Zero,
                                                     IntPtr.Zero,
                                                     IntPtr.Zero,
                                                     pSacl);
                       bResult = (result == 0);
                   }
                   LocalFree(pSD);
               }

               return bResult;
           }

           [DllImport("Advapi32.dll", EntryPoint = "SetSecurityInfo")]
           public static extern int SetSecurityInfo(IntPtr hFileMappingObject,
                                                       SE_OBJECT_TYPE objectType,
                                                       Int32 securityInfo,
                                                       IntPtr psidOwner,
                                                       IntPtr psidGroup,
                                                       IntPtr pDacl,
                                                       IntPtr pSacl);

           [DllImport("advapi32.dll", EntryPoint = "GetSecurityDescriptorSacl")]
           [return: MarshalAs(UnmanagedType.Bool)]
           public static extern Boolean GetSecurityDescriptorSacl(
               IntPtr pSecurityDescriptor,
               out IntPtr lpbSaclPresent,
               out IntPtr pSacl,
               out IntPtr lpbSaclDefaulted);

           [DllImport("advapi32.dll", EntryPoint = "ConvertStringSecurityDescriptorToSecurityDescriptorW")]
           [return: MarshalAs(UnmanagedType.Bool)]
           public static extern Boolean ConvertStringSecurityDescriptorToSecurityDescriptorW(
               [MarshalAs(UnmanagedType.LPWStr)] String strSecurityDescriptor,
               UInt32 sDRevision,
               ref IntPtr securityDescriptor,
               ref UInt32 securityDescriptorSize);

           [DllImport("kernel32.dll", EntryPoint = "LocalFree")]
           public static extern UInt32 LocalFree(IntPtr hMem);
    }
}