using Bank.Application.Interfaces;
using Bank.Application.Services;
using Bank.Data.Context;
using Bank.Domain.Interface;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.IoC
{
    public class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection services) 
        {
            // Application Layer
            services.AddScoped<IAccountService, AccountService>();

            // Infrastructure Layer - Data
            services.AddScoped<IAccountRepository, IAccountRepository>();
            services.AddScoped<BankDbContext>();
        }
    }
}
