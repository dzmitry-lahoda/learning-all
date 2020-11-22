using System;
using System.Text;
using NDceRpc.ExplicitBytes;
using NDceRpc.Microsoft.Interop;

namespace NDceRpc.Microsoft.ServerSample
{
    class Program
    {
        static void Main(string[] args)
        {
            var iid = Guid.Parse("FF9B1856-934A-459B-92AF-18AEBD745BC1");
            Console.WriteLine("Server id = " + iid);
            var server = new ExplicitBytesServer(iid);
            server.AddProtocol(RpcProtseq.ncacn_np, "\\pipe\\testnamedpipe" + iid, byte.MaxValue);
            server.OnExecute += (client, data) =>
                {
                    var request = Encoding.Unicode.GetString(data);
                    Console.WriteLine(Encoding.Unicode.GetString(data));
                    if (request == "Throw Client request")
                        throw new Exception();
                    return Encoding.Unicode.GetBytes("Server response");
                };
            server.StartListening();
            Console.WriteLine("Server started");
            

            Console.ReadKey();
        }
    }
}
