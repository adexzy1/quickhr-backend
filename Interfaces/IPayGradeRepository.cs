using qwikhr.Dtos.Payroll;
using qwikhr.Models.Payroll;

namespace qwikhr.Interfaces
{
    public interface IPayGradeRepository
    {
        Task<List<PayGrade>> GetAllAsync();
        Task<PayGrade?> GetByIdAsync(Guid id);
        Task<PayGrade> CreateAsync(PayGrade payGradeModel);
        Task<PayGrade?> UpdateAsync(Guid id, CreatePayGradeDto payGradeDto);
        Task<PayGrade?> DeleteAsync(Guid id);
    }
}