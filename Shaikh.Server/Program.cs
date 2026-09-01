using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);

string dbServer = builder.Configuration["DatabaseSettings:Server"];
string dbName = builder.Configuration["DatabaseSettings:Database"];
string dbUser = builder.Configuration["DatabaseSettings:UserId"];
string dbPass = builder.Configuration["DatabaseSettings:Password"];

string dynamicConnectionString = $"Server={dbServer};Database={dbName};User Id={dbUser};Password={dbPass};TrustServerCertificate=True;Encrypt=False;";

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
app.Run();