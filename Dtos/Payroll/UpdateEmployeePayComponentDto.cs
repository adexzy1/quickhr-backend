using System.ComponentModel.DataAnnotations;

namespace qwikhr.Dtos.Payroll
{
    public class UpdateEmployeePayComponentDto
    {
        [Required(ErrorMessage = "Id is required.")]
        public Guid Id { get; set; } // Unique identifier for the employee pay component
        [Required(ErrorMessage = "Amount is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "Amount must be a positive number.")]
        public decimal Amount { get; set; } // The updated value for the pay component

        [Required(ErrorMessage = "Frequency is required.")]
        public string Frequency { get; set; } = "Monthly"; // Updated frequency (e.g., Monthly, Weekly)

        public DateTime? EffectiveDate { get; set; } // Optional updated effective date
        public DateTime? EndDate { get; set; } // Optional updated end date
    }
}