using Leave.Application.Contracts.Persistence;
using Leave.Domain;
using Microsoft.EntityFrameworkCore;

namespace Leave.Persistence.Repositories
{
    public class LeaveAllocationRepository : GenericRepository<LeaveAllocation>, ILeaveAllocationRepository
    {
        private readonly LeaveDbContext _dbContext;

        public LeaveAllocationRepository(LeaveDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAllocations(List<LeaveAllocation> allocations)
        {
            await _dbContext.AddRangeAsync(allocations);
        }

        public async Task<bool> AllocationExists(string userId, int leaveTypeId, int period)
        {
            return await _dbContext.LeaveAllocations.AnyAsync(q => q.EmployeeId == userId 
                                && q.LeaveTypeId == leaveTypeId
                                && q.Period == period);
        }

        public async Task<List<LeaveAllocation>> GetLeaveAllocationsWithDetails()
        {
            var leaveAllocations = await _dbContext.LeaveAllocations
                .Include(q => q.LeaveType)
                .ToListAsync();
            return leaveAllocations;
        }

        public async Task<List<LeaveAllocation>> GetLeaveAllocationsWithDetails(string userId)
        {
            var leaveAllocations = await _dbContext.LeaveAllocations.Where(q => q.EmployeeId == userId)
               .Include(q => q.LeaveType)
               .ToListAsync();
            return leaveAllocations;
        }

        public async Task<LeaveAllocation> GetLeaveAllocationWithDetails(int id)
        {
            var leaveAllocations = await _dbContext.LeaveAllocations
                .Include(q => q.LeaveType)
                .FirstOrDefaultAsync(q => q.Id == id);
            return leaveAllocations;
        }

        public async Task<LeaveAllocation> GetUserAllocations(string userId, int leaveTypeId)
        {
            return await _dbContext.LeaveAllocations.FirstOrDefaultAsync(
                q => q.EmployeeId == userId
                && q.LeaveTypeId == leaveTypeId
                );
        }
    }
}