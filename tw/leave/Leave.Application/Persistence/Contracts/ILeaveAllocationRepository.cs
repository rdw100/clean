using Leave.Domain;

namespace Leave.Application.Persistence.Contracts
{
    public interface ILeaveAllocationRepository : IGenericRepository<LeaveAllocation>
    {
        Task<LeaveAllocation> GetLeaveAllocationWithDeatils(int id);
        Task<List<LeaveAllocation>> GetLeaveAllocationWithDetails(int id);
    }
}
