using AutoMapper;
//using FluentValidation;
using Leave.Application.Exceptions;
using Leave.Application.DTOs.LeaveType.Validators;
using Leave.Application.Features.LeaveTypes.Requests.Commands;
using Leave.Application.Persistence.Contracts;
using MediatR;

namespace Leave.Application.Features.LeaveTypes.Handlers.Commands
{
    public class UpdateLeaveTypeCommandHandler : IRequestHandler<UpdateLeaveTypeCommand, Unit>
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly IMapper _mapper;

        public UpdateLeaveTypeCommandHandler(ILeaveTypeRepository leaveTypeRepository, IMapper mapper)
        {
            _leaveTypeRepository = leaveTypeRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateLeaveTypeCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateLeaveTypeDtoValidator();
            var validationResult = await validator.ValidateAsync(request.LeaveTypeDto);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            var leaveType = await _leaveTypeRepository.GetById(request.LeaveTypeDto.Id);
            
            _mapper.Map(request.LeaveTypeDto, leaveType);

            await _leaveTypeRepository.Update(leaveType);

            return Unit.Value;
        }
    }
}