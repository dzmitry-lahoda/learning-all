using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

using Xunit;
namespace Dod.Ecs
{
    class Program
    {
        static unsafe void Main(string[] args)
        {
            var client1 = new Client();
            var client2 = new Client();
            var server = new Server();
            client1.Connect(server);
            client2.Connect(server);
            client1.CheckAndAttack();
            // var worlds = new World[32];
            // var world = new World();
            // world.positions[1] = new ReadPosition(1.0f, 2.0f, 3.0f);
            // Assert.Equal(sizeof(Position), sizeof(ReadPosition));
            // ref Position write = ref Ecs.AsWrite<ReadPosition,Position/* , IReadPosition, IPosition*/>(ref world.positions[1]);
            // write.x = 42.2f;


            // Assert.Equal(42.2f, world.positions[1].x);
        }


    }
}
