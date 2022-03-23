using System;
using System.Data.SqlClient;

namespace project0
{
    class AccountManager
    {
        public int accountNumber { get; set; }
        public string accountType { get; set; }
        public double accountBalance { get; set; }
        SqlConnection connection = new SqlConnection("server = DESKTOP-UF6LITD; database = bankingDB; integrated security = true");

        public AccountManager()
        {
            accountNumber = 0;
            accountBalance = 0;
            accountType = "checking";
        }

        public void CreateAccount(int userID, string accType)
        {
            SqlCommand cmdCreateAccount = new SqlCommand("INSERT INTO Accounts values (@accType, @accBalance, @userID)", connection);

            cmdCreateAccount.Parameters.AddWithValue("@accType", accType);
            cmdCreateAccount.Parameters.AddWithValue("@accBalance", 0);
            cmdCreateAccount.Parameters.AddWithValue("@userID", userID);

            try
            {
                connection.Open();
                cmdCreateAccount.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        public AccountManager FindAccountByID(int accountNum)
        {
            AccountManager account = new AccountManager();
            SqlCommand cmdFindAccountByID = new SqlCommand("SELECT * FROM Accounts WHERE accountNumber = @accountNum", connection);
            cmdFindAccountByID.Parameters.AddWithValue("@accountNum", accountNum);

            SqlDataReader reader;

            try
            {
                connection.Open();
                reader = cmdFindAccountByID.ExecuteReader();

                while (reader.Read())
                {
                    account.accountNumber = Convert.ToInt32(reader[0]);
                    account.accountType = Convert.ToString(reader[1]);
                    account.accountBalance = Convert.ToDouble(reader[2]);
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
                return account;
            }
            finally
            {
                connection.Close();
            }
            return account;
        }

        // no need for a return type, because i'm not actually doing anything with the money drawn
        public bool Withdraw (double amount, int accountNum)
        {
            double currentBalance = 0;
            SqlCommand cmdCheckBalance = new SqlCommand("SELECT accountBalance FROM Accounts WHERE accountNumber = @accountNum", connection);
            cmdCheckBalance.Parameters.AddWithValue("@accountNum", accountNum);

            try
            {
                connection.Open();
                currentBalance = (Double)cmdCheckBalance.ExecuteScalar();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            finally
            {
                connection.Close();
            }

            // need to validate amount. no negatives and nothing greater than the account balance
            if (amount < 0 || amount > currentBalance)
            {
                Console.WriteLine("You can't withdraw a negative amount, or you tried to withdraw more than your total balance. Please try again");
                return false;
            }

            currentBalance -= amount;

            //now we actually withdraw the amount
            SqlCommand cmdWithdraw = new SqlCommand("UPDATE Accounts SET accountBalance = @currentBalance WHERE accountNumber = @accountNum", connection);
            cmdWithdraw.Parameters.AddWithValue("@currentBalance", currentBalance);
            cmdWithdraw.Parameters.AddWithValue("@accountNum", accountNum);

            try
            {
                connection.Open();
                cmdWithdraw.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            finally
            {
                connection.Close();
            }

            return true;
        }

        public bool Deposit (double amount, int accountNum)
        {
            // SET accountBalance = accountBalance + amount
            SqlCommand cmdDeposit = new SqlCommand("UPDATE Accounts SET accountBalance = accountBalance + @amount WHERE accountNumber = @accountNum", connection);
            cmdDeposit.Parameters.AddWithValue("@amount", amount);
            cmdDeposit.Parameters.AddWithValue("@accountNum", accountNum);

            try
            {
                connection.Open();
                cmdDeposit.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            finally
            {
                connection.Close();
            }

            return true;
        }

        public void CloseAccount(int accountNum)
        {
            SqlCommand cmdDelete = new SqlCommand("DELETE FROM Accounts WHERE accountNumber = @accountNum", connection);
            cmdDelete.Parameters.AddWithValue("@accountNum", accountNum);

            try 
            {
                connection.Open();
                cmdDelete.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                connection.Close();
            }
            Console.WriteLine("Account closed");
        }
    }
}
