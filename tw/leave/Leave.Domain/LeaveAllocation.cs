using Leave.Domain.Common;

namespace Leave.Domain
{
    public class LeaveAllocation : BaseDomainEntity
    {
        public int NumberOfDays { get; set; }
        public LeaveType LeaveType { get; set; }
        public int LeaveTypeId { get; set; }    
        public int Period { get; set; }       
    }
}