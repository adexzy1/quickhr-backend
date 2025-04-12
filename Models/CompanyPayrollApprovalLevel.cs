using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using qwikhr.Common;

namespace qwikhr.Models
{
    public class CompanyPayrollApprovalLevel : CompanyEntity
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        // Name of the approval level
        public required string LevelName { get; set; } // e.g., "HR Review", "Finance", "CEO Approval"

        // Order of the approval level in the workflow
        public int Order { get; set; } // 1 = First approval, 2 = Second, etc.

        // Marks the last stage before payroll execution
        public bool IsFinalApproval { get; set; }

        // Link to the approval workflow
        public Guid WorkflowId { get; set; }
        [ForeignKey("WorkflowId")]
        public virtual ApprovalWorkflow Workflow { get; set; } = null!;

        // Link to the approver (e.g., employee or role)
        public Guid ApproverId { get; set; }
        [ForeignKey("ApproverId")]
        public virtual Employee Approver { get; set; } = null!;

        // Custom metadata
        public string? Metadata { get; set; } // JSON for custom data

        // Soft deletion flag
        public bool IsDeleted { get; set; } = false; // Soft deletion for deactivating levels
    }
}