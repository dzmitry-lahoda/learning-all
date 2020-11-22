using System;
using System.Collections.Generic;
using System.Text;

namespace EPAM.Trainings.Zadanie2_1
{

    public class Account
    {
/*        public event AccountEventHandler InvalidInitialBalance;
        public event AccountEventHandler DebitExceeded;
                 if (SortingStarted != null)
                {
                    SortingStarted(this,
                        new ByteSorterEventArgs(DateTime.Now.ToLongTimeString() + " " + StartedMessage));
                }*/
        public static readonly String InvalidInitialBalanceMessage = "Initial balance was invalid";
        public static readonly String DebitExceededMessage = "Debit amount exceeded account balance";

        private decimal currentBalance=0.0m;

        public Account(decimal initialBalance)
        {
            Balance = initialBalance;
        }

        /// <summary>
        /// Gets current balance
        /// </summary>
        public decimal Balance
        {
            get
            {
                return currentBalance;
            }
            private set
            {
                if (value < 0)
                {
                    Console.WriteLine(InvalidInitialBalanceMessage);
                    currentBalance = 0.0m;
                }
                else
                {
                    currentBalance = value;
                }
            }
        }

        /// <summary>
        /// Adds to balance
        /// </summary>
        /// <param name="addition"></param>
        public virtual void Credit(decimal addition)
        {
            if (addition > 0)
            {
                Balance += addition;
            }
        }

        /// <summary>
        /// Substructs from balance
        /// </summary>
        /// <param name="subtraction"></param>
        public virtual void Debit(decimal subtraction)
        {
            if (Balance >= subtraction)
            {
                Balance -= subtraction;
            }
            else
            {
                Console.WriteLine(DebitExceededMessage);
            }
        }

    }
}
