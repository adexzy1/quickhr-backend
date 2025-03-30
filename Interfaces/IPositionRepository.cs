using qwikhr.Models;

namespace qwikhr.Interfaces
{
    public interface IPositionRepository
    {
        Task<List<Position>> GetAllAsync();
        Task<Position?> GetByIdAsync(Guid id);
        Task<Position?> CreateAsync(Position position);
        Task<Position?> UpdateAsync(Guid id, Position position);
        Task<Position?> DeleteAsync(Guid id);
    }

}