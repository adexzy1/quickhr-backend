using System.ComponentModel.DataAnnotations;
using qwikhr.Common;

namespace qwikhr.Models.Payroll
{
    public class PayGradeComponent : CompanyEntity
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid PayGradeId { get; set; }
        public Guid PayComponentId { get; set; }
        public decimal DefaultAmount { get; set; }
        public bool IsOptional { get; set; }

        // Navigation properties
        public virtual PayGrade? PayGrade { get; set; }
        public virtual PayComponent? PayComponent { get; set; }
    }
}