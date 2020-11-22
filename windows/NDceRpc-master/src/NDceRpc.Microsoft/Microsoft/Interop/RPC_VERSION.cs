using System.Runtime.InteropServices;

namespace NDceRpc.Microsoft.Interop
{
    [StructLayout(LayoutKind.Sequential)]
    public struct RPC_VERSION
    {
        public ushort MajorVersion;
        public ushort MinorVersion;
    }
}