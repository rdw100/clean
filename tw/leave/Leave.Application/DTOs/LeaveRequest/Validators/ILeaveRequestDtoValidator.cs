﻿using FluentValidation;
using Leave.Application.Contracts.Persistence;

namespace Leave.Application.DTOs.LeaveRequest.Validators
{
    public class ILeaveRequestDtoValidator : AbstractValidator<ILeaveRequestDto>
    {
        private readonly ILeaveRequestRepository _leaveRequestRepository;

        public ILeaveRequestDtoValidator(ILeaveRequestRepository leaveRequestRepository)
        {
            _leaveRequestRepository = leaveRequestRepository;

            RuleFor(x => x.StartDate)
                .LessThan(x => x.EndDate)
                    .WithMessage("{PropertyName} must be before {ComparisonValue}.");

            RuleFor(x => x.EndDate)
                .GreaterThan(x => x.StartDate)
                    .WithMessage("{PropertyName} must be after {ComparisonValue}.");

            RuleFor(x => x.LeaveTypeId)
                .GreaterThan(0)
                .MustAsync(async (id, token) => {
                    var leaveTypeExists = await _leaveRequestRepository.Exists(id);
                    return leaveTypeExists;
                })
                .WithMessage("{PropertyName} does not exist.");
        }
    }
}