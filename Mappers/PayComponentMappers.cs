using qwikhr.Dtos.Payroll;
using qwikhr.Models.Payroll;

namespace qwikhr.Mappers
{
    public class PayComponentMappers
    {
        // Convert DTO to Entity (for saving to DB)
        public static PayComponent ToPayComponent(PayComponentDto dto)
        {
            return new PayComponent
            {
                Name = dto.Name,
                Value = dto.Value,
                IsPercentage = dto.IsPercentage,
                IsAllowance = dto.IsAllowance
            };
        }

        // Convert Entity to DTO (for returning data)
        public static PayComponentDto ToPayComponentFronCreateDto(CreatePayComponentDto entity)
        {
            return new PayComponentDto
            {
                Name = entity.Name,
                Value = entity.Value,
                IsPercentage = entity.IsPercentage,
                IsAllowance = entity.IsAllowance
            };
        }
    }
}