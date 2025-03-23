using System.ComponentModel.DataAnnotations;
using qwikhr.Common;

namespace qwikhr.Models
{
    public class LeaveType : CompanyEntity
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        public string Name { get; set; } = string.Empty;
        public bool IsPaid { get; set; } = true;
        [Required]
        public int MaxDaysPerYear { get; set; }
    }

}