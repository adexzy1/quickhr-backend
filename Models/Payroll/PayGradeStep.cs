using System.ComponentModel.DataAnnotations;
using qwikhr.Common;

namespace qwikhr.Models.Payroll
{
    public class PayGradeStep : CompanyEntity
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid PayGradeId { get; set; }
        public required string StepName { get; set; }
        public int StepNumber { get; set; }
        public decimal StepAmount { get; set; }

        // Navigation property
        public virtual PayGrade? PayGrade { get; set; }
    }
}