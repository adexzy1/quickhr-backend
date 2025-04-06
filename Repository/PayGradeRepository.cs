using Microsoft.EntityFrameworkCore;
using qwikhr.Data;
using qwikhr.Dtos.Payroll;
using qwikhr.Interfaces;
using qwikhr.Mappers;
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
            var grade = await GetByIdAsync(id);
            if (grade == null) return null;

            _context.PayGrades.Remove(grade);
            await _context.SaveChangesAsync();
            return grade;

        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _context.PayGrades.AnyAsync(p => p.Id == id);
        }

        public async Task<List<PayGrade>> GetAllAsync()
        {
            var payGrades = _context.PayGrades.Include(pg => pg.PayGradeComponents).ThenInclude(pgp => pgp.PayComponent);
            return await payGrades.ToListAsync();
        }

        public async Task<PayGrade?> GetByIdAsync(Guid id)
        {
            var payGradeModel = await _context.PayGrades.Include(pg => pg.PayGradeComponents).ThenInclude(pgp => pgp.PayComponent).FirstOrDefaultAsync(pg => pg.Id == id);
            if (payGradeModel == null)
            {
                return null;
            }
            return payGradeModel;
        }

        public async Task<PayGrade?> UpdateAsync(Guid id, UpdatePayGradeDto payGradeDto)
        {
            var existingGrade = await GetByIdAsync(id);
            if (existingGrade == null) return null;

            var updatedGrade = payGradeDto.ToPayGradeFromUpdateDto(existingGrade);
            _context.Entry(existingGrade).CurrentValues.SetValues(updatedGrade);
            existingGrade.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return existingGrade;
        }


    }
}