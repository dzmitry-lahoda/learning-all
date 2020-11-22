using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NDceRpc.Microsoft.Interop
{
    public enum RPC_C_BINDING_TIMEOUTS
    {
        ///Keeps trying to establish communications forever.
        RPC_C_BINDING_INFINITE_TIMEOUT = 10,

        ///Tries the minimum amount of time for the network protocol being used. This value favors response time over correctness in determining whether the server is running.
        RPC_C_BINDING_MIN_TIMEOUT = 0,

        ///Tries an average amount of time for the network protocol being used. This value gives correctness in determining whether a server is running and gives response time equal weight. This is the default value.
        RPC_C_BINDING_DEFAULT_TIMEOUT = 5,

        ///Tries the longest amount of time for the network protocol being used. This value favors correctness in determining whether a server is running over response time.
        RPC_C_BINDING_MAX_TIMEOUT = 9
        
    }
}
