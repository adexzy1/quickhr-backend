using qwikhr.Dtos.Employee;
using qwikhr.Models;

namespace qwikhr.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<List<Employee>> GetAllAsync();
        Task<Employee?> GetByIdAsync(Guid Id);
        Task<Employee> CreateAsync(Employee employee);
        Task<Employee?> UpdateAsync(Guid Id, UpdateEmployeeDto employeeDto);
        Task<Employee?> DeleteAsync(Guid Id);
    }
}