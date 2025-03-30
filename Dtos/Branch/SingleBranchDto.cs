using qwikhr.Dtos.Department;

namespace qwikhr.Dtos.Branch
{
    public class SingleBranchDto : BranchDto
    {
        public List<DepartmentDto> Departments { get; set; } = [];
    }
}