using System.ComponentModel.DataAnnotations;
using qwikhr.Common;

namespace qwikhr.Models.Payroll
{
    public class EmployeePayComponent : CompanyEntity
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid EmployeeId { get; set; }
        public Guid PayComponentId { get; set; }
        public decimal Amount { get; set; }
        public string? Frequency { get; set; } // Weekly, Bi-weekly, Monthly, etc.
        public DateTime EffectiveDate { get; set; } = DateTime.Now;
        public DateTime? EndDate { get; set; }
        public bool IsActive { get; set; } = true;
        public string? Notes { get; set; }

        // Navigation properties
        public virtual Employee? Employee { get; set; }
        public virtual PayComponent? PayComponent { get; set; }
    }
}