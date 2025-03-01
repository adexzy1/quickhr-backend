using qwikhr.Dtos.Company;
using qwikhr.Models;

namespace qwikhr.Interfaces
{
    public interface ICompanyRepository
    {
        Task<List<Company>> GetAllAsync();
        Task<Company?> GetBySlugAsync(Guid slug);
        Task<Company> CreateAsync(Company companyModel);
        Task<Company?> UpdateAsync(Guid slug, UpdateCompanyDto companyDto);
        Task<Company?> DeleteAsync(Guid slug);
    }
}