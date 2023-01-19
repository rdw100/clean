using Bank.Application.Interfaces;
using Bank.Application.Services;
using Bank.Domain.CommandHandlers;
using Bank.Domain.Commands;
using Bank.Domain.Core.Bus;
using Bank.Domain.Interface;
using Bank.Infra.Bus;
using Bank.Infra.Data.Context;
using Bank.Infra.Data.Repository;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Bank.Infra.IoC
{
    public class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection services) 
        {
            // Domain Layer - InMemoryBus MediatR
            services.AddScoped<IMediatorHandler, InMemoryBus>();

            // Domain Layer - Domain Handlers
            services.AddScoped<IRequestHandler<CreateAccountCommand, bool>, AccountCommandHandler>();

            // Application Layer
            services.AddScoped<IAccountService, AccountService>();

            // Infrastructure Layer - Data
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<BankDbContext>();
        }
    }
}