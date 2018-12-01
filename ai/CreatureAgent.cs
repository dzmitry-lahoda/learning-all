using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AIbyExample.Common
{
    public class CreatureAgent : Agent
    {

        public bool IsSafe = false;

        public bool IsThreatened = false;

        public virtual void MoveAway()
        {
            Console.WriteLine("MoveAway");
        }

        public void StaySlepping()
        {
            Console.WriteLine("StaySlepping");
        }

        public IState<CreatureAgent> CurrentState;

        public override void Update()
        {
            BeforeUpdate();
            if (CurrentState != null)
            {
                CurrentState.Execute(this);
            }
            AfterUpdate();
        }

        public void ChangeState(IState<CreatureAgent> state)
        {
            Assert.NotNull(state);
            if (CurrentState != null) 
            {
                CurrentState.Enter(this);
            }
            CurrentState = state;
            CurrentState.Exit(this);
        }
    }
}
