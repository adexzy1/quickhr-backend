using qwikhr.Dtos.Payroll;
using qwikhr.Models.Payroll;

namespace qwikhr.Interfaces
{
    public interface IPayComponentRepository
    {
        Task<List<PayComponent>> GetAllAsync();
        Task<PayComponent?> GetByIdAsync(Guid id);
        Task<PayComponent> CreateAsync(PayComponent payComponentModel);
        Task<PayComponent?> UpdateAsync(Guid id, CreatePayComponentDto payComponentDto);
        Task<(PayComponent?, bool)> DeleteAsync(Guid id);
    }
}