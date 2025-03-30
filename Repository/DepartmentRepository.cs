using Microsoft.EntityFrameworkCore;
using qwikhr.Data;
using qwikhr.Dtos.Department;
using qwikhr.Interfaces;
using qwikhr.Models;

namespace qwikhr.Repository
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly ApplicationDbContext _context;

        public DepartmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Department> CreateAsync(Department departmentModel)
        {
            await _context.Departments.AddAsync(departmentModel);
            await _context.SaveChangesAsync();
            return departmentModel;
        }

        public async Task<Department?> DeleteAsync(Guid id)
        {
            var departmentModel = await _context.Departments.FirstOrDefaultAsync(d => d.Id == id);
            if (departmentModel == null)
            {
                return null;
            }
            _context.Departments.Remove(departmentModel);
            await _context.SaveChangesAsync();
            return departmentModel;
        }

        public async Task<List<Department>> GetAllAsync()
        {
            var departments = _context.Departments;
            return await departments.ToListAsync();
        }

        public async Task<Department?> GetByIdAsync(Guid id)
        {
            var departmentModel = await _context.Departments.FirstOrDefaultAsync(d => d.Id == id);
            if (departmentModel == null)
            {
                return null;
            }
            return departmentModel;
        }

        public async Task<Department?> UpdateAsync(Guid id, UpdateDepartmentDto departmentDto)
        {
            var departmentModel = await _context.Departments.FirstOrDefaultAsync(b => b.Id == id);
            if (departmentModel == null)
            {
                return null;
            }
            departmentModel.Name = departmentDto.Name;
            departmentModel.ManagerId = departmentDto.ManagerId;
            departmentModel.BranchId = departmentDto.BranchId;
            _context.Departments.Update(departmentModel);
            await _context.SaveChangesAsync();
            return departmentModel;
        }
    }
}