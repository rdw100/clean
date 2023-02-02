using Leave.Application.DTOs.LeaveType;
using MediatR;

namespace Leave.Application.Features.LeaveTypes.Requests.Queries
{
    public class GetLeaveTypeListRequest : IRequest<List<LeaveTypeDto>>
    {
    }
}
