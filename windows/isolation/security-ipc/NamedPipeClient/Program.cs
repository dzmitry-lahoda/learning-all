using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Pipes;

namespace NamedPipeClient
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var pipeClient = new NamedPipeClientStream(".", "NamedPipe\\Test",
                    PipeDirection.InOut,
                    PipeOptions.None);
                pipeClient.Connect(100);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed: " + ex);
                Console.ReadLine();
                return;
            }

            Console.WriteLine("Connected");
            Console.ReadLine();
        }
    }
}