using System.ComponentModel.DataAnnotations;

namespace qwikhr.Dtos.Department
{
    public class CreateDepartmentDto
    {
        [Required]
        public required string Name { get; set; }
        public Guid? ManagerId { get; set; }
        public Guid? BranchId { get; set; }
    }
}