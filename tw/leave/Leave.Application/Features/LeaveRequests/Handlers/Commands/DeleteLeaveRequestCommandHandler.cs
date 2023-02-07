using AutoMapper;
using Leave.Application.Exceptions;
using Leave.Application.Features.LeaveRequests.Requests.Commands;
using Leave.Application.Persistence.Contracts;
using Leave.Domain;
using MediatR;

namespace Leave.Application.Features.LeaveRequests.Handlers.Commands
{
    public class DeleteLeaveRequestCommandHandler : IRequestHandler<DeleteLeaveRequestCommand>
    {
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly IMapper _mapper;

        public DeleteLeaveRequestCommandHandler(ILeaveRequestRepository leaveRequestRepository, IMapper mapper)
        {
            _leaveRequestRepository = leaveRequestRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            var leaveRequest = await _leaveRequestRepository.GetById(request.Id);

            if (leaveRequest == null)
                throw new NotFoundException(nameof(leaveRequest), request.Id);

            await _leaveRequestRepository.DeleteById(leaveRequest);

            return Unit.Value;
        }
    }
}
