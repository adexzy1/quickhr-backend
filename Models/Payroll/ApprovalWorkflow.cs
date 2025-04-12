using System.ComponentModel.DataAnnotations;
using qwikhr.Common;

namespace qwikhr.Models
{
    public class ApprovalWorkflow : CompanyEntity
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        // Name of the workflow
        public required string Name { get; set; } // e.g., "HR Workflow", "Finance Workflow"

        // Description of the workflow
        public string? Description { get; set; }

        // Navigation property for approval levels
        public virtual ICollection<CompanyPayrollApprovalLevel> ApprovalLevels { get; set; } = new List<CompanyPayrollApprovalLevel>();
    }
}