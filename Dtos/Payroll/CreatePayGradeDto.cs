using System.ComponentModel.DataAnnotations;

namespace qwikhr.Dtos.Payroll
{
    public class CreatePayGradeDto
    {
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100, ErrorMessage = "Name must be between 2 and 100 characters.", MinimumLength = 2)]
        public string Name { get; set; } = string.Empty;

        [Range(0, double.MaxValue, ErrorMessage = "Base salary must be a positive number.")]
        public decimal BaseSalary { get; set; }
        [Required]
        public List<Guid> PayComponentIds { get; set; } = [];
    }
}