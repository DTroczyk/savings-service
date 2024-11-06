using Microsoft.EntityFrameworkCore;
using Savings_API.Context;
using Savings_API.Services;

var anyCors = "_LocalCors";

var builder = WebApplication.CreateBuilder(args);

var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
var connectionString = Environment.GetEnvironmentVariable("savingsConnString");

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: anyCors,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:4200");
                      });
});

builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(opt => { opt.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)); });
builder.Services.AddDbContext<AuthDbContext>(opt => { opt.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)); });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ISavingsService, SavingsService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors(anyCors);

app.Run();
