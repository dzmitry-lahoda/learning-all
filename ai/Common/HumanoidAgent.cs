using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AIbyExample.Common
{
    public class HumanoidAgent : Agent
    {
        // the place where the miner is currently situated
        public Locations Location;

        //how many nuggets the miner has in his pockets
        public int GoldCarried;

        //how much money the miner has deposited in the bank
        public int MoneyInBank;

        //the higher the value, the thirstier the miner
        public int Thirst;

        //the higher the value, the more tired the miner
        public int Fatigue;

        public IState<HumanoidAgent> CurrentState=new EnterMineAndDigForNuggetState();

        public void ChangeState(IState<HumanoidAgent> state)
        {
            Assert.NotNull(state);
            if (CurrentState != null)
            {
                CurrentState.Enter(this);
            }
            CurrentState = state;
            CurrentState.Exit(this);
        }

        protected override void BeforeUpdate()
        {
            Thirst += 1;
            base.BeforeUpdate();
        }

        public override void Update()
        {
            BeforeUpdate();
            if (CurrentState != null)
            {
                CurrentState.Execute(this);
            }
            AfterUpdate();
        }

        public void AddToGoldCarried(int val)
        {
            GoldCarried += val;

            if (GoldCarried < 0) GoldCarried = 0;
        }



        public void AddToWealth(int val)
        {
            MoneyInBank += val;

            if (MoneyInBank < 0) MoneyInBank = 0;
        }


        public bool Thirsty()
        {
            if (Thirst >= HumanoidAgentStateLimits.ThirstLevel) { return true; }

            return false;
        }

        public bool Fatigued()
        {
            if (Fatigue > HumanoidAgentStateLimits.TirednessThreshold)
            {
                return true;
            }

            return false;
        }

        public void BuyAndDrinkAWhiskey() 
        {
            Thirst = 0; MoneyInBank -= 2;
        }

        public bool PocketsFull()
        {
            return GoldCarried >= HumanoidAgentStateLimits.MaxNuggets;
        }
    }
}
