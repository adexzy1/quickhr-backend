using qwikhr.Dtos.Department;
using qwikhr.Models;

namespace qwikhr.Interfaces
{
    public interface IDepartmentRepository
    {
        Task<List<Department>> GetAllAsync();
        Task<Department?> GetByIdAsync(Guid id);
        Task<Department> CreateAsync(Department departmentModel);
        Task<Department?> UpdateAsync(Guid id, UpdateDepartmentDto departmentDto);
        Task<Department?> DeleteAsync(Guid id);
    }
}