using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using qwikhr.Common;

namespace qwikhr.Models.Payroll
{
    public class PayrollApprovalHistory : CompanyEntity
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid PayrollApprovalId { get; set; }
        [ForeignKey("PayrollApprovalId")]
        public required PayrollApproval PayrollApproval { get; set; }

        public Guid ApproverId { get; set; }
        [ForeignKey("ApproverId")]
        public required Employee Approver { get; set; }

        public Guid ApprovalLevelId { get; set; }
        [ForeignKey("ApprovalLevelId")]
        public required CompanyPayrollApprovalLevel ApprovalLevel { get; set; }

        public ApprovalStatus Status { get; set; } = ApprovalStatus.Pending; // Enum for status
        public string? Comments { get; set; }
        public DateTime ApprovedAt { get; set; } = DateTime.UtcNow;

        // Optional fields
        public string? Metadata { get; set; } // JSON for custom data
        public bool IsDeleted { get; set; } = false; // Soft deletion flag
    }

    public enum ApprovalStatus
    {
        Pending,
        Approved,
        Rejected
    }
}