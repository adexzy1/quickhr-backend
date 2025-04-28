using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace qwikhr.Models.Payroll
{
    public class StatutoryDeduction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; } = new Guid();

        [Required, MaxLength(50)]
        public required string Name { get; set; } // "Pension", "NHF", "PAYE"

        [Required, MaxLength(10)]
        public required string Code { get; set; } // "PEN", "NHF", "PYE"

        public decimal EmployeeRate { get; set; } // 0.08 for pension
        public decimal? EmployerRate { get; set; } // 0.10 for pension

        [MaxLength(100)]
        public string? LegalReference { get; set; } // "PRA 2014 Sec 4"

        public bool ApplyToAllCompanies { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime EffectiveDate { get; set; } = DateTime.UtcNow;
    }
}