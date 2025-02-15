using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Savings_API.Context;
using Savings_API.Services;

var localCors = "_LocalCors";

var builder = WebApplication.CreateBuilder(args);

var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
var version = builder.Configuration["Version"] ?? "Unknown";
var connectionString = Environment.GetEnvironmentVariable("savingsConnString");

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: localCors,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:4200");
                      });
});

builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(opt => { opt.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)); });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Savings API",
        Version = version,
        Description = $"App version: {version}"
    });
});

builder.Services.AddAuthorization();

builder.Services.AddScoped<IGoalsService, GoalsService>();
builder.Services.AddScoped<ISavingsService, SavingsService>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.UseCors(localCors);

app.Run();
