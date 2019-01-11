using System.Runtime.InteropServices;

namespace Dod.Ecs
{

    // TODO: try out generator
    // TODO: try value tuples (so these are readonly...)
    public interface IReadWeapon
    {
        WeaponStates State { get; }
    }

    public interface IWeapon : IReadWeapon
    {
        WeaponStates State { get; set; }
    }

    public enum WeaponStates : byte
    {
        Passive,
        Shooting,
        Reloading
    }

    [StructLayout(LayoutKind.Sequential)]
    public readonly struct ReadWeapon : IReadWeapon
    {
        public readonly ushort bullets;
        public readonly ushort maxBullets;
        public readonly WeaponStates state;

        public ReadWeapon(ushort bullets)
        {
            this.bullets = bullets;
            this.state = WeaponStates.Passive;
            this.maxBullets = 256;
        }

        public WeaponStates State => state;

        public ReadWeapon Shoot(ushort count = 1) => new ReadWeapon((ushort)(bullets - count));
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct Weapon : IWeapon, IReadWeapon
    {
        public ushort bullets;
        public ushort maxBullets;
        public WeaponStates state;

        public Weapon(ushort bullets)
        {
            this.bullets = bullets;
            this.state = WeaponStates.Passive;
            this.maxBullets = 256;
        }

        public WeaponStates State
        {
            get => state;
            set => state = value;
        }
    }

    public readonly struct ReadPlayer
    {
        public readonly ushort health;
    }

    public struct Player
    {
        public ushort health;
        
    }



    public interface IReadPosition
    {
        float X { get; }
        float Y { get; }
        float Z { get; }
    }

    public interface IPosition : IReadPosition
    {
        float X { get; set; }
        float Y { get; set; }
        float Z { get; set; }
    }

    [StructLayout(LayoutKind.Sequential)]
    public readonly struct ReadPosition : IReadPosition
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

        public float X => x;

        public float Y => x;

        public float Z => x;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct Position : IPosition, IReadPosition
    {
        public float x;
        public float y;
        public float z;

        public float X { get => x; set => x = value; }
        public float Y { get => y; set => y = value; }
        public float Z { get => z; set => z = value; }
    }
}