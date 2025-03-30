using Microsoft.EntityFrameworkCore;
using qwikhr.Data;
using qwikhr.Dtos.Payroll;
using qwikhr.Interfaces;
using qwikhr.Models.Payroll;

namespace qwikhr.Repository
{
    public class PayComponentRepository : IPayComponentRepository
    {
        private readonly ApplicationDbContext _context;

        public PayComponentRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<PayComponent> CreateAsync(PayComponent payComponentModel)
        {
            await _context.PayComponents.AddAsync(payComponentModel);
            await _context.SaveChangesAsync();
            return payComponentModel;
        }

        public async Task<(PayComponent?, bool)> DeleteAsync(Guid id)
        {
            // Check if the PayComponent is being used in any PayGradePayComponent
            bool isInUse = await _context.PayGradePayComponents.AnyAsync(pc => pc.PayComponentId == id);

            if (isInUse)
            {
                return (null, true); // Prevent deletion if it's in use
            }

            // Retrieve the PayComponent
            var payComponent = await _context.PayComponents.FindAsync(id);

            if (payComponent == null)
            {
                return (null, false); // Return false if not found
            }

            _context.PayComponents.Remove(payComponent);
            await _context.SaveChangesAsync();

            return (payComponent, false);
        }

        public async Task<List<PayComponent>> GetAllAsync()
        {
            var payComponents = _context.PayComponents;
            return await payComponents.ToListAsync();
        }

        public async Task<PayComponent?> GetByIdAsync(Guid id)
        {
            var payComponentModel = await _context.PayComponents.FirstOrDefaultAsync(pc => pc.Id == id);
            if (payComponentModel == null)
            {
                return null;
            }
            return payComponentModel;
        }

        public async Task<PayComponent?> UpdateAsync(Guid id, CreatePayComponentDto payComponentDto)
        {
            var payComponentModel = await _context.PayComponents.FirstOrDefaultAsync(pc => pc.Id == id);
            if (payComponentModel == null)
            {
                return null;
            }
            payComponentModel.Name = payComponentDto.Name;
            payComponentModel.Value = payComponentDto.Value;
            payComponentModel.IsAllowance = payComponentDto.IsAllowance;
            payComponentModel.IsPercentage = payComponentDto.IsPercentage;

            _context.PayComponents.Update(payComponentModel);
            await _context.SaveChangesAsync();
            return payComponentModel;
        }
    }
}