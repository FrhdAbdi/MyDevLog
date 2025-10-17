using Microsoft.EntityFrameworkCore;
using MyDevLog.API.Data;
using MyDevLog.API.Services;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazorApp",
        policy =>
        {
            policy.WithOrigins("https://localhost:7123")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

builder.Services.AddResponseCaching();
builder.Services.AddScoped<IGitHubService, GitHubService>();
builder.Services.AddHttpClient("GitHubClient", client =>
{
    client.BaseAddress = new Uri("https://api.github.com/");
    client.DefaultRequestHeaders.Add("User-Agent", "MyDevLog-API");
    var sp = builder.Services.BuildServiceProvider();
    var configuration = sp.GetRequiredService<IConfiguration>();
    var pat = configuration["GitHub:PAT"];
    if (!string.IsNullOrEmpty(pat))
    {
        client.DefaultRequestHeaders.Add("Authorization", $"bearer {pat}");
    }
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseResponseCaching();

app.UseStaticFiles();

app.UseCors("AllowBlazorApp");

app.UseAuthorization();

app.MapControllers();

app.Run();
