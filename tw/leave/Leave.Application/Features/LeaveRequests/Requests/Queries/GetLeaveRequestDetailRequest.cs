using Leave.Application.DTOs.LeaveRequest;
using MediatR;

namespace Leave.Application.Features.LeaveRequests.Requests.Queries
{
    public class GetLeaveRequestDetailRequest : IRequest<LeaveRequestDto>
    {
        public int Id { get; set; }
    }
}
