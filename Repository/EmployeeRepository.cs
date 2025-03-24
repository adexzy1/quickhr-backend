using Microsoft.EntityFrameworkCore;
using qwikhr.Data;
using qwikhr.Dtos.Employee;
using qwikhr.Interfaces;
using qwikhr.Models;

namespace qwikhr.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _context;
        public EmployeeRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Employee> CreateAsync(Employee employee)
        {
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();
            return employee;
        }

        public async Task<Employee?> DeleteAsync(Guid Id)
        {
            var employeeModel = await _context.Employees.FirstOrDefaultAsync(e => e.Id == Id);
            if (employeeModel == null)
            {
                return null;
            }
            _context.Employees.Remove(employeeModel);
            await _context.SaveChangesAsync();
            return employeeModel;
        }

        public async Task<List<Employee>> GetAllAsync()
        {
            var employees = _context.Employees;
            return await employees.ToListAsync();
        }

        public Task<Employee?> GetByIdAsync(Guid Id)
        {
            throw new NotImplementedException();
        }

        public Task<Employee?> UpdateAsync(Guid Id, UpdateEmployeeDto employeeDto)
        {
            throw new NotImplementedException();
        }
    }
}