using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using qwikhr.Common;

namespace qwikhr.Models.Payroll
{
    public class PayGradePayComponent : CompanyEntity
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid PayGradeId { get; set; }
        [ForeignKey("PayGradeId")]
        public PayGrade? PayGrade { get; set; }
        public Guid PayComponentId { get; set; }
        [ForeignKey("PayComponentId")]
        public PayComponent? PayComponent { get; set; }
    }
}