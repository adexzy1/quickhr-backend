using System.ComponentModel.DataAnnotations;
using qwikhr.Common;

namespace qwikhr.Models.Payroll
{
    public class PayGrade : CompanyEntity
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public decimal BaseSalary { get; set; }
        public ICollection<PayGrdaePayComponent> PayComponents { get; set; } = [];
    }
}