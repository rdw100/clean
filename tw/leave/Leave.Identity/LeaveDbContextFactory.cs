using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Leave.Identity
{
    public class LeaveManagementIdentityDbContextFactory : IDesignTimeDbContextFactory<LeaveIdentityDbContext>
    {
        public LeaveIdentityDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var builder = new DbContextOptionsBuilder<LeaveIdentityDbContext>();
            var connectionString = configuration.GetConnectionString("LeaveIdentityConnectionString");

            builder.UseSqlServer(connectionString);

            return new LeaveIdentityDbContext(builder.Options);
        }
    }
}