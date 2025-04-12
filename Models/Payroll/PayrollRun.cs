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
        public Guid RunById { get; set; } // User ID of the person who initiated the payroll
        public PayrollRunStatus Status { get; set; } = PayrollRunStatus.Draft; // Enum for status
        public string? Notes { get; set; }

        // Summary fields
        public decimal TotalGrossPay { get; set; } // Total gross pay for the run
        public decimal TotalDeductions { get; set; } // Total deductions for the run
        public decimal TotalNetPay { get; set; } // Total net pay for the run

        // Custom metadata
        public string? Metadata { get; set; } // JSON for custom data

        // Finalization timestamp
        public DateTime? FinalizedAt { get; set; } // Nullable, set when payroll is finalized

        // Navigation properties
        [ForeignKey("PayrollPeriodId")]
        public virtual PayrollPeriod? PayrollPeriod { get; set; }
        [ForeignKey("RunById")]
        public virtual Employee? RunBy { get; set; }

        // Collection of employees associated with this payroll run
        public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

        public virtual ICollection<PayrollEntry> PayrollEntries { get; set; } = new List<PayrollEntry>();
    }

    public enum PayrollRunStatus
    {
        Draft,
        Calculated,
        Submitted,
        Approved,
        Rejected,
        Finalized
    }
}