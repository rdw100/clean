using Leave.Application.DTOs.LeaveAllocation;
using Leave.Application.Responses;
using MediatR;

namespace Leave.Application.Features.LeaveAllocations.Requests.Commands
{
    public class CreateLeaveAllocationCommand : IRequest<BaseCommandResponse>
    {
        public CreateLeaveAllocationDto LeaveAllocationDto { get; set; }
    }
}
