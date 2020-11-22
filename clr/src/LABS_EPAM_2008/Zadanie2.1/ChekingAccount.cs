using System;
using System.Collections.Generic;
using System.Text;

namespace EPAM.Trainings.Zadanie2_1
{
    public class ChekingAccount : Account
    {
        private decimal feeAmount;

        public ChekingAccount(decimal initialBalance, decimal feeAmount)
            : base(initialBalance)
        {
            this.feeAmount = feeAmount;
        }
        /// <summary>
        /// Adds (addition-fee) to balance
        /// </summary>
        /// <param name="addition"></param>
        public override void Credit(decimal addition)
        {
            addition -= feeAmount;
            base.Credit(addition);
        }

        /// <summary>
        /// Substructs (subtraction+fee) from balance
        /// </summary>
        /// <param name="subtraction"></param>
        public override void Debit(decimal subtraction)
        {
            subtraction += feeAmount;
            base.Debit(subtraction);
        }
    }
}
