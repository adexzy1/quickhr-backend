using System.ComponentModel.DataAnnotations;
using qwikhr.Models.Payroll;

namespace qwikhr.Dtos.Payroll
{
    public class CreatePayComponentDto
    {
        [Required(ErrorMessage = "Name is required.")]
        public required string Name { get; set; }
        public string? Code { get; set; }
        public string? Description { get; set; }
        [Required(ErrorMessage = "Category is required.")]
        [EnumDataType(typeof(PayComponentCategory), ErrorMessage = "Invalid category.")]
        [Display(Name = "Category")]
        public PayComponentCategory Category { get; set; }
        [Required(ErrorMessage = "Calculation type is required.")]
        [EnumDataType(typeof(CalculationType), ErrorMessage = "Invalid calculation type.")]
        [Display(Name = "Calculation Type")]
        public CalculationType CalculationType { get; set; }
        public string? CalculationFormula { get; set; } // For formula-based components
        public bool IsTaxable { get; set; }
        public bool IsRecurring { get; set; }
        public Guid? GLAccountId { get; set; }
    }
}