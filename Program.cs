using System;

namespace project0
{
    class Program
    {
        static void Main(string[] args)
        {
            // menu will ask for username/password, and once logged in will ask to {withdraw, deposit, transfer, get account details, create new account, close account (delete)}

            // i don't think i need a user class. all it'd really do is log in

            bool continueBanking = true;
            int choice = 0;

            while (continueBanking)
            {
                Console.WriteLine("Welcome to Bank of America\nTo log in, please enter your username");
                string username = Console.ReadLine();
                Console.WriteLine("Please enter your password");
                string password = Console.ReadLine();

                //continueBanking = Login(username, password);  // use continueBanking to check login success/failure

                switch (choice)
                {
                    case 1:
                        break;
                    case 2:
                        break;
                }
            }

            bool Login(string username, string password)
            {
                // here, do a sql search for username and password, and if there's a match, then return true, otherwise false
                // also, print out a message about success/failure
                return true;
            }
        }
    }
}
