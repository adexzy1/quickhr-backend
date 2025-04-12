using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using qwikhr.Common;
using qwikhr.Models.Payroll;

namespace qwikhr.Models
{
    public class PayrollApproval : CompanyEntity
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        // Link to the associated PayrollRun
        public Guid PayrollRunId { get; set; }
        [ForeignKey("PayrollRunId")]
        public PayrollRun? PayrollRun { get; set; }

        // Employee who initiated the approval
        public Guid EmployeeId { get; set; }
        [ForeignKey("EmployeeId")]
        public Employee? Employee { get; set; }

        // Payroll month and total pay
        public DateTime PayrollMonth { get; set; }
        public decimal TotalPay { get; set; }

        // Approval status
        public PayrollApprovalStatus Status { get; set; } = PayrollApprovalStatus.Pending; // Enum for status

        // Current approval level
        public Guid CurrentApprovalLevelId { get; set; }
        [ForeignKey("CurrentApprovalLevelId")]
        public required CompanyPayrollApprovalLevel CurrentPayrollApprovalLevel { get; set; }

        // Approval history
        public ICollection<PayrollApprovalHistory> ApprovalHistory { get; set; } = new List<PayrollApprovalHistory>();

        // Custom metadata
        public string? Metadata { get; set; } // JSON for custom data
    }
}