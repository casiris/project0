using System;
using System.Data.SqlClient;

namespace project0
{
    class Account
    {
        bool accountType = true;   // since there's only two account types, i can just use a bool. true for checking, false for savings
        double balance = 0;


        // call sql commands from these functions, rather than in program. that's abstraction
        public void CreateAccount(bool accType)
        {
            accountType = accType;

            // program will ask user what type of account they want to create. i can use this same function for both
        }

        public void Deposit (double amount)
        {
            balance += amount;      // probably should make sure amount is not negative
        }

        // no real need for a return type, because i'm not actually doing anything with the money drawn
        public void Withdraw (double amount)
        {
            balance -= amount;
        }

        public void Transfer (double amount, int accTransferFrom, int accTransferTo)
        {
            // user will need to enter both account numbers, which makes sense irl since you'd need to know the accounts to transfer from/to

            // the problem is, from where do i add/subtract the amount? in the database it makes sense, but i'd also need to update the account object
            // and from within the class, how do i know which object is which? 
        }
    }
}
