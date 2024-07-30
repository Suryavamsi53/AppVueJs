using System.Collections.Generic;
using System.Data.SqlClient;

public class YourService : IYourService
{
    private readonly string _connectionString = "Server=localhost\\SQLEXPRESS;Database=VuejsApp;Trusted_Connection=True;";

    // Lookup operations
    public List<Lookup> GetLookups()
    {
        var lookups = new List<Lookup>();
        using (var connection = new SqlConnection(_connectionString))
        {
            var command = new SqlCommand("SELECT * FROM Lookup_table", connection);
            connection.Open();
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                lookups.Add(new Lookup
                {
                    Lookup_Id = (int)reader["Lookup_Id"],
                    Lookup_Code = (int)reader["Lookup_Code"],
                    Lookup_Type = reader["Lookup_Type"].ToString(),
                    Lookup_Desc = reader["Lookup_Desc"].ToString(),
                    Is_Active = reader["Is_Active"].ToString()
                });
            }
        }
        return lookups;
    }

    public Lookup GetLookupById(int id)
    {
        Lookup lookup = null;
        using (var connection = new SqlConnection(_connectionString))
        {
            var command = new SqlCommand("SELECT * FROM Lookup_table WHERE Lookup_Id = @Lookup_Id", connection);
            command.Parameters.AddWithValue("@Lookup_Id", id);
            connection.Open();
            var reader = command.ExecuteReader();
            if (reader.Read())
            {
                lookup = new Lookup
                {
                    Lookup_Id = (int)reader["Lookup_Id"],
                    Lookup_Code = (int)reader["Lookup_Code"],
                    Lookup_Type = reader["Lookup_Type"].ToString(),
                    Lookup_Desc = reader["Lookup_Desc"].ToString(),
                    Is_Active = reader["Is_Active"].ToString()
                };
            }
        }
        return lookup;
    }

    public void AddLookup(Lookup lookup)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            var command = new SqlCommand("INSERT INTO Lookup_table (Lookup_Code, Lookup_Type, Lookup_Desc, Is_Active) VALUES (@Lookup_Code, @Lookup_Type, @Lookup_Desc, @Is_Active); SELECT SCOPE_IDENTITY();", connection);
            command.Parameters.AddWithValue("@Lookup_Code", lookup.Lookup_Code);
            command.Parameters.AddWithValue("@Lookup_Type", lookup.Lookup_Type);
            command.Parameters.AddWithValue("@Lookup_Desc", lookup.Lookup_Desc);
            command.Parameters.AddWithValue("@Is_Active", lookup.Is_Active);
            connection.Open();
            lookup.Lookup_Id = Convert.ToInt32(command.ExecuteScalar());
        }
    }

    public void UpdateLookup(Lookup lookup)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            var command = new SqlCommand("UPDATE Lookup_table SET Lookup_Code = @Lookup_Code, Lookup_Type = @Lookup_Type, Lookup_Desc = @Lookup_Desc, Is_Active = @Is_Active WHERE Lookup_Id = @Lookup_Id", connection);
            command.Parameters.AddWithValue("@Lookup_Id", lookup.Lookup_Id);
            command.Parameters.AddWithValue("@Lookup_Code", lookup.Lookup_Code);
            command.Parameters.AddWithValue("@Lookup_Type", lookup.Lookup_Type);
            command.Parameters.AddWithValue("@Lookup_Desc", lookup.Lookup_Desc);
            command.Parameters.AddWithValue("@Is_Active", lookup.Is_Active);
            connection.Open();
            command.ExecuteNonQuery();
        }
    }

    public void DeleteLookup(int id)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            var command = new SqlCommand("DELETE FROM Lookup_table WHERE Lookup_Id = @Lookup_Id", connection);
            command.Parameters.AddWithValue("@Lookup_Id", id);
            connection.Open();
            command.ExecuteNonQuery();
        }
    }

    // Account operations
    public List<Account> GetAccounts()
    {
        var accounts = new List<Account>();
        using (var connection = new SqlConnection(_connectionString))
        {
            var command = new SqlCommand("SELECT * FROM Account_table", connection);
            connection.Open();
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                accounts.Add(new Account
                {
                    AccId = (int)reader["AccId"],
                    AccountNumber = reader["AccountNumber"].ToString(),
                    AccountStatus_id = (int)reader["AccountStatus_id"]
                });
            }
        }
        return accounts;
    }

    public Account GetAccountById(int id)
    {
        Account account = null;
        using (var connection = new SqlConnection(_connectionString))
        {
            var command = new SqlCommand("SELECT * FROM Account_table WHERE AccId = @AccId", connection);
            command.Parameters.AddWithValue("@AccId", id);
            connection.Open();
            var reader = command.ExecuteReader();
            if (reader.Read())
            {
                account = new Account
                {
                    AccId = (int)reader["AccId"],
                    AccountNumber = reader["AccountNumber"].ToString(),
                    AccountStatus_id = (int)reader["AccountStatus_id"]
                };
            }
        }
        return account;
    }

    public void AddAccount(Account account)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            var command = new SqlCommand("INSERT INTO Account_table (AccountNumber, AccountStatus_id) VALUES (@AccountNumber, @AccountStatus_id); SELECT SCOPE_IDENTITY();", connection);
            command.Parameters.AddWithValue("@AccountNumber", account.AccountNumber);
            command.Parameters.AddWithValue("@AccountStatus_id", account.AccountStatus_id);
            connection.Open();
            account.AccId = Convert.ToInt32(command.ExecuteScalar());
        }
    }

    public void UpdateAccount(Account account)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            var command = new SqlCommand("UPDATE Account_table SET AccountNumber = @AccountNumber, AccountStatus_id = @AccountStatus_id WHERE AccId = @AccId", connection);
            command.Parameters.AddWithValue("@AccId", account.AccId);
            command.Parameters.AddWithValue("@AccountNumber", account.AccountNumber);
            command.Parameters.AddWithValue("@AccountStatus_id", account.AccountStatus_id);
            connection.Open();
            command.ExecuteNonQuery();
        }
    }

    public void DeleteAccount(int id)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            var command = new SqlCommand("DELETE FROM Account_table WHERE AccId = @AccId", connection);
            command.Parameters.AddWithValue("@AccId", id);
            connection.Open();
            command.ExecuteNonQuery();
        }
    }

    // Transaction operations
    public List<Transaction> GetTransactions()
    {
        var transactions = new List<Transaction>();
        using (var connection = new SqlConnection(_connectionString))
        {
            var command = new SqlCommand("SELECT * FROM Transaction_table", connection);
            connection.Open();
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                transactions.Add(new Transaction
                {
                    TransID = (int)reader["TransID"],
                    AccountID = (int)reader["AccountID"],
                    TransTypeID = (int)reader["TransTypeID"],
                    Amount = (decimal)reader["Amount"],
                    Date = (DateTime)reader["Date"],
                    Transaction_type = reader["Transaction_type"].ToString(),
                    CreatedDate = (DateTime)reader["CreatedDate"],
                    CreatedBy = reader["CreatedBy"].ToString(),
                    UpdatedDate = reader["UpdatedDate"] != DBNull.Value ? (DateTime)reader["UpdatedDate"] : (DateTime?)null,
                    UpdatedBy = reader["UpdatedBy"].ToString()
                });
            }
        }
        return transactions;
    }

    public Transaction GetTransactionById(int id)
    {
        Transaction transaction = null;
        using (var connection = new SqlConnection(_connectionString))
        {
            var command = new SqlCommand("SELECT * FROM Transaction_table WHERE TransID = @TransID", connection);
            command.Parameters.AddWithValue("@TransID", id);
            connection.Open();
            var reader = command.ExecuteReader();
            if (reader.Read())
            {
                transaction = new Transaction
                {
                    TransID = (int)reader["TransID"],
                    AccountID = (int)reader["AccountID"],
                    TransTypeID = (int)reader["TransTypeID"],
                    Amount = (decimal)reader["Amount"],
                    Date = (DateTime)reader["Date"],
                    Transaction_type = reader["Transaction_type"].ToString(),
                    CreatedDate = (DateTime)reader["CreatedDate"],
                    CreatedBy = reader["CreatedBy"].ToString(),
                    UpdatedDate = reader["UpdatedDate"] != DBNull.Value ? (DateTime)reader["UpdatedDate"] : (DateTime?)null,
                    UpdatedBy = reader["UpdatedBy"].ToString()
                };
            }
        }
        return transaction;
    }

    public void AddTransaction(Transaction transaction)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            var command = new SqlCommand("INSERT INTO Transaction_table (AccountID, TransTypeID, Amount, Date, Transaction_type, CreatedBy, CreatedDate) VALUES (@AccountID, @TransTypeID, @Amount, @Date, @Transaction_type, @CreatedBy, @CreatedDate); SELECT SCOPE_IDENTITY();", connection);
            command.Parameters.AddWithValue("@AccountID", transaction.AccountID);
            command.Parameters.AddWithValue("@TransTypeID", transaction.TransTypeID);
            command.Parameters.AddWithValue("@Amount", transaction.Amount);
            command.Parameters.AddWithValue("@Date", transaction.Date);
            command.Parameters.AddWithValue("@Transaction_type", transaction.Transaction_type);
            command.Parameters.AddWithValue("@CreatedBy", transaction.CreatedBy);
            command.Parameters.AddWithValue("@CreatedDate", transaction.CreatedDate);
            connection.Open();
            transaction.TransID = Convert.ToInt32(command.ExecuteScalar());
        }
    }

    public void UpdateTransaction(Transaction transaction)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            var command = new SqlCommand("UPDATE Transaction_table SET AccountID = @AccountID, TransTypeID = @TransTypeID, Amount = @Amount, Date = @Date, Transaction_type = @Transaction_type, UpdatedBy = @UpdatedBy, UpdatedDate = @UpdatedDate WHERE TransID = @TransID", connection);
            command.Parameters.AddWithValue("@TransID", transaction.TransID);
            command.Parameters.AddWithValue("@AccountID", transaction.AccountID);
            command.Parameters.AddWithValue("@TransTypeID", transaction.TransTypeID);
            command.Parameters.AddWithValue("@Amount", transaction.Amount);
            command.Parameters.AddWithValue("@Date", transaction.Date);
            command.Parameters.AddWithValue("@Transaction_type", transaction.Transaction_type);
            command.Parameters.AddWithValue("@UpdatedBy", transaction.UpdatedBy);
            command.Parameters.AddWithValue("@UpdatedDate", transaction.UpdatedDate);
            connection.Open();
            command.ExecuteNonQuery();
        }
    }

    public void DeleteTransaction(int id)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            var command = new SqlCommand("DELETE FROM Transaction_table WHERE TransID = @TransID", connection);
            command.Parameters.AddWithValue("@TransID", id);
            connection.Open();
            command.ExecuteNonQuery();
        }
    }

    // Repeat for Address and AccountHolder
}
