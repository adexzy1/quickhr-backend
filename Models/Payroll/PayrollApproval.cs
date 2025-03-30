using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using qwikhr.Common;

namespace qwikhr.Models
{
    public class PayrollApproval : CompanyEntity
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid EmployeeId { get; set; }
        [ForeignKey("EmployeeId")]
        public Employee? Employee { get; set; }

        public DateTime PayrollMonth { get; set; }
        public decimal TotalPay { get; set; }

        public string Status { get; set; } = "Pending";

        public Guid CurrentApprovalLevelId { get; set; }
        [ForeignKey("CurrentApprovalLevelId")]
        public required CompanyPayrollApprovalLevel CurrentPayrollApprovalLevel { get; set; }

        public ICollection<PayrollApprovalHistory> ApprovalHistory { get; set; } = [];
    }

}