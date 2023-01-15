using Bank.Application.Interfaces;
using Bank.Application.Services;
using Bank.Data.Repository;
using Bank.Domain.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace Bank.IoC
{
    public class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection services) 
        {
            // Application Layer
            services.AddScoped<IAccountService, AccountService>();

            // Infrastructure Layer - Data
            services.AddScoped<IAccountRepository, AccountRepository>();
            //services.AddScoped<BankDbContext>();
        }
    }
}
