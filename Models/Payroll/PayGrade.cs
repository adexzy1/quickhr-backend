using System.ComponentModel.DataAnnotations;
using qwikhr.Common;

namespace qwikhr.Models.Payroll
{
    public class PayGrade : CompanyEntity
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public required string Name { get; set; }
        public string? Code { get; set; }
        public string? Description { get; set; }
        public decimal? MinimumSalary { get; set; }
        public decimal? MaximumSalary { get; set; }
        public decimal? MidPointSalary { get; set; }
        public bool? IsExempt { get; set; } // Exempt from overtime
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        // Navigation properties
        public virtual ICollection<Employee> Employees { get; set; } = [];
        public virtual ICollection<PayGradeComponent> PayGradeComponents { get; set; } = [];
        public virtual ICollection<PayGradeStep> Steps { get; set; } = [];
    }
}