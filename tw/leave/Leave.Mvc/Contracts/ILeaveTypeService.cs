using Leave.Mvc.Models;
using Leave.Mvc.Services.Base;

namespace Leave.Mvc.Contracts
{
    public interface ILeaveTypeService
    {
        Task<List<LeaveTypeVM>> GetLeaveTypes();
        Task<LeaveTypeVM> GetLeaveTypeDetails(int id);
        Task<Response<int>> CreateLeaveType(CreateLeaveTypeVM leaveType);
        Task<Response<int>> UpdateLeaveType(int id, LeaveTypeVM leaveType);
        Task<Response<int>> DeleteLeaveType(int id);
    }
}