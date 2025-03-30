using qwikhr.Dtos.Payroll;
using qwikhr.Models.Payroll;

namespace qwikhr.Mappers
{
    public static class PayGradeMappers
    {

        public static PayGradeDto ToPayGradeDto(this PayGrade payGradeModel)
        {
            return new PayGradeDto
            {
                Id = payGradeModel.Id,
                Name = payGradeModel.Name,
                BaseSalary = payGradeModel.BaseSalary,
                PayComponents = payGradeModel.PayComponents?
                    .Select(pc => pc.PayComponent != null ? new PayComponentDto
                    {
                        Id = pc.PayComponent.Id,
                        Name = pc.PayComponent.Name,
                        Value = pc.PayComponent.Value,
                        IsPercentage = pc.PayComponent.IsPercentage,
                        IsAllowance = pc.PayComponent.IsAllowance
                    } : null) // Handle null PayComponent
                    .Where(dto => dto != null)
                    .Select(dto => dto!) // Explicitly cast to non-nullable
                    .ToList() ?? new List<PayComponentDto>() // Ensure non-null list
            };
        }

        public static PayGrade ToPayGradeFromCreateDto(this CreatePayGradeDto payGradeDto)
        {
            return new PayGrade
            {
                Name = payGradeDto.Name,
                BaseSalary = payGradeDto.BaseSalary,
                PayComponents = [.. payGradeDto.PayComponentIds
                .Select(id => new PayGradePayComponent
                {
                    PayComponentId = id
                })]
            };
        }
    }
}