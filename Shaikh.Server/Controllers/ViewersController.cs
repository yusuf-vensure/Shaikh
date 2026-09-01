using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Dapper;
using System.Data;
using Microsoft.Extensions.Configuration;
namespace Shaikh.Server.Controllers
{
    [ApiController]
    [Route("api/viewers")] // Explicitly hardcode the path segment
    public class ViewersController : ControllerBase
    {
        private readonly string _connectionString;

        public ViewersController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        [HttpPost("increment")]
        public async Task<IActionResult> IncrementViewers()
        {
            using IDbConnection db = new SqlConnection(_connectionString);

            string updateSql = "UPDATE TotalViewers SET ViewerCount = ViewerCount + 1;";
            await db.ExecuteAsync(updateSql);

            string selectSql = "SELECT ISNULL(ViewerCount, 0) FROM TotalViewers;";
            int newCount = await db.QuerySingleAsync<int>(selectSql);

            return Ok(new { viewerCount = newCount });
        }
    }
}