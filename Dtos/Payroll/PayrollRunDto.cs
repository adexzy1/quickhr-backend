using System.ComponentModel.DataAnnotations;

namespace qwikhr.Dtos.Payroll
{
    public class PayrollRunDto
    {
        public Guid Id { get; set; }
        public Guid PayrollPeriodId { get; set; }
        public DateTime RunDate { get; set; }
        public Guid RunById { get; set; }
        public string Status { get; set; } = string.Empty; // Enum as string
        public string? Notes { get; set; }
        public decimal TotalGrossPay { get; set; }
        public decimal TotalDeductions { get; set; }
        public decimal TotalNetPay { get; set; }
        public DateTime? FinalizedAt { get; set; }
    }

    public class CreatePayrollRunDto
    {
        [Required(ErrorMessage = "PayrollPeriodId is required.")]
        public required Guid PayrollPeriodId { get; set; }

        [Required(ErrorMessage = "RunDate is required.")]
        public required DateTime RunDate { get; set; }

        [Required(ErrorMessage = "RunById is required.")]
        public required Guid RunById { get; set; }
        [Required(ErrorMessage = "EmployeeIds is required.")]
        public required List<Guid> EmployeeIds { get; set; } = [];

        [MaxLength(500, ErrorMessage = "Notes cannot exceed 500 characters.")]
        public string? Notes { get; set; }
    }

    public class UpdatePayrollRunDto
    {
        [Required(ErrorMessage = "PayrollPeriodId is required.")]
        public required Guid PayrollPeriodId { get; set; }

        [Required(ErrorMessage = "RunDate is required.")]
        public required DateTime RunDate { get; set; }

        [Required(ErrorMessage = "RunById is required.")]
        public required Guid RunById { get; set; }

        [MaxLength(500, ErrorMessage = "Notes cannot exceed 500 characters.")]
        public string? Notes { get; set; }

        [Required(ErrorMessage = "Status is required.")]
        [RegularExpression("^(Draft|Processing|Approved|Finalized)$", ErrorMessage = "Status must be one of the following: Draft, Processing, Approved, Finalized.")]
        public string Status { get; set; } = string.Empty; // Enum as string
    }
}