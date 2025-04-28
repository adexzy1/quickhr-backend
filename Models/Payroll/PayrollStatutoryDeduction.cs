using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace qwikhr.Models.Payroll
{
    public class PayrollStatutoryDeduction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; } = new Guid();

        [ForeignKey("PayrollEntry")]
        public Guid PayrollEntryId { get; set; }
        [ForeignKey("PayrollEntryId")]
        public PayrollEntry PayrollEntry { get; set; } = new();

        [Required, MaxLength(10)]
        public required string RateCode { get; set; } // References StatutoryRate.Code

        [Column(TypeName = "decimal(12,2)")]
        public decimal EmployeeAmount { get; set; }

        [Column(TypeName = "decimal(12,2)")]
        public decimal? EmployerAmount { get; set; }

        [Column(TypeName = "jsonb")]
        public Dictionary<string, object> CalculationMetadata { get; set; } = [];
    }
}