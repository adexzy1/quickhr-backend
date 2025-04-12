using System.ComponentModel.DataAnnotations;
using qwikhr.Common;

namespace qwikhr.Models.Payroll
{
    public class PayrollPeriod : CompanyEntity
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public required string Name { get; set; } // e.g., "April 2025"
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime PayDate { get; set; }
        public PayrollPeriodStatus Status { get; set; } = PayrollPeriodStatus.Draft; // Enum for status
        public bool IsLocked { get; set; } = false; // Prevent modifications after approval

        // Navigation properties
        public virtual ICollection<PayrollRun>? PayrollRuns { get; set; } = [];
    }

    public enum PayrollPeriodStatus
    {
        Draft,
        Processing,
        Approved,
        Paid
    }
}