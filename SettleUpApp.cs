using Microsoft.Data.SqlClient;
using System;
using dluznik3;
using System.Collections.Generic;
using System.Transactions;

class SettleUpApp
{
    private string _connectionString = @"Data Source=(local);Initial Catalog=dluhy_db;User ID=user;Password=owo;Encrypt=false;Trusted_Connection=True;";

    public void CreateTransaction(int giverId, int receiverId, decimal amount)
    {
        // Create a new transaction and add it to the database
        var transaction = new Transactions()
        {
            GID = giverId,
            RID = receiverId,
            Amount = amount,
            Date = DateTime.Now
        };

        using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            var command = new SqlCommand("INSERT INTO dbo.Transactions (GID, RID, Amount, Date) VALUES (@giverId, @receiverId, @amount, @date)", connection);
            command.Parameters.AddWithValue("@giverId", transaction.GID);
            command.Parameters.AddWithValue("@receiverId", transaction.RID);
            command.Parameters.AddWithValue("@amount", transaction.Amount);
            command.Parameters.AddWithValue("@date", transaction.Date);
            command.ExecuteNonQuery();
        }
    }

    public List<Transactions> GetTransactionsForUser(int userId)
    {
        // Retrieve all transactions for the specified user
        var transactions = new List<Transactions>();

        using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            var command = new SqlCommand("SELECT * FROM dbo.Transactions WHERE GID = @userId OR RID = @userId", connection);
            command.Parameters.AddWithValue("@userId", userId);

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var transaction = new Transactions()
                    {
                        ID = reader.GetInt32(0),
                        GID = reader.GetInt32(1),
                        RID = reader.GetInt32(2),
                        Amount = reader.GetDecimal(3),
                        Date = reader.GetDateTime(4)
                    };
                    transactions.Add(transaction);
                }
            }
        }

        return transactions;
    }

    public decimal GetBalanceForUser(int userId)
    {
        // Retrieve all transactions for the specified user
        decimal balance = 0;

        using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open(); var command = new SqlCommand("SELECT SUM(CASE WHEN GID = @userId THEN Amount ELSE -Amount END) FROM dbo.Transactions WHERE GID = @userId OR RID = @userId", connection);
            command.Parameters.AddWithValue("@userId", userId);

            using (var reader = command.ExecuteReader())
            {
                balance = reader.GetDecimal(0);
            }
        }

        return balance;
    }

    public void SettleUpBalances(int userId1, int userId2)
    {
        // Retrieve the balances for the two users
        decimal balance1 = GetBalanceForUser(userId1);
        decimal balance2 = GetBalanceForUser(userId2);

        // Determine the difference in balances
        decimal difference = balance1 - balance2;

        // If the difference is negative, swap the user IDs
        if (difference < 0)
        {
            int temp = userId1;
            userId1 = userId2;
            userId2 = temp;
            difference = -difference;
        }

        // Create a new transaction to transfer the difference in balances
        CreateTransaction(userId1, userId2, difference);
    }
}
