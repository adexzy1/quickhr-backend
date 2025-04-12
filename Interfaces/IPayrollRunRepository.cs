using qwikhr.Dtos.Payroll;
using qwikhr.Models.Payroll;

namespace qwikhr.Interfaces
{
    public interface IPayrollRunRepository
    {
        Task<PayrollRun?> GetByIdAsync(Guid id);
        Task<List<PayrollRun>> GetAllAsync();
        Task<PayrollRun?> AddAsync(PayrollRun payrollRun, List<Guid> employeeIds);
        Task<PayrollRun?> UpdateAsync(Guid id, UpdatePayrollRunDto payrollRun);
        Task<PayrollRun?> DeleteAsync(Guid id);
    }
}