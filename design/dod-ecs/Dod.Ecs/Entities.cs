using System.Runtime.InteropServices;

namespace Dod.Ecs
{
    public struct PlayerEntity
    {
        World x;
        public ushort weaponId;
        public ushort playerId;
        public ushort positionId;

        public ref ReadWeapon Weapon => ref x.weapons[weaponId];
    }

    public struct PlayerAttachEntity
    {
       public ushort playerId;
       public ushort weaponId;
    }
}


