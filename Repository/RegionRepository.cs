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

        public async Task<Region?> DeleteAsync(Guid id)
        {
            var regionModel = await _context.Regions.FirstOrDefaultAsync(r => r.Id == id);
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

        public async Task<Region?> GetByIdAsync(Guid id)
        {
            var regionModel = await _context.Regions.Include(b => b.Branches).FirstOrDefaultAsync(r => r.Id == id);
            if (regionModel == null)
            {
                return null;
            }
            return regionModel;
        }

        public async Task<Region?> UpdateAsync(Guid id, UpdateRegionDto regionDto)
        {
            var regionModel = await _context.Regions.FirstOrDefaultAsync(r => r.Id == id);
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