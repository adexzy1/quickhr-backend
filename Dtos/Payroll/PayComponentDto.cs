using System.ComponentModel.DataAnnotations;

namespace qwikhr.Dtos.Payroll
{
    public class PayComponentDto
    {
        public Guid Id { get; set; }

        // Basic Information
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
        public string Name { get; set; } = string.Empty; // Name of the pay component

        [StringLength(50, ErrorMessage = "Code cannot exceed 50 characters.")]
        public string? Code { get; set; } // Optional code for the pay component

        [StringLength(250, ErrorMessage = "Description cannot exceed 250 characters.")]
        public string? Description { get; set; } // Optional description of the pay component

        // Category and Calculation
        [Required(ErrorMessage = "Category is required.")]
        public string Category { get; set; } = string.Empty; // Earnings, Deduction, etc. (enum as string)

        [Required(ErrorMessage = "Calculation Type is required.")]
        public string CalculationType { get; set; } = string.Empty; // FixedAmount, PercentageOfBase, etc. (enum as string)

        [StringLength(500, ErrorMessage = "Calculation formula cannot exceed 500 characters.")]
        public string? CalculationFormula { get; set; } // Optional formula for dynamic calculations

        // Flags
        [Required(ErrorMessage = "IsTaxable is required.")]
        public bool IsTaxable { get; set; } // Indicates if the component is taxable

        [Required(ErrorMessage = "IsRecurring is required.")]
        public bool IsRecurring { get; set; } // Indicates if the component is recurring

        // Accounting Integration
        public Guid? GLAccountId { get; set; } // Optional General Ledger account ID
    }
}