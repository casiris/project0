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
            }
            finally
            {
                connection.Close();
            }
            return userID;
        }

        public List<AccountManager> GetAllAccounts(int uID)
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
