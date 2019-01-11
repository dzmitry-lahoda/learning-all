


//shared
    // Execute(IGame game);// all plyers except initiator
    // Execute(LocalPlayer owner, IGame game);
    // Execute(LocalPlayer owner);

// client 
    // IClientGame: IGame
   
    // Execute(IClientGame game);// only remte players + separate prop for local
    
    // call shared via new 

// server 
    // IServerGame
    // SharedGameView: IGame (IServerGame game)
    
    // Execute(IServerGame game);// all local players
    // Execute(LocalPlayer owner, IServerGame game);
    // Execute(LocalPlayer owner);

    // call shared via new 

    // remoteRocketsComponent[]
    // remoteWeapons[]  // withput ammo
    // remotePlayerComponet[]
    // 
    // client
    // localWeaponComponet // with ammo
    // localPlayerComponnt
    // 
    // ReamotePlaeyrEntity[] // remoteWeapons
    // LocalPlayerEntity // localWeaponComponet remoteWeapons
    //
    //
    // server
    // localWeaponsComponent[]
    // 
    // WeaponEntit { int localWeapons, in reammoteWapons}
}

for devs. if to have ecs than model will be -   RangeServerWeaponEntity { size_t sharedWeaponComponent, size_t rangeWeaponCompoent}  and MeleeClientWeaponEntity {size_t  sharedWeaponComponent, size_t  notSharedWeaponComponent, size_t  meleeWeaponComponent} and other combinations. there is no direct need to model code after how it is written in text.


                // containers
                // - intention
                // - current world
                // - effect
                // - previes world
                // - diff (network, obseravle)