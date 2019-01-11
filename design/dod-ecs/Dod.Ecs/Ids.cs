using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Dod.Ecs
{
    public readonly struct PlayerId
    {
        readonly byte id;

        public PlayerId(byte id) => this.id = id;
    }
}