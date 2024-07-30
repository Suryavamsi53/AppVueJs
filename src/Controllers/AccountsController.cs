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
    public class AccountsController : ControllerBase
    {
        private readonly string _connectionString;

        public AccountsController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        [HttpGet]
        public async Task<IActionResult> GetAccounts()
        {
            var accounts = new List<Account>();
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("SELECT * FROM Account_table", connection);
                await connection.OpenAsync();
                var reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    accounts.Add(new Account
                    {
                        AccId = (int)reader["AccId"],
                        AccountNumber = reader["AccountNumber"].ToString(),
                        AccountStatus_id = (int)reader["AccountStatus_id"]
                    });
                }
            }
            return Ok(accounts);
        }

        [HttpPost]
        public async Task<IActionResult> PostAccount([FromBody] Account account)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("INSERT INTO Account_table (AccountNumber, AccountStatus_id) VALUES (@AccountNumber, @AccountStatus_id); SELECT SCOPE_IDENTITY();", connection);
                command.Parameters.AddWithValue("@AccountNumber", account.AccountNumber);
                command.Parameters.AddWithValue("@AccountStatus_id", account.AccountStatus_id);
                await connection.OpenAsync();
                account.AccId = Convert.ToInt32(await command.ExecuteScalarAsync());
            }
            return CreatedAtAction(nameof(GetAccounts), new { id = account.AccId }, account);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAccount(int id, [FromBody] Account account)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("UPDATE Account_table SET AccountNumber = @AccountNumber, AccountStatus_id = @AccountStatus_id WHERE AccId = @AccId", connection);
                command.Parameters.AddWithValue("@AccId", id);
                command.Parameters.AddWithValue("@AccountNumber", account.AccountNumber);
                command.Parameters.AddWithValue("@AccountStatus_id", account.AccountStatus_id);
                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccount(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("DELETE FROM Account_table WHERE AccId = @AccId", connection);
                command.Parameters.AddWithValue("@AccId", id);
                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
            }
            return NoContent();
        }
    }
}
