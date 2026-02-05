using Microsoft.EntityFrameworkCore;
using SuperStore.Infrastructure.Data;
using SuperStore.Application.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register DbContext with PostgreSQL provider (Npgsql)
// Connection string can come from appsettings.json or environment variables.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);


builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IUserService, UserService>();


builder.Services.AddHealthChecks()
    .AddNpgSql(builder.Configuration.GetConnectionString("DefaultConnection")!);

var app = builder.Build();

// Enable Swagger UI in Development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Redirect HTTP to HTTPS 
app.UseHttpsRedirection();

// Authorization middleware (authentication later)
app.UseAuthorization();

// Map controllers (activates API routes)
app.MapControllers();

app.MapHealthChecks("/health");

app.Run();
