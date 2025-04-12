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
            var employees = _context.Employees.Include(em => em.EmploymentType).Include(em => em.Position).Include(em => em.Department).Include(em => em.PayGrade);
            return await employees.ToListAsync();
        }

        public async Task<Employee?> GetByIdAsync(Guid Id)
        {
            var employeeModel = await _context.Employees.
            Include(em => em.EmploymentType).
            Include(em => em.Position).
            Include(em => em.Department).
            Include(em => em.PayGrade)
            .Include(em => em.PayComponents)
            .ThenInclude(emp => emp.PayComponent)
            .FirstOrDefaultAsync(r => r.Id == Id);

            if (employeeModel == null)
            {
                return null;
            }
            return employeeModel;
        }

        public async Task<Employee?> UpdateAsync(Guid Id, UpdateEmployeeDto employeeDto)
        {
            var regionModel = await _context.Employees.FirstOrDefaultAsync(r => r.Id == Id);
            var employeeModel = await _context.Employees.FirstOrDefaultAsync(e => e.Id == Id);
            if (employeeModel == null)
            {
                return null; // Return null if the employee does not exist
            }

            // Update the employee fields with the data from the DTO
            employeeModel.FirstName = employeeDto.FirstName;
            employeeModel.LastName = employeeDto.LastName;
            employeeModel.MiddleName = employeeDto.MiddleName;
            employeeModel.Email = employeeDto.Email;
            employeeModel.PhoneNumber = employeeDto.PhoneNumber;
            employeeModel.DateOfBirth = (DateTime)employeeDto.DateOfBirth;
            employeeModel.Gender = employeeDto.Gender;
            employeeModel.MaritalStatus = employeeDto.MaritalStatus;
            employeeModel.EmploymentDate = (DateTime)employeeDto.EmploymentDate;
            employeeModel.EmploymentType = employeeDto.EmploymentType;
            employeeModel.PositionId = (Guid)employeeDto.PositionId;
            employeeModel.DepartmentId = (Guid)employeeDto.DepartmentId;
            employeeModel.BankName = employeeDto.BankName;
            employeeModel.AccountNumber = employeeDto.AccountNumber;
            employeeModel.BVN = employeeDto.BVN;
            employeeModel.PensionFundAdministrator = employeeDto.PensionFundAdministrator;
            employeeModel.PensionNumber = employeeDto.PensionNumber;
            employeeModel.TaxIdentificationNumber = employeeDto.TaxIdentificationNumber;
            employeeModel.NextOfKinName = employeeDto.NextOfKinName;
            employeeModel.NextOfKinPhone = employeeDto.NextOfKinPhone;
            employeeModel.NextOfKinRelationship = employeeDto.NextOfKinRelationship;
            employeeModel.PayGradeId = (Guid)employeeDto.PayGradeId;

            // Save the changes to the database
            _context.Employees.Update(employeeModel);
            await _context.SaveChangesAsync();

            return employeeModel; // Return the updated employee
        }

    }
}