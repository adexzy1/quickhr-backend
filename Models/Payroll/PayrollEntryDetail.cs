using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using qwikhr.Common;

namespace qwikhr.Models.Payroll
{
    public class PayrollEntryDetail : CompanyEntity
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid PayrollEntryId { get; set; }
        public Guid PayComponentId { get; set; }
        public decimal Amount { get; set; }
        public decimal Units { get; set; } // Hours, days, etc.
        public string? Rate { get; set; } // $X per unit
        public string? Description { get; set; }
        public string Category { get; set; } = "Allowance"; // Allowance, Deduction, Other
        public bool IsTaxable { get; set; } = false; // Default to true
        // Navigation properties
        [ForeignKey("PayrollEntryId")]
        public virtual PayrollEntry? PayrollEntry { get; set; }
        [ForeignKey("PayComponentId")]
        public virtual PayComponent? PayComponent { get; set; }

        // Optional fields
        public string? Metadata { get; set; } // JSON for custom data
        public bool IsDeleted { get; set; } = false; // Soft deletion flag
    }
}