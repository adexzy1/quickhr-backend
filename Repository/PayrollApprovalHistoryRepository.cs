using Microsoft.EntityFrameworkCore;
using qwikhr.Data;
using qwikhr.Interfaces;
using qwikhr.Models.Payroll;

namespace qwikhr.Repository
{
    public class PayrollApprovalHistoryRepository : IPayrollApprovalHistoryRepository
    {
        private readonly ApplicationDbContext _context;

        public PayrollApprovalHistoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Add a new PayrollApprovalHistory record
        public async Task AddAsync(PayrollApprovalHistory history)
        {
            await _context.PayrollApprovalHistories.AddAsync(history);
            await _context.SaveChangesAsync();
        }

        // Get all PayrollApprovalHistory records by ApprovalId
        public async Task<List<PayrollApprovalHistory>> GetByApprovalIdAsync(Guid approvalId)
        {
            return await _context.PayrollApprovalHistories
                .Where(history => history.PayrollApprovalId == approvalId)
                .OrderBy(history => history.ApprovedAt)
                .ToListAsync();
        }
    }
}