using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using qwikhr.Common;

namespace qwikhr.Models.Payroll
{
    public class PayrollEntry : CompanyEntity
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid PayrollRunId { get; set; }
        public Guid EmployeeId { get; set; }
        public decimal GrossPay { get; set; }
        public decimal TotalDeductions { get; set; }
        public decimal NetPay { get; set; }
        public decimal PAYETax { get; set; } // PAYE tax deduction
        public decimal PensionEmployee { get; set; } // Employee pension contribution
        public decimal PensionEmployer { get; set; } // Employer pension contribution
        public decimal NHF { get; set; } // National Housing Fund deduction
        public PaymentStatus Status { get; set; } = PaymentStatus.Pending; // Enum for status
        public string? PaymentMethod { get; set; }
        public string? BankAccountNumber { get; set; }

        // Navigation properties
        [ForeignKey("PayrollRunId")]
        public virtual PayrollRun? PayrollRun { get; set; }
        [ForeignKey("EmployeeId")]
        public virtual Employee? Employee { get; set; }
        public virtual ICollection<PayrollEntryDetail>? Details { get; set; } = [];
    }

    public enum PaymentStatus
    {
        Pending,
        Paid,
        Reversed
    }
}