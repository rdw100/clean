using AutoMapper;
using Leave.Application.DTOs.LeaveType;
using Leave.Application.Features.LeaveTypes.Requests.Queries;
using Leave.Application.Contracts.Persistence;
using MediatR;

namespace Leave.Application.Features.LeaveTypes.Handlers.Queries
{
    public class GetLeaveTypeDetailRequestHandler : IRequestHandler<GetLeaveTypeDetailRequest, LeaveTypeDto>
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly IMapper _mapper;

        public GetLeaveTypeDetailRequestHandler(ILeaveTypeRepository leaveTypeRepository, IMapper mapper)
        {
            _leaveTypeRepository = leaveTypeRepository;
            _mapper = mapper;
        }
        public async Task<LeaveTypeDto> Handle(GetLeaveTypeDetailRequest request, CancellationToken cancellationToken)
        {
            var leaveType = await _leaveTypeRepository.GetById(request.Id);
            return _mapper.Map<LeaveTypeDto>(leaveType);
        }
    }
}
