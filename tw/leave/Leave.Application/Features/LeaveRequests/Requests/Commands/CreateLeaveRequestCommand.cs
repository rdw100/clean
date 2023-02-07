using Leave.Application.DTOs.LeaveRequest;
using Leave.Application.Responses;
using MediatR;

namespace Leave.Application.Features.LeaveRequests.Requests.Commands
{
    public class CreateLeaveRequestCommand : IRequest<BaseCommandResponse>
    {
        public CreateLeaveRequestDto LeaveRequestDto { get; set; }
    }
}
