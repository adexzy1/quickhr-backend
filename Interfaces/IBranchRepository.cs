using qwikhr.Dtos.Branch;
using qwikhr.Models;

namespace qwikhr.Interfaces
{
    public interface IBranchRepository
    {
        Task<List<Branch>> GetAllAsync();
        Task<Branch?> GetBySlugAsync(Guid slug);
        Task<Branch> CreateAsync(Branch branchModel);
        Task<Branch?> UpdateAsync(Guid slug, UpdateBranchDto branchDto);
        Task<Branch?> DeleteAsync(Guid slug);
    }
}