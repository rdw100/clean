using AutoMapper;
using Leave.Application.DTOs.LeaveAllocation;
using Leave.Application.DTOs.LeaveRequest;
using Leave.Application.DTOs.LeaveType;
using Leave.Domain;

namespace Leave.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<LeaveRequest, LeaveRequestDto>().ReverseMap();
            CreateMap<LeaveRequest, LeaveRequestListDto>().ReverseMap();
            CreateMap<LeaveAllocation, LeaveAllocationDto>().ReverseMap();
            CreateMap<LeaveType, LeaveTypeDto>().ReverseMap();
        }
    }
}
