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
                Name = payComponentModel.Name,
                Value = payComponentModel.Value,
                IsPercentage = payComponentModel.IsPercentage,
                IsAllowance = payComponentModel.IsAllowance
            };
        }

        // Convert Entity to DTO (for returning data)
        public static PayComponent ToPayComponentFronCreateDto(this CreatePayComponentDto entity)
        {
            return new PayComponent
            {
                Name = entity.Name,
                Value = entity.Value,
                IsPercentage = entity.IsPercentage,
                IsAllowance = entity.IsAllowance
            };
        }
    }
}