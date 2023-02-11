using AutoMapper;
using Leave.Application.Contracts.Persistence;
using Leave.Application.DTOs.LeaveType;
using Leave.Application.Exceptions;
using Leave.Application.Features.LeaveTypes.Handlers.Commands;
using Leave.Application.Features.LeaveTypes.Requests.Commands;
using Leave.Application.Profiles;
using Leave.Application.UnitTests.Mocks;
using Moq;
using Shouldly;

namespace Leave.Application.UnitTests.LeaveTypes.Commands
{
    public class CreateLeaveTypeCommandHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly CreateLeaveTypeDto _leaveTypeDto;
        private readonly Mock<ILeaveTypeRepository> _mockRepo;
        private readonly CreateLeaveTypeCommandHandler _handler;

        public CreateLeaveTypeCommandHandlerTests()
        {
            _mockRepo = MockLeaveTypeRepository.GetLeaveTypeRepository();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });

            _mapper = mapperConfig.CreateMapper();
            _handler = new CreateLeaveTypeCommandHandler(_mockRepo.Object, _mapper);
            _leaveTypeDto = new CreateLeaveTypeDto
            {
                DefaultDays = 15,
                Name = "Test DTO"
            };
        }

        [Fact]
        public async Task CreateLeaveTypeListTest()
        {
            var result = await _handler.Handle(new CreateLeaveTypeCommand() { LeaveTypeDto = _leaveTypeDto }, CancellationToken.None);

            var leaveTypes = await _mockRepo.Object.GetAll();

            result.ShouldBeOfType<int>();
            
            leaveTypes.Count.ShouldBe(4);
        }

        [Fact]
        public async Task InValid_LeaveType_Added()
        {
            _leaveTypeDto.DefaultDays = -1;

            ValidationException ex = await Should.ThrowAsync<ValidationException>
                ( async () =>
                    await _handler.Handle(new CreateLeaveTypeCommand() { LeaveTypeDto = _leaveTypeDto}, CancellationToken.None)
                );

            var leaveTypes = await _mockRepo.Object.GetAll();

            leaveTypes.Count.ShouldBe(3);

            ex.ShouldNotBeNull();
        }
    }
}