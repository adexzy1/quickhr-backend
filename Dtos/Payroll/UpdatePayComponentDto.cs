using System.ComponentModel.DataAnnotations;
using qwikhr.Models.Payroll;

namespace qwikhr.Dtos.Payroll
{
    public class UpdatePayComponentDto
    {
        [Required(ErrorMessage = "Name is required.")]
        public string? Name { get; set; }
        public string? Code { get; set; }
        public string? Description { get; set; }
        public PayComponentCategory? Category { get; set; }
        public CalculationType? CalculationType { get; set; }
        public string? CalculationFormula { get; set; } // For formula-based components
        public bool? IsTaxable { get; set; }
        public bool? IsRecurring { get; set; }
        public Guid? GLAccountId { get; set; }
    }
}