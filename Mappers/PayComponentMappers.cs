using qwikhr.Dtos.Payroll;
using qwikhr.Models.Payroll;

namespace qwikhr.Mappers
{
    public static class PayComponentMappers
    {
        public static PayComponentDto ToPayComponentDto(this PayComponent payComponentModel)
        {
            return new PayComponentDto
            {
                Id = payComponentModel.Id,
                Name = payComponentModel.Name,
                Code = payComponentModel.Code,
                Description = payComponentModel.Description,
                Category = payComponentModel.Category.ToString(), // Assuming Category is an enum
                CalculationType = payComponentModel.CalculationType.ToString(), // Assuming CalculationType is an enum
                CalculationFormula = payComponentModel.CalculationFormula,
                IsTaxable = payComponentModel.IsTaxable,
                IsRecurring = payComponentModel.IsRecurring,
                GLAccountId = payComponentModel.GLAccountId
            };
        }

        // Convert Entity to DTO (for returning data)
        public static PayComponent ToPayComponentFronCreateDto(this CreatePayComponentDto dto)
        {
            return new PayComponent
            {
                Name = dto.Name,
                Code = dto.Code,
                Description = dto.Description,
                Category = dto.Category,
                CalculationType = dto.CalculationType, // Convert string to enum
                CalculationFormula = dto.CalculationFormula,
                IsTaxable = dto.IsTaxable,
                IsRecurring = dto.IsRecurring,
                GLAccountId = dto.GLAccountId
            };
        }

        // Convert UpdatePayComponentDto to PayComponent model
        public static UpdatePayComponentDto ToUpdatePayComponentDto(this PayComponent payComponent, UpdatePayComponentDto dto)
        {
            return new UpdatePayComponentDto
            {
                Name = dto.Name,
                Code = dto.Code,
                Description = dto.Description,
                Category = dto.Category,
                CalculationType = dto.CalculationType,
                CalculationFormula = dto.CalculationFormula,
                IsTaxable = dto.IsTaxable,
                IsRecurring = dto.IsRecurring,
                GLAccountId = dto.GLAccountId
            };

        }
    }
}