using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AIbyExample.Common
{
    public class EnterMineAndDigForNuggetState : IState<HumanoidAgent>
    {

        public void Enter(HumanoidAgent humanoid)
        {
            //if the miner is not already located at the goldmine, he must
            //change location to the gold mine
            if (humanoid.Location != Locations.Mine)
            {
                Console.WriteLine(humanoid.Id + ": " + "Walkin' to the goldmine");
                humanoid.Location = Locations.Mine;
            }
        }


        public void Execute(HumanoidAgent humanoid)
        {
            //the miner digs for gold until he is carrying in excess of MaxNuggets. 
            //If he gets thirsty during his digging he packs up work for a while and 
            //changes state to go to the saloon for a whiskey.
            humanoid.GoldCarried += 1;

            humanoid.Fatigue += 1;

            Console.WriteLine(humanoid.Id + ": " + "Pickin' up a nugget");

            //if enough gold mined, go and put it in the bank
            if (humanoid.PocketsFull())
            {
                humanoid.ChangeState(new VisitBankAndDepositGoldState());
            }

            if (humanoid.Thirsty())
            {
                humanoid.ChangeState(new QuenchThirstState());
            }
        }


        public void Exit(HumanoidAgent humanoid)
        {
            Console.WriteLine(humanoid.Id + ": " + "Ah'm leavin' the goldmine with mah pockets full o' sweet gold");
        }

    }

    public class VisitBankAndDepositGoldState : IState<HumanoidAgent>
    {

        public void Enter(HumanoidAgent humanoid)
        {
            //on entry the miner makes sure he is located at the bank
            if (humanoid.Location != Locations.Bank)
            {
                Console.WriteLine(humanoid.Id + ": " + "Goin' to the bank. Yes siree");

                humanoid.Location = Locations.Bank;
            }
        }


        public void Execute(HumanoidAgent humanoid)
        {

            //deposit the gold
            humanoid.MoneyInBank += humanoid.GoldCarried;

            humanoid.GoldCarried = 0;

            Console.WriteLine(humanoid.Id + ": "
                 + "Depositing gold. Total savings now: " + humanoid.MoneyInBank);

            //wealthy enough to have a well earned rest?
            if (humanoid.MoneyInBank >= HumanoidAgentStateLimits.ComfortLevel)
            {
                Console.WriteLine(humanoid.Id + ": "
                     + "WooHoo! Rich enough for now. Back home to mah li'lle lady");

                humanoid.ChangeState(new GoHomeAndSleepTilRestedState());
            }

            //otherwise get more gold
            else
            {
                humanoid.ChangeState(new EnterMineAndDigForNuggetState());
            }
        }


        public void Exit(HumanoidAgent humanoid)
        {
            Console.WriteLine(humanoid.Id + ": " + "Leavin' the bank");
        }

    }

    public class GoHomeAndSleepTilRestedState : IState<HumanoidAgent>
    {
        public void Enter(HumanoidAgent humanoid)
        {
            if (humanoid.Location != Locations.Home)
            {
                Console.WriteLine(humanoid.Id + ": " + "Walkin' home");

                humanoid.Location = Locations.Home;
            }
        }

        public void Execute(HumanoidAgent humanoid)
        {
            //if miner is not fatigued start to dig for nuggets again.
            if (!humanoid.Fatigued())
            {
                Console.WriteLine(humanoid.Id + ": "
                                  + "What a God darn fantastic nap! Time to find more gold");

                humanoid.ChangeState(new EnterMineAndDigForNuggetState());
            }

            else
            {
                //sleep
                humanoid.Fatigue -= 1;
                Console.WriteLine(humanoid.Id + ": " + "ZZZZ... ");
            }
        }

        public void Exit(HumanoidAgent humanoid)
        {
            Console.WriteLine(humanoid.Id + ": " + "Leaving the house");
        }



    }
    public class QuenchThirstState : IState<HumanoidAgent>
    {


        public void Enter(HumanoidAgent humanoid)
        {
            if (humanoid.Location != Locations.Tavern)
            {
                humanoid.Location = Locations.Tavern;

                Console.WriteLine(humanoid.Id + ": " + "Boy, ah sure is thusty! Walking to the saloon");
            }
        }

        public void Execute(HumanoidAgent humanoid)
        {
            if (humanoid.Thirsty())
            {
                humanoid.BuyAndDrinkAWhiskey();

                Console.WriteLine(humanoid.Id + ": " + "That's mighty fine sippin liquer");

                humanoid.ChangeState(new EnterMineAndDigForNuggetState());
            }

            else
            {
                Console.WriteLine("\nERROR!\nERROR!\nERROR!");
            }
        }

        public void Exit(HumanoidAgent humanoid)
        {
            Console.WriteLine(humanoid.Id + ": " + "Leaving the saloon, feelin' good");
        }
    }
}
