using System;
using System.Collection.Generics;
using System.Memory;

public class Program
{



  readonly stuct Position
  {
     // p1
      // p2
      //...
  }

  class WeaponSystem
  {

     IObservavle<Weapon> _a;
      public void Shoot(ref Weapon weapon, in Player player)
      {    
            weapon.bullets--;
            Notify(indexCur);
      }
      // if weaponPrev!=waponNext Delta()
  }

  readonly struct Player 
  {
      private readonly ushort weaponIndex;
      private readonly ushort positionIndex;
      // numbers-property
      // p1
      // p2

      public ref Weapon => ref World._weapons[weaponIndex];
      public ref Position => ref World._xyz[positionIndex];
  }

  stuct Rocket
  {
      State {NonExisting, Flying};
      x
      y
      z

  }
  
  public  class World // 32
  {
      public byte currentLocalPlayerIndex = 0; // Max is 16 * 16 (16 local players * 16 frames stored max)
      // lastWeapon++
      //public MyRange<int,int> waeponsRange; 
      Player[] _players = new Player[](byte.sMaxValue); // 16 * 32
      Weapon[] _weapons = new List<Weapon>(byte.MaxValue); // 
      List<Position> _xyz = new List<Position>(short.MaxValue);
      List<Attackers> _xyz = new List<Attackers>(short.MaxValue);
       List<Rocket> _xyz = new List<Rocket>(16*16);
      // 1,  10, 40, //flying bullet
      // 2-9 -> 0-8, 11-39 -> 8 - 36, 
      // 1 2 3 4 5
      // 1   3 4 5
      // 1 2 3 4
  }

  var circBufer =  new CF<World>();// 1, 2  ,  w32, w31 -> d


  public void Main()
  {
    // # Mutable design (need to copy arrays to store frames)
    var player1 = new Player(bulletIndex:1, health:100);
    var root = player1;// in contoller
    _weapons[1] = new Weapon(bullets:100);
    // shoot here
    ref weapon = ref player1.Weapon;//read is not copy
    weapon.bullets--;   // notify observer  // ui redner bullets less
    weapons[1] = weapon.Shoot();//replace
    world[0][1] = weapon.Shoot();// new world
    
    // # Immutable design
    ushort lastIndexInBulletMemory = 0;
    var player1 = new Player(bulletIndex:lastIndexInMemory, postionIndex:123);
    _players[currentLocalPlayerIndex] = new Player(lastIndexInBulletMemory)
    _weapons[lastIndexInMemory] = new Weapon(bullets:100);
    // shoot here
    ref weapon = ref player1.Weapon;
    (ref w[k+1]._weapons[lastIndexInBulletMemory++]) =  weapon.Shoot();
    var p1w2 = new Player(lastIndexInBulletMemory, postionIndex:123);
    w[l+1]._players[currentLocalPlayerIndex++] = p1w2;

    var root = p1w2;
    // notify observer
    // ui redner bullets less

    // inter player
    _players[lastLocal].attacerId = lastRemoteAttacher;
    // other conenction
    

    // Possible:
    // 1. Do update-cloning API well undrestood
    // 2. Made observers aboce
    //
    // Possible with immutable:
    // Good:
    // 1. Share world of player with other thread as these where a frame before
    // 2. No need to special copy world for sotrign frames
    // Bad:
    // 1. Cannot consider arrays as whole related to current world. Solution. Preacllocate for each stored simlutaion a set of arrays (somewhta more memory, but faster all)
    // bots may run in separate threads on old mode of world and sometiems rerun simultaion, but never. BOTS MAY BE VERY VERY CLEVER (and fun)
  }
}


