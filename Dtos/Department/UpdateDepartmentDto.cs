namespace qwikhr.Dtos.Department
{
    public class UpdateDepartmentDto
    {
        public string? Name { get; set; }
        public Guid? ManagerId { get; set; }
        public Guid? BranchId { get; set; }
    }
}