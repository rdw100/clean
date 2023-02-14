using Leave.Mvc.Services.Base;

namespace Leave.Mvc.Contracts
{
    public interface ILeaveAllocationService
    {
        Task<Response<int>> CreateLeaveAllocations(int leaveTypeId);
    }
}
