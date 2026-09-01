using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class ProjectsController : ControllerBase
{
    private readonly PortfolioContext _context;

    public ProjectsController(PortfolioContext context)
    {
        _context = context;
    }

    // 1. Existing endpoint to fetch projects
    [HttpGet]
    public async Task<IActionResult> GetProjects()
    {
        var projects = await _context.Projects.ToListAsync();
        return Ok(projects);
    }

    // 2. Temporary Test Endpoint
    [HttpGet("test-db")]
    public async Task<IActionResult> TestDatabaseConnection()
    {
        try
        {
            // Tries to connect to the SQL Server database using your connection string
            bool canConnect = await _context.Database.CanConnectAsync();

            if (canConnect)
            {
                return Ok(new { success = true, message = "Successfully connected to the database!" });
            }
            else
            {
                return StatusCode(500, new { success = false, message = "Could not connect to the database." });
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { success = false, error = ex.Message });
        }
    }
}