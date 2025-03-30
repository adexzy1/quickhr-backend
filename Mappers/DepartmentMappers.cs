using qwikhr.Dtos.Department;
using qwikhr.Models;

namespace qwikhr.Mappers
{
    public static class DepartmentMappers
    {
        public static DepartmentDto ToDepartmentDto(this Department departmentModel)
        {
            return new DepartmentDto
            {
                Id = departmentModel.Id,
                Name = departmentModel.Name,
                ManagerId = departmentModel.ManagerId,
                BranchId = departmentModel.BranchId
            };
        }

        public static Department ToDepartmentFromCreateDto(this CreateDepartmentDto departmentDto)
        {
            return new Department
            {
                Name = departmentDto.Name,
                ManagerId = departmentDto.ManagerId,
                BranchId = departmentDto.BranchId
            };
        }
    }
}