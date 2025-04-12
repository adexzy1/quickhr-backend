using Microsoft.EntityFrameworkCore;
using qwikhr.Data;
using qwikhr.Dtos.Payroll;
using qwikhr.Interfaces;
using qwikhr.Models.Payroll;

namespace qwikhr.Repository
{
    public class PayrollPeriodRepository : IPayrollPeriodRepository
    {
        private readonly ApplicationDbContext _context;

        public PayrollPeriodRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<PayrollPeriod>> GetAllAsync()
        {
            return await _context.PayrollPeriods.ToListAsync();
        }

        public async Task<PayrollPeriod?> GetByIdAsync(Guid id)
        {
            var payrollPeriods = await _context.PayrollPeriods
                // .Include(p => p.PayrollRuns)
                .FirstOrDefaultAsync(p => p.Id == id);
            if (payrollPeriods == null)
            {
                return null;
            }
            return payrollPeriods;
        }

        public async Task<PayrollPeriod> AddAsync(PayrollPeriod payrollPeriod)
        {
            await _context.PayrollPeriods.AddAsync(payrollPeriod);
            await _context.SaveChangesAsync();
            return payrollPeriod;
        }

        public async Task<PayrollPeriod?> UpdateAsync(Guid Id, UpdatePayrollPeriodDto payrollPeriod)
        {
            var payrollPeriodModel = await _context.PayrollPeriods.FirstOrDefaultAsync(p => p.Id == Id);
            if (payrollPeriodModel == null)
            {
                return null;
            }
            payrollPeriodModel.Name = payrollPeriod.Name;
            payrollPeriodModel.StartDate = payrollPeriod.StartDate;
            payrollPeriodModel.EndDate = payrollPeriod.EndDate;
            payrollPeriodModel.PayDate = payrollPeriod.PayDate;
            payrollPeriodModel.IsLocked = payrollPeriod.IsLocked;
            _context.PayrollPeriods.Update(payrollPeriodModel);
            await _context.SaveChangesAsync();
            return payrollPeriodModel;
        }

        public async Task<PayrollPeriod?> DeleteAsync(Guid id)
        {
            var payrollPeriodModel = await GetByIdAsync(id);
            if (payrollPeriodModel == null)
            {
                return null;

            }
            _context.PayrollPeriods.Remove(payrollPeriodModel);
            await _context.SaveChangesAsync();
            return payrollPeriodModel;
        }
    }
}