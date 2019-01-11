using System.Runtime.InteropServices;
using System.Collections.Generic;
namespace Dod.Ecs
{
    public class World
    {
        public World(ushort maxPositions = ushort.MaxValue, byte maxPlayers = 16, ushort maxWeapons = 256)
        {
            positions = new ReadPosition[maxPositions];
            players = new ReadPlayer[maxPlayers];
            weapons = new ReadWeapon[maxWeapons];
            playerEntities = new PlayerEntity[maxPlayers];            
        }
        public readonly ReadPosition[] positions;
        public readonly ReadPlayer[] players;
        public readonly ReadWeapon[] weapons;
        public readonly PlayerEntity[] playerEntities;
    }

    public class Commands
    {

    }

    public class Game
    {
        Queue<(World,Commands)> worlds = new Queue<(World,Commands)>(32);

        (World,Commands) Current => worlds.Peek();
    }    
}
