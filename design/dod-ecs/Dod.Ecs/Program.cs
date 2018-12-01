using System;
using System.Runtime.CompilerServices;
using Xunit;
namespace Dod.Ecs
{
    
    public  readonly struct ReadPosition
    {
        public ReadPosition(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public readonly float x;
        public readonly float y;
        public readonly float z;
    }

     public struct  Position
    {
        public float x;
        public float y;
        public float z;
    }

    public class World
    {
        public World(ushort maxPostions = ushort.MaxValue)
        {
            positions = new ReadPosition[maxPostions];
        }
        public readonly ReadPosition[] positions;
    }

    class Program
    {
        static unsafe void Main(string[] args)
        {
            var world = new World();
            world.positions[1] = new ReadPosition(1.0f, 2.0f, 3.0f);
            Assert.Equal(sizeof(Position), sizeof(ReadPosition));
            var first = (ReadPosition*)Unsafe.AsPointer(ref world.positions[0]);
            first++;
            ref Position write = ref Unsafe.AsRef<Position>(first);
            write.x = 42.2f;
            Assert.Equal(42.2f, world.positions[1].x);
            Console.WriteLine("Hello World!");
        }
    }
}
