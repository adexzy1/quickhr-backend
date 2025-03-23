using System.ComponentModel.DataAnnotations;
using qwikhr.Common;

namespace qwikhr.Models
{
    public class CompanyPayrollApprovalLevel : CompanyEntity
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        public required string LevelName { get; set; } // e.g., "HR Review", "Finance", "CEO Approval"
        public int Order { get; set; } // 1 = First approval, 2 = Second, etc.

        public bool IsFinalApproval { get; set; } // Marks the last stage before payroll execution
    }
}