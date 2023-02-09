using AutoMapper;
using Leave.Application.Exceptions;
using Leave.Application.Features.LeaveTypes.Requests.Commands;
using Leave.Application.Contracts.Persistence;
using MediatR;

namespace Leave.Application.Features.LeaveTypes.Handlers.Commands
{
    public class DeleteLeaveTypeCommandHandler : IRequestHandler<DeleteLeaveTypeCommand>
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly IMapper _mapper;

        public DeleteLeaveTypeCommandHandler(ILeaveTypeRepository leaveTypeRepository, IMapper mapper)
        {
            _leaveTypeRepository = leaveTypeRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteLeaveTypeCommand request, CancellationToken cancellationToken)
        {
            var leaveType = await _leaveTypeRepository.GetById(request.Id);

            if (leaveType == null)
                throw new NotFoundException(nameof(leaveType), request.Id);

            await _leaveTypeRepository.DeleteById(leaveType);

            return Unit.Value;
        }
    }
}
