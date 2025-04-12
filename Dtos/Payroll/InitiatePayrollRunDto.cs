using System.ComponentModel.DataAnnotations;

namespace qwikhr.Dtos.Payroll
{
    public class InitiatePayrollRunDto
    {
        [Required(ErrorMessage = "Employees list is required.")]
        public List<Guid> Employees { get; set; } = [];
    }
}