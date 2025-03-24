using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using qwikhr.Common;

namespace qwikhr.Models.ShitSchedule
{
    public class EmployeeShift : CompanyEntity
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid EmployeeId { get; set; }
        [ForeignKey("EmployeeId")]
        public Employee? Employee { get; set; }

        public Guid ShiftScheduleId { get; set; }
        [ForeignKey("ShiftScheduleId")]
        public ShiftSchedule? ShiftSchedule { get; set; }

        public DateTime AssignedAt { get; set; } = DateTime.UtcNow;
    }
}