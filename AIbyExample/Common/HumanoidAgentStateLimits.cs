using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AIbyExample.Common
{
    public static class HumanoidAgentStateLimits
    {
        //the amount of gold a miner must have before he feels comfortable
        public const int ComfortLevel = 5;
        //the amount of nuggets a miner can carry
        public const int MaxNuggets = 3;
        //above this value a miner is thirsty
        public const int ThirstLevel = 5;
        //above this value a miner is sleepy
        public const int TirednessThreshold = 5;
    }
}
