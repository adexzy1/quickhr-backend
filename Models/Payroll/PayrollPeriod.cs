using System.ComponentModel.DataAnnotations;
using qwikhr.Common;

namespace qwikhr.Models.Payroll
{
    public class PayrollPeriod : CompanyEntity
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public required string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime PayDate { get; set; }
        public string Status { get; set; } = "Draft"; // Draft, Processing, Approved, Paid

        // Navigation properties
        public virtual ICollection<PayrollRun>? PayrollRuns { get; set; }
    }
}