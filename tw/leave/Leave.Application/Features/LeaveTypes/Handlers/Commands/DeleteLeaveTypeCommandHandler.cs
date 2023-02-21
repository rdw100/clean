using AutoMapper;
using Leave.Application.Exceptions;
using Leave.Application.Features.LeaveTypes.Requests.Commands;
using Leave.Application.Contracts.Persistence;
using MediatR;
using Leave.Application.Features.LeaveAllocations.Requests.Commands;
using Leave.Domain;

namespace Leave.Application.Features.LeaveTypes.Handlers.Commands
{
    public class DeleteLeaveTypeCommandHandler : IRequestHandler<DeleteLeaveTypeCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteLeaveTypeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteLeaveTypeCommand request, CancellationToken cancellationToken)
        {
            var leaveType = await _unitOfWork.LeaveTypeRepository.GetById(request.Id);

            if (leaveType == null)
                throw new NotFoundException(nameof(LeaveType), request.Id);

            await _unitOfWork.LeaveTypeRepository.Delete(leaveType);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}
