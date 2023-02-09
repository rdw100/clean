using Leave.Application.Contracts.Persistence;
using Leave.Domain;

namespace Leave.Persistence.Repositories
{
    public class LeaveTypeRepository : GenericRepository<LeaveType>, ILeaveTypeRepository
    {
        private readonly LeaveDbContext _dbContext;

        public LeaveTypeRepository(LeaveDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}