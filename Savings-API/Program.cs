using Microsoft.EntityFrameworkCore;
using Savings_API.Context;
using Savings_API.Services;

var builder = WebApplication.CreateBuilder(args);

var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
var connectionString = configuration.GetValue<string>("ConnectionStrings:Default");

builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(opt => { opt.UseSqlServer(connectionString); });
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

app.Run();
