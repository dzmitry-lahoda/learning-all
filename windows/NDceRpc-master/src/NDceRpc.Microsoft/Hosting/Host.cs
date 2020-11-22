using System;
using System.Collections.Generic;
using NDceRpc.Microsoft.Interop;

namespace NDceRpc.Native
{
    public static class NativeHost
    {
        
        public static List<Object> _pinned = new List<object>();

        public static NativeServer StartServer(EndpointBindingInfo info, IntPtr dummyPtr)
        {
            return StartServer(info, dummyPtr, IntPtr.Zero,  IntPtr.Zero);
        }

        public static NativeServer StartServer(EndpointBindingInfo info, IntPtr dummyPtr, IntPtr mgrTypeUuid, IntPtr mgrEpv)
        {
            var server = new NativeServer(dummyPtr, mgrTypeUuid, mgrEpv);
            server.AddAuthentication(RPC_C_AUTHN.RPC_C_AUTHN_NONE);
            server.AddProtocol(info.Protseq,info.EndPoint,NativeServer.MAX_CALL_LIMIT);
            server.StartListening();
            return server;
            //TODO: make server isolated of other process services, make it not static as possible
            //    RPC_STATUS status;
            //    status = NDceRpc.Interop.NativeMethods.RpcServerUseProtseqEp(
            //        info.Protseq.ToString(),
            //        NDceRpc.Interop.CONSTANTS.RPC_C_PROTSEQ_MAX_REQS_DEFAULT,
            //        info.EndPoint,
            //        IntPtr.Zero); // No security.

            //    Guard.Assert(status);
            //    RPC_IF_CALLBACK_FN securityCallback = SecurityCallback;

            //    Guid guid = Guid.Empty;
            //    status = NativeMethods.RpcServerRegisterIf2(
            //        dummyPtr,
            //        ref guid, // Use the MIDL generated entry-point vector.
            //        IntPtr.Zero, // Use the MIDL generated entry-point vector.
            //        (uint)InterfacRegistrationFlags.RPC_IF_ALLOW_CALLBACKS_WITH_NO_AUTH, // Forces use of security callback.
            //        CONSTANTS.RPC_C_LISTEN_MAX_CALLS_DEFAULT, // Use default number of concurrent calls.
            //        int.MaxValue, // Infinite max size of incoming data blocks.
            //        ref securityCallback); // Naive security callback.
            //    _pinned.Add(securityCallback);
            //    Guard.Assert(status);


            //    // Start to listen for remote procedure
            //    // calls for all registered interfaces.
            //    // This call will not return until
            //    // RpcMgmtStopServerListening is called.
            //    status = NativeMethods.RpcServerListen(
            //        1, // Recommended minimum number of threads.
            //        CONSTANTS.RPC_C_LISTEN_MAX_CALLS_DEFAULT, // Recommended maximum number of threads.
            //        1); // Start listening now.

            //    if (status == RPC_STATUS.RPC_S_ALREADY_LISTENING) return;
            //    Guard.Assert(status);
        }

        //private static RPC_STATUS SecurityCallback(IntPtr interfaceUuid, IntPtr context)
        //{
        //    return RPC_STATUS.RPC_S_OK;
        //}
    }
}