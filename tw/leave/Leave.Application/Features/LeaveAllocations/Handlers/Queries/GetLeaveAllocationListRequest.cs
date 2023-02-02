using Leave.Application.DTOs.LeaveAllocation;
using MediatR;

namespace Leave.Application.Features.LeaveAllocations.Handlers.Queries
{
    public class GetLeaveAllocationListRequest : IRequest<List<LeaveAllocationDto>>
    {
        public bool IsLoggedInUser { get; set; }
    }
}
