using Leave.Application.DTOs.LeaveType;
using Leave.Application.Responses;
using MediatR;

namespace Leave.Application.Features.LeaveTypes.Requests.Commands
{
    public class CreateLeaveTypeCommand : IRequest<BaseCommandResponse>
    {
        public CreateLeaveTypeDto LeaveTypeDto { get; set; }
    }
}
