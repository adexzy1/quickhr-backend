using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using qwikhr.Common;

namespace qwikhr.Models.Payroll
{
    public class EmployeeSalary : CompanyEntity
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid(); // Foreign Key to Employee Table
        public decimal GrossSalary { get; set; }
        public Guid EmployeeId { get; set; }
        [ForeignKey("EmployeeId")]
        public Employee? Employee { get; set; }
        public DateTime EffectiveDate { get; set; } // Date when salary change takes effect
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // Timestamp for record creation
    }
}