﻿using AutoMapper;
using Leave.Application.Features.LeaveAllocations.Requests.Commands;
using Leave.Application.Persistence.Contracts;
using MediatR;

namespace Leave.Application.Features.LeaveAllocations.Handlers.Commands
{
    public class DeleteLeaveAllocationCommandHandler : IRequestHandler<DeleteLeaveAllocationCommand>
    {
        private readonly ILeaveAllocationRepository _leaveAllocationRepository;
        private readonly IMapper _mapper;

        public DeleteLeaveAllocationCommandHandler(ILeaveAllocationRepository leaveAllocationRepository, IMapper mapper)
        {
            _leaveAllocationRepository = leaveAllocationRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteLeaveAllocationCommand request, CancellationToken cancellationToken)
        {
            var leaveAllocation = await _leaveAllocationRepository.GetById(request.Id);

            await _leaveAllocationRepository.DeleteById(leaveAllocation);

            return Unit.Value;
        }        
    }
}
