using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NDceRpc.ExplicitBytes;
using NDceRpc.Microsoft.Interop;

namespace NDceRpc.Microsoft.ClientSample
{
    class Program
    {
        static void Main(string[] args)
        {
            var iid = Guid.Parse("FF9B1856-934A-459B-92AF-18AEBD745BC1");
            Console.WriteLine("Server id = " + iid);
            var client = new ExplicitBytesClient(iid, new EndpointBindingInfo(RpcProtseq.ncacn_np, null, "\\pipe\\testnamedpipe" + iid));
            Console.WriteLine("Client started");

            Console.WriteLine("Doing several requests to server");
            for (int i = 0; i < 10; i++)
            {
                var resp = client.Execute(Encoding.Unicode.GetBytes("Client request"));
                Console.WriteLine(Encoding.Unicode.GetString(resp));
            }

            Console.WriteLine("Press any key to send request on which server throws");
            Console.ReadKey();
            try
            {
                client.Execute(Encoding.Unicode.GetBytes("Throw Client request"));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine("Kill server process now and press any key");
            Console.ReadKey();
            try
            {
                client.Execute(Encoding.Unicode.GetBytes("This will not be delivered"));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadKey();
        }
    }
}
