using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using qwikhr.Common;

namespace qwikhr.Models.Leave
{
    public class EmployeeLeaveRequest : CompanyEntity
    {
        [Key]
        public Guid Id { get; set; }
        public Guid EmployeeId { get; set; }
        [ForeignKey("EmployeeId")]
        public Employee? Employee { get; set; }

        public Guid LeaveRequestId { get; set; }
        [ForeignKey("LeaveRequestId")]
        public LeaveRequest? LeaveRequest { get; set; }

        public DateTime RequestedAt { get; set; } = DateTime.UtcNow;
    }
}