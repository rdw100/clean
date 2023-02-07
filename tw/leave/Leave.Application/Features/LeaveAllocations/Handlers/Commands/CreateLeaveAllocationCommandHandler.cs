using AutoMapper;
using Leave.Application.DTOs.LeaveAllocation.Validators;
using Leave.Application.DTOs.LeaveRequest.Validators;
using Leave.Application.Features.LeaveAllocations.Requests.Commands;
using Leave.Application.Persistence.Contracts;
using Leave.Domain;
using MediatR;

namespace Leave.Application.Features.LeaveAllocations.Handlers.Commands
{
    public class CreateLeaveAllocationCommandHandler : IRequestHandler<CreateLeaveAllocationCommand, int>
    {
        private readonly ILeaveAllocationRepository _leaveAllocationRepository;
        private readonly IMapper _mapper;

        public CreateLeaveAllocationCommandHandler(ILeaveAllocationRepository leaveAllocationRepository, IMapper mapper)
        {
            _leaveAllocationRepository = leaveAllocationRepository;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateLeaveAllocationCommand request, CancellationToken cancellationToken)
        {
            //var validator = new CreateLeaveAllocationDtoValidator(_leaveAllocationRepository);
            //var validationResult = await validator.ValidateAsync(request.LeaveAllocationDto);

            //if (validationResult.IsValid == false)
            //    throw new Exception();

            var leaveAllocation = _mapper.Map<LeaveAllocation>(request.LeaveAllocationDto);

            leaveAllocation = await _leaveAllocationRepository.Add(leaveAllocation);

            return leaveAllocation.Id;
        }
    }
}