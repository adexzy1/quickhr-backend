using Microsoft.EntityFrameworkCore;
using qwikhr.Data;
using qwikhr.Dtos.Payroll;
using qwikhr.Interfaces;
using qwikhr.Models.Payroll;

namespace qwikhr.Repository
{
    public class PayrollRunRepository : IPayrollRunRepository
    {
        private readonly ApplicationDbContext _context;

        public PayrollRunRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PayrollRun?> GetByIdAsync(Guid id)
        {
            return await _context.PayrollRuns
                .Include(pr => pr.PayrollPeriod) // Include related PayrollPeriod
                .Include(pr => pr.PayrollEntries) // Include related PayrollEntries
                .FirstOrDefaultAsync(pr => pr.Id == id);
        }

        public async Task<List<PayrollRun>> GetAllAsync()
        {
            var payrollRuns = await _context.PayrollRuns
                .Include(pr => pr.PayrollPeriod)
                .ToListAsync();
            return payrollRuns;
        }

        public async Task<PayrollRun?> AddAsync(PayrollRun payrollRunModel, List<Guid> employeeIds)
        {
            // Fetch the employees to include in the payroll run
            var employees = await _context.Employees
                .Where(e => employeeIds.Contains(e.Id))
                .ToListAsync();

            if (employees.Count == 0)
            {
                throw new Exception("No valid employees found for the payroll run.");
            }
            var payrollRun = new PayrollRun
            {
                PayrollPeriodId = payrollRunModel.PayrollPeriodId,
                Notes = payrollRunModel.Notes,
                RunDate = payrollRunModel.RunDate,
                RunById = payrollRunModel.RunById,
                Status = PayrollRunStatus.Draft,
                Employees = employees
            };
            await _context.PayrollRuns.AddAsync(payrollRun);
            await _context.SaveChangesAsync();
            return payrollRun;
        }

        public async Task<PayrollRun?> UpdateAsync(Guid id, UpdatePayrollRunDto payrollRun)
        {
            var payrollRunModel = await _context.PayrollRuns.FirstOrDefaultAsync(p => p.Id == id);
            if (payrollRunModel == null)
            {
                return null; // Not found
            }
            payrollRunModel.PayrollPeriodId = payrollRun.PayrollPeriodId;
            payrollRunModel.RunDate = payrollRun.RunDate;
            payrollRunModel.RunById = payrollRun.RunById;
            payrollRunModel.Notes = payrollRun.Notes;
            payrollRunModel.Status = Enum.TryParse(payrollRun.Status, out PayrollRunStatus status) ? status : payrollRunModel.Status;
            _context.PayrollRuns.Update(payrollRunModel);
            await _context.SaveChangesAsync();
            return payrollRunModel;
        }

        public async Task<PayrollRun?> DeleteAsync(Guid id)
        {
            var payrollRunModel = await _context.PayrollRuns.FirstOrDefaultAsync(p => p.Id == id);
            if (payrollRunModel == null)
            {
                return null; // Not found
            }
            _context.PayrollRuns.Remove(payrollRunModel);
            await _context.SaveChangesAsync();
            return payrollRunModel;
        }


    }
}