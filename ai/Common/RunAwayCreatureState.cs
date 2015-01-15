using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AIbyExample.Common
{
    public class RunAwayCreatureState : IState<CreatureAgent>
    {
        public void Enter(CreatureAgent agent)
        {
            
        }

        public void Execute(CreatureAgent agent)
        {
            if (agent.IsSafe)
            {
                agent.ChangeState(new SleepCreatureState());
            }
            else
            {
                agent.MoveAway();
            }
        }

        public void Exit(CreatureAgent agent)
        {
            
        }
    }
}
