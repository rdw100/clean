using Leave.Application.DTOs.Common;

namespace Leave.Application.DTOs.LeaveAllocation
{
    public class CreateLeaveAllocationDto : BaseDto
    {
        public int NumberOfDays { get; set; }
        public int LeaveTypeId { get; set; }
        public int Period { get; set; }
    }
}
