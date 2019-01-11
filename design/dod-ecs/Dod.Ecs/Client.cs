using System;
using System.Runtime.InteropServices;

namespace Dod.Ecs
{
    class Client
    {
        internal ClientGameplay Connect(IServer server)
        {
            PlayerId playerId = server.Connect();
            return new ClientGameplay(playerId, new Game()));
        }
    }
}