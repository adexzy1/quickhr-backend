using Microsoft.EntityFrameworkCore;
using qwikhr.Data;
using qwikhr.Interfaces;
using qwikhr.Models;

namespace qwikhr.Repository
{
    public class PayrollApprovalRepository : IPayrollApprovalRepository
    {
        private readonly ApplicationDbContext _context;

        public PayrollApprovalRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PayrollApproval?> GetByPayrollRunIdAsync(Guid payrollRunId)
        {
            return await _context.PayrollApprovals
                .Include(pa => pa.CurrentPayrollApprovalLevel)
                .ThenInclude(level => level.Workflow)
                .FirstOrDefaultAsync(pa => pa.PayrollRunId == payrollRunId);
        }

        public async Task<PayrollApproval?> GetByIdAsync(Guid approvalId)
        {
            return await _context.PayrollApprovals
                .Include(pa => pa.CurrentPayrollApprovalLevel)
                .ThenInclude(level => level.Workflow)
                .FirstOrDefaultAsync(pa => pa.Id == approvalId);
        }

        public async Task UpdateAsync(PayrollApproval approval)
        {
            _context.PayrollApprovals.Update(approval);
            await _context.SaveChangesAsync();
        }

        public async Task AddAsync(PayrollApproval approval)
        {
            await _context.PayrollApprovals.AddAsync(approval);
            await _context.SaveChangesAsync();
        }
    }
}