using System.ComponentModel.DataAnnotations;

namespace qwikhr.Dtos.Employee
{
    public class EmployeeAllowanceDto
    {
        [Required]
        public string AllowanceType { get; set; } = string.Empty;

        [Required]
        public decimal Amount { get; set; }
    }
}