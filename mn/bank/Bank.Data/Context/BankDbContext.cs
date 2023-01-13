using Bank.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Bank.Data.Context
{
    public class BankDbContext : DbContext
    {
        public BankDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Account> Accounts { get; set; }
    }
}