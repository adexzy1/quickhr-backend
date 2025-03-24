using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using qwikhr.Common;

namespace qwikhr.Models.Payroll
{
    public class EmployeePayAdjustment : CompanyEntity
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid EmployeeId { get; set; }
        [ForeignKey("EmployeeId")]
        public Employee? Employee { get; set; }
        public Guid PayComponentId { get; set; }
        [ForeignKey("PayComponentId")]
        public PayComponent? PayComponent { get; set; }
        public decimal Amount { get; set; }
    }

}