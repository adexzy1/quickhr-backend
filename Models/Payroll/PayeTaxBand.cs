using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace qwikhr.Models.Payroll;
public class PayeTaxBand
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Column(TypeName = "decimal(12,2)")]
    public decimal LowerBound { get; set; }

    [Column(TypeName = "decimal(12,2)")]
    public decimal? UpperBound { get; set; }

    [Column(TypeName = "decimal(5,4)")]
    public decimal Rate { get; set; }

    [Column(TypeName = "decimal(12,2)")]
    public decimal AnnualCumulative { get; set; }
}