using qwikhr.Dtos.Payroll;
using qwikhr.Models.Payroll;

namespace qwikhr.Interfaces
{
    public interface IPayrollPeriodRepository
    {
        Task<List<PayrollPeriod>> GetAllAsync();
        Task<PayrollPeriod?> GetByIdAsync(Guid id);
        Task<PayrollPeriod> AddAsync(PayrollPeriod payrollPeriod);
        Task<PayrollPeriod?> UpdateAsync(Guid id, UpdatePayrollPeriodDto payrollPeriod);
        Task<PayrollPeriod?> DeleteAsync(Guid id);
    }
}