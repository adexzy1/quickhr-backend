using Microsoft.EntityFrameworkCore;
using qwikhr.Data;
using qwikhr.Interfaces;
using qwikhr.Models;

namespace qwikhr.Repository
{
    public class PositionRepository : IPositionRepository
    {
        private readonly ApplicationDbContext _context;

        public PositionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Position>> GetAllAsync()
        {
            return await _context.Positions.ToListAsync();
        }

        public async Task<Position?> GetByIdAsync(Guid id)
        {
            var positionModel = await _context.Positions.FirstOrDefaultAsync(p => p.Id == id);
            if (positionModel == null)
            {
                return null;
            }
            return positionModel;
        }

        public async Task<Position?> CreateAsync(Position position)
        {
            await _context.Positions.AddAsync(position);
            await _context.SaveChangesAsync();
            return position;
        }

        public async Task<Position?> UpdateAsync(Guid id, Position position)
        {
            var positionModel = await _context.Positions.FirstOrDefaultAsync(p => p.Id == id);
            if (positionModel == null)
            {
                return null;
            }
            positionModel.Name = position.Name;
            _context.Positions.Update(positionModel);
            await _context.SaveChangesAsync();
            return positionModel;
        }

        public async Task<Position?> DeleteAsync(Guid id)
        {
            var positionModel = await _context.Positions.FirstOrDefaultAsync(p => p.Id == id);
            if (positionModel == null)
            {
                return null;
            }
            _context.Positions.Remove(positionModel);
            await _context.SaveChangesAsync();
            return positionModel;
        }
    }

}