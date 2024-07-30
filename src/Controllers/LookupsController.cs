using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace VueJs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LookupsController : ControllerBase
    {
        private readonly string _connectionString;

        public LookupsController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        [HttpGet]
        public async Task<IActionResult> GetLookups()
        {
            var lookups = new List<Lookup>();
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("SELECT * FROM Lookup_table", connection);
                await connection.OpenAsync();
                var reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
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
            return Ok(lookups);
        }

        [HttpPost]
        public async Task<IActionResult> PostLookup([FromBody] Lookup lookup)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("INSERT INTO Lookup_table (Lookup_Code, Lookup_Type, Lookup_Desc, Is_Active) VALUES (@Lookup_Code, @Lookup_Type, @Lookup_Desc, @Is_Active); SELECT SCOPE_IDENTITY();", connection);
                command.Parameters.AddWithValue("@Lookup_Code", lookup.Lookup_Code);
                command.Parameters.AddWithValue("@Lookup_Type", lookup.Lookup_Type);
                command.Parameters.AddWithValue("@Lookup_Desc", lookup.Lookup_Desc);
                command.Parameters.AddWithValue("@Is_Active", lookup.Is_Active);
                await connection.OpenAsync();
                lookup.Lookup_Id = Convert.ToInt32(await command.ExecuteScalarAsync());
            }
            return CreatedAtAction(nameof(GetLookups), new { id = lookup.Lookup_Id }, lookup);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutLookup(int id, [FromBody] Lookup lookup)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("UPDATE Lookup_table SET Lookup_Code = @Lookup_Code, Lookup_Type = @Lookup_Type, Lookup_Desc = @Lookup_Desc, Is_Active = @Is_Active WHERE Lookup_Id = @Lookup_Id", connection);
                command.Parameters.AddWithValue("@Lookup_Id", id);
                command.Parameters.AddWithValue("@Lookup_Code", lookup.Lookup_Code);
                command.Parameters.AddWithValue("@Lookup_Type", lookup.Lookup_Type);
                command.Parameters.AddWithValue("@Lookup_Desc", lookup.Lookup_Desc);
                command.Parameters.AddWithValue("@Is_Active", lookup.Is_Active);
                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLookup(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("DELETE FROM Lookup_table WHERE Lookup_Id = @Lookup_Id", connection);
                command.Parameters.AddWithValue("@Lookup_Id", id);
                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
            }
            return NoContent();
        }
    }
}
