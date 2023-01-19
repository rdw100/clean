using Bank.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Bank.Infra.Data.Context
{
    public class BankDbContext : DbContext
    {
        public BankDbContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Account> Accounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                .HasData(
                    new Account() { Id = 1, Owner = "John", Balance = 100.00M },
                    new Account() { Id = 2, Owner = "John", Balance = 200.00M },
                    new Account() { Id = 3, Owner = "Jane", Balance = 300.00M }
                );
        }
    }
}