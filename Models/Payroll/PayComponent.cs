using System.ComponentModel.DataAnnotations;
using qwikhr.Common;

namespace qwikhr.Models.Payroll
{
    public class PayComponent : CompanyEntity
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public required string Name { get; set; }
        public string? Code { get; set; }
        public string? Description { get; set; }
        public PayComponentCategory Category { get; set; }
        public CalculationType CalculationType { get; set; }
        public string? CalculationFormula { get; set; } // For formula-based components
        public bool IsTaxable { get; set; }
        public bool IsRecurring { get; set; }
        public Guid? GLAccountId { get; set; } // For accounting integration

        // Navigation properties
        public virtual ICollection<EmployeePayComponent> EmployeeComponents { get; set; } = [];
        public virtual ICollection<PayGradeComponent> PayGradeComponents { get; set; } = [];
    }


    public enum PayComponentCategory
    {
        Earnings,
        Deduction,
        Reimbursement,
        Benefit,
        Tax
    }

    public enum CalculationType
    {
        FixedAmount,
        PercentageOfBase,
        PercentageOfEarnings,
        Formula,
        UnitBased
    }

}