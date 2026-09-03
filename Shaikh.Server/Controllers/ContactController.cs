using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Shaikh.Server.Models; // adjust this if ContactModel.cs lives in a different namespace
using System.Data;

namespace Shaikh.Server.Controllers
{
    [ApiController]
    [Route("api/contact")]
    public class ContactController : ControllerBase
    {
        private readonly string _connectionString;

        public ContactController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        [HttpPost]
        public async Task<IActionResult> SubmitContact([FromBody] ContactModel request)
        {
            if (string.IsNullOrWhiteSpace(request.Name) ||
                string.IsNullOrWhiteSpace(request.Email) ||
                string.IsNullOrWhiteSpace(request.Subject) ||
                string.IsNullOrWhiteSpace(request.Body))
            {
                return BadRequest(new { message = "Name, Email, Subject, and Body are all required." });
            }

            using IDbConnection db = new SqlConnection(_connectionString);

            string sql = @"
        INSERT INTO dbo.Contacts
        (
            Name,
            Email,
            Subject,
            Body
        )
        VALUES
        (
            @Name,
            @Email,
            @Subject,
            @Body
        );";

            await db.ExecuteAsync(sql, request);

            return Ok(new { message = "Message received. Thank you for reaching out!" });
        }
    }
}