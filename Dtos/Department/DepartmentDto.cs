using System.ComponentModel.DataAnnotations;
using qwikhr.Models;


namespace qwikhr.Dtos.Department
{
    public class DepartmentDto
    {
        public Guid Id { get; set; }
        [Required]
        public required string Name { get; set; }
        public Guid? ManagerId { get; set; }
        public Guid? BranchId { get; set; }
    }

}