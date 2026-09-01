using Azure.Core;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Shaikh.Server.Models;
using System.Data;

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

        [HttpPost("role")]
        public async Task<IActionResult> SaveRole([FromBody] VisitorTypeRequest request)
        {
            Console.WriteLine(request.VisitorType);
            using IDbConnection db = new SqlConnection(_connectionString);

            string sql = @"
        INSERT INTO VisitorTypes
        (
            VisitorType,
            TimeZone
        )
        VALUES
        (
            @VisitorType,
            @TimeZone
        );";

            await db.ExecuteAsync(sql, request);

            return Ok(new
            {
                Message = "Role saved successfully",
                Role = request.VisitorType,
                TimeZone = request.TimeZone
            });
        }
    }
}



