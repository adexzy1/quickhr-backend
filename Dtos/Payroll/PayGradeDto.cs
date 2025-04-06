// PayGradeDtos.cs
using System.ComponentModel.DataAnnotations;
using qwikhr.Models.Payroll;

namespace qwikhr.Dtos.Payroll
{
    public class PayGradeDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Code { get; set; }
        public string? Description { get; set; }
        public decimal? MinimumSalary { get; set; }
        public decimal? MidPointSalary { get; set; }
        public bool? IsExempt { get; set; }
        public decimal? MaximumSalary { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public List<SimplifiedPayComponentDto> PayComponents { get; set; } = [];
    }

    public class UpdatePayGradeDto
    {
        public string Name { get; set; } = string.Empty;

        public string? Code { get; set; }

        public string? Description { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Minimum salary must be a positive number.")]
        public decimal? MinimumSalary { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Maximum salary must be a positive number.")]
        public decimal? MaximumSalary { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Midpoint salary must be a positive number.")]
        public decimal? MidPointSalary { get; set; } // Midpoint salary for benchmarking
        public bool? IsExempt { get; set; }
        // List of PayComponent IDs to associate with the PayGrade
        public List<Guid> PayComponentIds { get; set; } = [];
    }

    public class SimplifiedPayComponentDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}