using Api.Data;
using Api.Domain;
using Api.Middleware;
using Microsoft.EntityFrameworkCore;

const string Schema = "HMI";
const string ConectionStringName = "SqlServer";
const string TableMigrationName = "_EFMigrationsHistory";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Data
IConfigurationRoot configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();

var connectionString = configuration.GetConnectionString(ConectionStringName);

builder.Services.AddDbContext<SqlTestContext>(options =>
       options.UseSqlServer(connectionString, x => x.MigrationsHistoryTable(TableMigrationName, Schema).EnableRetryOnFailure()),
       ServiceLifetime.Transient);

// Services
builder.Services.AddTransient<IMunicipioService, MunicipioService>();

// App
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    SqlTestContext context = scope.ServiceProvider.GetRequiredService<SqlTestContext>();
    context.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<CustomMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();