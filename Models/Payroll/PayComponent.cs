using System.ComponentModel.DataAnnotations;
using qwikhr.Common;

namespace qwikhr.Models.Payroll
{
    public class PayComponent : CompanyEntity
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public decimal Value { get; set; }
        public bool IsPercentage { get; set; }
        public bool IsAllowance { get; set; }
    }
}