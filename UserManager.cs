using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace project0
{
    class UserManager
    {
        SqlConnection connection = new SqlConnection("server = DESKTOP-UF6LITD; database = bankingDB; integrated security = true");
        int userID = 0;

        // do a sql search for the given username/password, then return the userID if a match is found
        public int Login(string username, string password)
        {
            SqlCommand cmdLogin = new SqlCommand("SELECT * FROM Users WHERE username = @uname AND password = @pword", connection);
            cmdLogin.Parameters.AddWithValue("@uname", username);
            cmdLogin.Parameters.AddWithValue("@pword", password);

            try 
            {
                connection.Open();
                SqlDataReader reader = cmdLogin.ExecuteReader();

                reader.Read();
                userID = Convert.ToInt32(reader[0]);
            }
            catch (SqlException e)
            {
                Console.WriteLine("Invalid login creditials, please try again");
                Console.WriteLine(e.Message);
            }
            finally
            {
                connection.Close();
            }
            return userID;
        }

        public List<AccountManager> GetAllAccountsByUserID(int uID)
        {
            List<AccountManager> accounts = new List<AccountManager>();
            SqlCommand cmdAllAccounts = new SqlCommand("SELECT * FROM Accounts WHERE userID = @userID", connection);
            cmdAllAccounts.Parameters.AddWithValue("@userID", uID);

            SqlDataReader reader;

            try
            {
                connection.Open();
                reader = cmdAllAccounts.ExecuteReader();

                while(reader.Read())
                {
                    accounts.Add(new AccountManager()
                    {
                        accountNumber = Convert.ToInt32(reader[0]),
                        accountType = Convert.ToString(reader[1]),
                        accountBalance = Convert.ToDouble(reader[2])
                    });
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
                return accounts;
            }
            finally
            {
                connection.Close();
            }

            return accounts;
        }

        // get all accounts, order by balance, group by type
        public List<AccountManager> OrderBy(int uID)
        {
            List<AccountManager> accounts = new List<AccountManager>();
            SqlCommand cmdAllAccounts = new SqlCommand("SELECT * FROM Accounts WHERE userID = @userID ORDER BY accountBalance DESC", connection);
            cmdAllAccounts.Parameters.AddWithValue("@userID", uID);

            SqlDataReader reader;

            try
            {
                connection.Open();
                reader = cmdAllAccounts.ExecuteReader();

                while(reader.Read())
                {
                    accounts.Add(new AccountManager()
                    {
                        accountNumber = Convert.ToInt32(reader[0]),
                        accountType = Convert.ToString(reader[1]),
                        accountBalance = Convert.ToDouble(reader[2])
                    });
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
                return accounts;
            }
            finally
            {
                connection.Close();
            }

            return accounts;
        }

        // sum all of user's accounts by type, group by type
        public List<GroupByStorage> GroupBy(int uID)
        {
            List<GroupByStorage> accounts = new List<GroupByStorage>();
            SqlCommand cmdAllAccounts = new SqlCommand("SELECT userID, accountTYPE, SUM(accountBalance) FROM Accounts GROUP BY accountTYPE, userID", connection);

            SqlDataReader reader;

            try
            {
                connection.Open();
                reader = cmdAllAccounts.ExecuteReader();

                while(reader.Read())
                {
                    accounts.Add(new GroupByStorage()
                    {
                        userID = Convert.ToInt32(reader[0]),
                        accountType = Convert.ToString(reader[1]),
                        //accountCount = Convert.ToInt32(reader[2]),        // if I have AS 'Total Balance' in the command, it doesn't show up in the results in c# for some reason
                        totalBalance = Convert.ToInt32(reader[2])
                    });
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
                return accounts;
            }
            finally
            {
                connection.Close();
            }

            return accounts;
        }
    }
}
