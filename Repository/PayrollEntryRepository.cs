using Microsoft.EntityFrameworkCore;
using qwikhr.Data;
using qwikhr.Interfaces;
using qwikhr.Models.Payroll;

namespace qwikhr.Repository
{
    public class PayrollEntryRepository : IPayrollEntryRepository
    {
        private readonly ApplicationDbContext _context;

        public PayrollEntryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PayrollEntry?> GetByIdAsync(Guid entryId)
        {
            return await _context.PayrollEntries
                .Include(pe => pe.Details)
                .FirstOrDefaultAsync(pe => pe.Id == entryId);
        }

        public async Task<List<PayrollEntry>> GetByPayrollRunIdAsync(Guid payrollRunId)
        {
            return await _context.PayrollEntries
                .Where(pe => pe.PayrollRunId == payrollRunId)
                .Include(pe => pe.Details)
                .ToListAsync();
        }

        public async Task UpdateAsync(PayrollEntry entry)
        {
            _context.PayrollEntries.Update(entry);
            await _context.SaveChangesAsync();
        }

        public async Task AddAsync(PayrollEntry entry)
        {
            await _context.PayrollEntries.AddAsync(entry);
            await _context.SaveChangesAsync();
        }
    }
}