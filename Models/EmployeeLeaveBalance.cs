using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using qwikhr.Common;

namespace qwikhr.Models
{
    public class EmployeeLeaveBalance : CompanyEntity
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid EmployeeId { get; set; }

        [ForeignKey("EmployeeId")]
        public Employee? Employee { get; set; }

        public Guid LeaveTypeId { get; set; }
        [ForeignKey("LeaveTypeId")]
        public LeaveType? LeaveType { get; set; }

        public int TotalDaysAllowed { get; set; }
        public int DaysUsed { get; set; } = 0;
        public int DaysRemaining => TotalDaysAllowed - DaysUsed;
    }

}