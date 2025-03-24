using System.ComponentModel.DataAnnotations;
using qwikhr.Common;

namespace qwikhr.Models
{
    public class ShiftSchedule : CompanyEntity
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid EmployeeId { get; set; }
        public Employee? Employee { get; set; }

        public DateTime ShiftStart { get; set; }
        public DateTime ShiftEnd { get; set; }

        public bool IsOvernightShift => ShiftEnd.Date > ShiftStart.Date;
    }

}