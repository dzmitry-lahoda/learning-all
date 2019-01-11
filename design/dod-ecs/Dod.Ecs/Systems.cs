using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Dod.Ecs
{ 
    public class AttackSystem
    {
        public void Execute(ref PlayerEntity attacker, ref World world, ref Commands command)
        {
            ref var weapon = ref attacker.Weapon;

            ref var weapon = ref world.weapons[attacker.weaponId];
            ref var player = ref world.players[attacker.playerId];
            if (weapon.bullets > 0 && player.health > 0 world.NotMe.AnyWithout)
            {
                world.attacksLast++;
                command.attacks[world.attacksLast] = new AttachPossibleEntity(attacker.playerId, attacker.weaponId);
            } 
        }
    }
}
