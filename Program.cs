using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace project0
{
    class Program
    {
        static void Main(string[] args)
        {
            // menu will ask for username/password, and once logged in will ask to {withdraw, deposit, transfer, get account details, create new account, close account (delete)}

            // i don't think i need a user class. all it'd really do is log in

            SqlConnection connection = new SqlConnection("server = DESKTOP-UF6LITD; database = bankingDB; integrated security = true");

            bool continueBanking = true;
            int choice = 0;
            AccountManager accManager = new AccountManager();
            UserManager userManager = new UserManager();
            int currentUserID = 0;
            int accountNum = 0;
            List<AccountManager> accountsByID = new List<AccountManager>();

            while (currentUserID < 1)
            {
                Console.WriteLine("Welcome to Bank of America\n\nTo log in, please enter your username");
                string username = Console.ReadLine();
                Console.WriteLine("\nPlease enter your password");
                string password = Console.ReadLine();

                // will need the user ID of whoever logged in to access their associated accounts
                currentUserID = userManager.Login(username, password);
            }

            while (continueBanking)
            {
                // present the user all their accounts
                Console.Clear();
                Console.WriteLine("Here are all your accounts:\n");
                accountsByID = userManager.GetAllAccountsByUserID(currentUserID);

                foreach(var account in accountsByID)
                {
                    Console.WriteLine("Account number: {0}\nAccount type: {1}\nAccount balance: {2}\n", account.accountNumber, account.accountType, account.accountBalance);
                }

                Console.WriteLine("What would you like to do?\n1. Open a new account\n2. Withdraw from your account\n3. Deposit to your account\n4. Close account\n5. Order accounts by balance\n6. Group by\n7. Exit");
                choice = Convert.ToInt32(Console.ReadLine());

                switch (choice)
                {
                    case 1:     // create new account
                     Console.Clear();
                        Console.WriteLine("To create a new account, please enter what account type you would like (\"checking\" or \"savings\")");
                        accManager.CreateAccount(currentUserID, Console.ReadLine());
                        Console.WriteLine("Account created");
                        break;
                    case 2:     // withdraw
                        Console.Clear();
                        Console.WriteLine("Please enter the account number to withdraw from");
                        accountNum = Convert.ToInt32(Console.ReadLine());

                        // find the account by the account number given
                        accManager = accManager.FindAccountByID(accountNum);

                        Console.WriteLine("Enter withdrawal amount");
                        int withdrawalAmount = Convert.ToInt32(Console.ReadLine());

                        accManager.Withdraw(withdrawalAmount, accountNum);
                        break;
                    case 3:     // deposit
                        Console.Clear();
                        Console.WriteLine("Please enter the account number to deposit to");
                        accountNum = Convert.ToInt32(Console.ReadLine());

                        accManager = accManager.FindAccountByID(accountNum);

                        Console.WriteLine("Enter deposit amount");
                        int depositAmount = Convert.ToInt32(Console.ReadLine());

                        accManager.Deposit(depositAmount, accountNum);
                        break;
                    case 4:
                        Console.Clear();
                        Console.WriteLine("Please enter the account number to close");
                        accountNum = Convert.ToInt32(Console.ReadLine());

                        accManager = accManager.FindAccountByID(accountNum);
                        accManager.CloseAccount(accountNum);
                        break;
                    case 5:
                        Console.Clear();
                        // order by
                        // return all of user's accounts, order by balance
                        // doesn't work
                        List<AccountManager> accounts = userManager.OrderBy(currentUserID);

                        foreach(var account in accountsByID)
                        {
                            Console.WriteLine("Account number: {0}\nAccount type: {1}\nAccount balance: {2}\n", account.accountNumber, account.accountType, account.accountBalance);
                        }
                        break;
                    case 6:
                        // sum all of user's accounts by type, group by type
                        // doesn't work
                        Console.Clear();
                        List<GroupByStorage> group = userManager.GroupBy(currentUserID);

                        foreach(var g in group)
                        {
                            Console.WriteLine("User ID: {0}\nAccount type: {1}\nNumber of accounts: {2}\nTotal balance: {3}\n", g.userID, g.accountType, g.accountCount, g.totalBalance);
                        }
                        break;
                    default:
                        break;
                }

                Console.Clear();
                Console.WriteLine("\nDo you have another transaction? (y/n)");
                string response = Console.ReadLine();

                if (!(response == "y" || response == "Y"))
                {
                    Console.WriteLine("\nThank you for banking with us");
                    continueBanking = false;
                }
            }
        }
    }
}
