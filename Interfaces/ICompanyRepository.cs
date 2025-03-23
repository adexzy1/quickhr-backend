using qwikhr.Dtos.Company;
using qwikhr.Models;

namespace qwikhr.Interfaces
{
    public interface ICompanyRepository
    {
        Task<List<Company>> GetAllAsync();
        Task<Company?> GetByIdAsync(Guid id);
        Task<Company> CreateAsync(Company companyModel);
        Task<Company?> UpdateAsync(Guid id, UpdateCompanyDto companyDto);
        Task<Company?> DeleteAsync(Guid id);
    }
}