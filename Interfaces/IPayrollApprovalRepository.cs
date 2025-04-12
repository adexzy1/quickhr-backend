using qwikhr.Models;

namespace qwikhr.Interfaces
{
    public interface IPayrollApprovalRepository
    {
        Task<PayrollApproval?> GetByPayrollRunIdAsync(Guid payrollRunId);
        Task<PayrollApproval?> GetByIdAsync(Guid approvalId);
        Task UpdateAsync(PayrollApproval approval);
        Task AddAsync(PayrollApproval approval);
    }
}