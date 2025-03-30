using System.ComponentModel.DataAnnotations;

namespace qwikhr.Dtos.Payroll
{
    public class CreatePayComponentDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public decimal Value { get; set; }
        [Required]
        public bool IsPercentage { get; set; }
        [Required]
        public bool IsAllowance { get; set; }
    }
}