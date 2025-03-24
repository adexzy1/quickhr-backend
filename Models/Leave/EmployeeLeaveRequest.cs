using System.ComponentModel.DataAnnotations.Schema;

namespace qwikhr.Models.Leave
{
    public class EmployeeLeaveRequest
    {
        public Guid EmployeeId { get; set; }
        [ForeignKey("EmployeeId")]
        public Employee? Employee { get; set; }

        public Guid LeaveRequestId { get; set; }
        [ForeignKey("LeaveRequestId")]
        public LeaveRequest? LeaveRequest { get; set; }

        public DateTime RequestedAt { get; set; } = DateTime.UtcNow;
    }
}