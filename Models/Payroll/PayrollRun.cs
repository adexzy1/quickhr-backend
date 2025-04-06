using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using qwikhr.Common;

namespace qwikhr.Models.Payroll
{
    public class PayrollRun : CompanyEntity
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid PayrollPeriodId { get; set; }
        public DateTime RunDate { get; set; }
        public Guid RunById { get; set; } // User ID
        public string Status { get; set; } = "Draft"; // Draft, Calculated, Approved, Processed
        public string? Notes { get; set; }

        // Navigation properties
        [ForeignKey("PayrollPeriodId")]
        public virtual PayrollPeriod? PayrollPeriod { get; set; }
        [ForeignKey("RunById")]
        public virtual Employee? RunBy { get; set; }
        public virtual ICollection<PayrollEntry> PayrollEntries { get; set; } = [];
    }
}