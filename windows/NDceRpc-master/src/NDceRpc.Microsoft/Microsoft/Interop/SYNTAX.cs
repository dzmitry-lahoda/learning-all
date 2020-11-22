using System;

namespace NDceRpc.Microsoft.Interop
{
    class SYNTAX
    {
        public static readonly RPC_VERSION SYNTAX_VERSION = new RPC_VERSION() { MajorVersion = 2, MinorVersion = 0 };

        public static readonly Guid SYNTAX_IID = new Guid(0x8A885D04u, 0x1CEB, 0x11C9, 0x9F, 0xE8, 0x08, 0x00, 0x2B,
                                          0x10,
                                          0x48, 0x60);
    }
}
