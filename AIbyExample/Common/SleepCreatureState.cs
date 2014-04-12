using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AIbyExample.Common
{
    public class SleepCreatureState : IState<CreatureAgent>
    {
        public void Enter(CreatureAgent agent)
        {
            
        }

        public void Execute(CreatureAgent agent)
        {
            if (agent.IsThreatened)
            {
                agent.ChangeState(new RunAwayCreatureState());
            }

            else
            {
                agent.StaySlepping();
            }
        }

        public void Exit(CreatureAgent agent)
        {
            
        }
    }
}
