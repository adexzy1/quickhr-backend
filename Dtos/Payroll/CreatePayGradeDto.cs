using System.ComponentModel.DataAnnotations;

namespace qwikhr.Dtos.Payroll
{
    public class CreatePayGradeDto
    {
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; } = string.Empty;

        public string? Code { get; set; } // Optional code for the pay grade

        public string? Description { get; set; } // Optional description of the pay grade

        [Range(0, double.MaxValue, ErrorMessage = "Minimum salary must be a positive number.")]
        public decimal MinimumSalary { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Maximum salary must be a positive number.")]
        public decimal MaximumSalary { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Midpoint salary must be a positive number.")]
        public decimal MidPointSalary { get; set; } // Midpoint salary for benchmarking

        public bool IsExempt { get; set; }
        // List of PayComponent IDs to associate with the PayGrade
        public List<Guid> PayComponentIds { get; set; } = [];
    }
}