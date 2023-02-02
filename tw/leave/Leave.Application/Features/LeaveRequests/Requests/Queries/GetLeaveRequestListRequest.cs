using Leave.Application.DTOs.LeaveRequest;
using MediatR;

namespace Leave.Application.Features.LeaveRequests.Requests.Queries
{
    public class GetLeaveRequestListRequest : IRequest<List<LeaveRequestListDto>>
    {
    }
}
