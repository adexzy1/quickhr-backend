using qwikhr.Models;
using qwikhr.Models.Payroll;

namespace qwikhr.Interfaces
{
    public interface IPayrollApprovalHistoryRepository
    {
        Task AddAsync(PayrollApprovalHistory history);
        Task<List<PayrollApprovalHistory>> GetByApprovalIdAsync(Guid approvalId);
    }
}