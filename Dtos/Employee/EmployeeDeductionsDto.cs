using System.ComponentModel.DataAnnotations;

namespace qwikhr.Dtos.Employee
{
    public class EmployeeDeductionDto
    {
        [Required]
        public string DeductionType { get; set; } = string.Empty;

        [Required]
        public decimal Amount { get; set; }
    }
}