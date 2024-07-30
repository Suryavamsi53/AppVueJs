using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace YourNamespace.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly string _connectionString;

        public TransactionsController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        [HttpGet]
        public async Task<IActionResult> GetTransactions()
        {
            var transactions = new List<Transaction>();
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("SELECT * FROM Transaction_table", connection);
                await connection.OpenAsync();
                var reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
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
            return Ok(transactions);
        }

        [HttpPost]
        public async Task<IActionResult> PostTransaction([FromBody] Transaction transaction)
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
                await connection.OpenAsync();
                transaction.TransID = Convert.ToInt32(await command.ExecuteScalarAsync());
            }
            return CreatedAtAction(nameof(GetTransactions), new { id = transaction.TransID }, transaction);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTransaction(int id, [FromBody] Transaction transaction)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("UPDATE Transaction_table SET AccountID = @AccountID, TransTypeID = @TransTypeID, Amount = @Amount, Date = @Date, Transaction_type = @Transaction_type, UpdatedBy = @UpdatedBy, UpdatedDate = @UpdatedDate WHERE TransID = @TransID", connection);
                command.Parameters.AddWithValue("@TransID", id);
                command.Parameters.AddWithValue("@AccountID", transaction.AccountID);
                command.Parameters.AddWithValue("@TransTypeID", transaction.TransTypeID);
                command.Parameters.AddWithValue("@Amount", transaction.Amount);
                command.Parameters.AddWithValue("@Date", transaction.Date);
                command.Parameters.AddWithValue("@Transaction_type", transaction.Transaction_type);
                command.Parameters.AddWithValue("@UpdatedBy", transaction.UpdatedBy);
                command.Parameters.AddWithValue("@UpdatedDate", transaction.UpdatedDate);
                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransaction(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("DELETE FROM Transaction_table WHERE TransID = @TransID", connection);
                command.Parameters.AddWithValue("@TransID", id);
                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
            }
            return NoContent();
        }
    }
}
