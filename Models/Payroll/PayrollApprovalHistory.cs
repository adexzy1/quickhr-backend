using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using qwikhr.Common;

namespace qwikhr.Models
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

        public required string Status { get; set; }
        public string? Comments { get; set; }

        public DateTime ApprovedAt { get; set; } = DateTime.UtcNow;
    }

}