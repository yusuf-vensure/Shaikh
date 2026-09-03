using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);

string dbServer = builder.Configuration["DatabaseSettings:Server"];
string dbName = builder.Configuration["DatabaseSettings:Database"];
string dbUser = builder.Configuration["DatabaseSettings:UserId"];
string dbPass = builder.Configuration["DatabaseSettings:Password"];

string dynamicConnectionString = $"Server={dbServer};Database={dbName};User Id={dbUser};Password={dbPass};TrustServerCertificate=True;Encrypt=False;";
Console.WriteLine("Apple");
Console.WriteLine($"[DB] Connecting to Server={dbServer} Database={dbName}");
Console.WriteLine("Apple");

builder.Services.AddSingleton<IConfiguration>(provider =>
{
    var configBuilder = new ConfigurationBuilder()
        .AddConfiguration(builder.Configuration)
        .AddInMemoryCollection(new Dictionary<string, string>
        {
            {"ConnectionStrings:DefaultConnection", dynamicConnectionString}
        });
    return configBuilder.Build();
});

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddDbContext<PortfolioContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular",
        policy =>
        {
            policy.WithOrigins("https://localhost:64262") // Your Angular frontend port
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});


var app = builder.Build();

app.UseCors("AllowAngular");
app.UseDefaultFiles();
app.MapStaticAssets();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.MapFallbackToFile("/index.html");
app.MapPut("/", () => "This is a put");
app.MapPut("/downloads", () => "This is the downloads");
app.MapGet("/users/{userId:int:min(0)}/posts/{slug}", (int userId, string slug) =>
{
    return $"User ID: {userId}, Post ID: {slug}";
});
app.MapGet("/report/{year:int:min(1900)?}", (int? year) =>
{
    return $"You were born in: {year ?? 2000}";
});
app.MapGet("/search", (string? q, int page = 1) =>
{
    return $"Searching for {q} on page {page}";
});

var blogs = new List<Blog>
{
    new Blog { Title = "First Blog", Body = "This is the first blog post." },
    new Blog { Title = "Second Blog", Body = "This is the second blog post." }
};

app.MapGet("/", () => "I am root");

app.MapGet("/blogs", () =>
{
    return blogs;
});

app.MapGet("/blogs/{id}", (int id) =>
{
    if (id < 0 || id >= blogs.Count)
    {
        return Results.NotFound();
    }
    else
    {
        return Results.Ok(blogs[id]);
    }
});

app.MapDelete("/blogs/{id}", (int id) =>
{
    if (id < 0 || id >= blogs.Count)
    {
        return Results.NotFound();
    }
    else
    {
        var blog = blogs[id];
        blogs.RemoveAt(id);
        return Results.NoContent();
    }
});

app.MapPost("/blogs", (Blog blog) =>
{
    blogs.Add(blog);
    return Results.Created($"/blogs/{blogs.Count - 1}", blog);
});

app.MapPut("/blogs/{id}", (int id, Blog blog) =>{
    if (id < 0 || id >= blogs.Count)
    {
        return Results.NotFound();
    }
    else
    {
        blogs[id] = blog;
        return Results.Ok(blog);
    }
});

app.Run();

public class Blog
{
    public required string Title { get; set; }
    public required string Body { get; set; }
}

