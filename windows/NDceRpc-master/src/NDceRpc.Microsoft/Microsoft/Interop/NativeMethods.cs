using System;
using System.Runtime.InteropServices;
using System.Text;
using NDceRpc.Microsoft.Interop.Async;

namespace NDceRpc.Microsoft.Interop
{
    /// <summary>
    /// 
    /// </summary>
    public static class NativeMethods
    {


        ///<seealso href="http://msdn.microsoft.com/en-us/library/windows/desktop/aa375771.aspx"/>
        [DllImport("Rpcrt4.dll", EntryPoint = "RpcMgmtSetCancelTimeout", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern RPC_STATUS RpcMgmtSetCancelTimeout(int Seconds);

        ///<seealso href="http://msdn.microsoft.com/en-us/library/windows/desktop/aa375746.aspx"/>
        [DllImport("Rpcrt4.dll", EntryPoint = "RpcMgmtInqComTimeout", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern RPC_STATUS RpcMgmtInqComTimeout(IntPtr Binding, out uint Timeout);

		///<seealso href="https://msdn.microsoft.com/en-us/library/windows/desktop/aa375611.aspx"/>
		[DllImport("Rpcrt4.dll", EntryPoint = "RpcBindingSetOption", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
		public static extern RPC_STATUS RpcBindingSetOption(IntPtr Binding, uint Option,IntPtr OptionValue);

		///<summary>
		/// RPC client processes use RpcBindingInqOption to determine current values of the binding options for a given binding handle.
		/// </summary>
		///<seealso href="https://msdn.microsoft.com/en-us/library/windows/desktop/aa375600.aspx"/>
		[DllImport("Rpcrt4.dll", EntryPoint = "RpcBindingInqOption", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
		public static extern RPC_STATUS RpcBindingInqOption(IntPtr Binding, uint Option,out IntPtr OptionValue);

		// undocumented
		[DllImport("Rpcrt4.dll", EntryPoint = "I_RpcBindingInqSecurityContext", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
		public static extern  RPC_STATUS  I_RpcBindingInqSecurityContext(IntPtr Binding,out IntPtr SecurityContextHandle);

        ///<summary>
        /// This option is ignored for <seealso cref="RpcProtseq.ncalrpc"/>
        /// </summary>
        ///<seealso href="http://msdn.microsoft.com/en-us/library/windows/desktop/aa375779.aspx"/>
        [DllImport("Rpcrt4.dll", EntryPoint = "RpcMgmtSetComTimeout", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern RPC_STATUS RpcMgmtSetComTimeout(IntPtr Binding, uint Timeout);

        /// <summary>
        /// The function registers an object-inquiry function. A null value turns off a previously registered object-inquiry function.
        /// </summary>
        /// <param name="InquiryFn"></param>
        /// <returns></returns>
        [DllImport("Rpcrt4.dll", EntryPoint = "RpcObjectSetInqFn",
CallingConvention = CallingConvention.StdCall,
CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern RPC_STATUS RpcObjectSetInqFn(RPC_OBJECT_INQ_FN InquiryFn);


        ///<summary>
        /// The function sets the object UUID value in a binding handle.
        /// </summary>
        /// <param name="Binding">Server binding into which the ObjectUuid is set.</param>
        /// <param name="ObjectUuid">
        /// Pointer to the UUID of the object serviced by the server specified in the Binding parameter. 
        /// ObjectUuid is a unique identifier of an object to which a remote procedure call can be made.
        /// </param>
        /// <seealso href="http://msdn.microsoft.com/en-us/library/windows/desktop/aa375609.aspx"/>
        [DllImport("Rpcrt4.dll", EntryPoint = "RpcBindingSetObject",
CallingConvention = CallingConvention.StdCall,
CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern RPC_STATUS RpcBindingSetObject(IntPtr Binding, ref Guid ObjectUuid);

        /// <summary>
        /// The RpcObjectSetType function assigns the type of an object.
        /// </summary>
        /// <param name="ObjUuid">Pointer to an object UUID to associate with the type UUID in the TypeUuid parameter.</param>
        /// <param name="TypeUuid">
        /// Pointer to the type UUID of the ObjUuid parameter. 
        /// Specify a parameter value of NULL or a nil UUID to reset the object type to the default association of object UUID/nil-type UUID.
        /// </param>
        /// <returns></returns>
        /// <seealso href="http://msdn.microsoft.com/en-us/library/windows/desktop/aa378427.aspx"/>
        [DllImport("Rpcrt4.dll", EntryPoint = "RpcObjectSetType",
CallingConvention = CallingConvention.StdCall,
CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern RPC_STATUS RpcObjectSetType(ref Guid ObjUuid, ref Guid TypeUuid);

        /// <summary>
        /// The RpcBindingReset function resets a binding handle so that the host is specified but the server on that host is unspecified.
        /// </summary>
        /// <param name="Binding">Server binding handle to reset.</param>
        /// <returns>
        ///        <see cref="RPC_STATUS.RPC_S_OK"/> The call succeeded.
        ///<see cref="RPC_STATUS.RPC_S_INVALID_BINDING"/>  The binding handle was invalid.
        ///<see cref="RPC_STATUS.RPC_S_WRONG_KIND_OF_BINDING"/> This was the wrong kind of binding for the operation.
        /// </returns>
        [DllImport("Rpcrt4.dll", EntryPoint = "RpcBindingReset",
CallingConvention = CallingConvention.StdCall,
CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern RPC_STATUS RpcBindingReset(IntPtr Binding);

        [DllImport("Rpcrt4.dll",
CallingConvention = CallingConvention.StdCall,
CharSet = CharSet.Unicode, SetLastError = true)]
        public extern static RPC_STATUS RpcAsyncInitializeHandle(
            /* PRPC_ASYNC_STATE pAsync*/
                     ref RPC_ASYNC_STATE pAsync,
            /* unsigned int Size*/
         ushort Size
       );

        ///<summary>
        /// Validates the format of the string binding handle and converts
        /// it to a binding handle.
        /// Connection is not done here either.
        /// </summary>
        [DllImport("Rpcrt4.dll", EntryPoint = "RpcBindingFromStringBindingW",
    CallingConvention = CallingConvention.StdCall,
    CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern RPC_STATUS RpcBindingFromStringBinding(String bindingString, out IntPtr lpBinding);

        /// <summary>
        /// The function sets a binding handle's authentication and authorization information.
        /// </summary>
        /// <param name="Binding"></param>
        /// <param name="ServerPrincName"></param>
        /// <param name="AuthnLevel"></param>
        /// <param name="AuthnSvc">Authentication service to use. </param>
        /// <param name="AuthIdentity"></param>
        /// <param name="AuthzService">Authorization service implemented by the server for the interface of interest. </param>
        /// <returns></returns>
        [DllImport("Rpcrt4.dll", EntryPoint = "RpcBindingSetAuthInfoW", CallingConvention = CallingConvention.StdCall,
    CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern RPC_STATUS RpcBindingSetAuthInfo(IntPtr Binding, String ServerPrincName,
                                                             RPC_C_AUTHN_LEVEL AuthnLevel, RPC_C_AUTHN AuthnSvc,
                                                             [In] ref SEC_WINNT_AUTH_IDENTITY AuthIdentity,
                                                             RPC_C_AUTHZ AuthzService);

        [DllImport("Rpcrt4.dll", EntryPoint = "RpcBindingSetAuthInfoW", CallingConvention = CallingConvention.StdCall,
            CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern RPC_STATUS RpcBindingSetAuthInfo2(IntPtr Binding, String ServerPrincName,
                                                              RPC_C_AUTHN_LEVEL AuthnLevel, RPC_C_AUTHN AuthnSvc,
                                                              IntPtr p, RPC_C_AUTHZ AuthzService);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ClientBinding"></param>
        /// <param name="Privs">Returns a pointer to a handle to the privileged information for the client application that made the remote procedure call on the ClientBinding binding handle. For ncalrpc calls, Privs contains a string with the client's principal name.</param>
        /// <param name="ServerPrincName"></param>
        /// <param name="AuthnLevel"></param>
        /// <param name="AuthnSvc"></param>
        /// <param name="AuthzSvc"></param>
        /// <returns></returns>
        [DllImport("Rpcrt4.dll", EntryPoint = "RpcBindingInqAuthClientW", CallingConvention = CallingConvention.StdCall,
            CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern RPC_STATUS RpcBindingInqAuthClient(
            IntPtr ClientBinding,
            ref IntPtr Privs,
            StringBuilder ServerPrincName,
            ref RPC_C_AUTHN_LEVEL AuthnLevel,
            ref RPC_C_AUTHN AuthnSvc,
            ref RPC_C_AUTHZ AuthzSvc);


        [DllImport("Rpcrt4.dll", EntryPoint = "RpcBindingInqAuthInfoW", CallingConvention = CallingConvention.StdCall,
    CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern RPC_STATUS RpcBindingInqAuthInfo(
            IntPtr Binding,
            StringBuilder ServerPrincName,
            ref RPC_C_AUTHN_LEVEL AuthnLevel,
            ref RPC_C_AUTHN AuthnSvc,
               ref     IntPtr AuthIdentity,
            ref RPC_C_AUTHZ AuthzSvc);


        [DllImport("Rpcrt4.dll", EntryPoint = "NdrClientCall2", CallingConvention = CallingConvention.Cdecl,
    CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern IntPtr NdrClientCall2x86(IntPtr pMIDL_STUB_DESC, IntPtr formatString, IntPtr args);

        [DllImport("Rpcrt4.dll", EntryPoint = "NdrClientCall2", CallingConvention = CallingConvention.Cdecl,
            CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern IntPtr NdrClientCall2x64(IntPtr pMIDL_STUB_DESC, IntPtr formatString, IntPtr Handle,
                                                       int DataSize, IntPtr Data, [Out] out int ResponseSize,
                                                       [Out] out IntPtr Response);

        [DllImport("Rpcrt4.dll", EntryPoint = "RpcStringFreeW", CallingConvention = CallingConvention.StdCall,
    CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern RPC_STATUS RpcStringFree(ref IntPtr lpString);

        [DllImport("Rpcrt4.dll", EntryPoint = "RpcBindingFree", CallingConvention = CallingConvention.StdCall,
            CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern RPC_STATUS RpcBindingFree(ref IntPtr lpString);



        [DllImport("Rpcrt4.dll", EntryPoint = "RpcStringBindingComposeW", CallingConvention = CallingConvention.StdCall,
            CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern RPC_STATUS RpcStringBindingCompose(
            String ObjUuid, String ProtSeq, String NetworkAddr, String Endpoint, String Options,
            out IntPtr lpBindingString
            );

        [DllImport("Kernel32.dll", EntryPoint = "LocalFree", SetLastError = true,
    CharSet = CharSet.Unicode, ExactSpelling = true, CallingConvention = CallingConvention.Winapi)]
        public static extern IntPtr LocalFree(IntPtr memHandle);

        /// Return Type: RPC_STATUS->int  
        ///IfSpec: RPC_IF_HANDLE->void*  
        ///MgrTypeUuid: UUID*  
        ///MgrEpv: void*  
        ///Flags: unsigned int  
        ///MaxCalls: unsigned int  
        ///IfCallback: RPC_IF_CALLBACK_FN*  
        [DllImport("Rpcrt4.dll", EntryPoint = "RpcServerRegisterIfEx", CallingConvention = CallingConvention.StdCall,
             CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern int RpcServerRegisterIfEx(IntPtr IfSpec, IntPtr MgrTypeUuid, IntPtr MgrEpv, InterfacRegistrationFlags Flags, uint MaxCalls, ref RPC_IF_CALLBACK_FN IfCallback);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="IfSpec"></param>
        /// <param name="MgrTypeUuid"></param>
        /// <param name="MgrEpv"></param>
        /// <returns></returns>
        [DllImport("Rpcrt4.dll", EntryPoint = "RpcServerRegisterIf", CallingConvention = CallingConvention.StdCall,
    CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern RPC_STATUS RpcServerRegisterIf(IntPtr IfSpec, IntPtr MgrTypeUuid, IntPtr MgrEpv);

        ///IfSpec: RPC_IF_HANDLE->void*     
        ///MgrTypeUuid: UUID*     
        ///MgrEpv: void*     
        ///Flags: unsigned int     
        ///MaxCalls: unsigned int     
        ///MaxRpcSize: unsigned int     
        ///IfCallbackFn: RPC_IF_CALLBACK_FN*     
        [DllImport("rpcrt4.dll", EntryPoint = "RpcServerRegisterIf2", CallingConvention = CallingConvention.StdCall,SetLastError = true)]
        public static extern RPC_STATUS RpcServerRegisterIf2_Marshaled(IntPtr IfSpec, ref Guid MgrTypeUuid, IntPtr MgrEpv, InterfacRegistrationFlags Flags, int MaxCalls, int MaxRpcSize, RPC_IF_CALLBACK_FN IfCallbackFn);

        [DllImport("rpcrt4.dll", EntryPoint = "RpcServerRegisterIf2", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        public static extern RPC_STATUS RpcServerRegisterIf2(IntPtr IfSpec, IntPtr MgrTypeUuid, IntPtr MgrEpv, InterfacRegistrationFlags Flags, int MaxCalls, int MaxRpcSize, IntPtr IfCallbackFn);

        ///<summary>
        /// The  function returns the message text for a status code.
        /// </summary>
        /// <param name="ErrorText">Returns the text corresponding to the error code.</param>
        /// <param name="StatusToConvert">Status code to convert to a text string.</param>
        /// <seealso href="http://msdn.microsoft.com/en-us/library/aa373623.aspx"/>
        [DllImport("Rpcrt4.dll", EntryPoint = "DceErrorInqText", CallingConvention = CallingConvention.StdCall,
CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern RPC_STATUS DceErrorInqText(uint StatusToConvert, out string ErrorText);

        [DllImport("Rpcrt4.dll", EntryPoint = "RpcServerUnregisterIf", CallingConvention = CallingConvention.StdCall,
            CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern RPC_STATUS RpcServerUnregisterIf(IntPtr IfSpec, IntPtr MgrTypeUuid,
                                                            uint WaitForCallsToComplete);

        [DllImport("Rpcrt4.dll", EntryPoint = "RpcServerUseProtseqEpW", CallingConvention = CallingConvention.StdCall,
            CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern RPC_STATUS RpcServerUseProtseqEp(String Protseq, int MaxCalls, String Endpoint,
                                                            IntPtr SecurityDescriptor);

        [DllImport("Rpcrt4.dll", EntryPoint = "NdrServerCall2", CallingConvention = CallingConvention.StdCall,
            CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern void NdrServerCall2(IntPtr ptr);


        [DllImport("Kernel32.dll", EntryPoint = "LocalAlloc", SetLastError = true,
            CharSet = CharSet.Unicode, ExactSpelling = true, CallingConvention = CallingConvention.Winapi)]
        public static extern IntPtr LocalAlloc(UInt32 flags, UInt32 nBytes);

        [DllImport("Rpcrt4.dll", EntryPoint = "RpcServerInqCallAttributesW",
    CallingConvention = CallingConvention.StdCall,
    CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern RPC_STATUS RpcServerInqCallAttributes(IntPtr binding,
                                                                  [In, Out] ref RPC_CALL_ATTRIBUTES_V2 attributes);

        [DllImport("Rpcrt4.dll", EntryPoint = "RpcImpersonateClient", CallingConvention = CallingConvention.StdCall,
            CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern RPC_STATUS RpcImpersonateClient(IntPtr binding);

        [DllImport("Rpcrt4.dll", EntryPoint = "RpcRevertToSelfEx", CallingConvention = CallingConvention.StdCall,
            CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern RPC_STATUS RpcRevertToSelfEx(IntPtr binding);

        [DllImport("Rpcrt4.dll", EntryPoint = "RpcServerListen", CallingConvention = CallingConvention.StdCall,
    CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern RPC_STATUS RpcServerListen(uint MinimumCallThreads, int MaxCalls, uint DontWait);

        [DllImport("Rpcrt4.dll", EntryPoint = "RpcMgmtStopServerListening",
     CallingConvention = CallingConvention.StdCall,
     CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern RPC_STATUS RpcMgmtStopServerListening(IntPtr ignore);

        [DllImport("Rpcrt4.dll", EntryPoint = "RpcMgmtWaitServerListen", CallingConvention = CallingConvention.StdCall,
            CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern RPC_STATUS RpcMgmtWaitServerListen();

        [DllImport("Rpcrt4.dll", EntryPoint = "RpcServerRegisterAuthInfoW",
    CallingConvention = CallingConvention.StdCall,
    CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern RPC_STATUS RpcServerRegisterAuthInfo(String ServerPrincName, uint AuthnSvc, IntPtr GetKeyFn,
                                                                 IntPtr Arg);


    }
}
