using qwikhr.Dtos.Region;
using qwikhr.Models;

namespace qwikhr.Interfaces
{
    public interface IRegionRepository
    {

        Task<List<Region>> GetAllAsync();
        Task<Region?> GetBySlugAsync(Guid slug);
        Task<Region> CreateAsync(Region regionModel);
        Task<Region?> UpdateAsync(Guid slug, UpdateRegionDto regionDto);
        Task<Region?> DeleteAsync(Guid slug);
    }
}