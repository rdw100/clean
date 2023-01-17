using Bank.Application.Interfaces;
using Bank.Application.Services;
using Bank.Bus;
using Bank.Core.Bus;
using Bank.Data.Context;
using Bank.Data.Repository;
using Bank.Domain.CommandHandlers;
using Bank.Domain.Commands;
using Bank.Domain.Interface;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Bank.IoC
{
    public class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection services) 
        {
            // Domain Layer - InMemoryBus MediatR
            services.AddScoped<IMediatorHandler, InMemoryBus>();

            // Domain Layer - Domain Handlers
            services.AddScoped<IRequestHandler<CreateAccountCommand, bool>, CourseCommandHandler>();

            // Application Layer
            services.AddScoped<IAccountService, AccountService>();

            // Infrastructure Layer - Data
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<BankDbContext>();
        }
    }
}