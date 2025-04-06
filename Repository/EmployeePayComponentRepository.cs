using Microsoft.EntityFrameworkCore;
using qwikhr.Data;
using qwikhr.Interfaces;
using qwikhr.Models.Payroll;

namespace qwikhr.Repository
{
    public class EmployeePayComponentRepository : IEmployeePayComponentRepository
    {
        private readonly ApplicationDbContext _context;

        public EmployeePayComponentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<EmployeePayComponent?> GetByIdAsync(Guid id)
        {
            return await _context.EmployeePayComponents
                .FirstOrDefaultAsync(epc => epc.Id == id);
        }

        public async Task<EmployeePayComponent?> UpdateAsync(EmployeePayComponent employeePayComponent)
        {
            _context.EmployeePayComponents.Update(employeePayComponent);
            await _context.SaveChangesAsync();
            return employeePayComponent;
        }
    }
}