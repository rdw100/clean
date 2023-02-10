using Leave.Application.Contracts.Persistence;
using Leave.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Leave.Persistence
{
    public static class PersistenceServicesRegistration
    {
        public static IServiceCollection ConfigurePersistenceServices
        (
            this IServiceCollection services, 
            IConfiguration configuration
        )
        {
            services.AddDbContext<LeaveDbContext>
                (
                    options =>
                    options.UseSqlServer
                    (
                        configuration.GetConnectionString("LeaveConnectionString")
                    )
                );

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            services.AddScoped<ILeaveTypeRepository, LeaveTypeRepository>();
            services.AddScoped<ILeaveRequestRepository, LeaveRequestRepository>();
            services.AddScoped<ILeaveAllocationRepository, LeaveAllocationRepository>();

            return services;
        }
    }
}
