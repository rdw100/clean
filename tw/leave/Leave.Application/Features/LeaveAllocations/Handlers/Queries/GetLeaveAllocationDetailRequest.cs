using Leave.Application.DTOs.LeaveAllocation;
using MediatR;

namespace Leave.Application.Features.LeaveAllocations.Handlers.Queries
{
    public class GetLeaveAllocationDetailRequest : IRequest<LeaveAllocationDto>
    {
        public int Id { get; set; }
    }
}
