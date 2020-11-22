using System;
using System.Collections.Generic;
using System.Text;

namespace EPAM.Trainings.Zadanie2_1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("\n Account Test");
            Account account = new Account(-1);
            Console.WriteLine(account.Balance);

            Console.WriteLine("\n ChekingAccount Test");
            ChekingAccount chekingAccount = new ChekingAccount(100, 2);
            Console.WriteLine(chekingAccount.Balance);
            chekingAccount.Debit(98);
            Console.WriteLine(chekingAccount.Balance);
            chekingAccount.Credit(10);
            Console.WriteLine(chekingAccount.Balance);
            chekingAccount.Debit(10);
            Console.WriteLine(chekingAccount.Balance);

            Console.WriteLine("\n SavingsAccount Test");
            SavingsAccount savingsAccount = new SavingsAccount(100, 0.05m);
            Console.WriteLine(savingsAccount.Balance);
            savingsAccount.Credit(savingsAccount.CalculateInterest());
            Console.WriteLine(savingsAccount.Balance);

            Console.ReadLine();
        }
    }
}
