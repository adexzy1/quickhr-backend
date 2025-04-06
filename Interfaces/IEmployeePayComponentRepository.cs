using qwikhr.Models.Payroll;

namespace qwikhr.Interfaces
{
    public interface IEmployeePayComponentRepository
    {
        Task<EmployeePayComponent?> GetByIdAsync(Guid id);
        Task<EmployeePayComponent?> UpdateAsync(EmployeePayComponent employeePayComponent);
    }
}