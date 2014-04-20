using System;
using System.Collections.Generic;
using System.Text;

namespace EPAM.Trainings.Zadanie2_1
{
    public class SavingsAccount : Account
    {
        private decimal interestRate = 0.0m;

        public SavingsAccount(decimal initialBalance, decimal interestRate)
            : base(initialBalance)
        {
            this.interestRate = interestRate;    
        }

        public decimal CalculateInterest()
        {
            return Balance * interestRate;
        }
    }
}
