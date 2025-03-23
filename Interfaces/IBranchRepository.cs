using qwikhr.Dtos.Branch;
using qwikhr.Models;

namespace qwikhr.Interfaces
{
    public interface IBranchRepository
    {
        Task<List<Branch>> GetAllAsync();
        Task<Branch?> GetByIdAsync(Guid id);
        Task<Branch> CreateAsync(Branch branchModel);
        Task<Branch?> UpdateAsync(Guid id, UpdateBranchDto branchDto);
        Task<Branch?> DeleteAsync(Guid id);
    }
}