using Microsoft.EntityFrameworkCore;
using qwikhr.Data;
using qwikhr.Dtos.Company;
using qwikhr.Interfaces;
using qwikhr.Models;

namespace qwikhr.Repository
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly ApplicationDbContext _context;

        public CompanyRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Company> CreateAsync(Company branchModel)
        {
            await _context.Companies.AddAsync(branchModel);
            await _context.SaveChangesAsync();
            return branchModel;
        }

        public async Task<Company?> DeleteAsync(Guid id)
        {
            var companyModel = await _context.Companies.FirstOrDefaultAsync(c => c.Id == id);
            if (companyModel == null)
            {
                return null;
            }
            _context.Remove(companyModel);
            await _context.SaveChangesAsync();
            return companyModel;
        }

        public async Task<List<Company>> GetAllAsync()
        {
            var companies = _context.Companies;
            return await companies.ToListAsync();
        }

        public async Task<Company?> GetByIdAsync(Guid id)
        {
            var companyModel = await _context.Companies.FirstOrDefaultAsync(c => c.Id == id);
            if (companyModel == null)
            {
                return null;
            }
            return companyModel;
        }

        public async Task<Company?> UpdateAsync(Guid id, UpdateCompanyDto companyDto)
        {
            var companyModel = await _context.Companies.FirstOrDefaultAsync(c => c.Id == id);
            if (companyModel == null)
            {
                return null;
            }
            companyModel.Name = companyDto.Name;
            _context.Companies.Update(companyModel);
            await _context.SaveChangesAsync();
            return companyModel;
        }
    }
}