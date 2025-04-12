using qwikhr.Models.Payroll;

namespace qwikhr.Interfaces
{
    public interface IPayrollEntryRepository
    {
        Task<PayrollEntry?> GetByIdAsync(Guid entryId);
        Task<List<PayrollEntry>> GetByPayrollRunIdAsync(Guid payrollRunId);
        Task UpdateAsync(PayrollEntry entry);
        Task AddAsync(PayrollEntry entry);
    }
}