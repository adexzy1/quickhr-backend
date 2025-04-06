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
        public string? PaymentMethod { get; set; }
        public string? BankAccountNumber { get; set; }
        public string? Status { get; set; } = "Pending"; // Pending, Paid, Reversed

        // Navigation properties
        [ForeignKey("PayrollRunId")]
        public virtual PayrollRun? PayrollRun { get; set; }
        [ForeignKey("EmployeeId")]
        public virtual Employee? Employee { get; set; }
        public virtual ICollection<PayrollEntryDetail>? Details { get; set; } = [];
    }
}