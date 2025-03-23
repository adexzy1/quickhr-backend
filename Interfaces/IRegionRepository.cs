using qwikhr.Dtos.Region;
using qwikhr.Models;

namespace qwikhr.Interfaces
{
    public interface IRegionRepository
    {

        Task<List<Region>> GetAllAsync();
        Task<Region?> GetByIdAsync(Guid Id);
        Task<Region> CreateAsync(Region regionModel);
        Task<Region?> UpdateAsync(Guid Id, UpdateRegionDto regionDto);
        Task<Region?> DeleteAsync(Guid Id);
    }
}