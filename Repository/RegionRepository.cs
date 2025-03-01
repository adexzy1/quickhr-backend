using Microsoft.EntityFrameworkCore;
using qwikhr.Data;
using qwikhr.Dtos.Region;
using qwikhr.Interfaces;
using qwikhr.Models;

namespace qwikhr.Repository
{
    public class RegionRepository(ApplicationDbContext context) : IRegionRepository
    {
        private readonly ApplicationDbContext _context = context;

        public async Task<Region> CreateAsync(Region regionModel)
        {
            await _context.AddAsync(regionModel);
            await _context.SaveChangesAsync();
            return regionModel;
        }

        public async Task<Region?> DeleteAsync(Guid slug)
        {
            var regionModel = await _context.Regions.FirstOrDefaultAsync(r => r.Slug == slug);
            if (regionModel == null)
            {
                return null;
            }
            _context.Regions.Remove(regionModel);
            await _context.SaveChangesAsync();
            return regionModel;
        }

        public Task<List<Region>> GetAllAsync()
        {
            var regions = _context.Regions;
            return regions.ToListAsync();
        }

        public async Task<Region?> GetBySlugAsync(Guid slug)
        {
            var regionModel = await _context.Regions.FirstOrDefaultAsync(r => r.Slug == slug);
            if (regionModel == null)
            {
                return null;
            }
            return regionModel;
        }

        public async Task<Region?> UpdateAsync(Guid slug, UpdateRegionDto regionDto)
        {
            var regionModel = await _context.Regions.FirstOrDefaultAsync(r => r.Slug == slug);
            if (regionModel == null)
            {
                return null;
            }
            regionModel.Name = regionDto.Name;
            _context.Regions.Update(regionModel);
            await _context.SaveChangesAsync();
            return regionModel;
        }
    }
}