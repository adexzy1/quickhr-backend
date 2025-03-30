using Microsoft.EntityFrameworkCore;
using qwikhr.Data;
using qwikhr.Dtos.Payroll;
using qwikhr.Interfaces;
using qwikhr.Models.Payroll;

namespace qwikhr.Repository
{
    public class PayGradeRepository : IPayGradeRepository
    {
        private readonly ApplicationDbContext _context;

        public PayGradeRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<PayGrade> CreateAsync(PayGrade payGradeModel)
        {
            await _context.PayGrades.AddAsync(payGradeModel);
            await _context.SaveChangesAsync();
            return payGradeModel;
        }

        public async Task<PayGrade?> DeleteAsync(Guid id)
        {
            var pgModel = await _context.PayGrades.Include(pg => pg.PayComponents).FirstOrDefaultAsync(pg => pg.Id == id);
            if (pgModel == null)
            {
                return null;
            }
            // Remove existing pay components
            _context.PayGradePayComponents.RemoveRange(pgModel.PayComponents);
            _context.PayGrades.Remove(pgModel);
            await _context.SaveChangesAsync();
            return pgModel;
        }

        public async Task<List<PayGrade>> GetAllAsync()
        {
            var payGrades = _context.PayGrades.Include(pg => pg.PayComponents).ThenInclude(pgp => pgp.PayComponent);
            return await payGrades.ToListAsync();
        }

        public async Task<PayGrade?> GetByIdAsync(Guid id)
        {
            var payGradeModel = await _context.PayGrades.Include(pg => pg.PayComponents).ThenInclude(pgp => pgp.PayComponent).FirstOrDefaultAsync(pg => pg.Id == id);
            if (payGradeModel == null)
            {
                return null;
            }
            return payGradeModel;
        }

        public async Task<PayGrade?> UpdateAsync(Guid id, CreatePayGradeDto payGradeDto)
        {
            var payGradeModel = await _context.PayGrades.Include(pg => pg.PayComponents).FirstOrDefaultAsync(b => b.Id == id);
            if (payGradeModel == null)
            {
                return null;
            }
            payGradeModel.Name = payGradeDto.Name;
            payGradeModel.BaseSalary = payGradeDto.BaseSalary;

            // Remove existing pay components
            _context.PayGradePayComponents.RemoveRange(payGradeModel.PayComponents);

            // Add new pay components
            payGradeModel.PayComponents = [.. payGradeDto.PayComponentIds
                .Select(payComponentId => new PayGradePayComponent
                {
                    PayGradeId = id,
                    PayComponentId = payComponentId
                })];

            _context.PayGrades.Update(payGradeModel);
            await _context.SaveChangesAsync();
            return payGradeModel;
        }
    }
}