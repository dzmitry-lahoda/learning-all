using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using NDceRpc.Microsoft.Interop;

namespace NDceRpc.ExplicitBytes
{
    public class ExplicitBytesClient :Client, IExplicitBytesClient
    {
        /// <summary> The interface Id the client is connected to </summary>
        public readonly Guid IID;

        /// <summary>
        /// Connects to the provided server interface with the given protocol and server:endpoint
        /// </summary>
        public ExplicitBytesClient(Guid iid, EndpointBindingInfo endpointBindingInfo):base(endpointBindingInfo)
        {
          
            IID = iid;
   
        }


        /// <summary>
        /// Sends a message as an array of bytes and retrieves the response from the server, if
        /// AuthenticateAs() has not been called, the client will authenticate as Anonymous.
        /// </summary>
        public byte[] Execute(byte[] input)
        {
            if (!_authenticated)
            {
                RpcTrace.Warning("AuthenticateAs was not called, assuming Anonymous.");
                AuthenticateAs(Anonymous);
            }
            RpcTrace.Verbose("ExplicitBytesExecute(byte[{0}])", input.Length);
            return InvokeRpc(_handle, IID, input);
       
        }

     
        [MethodImpl(MethodImplOptions.NoInlining | (MethodImplOptions)64 /* MethodImplOptions.NoOptimization undefined in 2.0 */)]
        private static byte[] InvokeRpc(RpcHandle handle, Guid iid, byte[] input)
        {
            RpcTrace.Verbose("InvokeRpc on {0}, sending {1} bytes", handle.Handle, input.Length);
            Ptr<MIDL_STUB_DESC> pStub;
            if (!handle.GetPtr(out pStub))
            {
                pStub =
                    handle.CreatePtr(new MIDL_STUB_DESC(handle, handle.Pin(ClientInterfaceFactory.CreatExplicitBytesClient(iid)),
                                                        RpcRuntime.TYPE_FORMAT,
                                                        false));
            }
            int szResponse = 0;
            IntPtr response, result;
            
            using (Ptr<byte[]> pInputBuffer = new Ptr<byte[]>(input))
            {
                if (RpcRuntime.Is64BitProcess)
                {
                    try
                    {
                        result = NativeMethods.NdrClientCall2x64(pStub.Handle, RpcRuntime.FUNC_FORMAT_PTR.Handle, handle.Handle,
                                                   input.Length,
                                                   pInputBuffer.Handle, out szResponse, out response);
                    }
                    catch (SEHException ex)
                    {
                        RpcTrace.Error(ex);
                        Guard.Assert(ex.ErrorCode);
                        throw;
                    }
                }
                else
                {
                    using (Ptr<Int32[]> pStack32 = new Ptr<Int32[]>(new Int32[10]))
                    {
                        pStack32.Data[0] = handle.Handle.ToInt32();
                        pStack32.Data[1] = input.Length;
                        pStack32.Data[2] = pInputBuffer.Handle.ToInt32();
                        pStack32.Data[3] = pStack32.Handle.ToInt32() + (sizeof (int)*6);
                        pStack32.Data[4] = pStack32.Handle.ToInt32() + (sizeof (int)*8);
                        pStack32.Data[5] = 0; //reserved
                        pStack32.Data[6] = 0; //output: int dwSizeResponse
                        pStack32.Data[8] = 0; //output: byte* lpResponse

                        try
                        {
                            result = NativeMethods.NdrClientCall2x86(pStub.Handle, RpcRuntime.FUNC_FORMAT_PTR.Handle, pStack32.Handle);
                        }
                        catch (SEHException ex)
                        {
                         
                            RpcTrace.Error(ex);
                            Guard.Assert(ex.ErrorCode);
                            throw;
                        }

                        szResponse = pStack32.Data[6];
                        response = new IntPtr(pStack32.Data[8]);
                    }
                }
                GC.KeepAlive(pInputBuffer);
            }

            // on local machine
            // if 32 bit server just throws Exception in server side handler 
            // then 32 bits client can see "-2147467259" debug view of pointer result
            // then 64 bits client can see "2147500037" debug view of pointer result
            // then 32 bits client can do ToInt32 and things work fine
            // then 64 bits client can do ToInt32 and get arithmetic overflow exception
            // hence using ToInt64 for 64 bits client
            if (IntPtr.Size == 8)
            {
                Guard.Assert(result.ToInt64());    
            }
            else
            {
                Guard.Assert(result.ToInt32());   
            }
            
            RpcTrace.Verbose("InvokeRpc.InvokeRpc response on {0}, received {1} bytes", handle.Handle, szResponse);
            byte[] output = new byte[szResponse];
            if (szResponse > 0 && response != IntPtr.Zero)
            {
                Marshal.Copy(response, output, 0, output.Length);
            }
            RpcRuntime.Free(response);

            return output;
        }

   
    }
}