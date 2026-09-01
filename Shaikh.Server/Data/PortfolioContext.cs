using Microsoft.EntityFrameworkCore;

public class PortfolioContext : DbContext
{
    public PortfolioContext(DbContextOptions<PortfolioContext> options) : base(options) { }
    public DbSet<ProjectModel> Projects { get; set; }
}

public class ProjectModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
}