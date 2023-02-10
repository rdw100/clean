using Leave.Domain.Common;

namespace Leave.Domain
{
    public class LeaveType : BaseDomainEntity
    {
        public string Name { get; set; }    
        public int DefaultDays { get; set; }
    }
}
