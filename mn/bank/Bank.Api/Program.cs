using Bank.Infra.Data.Context;
using Bank.Infra.IoC;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var bankConnectionString = builder.Configuration.GetConnectionString("BankDbConnection") ?? throw new InvalidOperationException("Connection string 'BankDbConnection' not found.");
builder.Services.AddDbContext<BankDbContext>(options =>
{
    options.UseSqlServer(bankConnectionString);
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

RegisterServices(builder.Services);

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

static void RegisterServices(IServiceCollection services)
{
    DependencyContainer.RegisterServices(services);
}