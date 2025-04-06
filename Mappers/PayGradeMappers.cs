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
                Code = payGradeModel.Code,
                Description = payGradeModel.Description,
                MinimumSalary = payGradeModel.MinimumSalary,
                MaximumSalary = payGradeModel.MaximumSalary,
                MidPointSalary = payGradeModel.MidPointSalary,
                IsExempt = payGradeModel.IsExempt,
                CreatedAt = payGradeModel.CreatedAt,
                UpdatedAt = payGradeModel.UpdatedAt,
                PayComponents = [.. payGradeModel.PayGradeComponents.Select(pc => new SimplifiedPayComponentDto
                {
                    Id = pc.PayComponentId,
                    Name = pc.PayComponent?.Name ?? string.Empty
                })]
            };
        }

        public static PayGrade ToPayGradeFromCreateDto(this CreatePayGradeDto payGradeDto)
        {
            return new PayGrade
            {
                Name = payGradeDto.Name,
                Code = payGradeDto.Code,
                Description = payGradeDto.Description,
                MinimumSalary = payGradeDto.MinimumSalary,
                MaximumSalary = payGradeDto.MaximumSalary,
                MidPointSalary = payGradeDto.MidPointSalary,
                IsExempt = payGradeDto.IsExempt,
                CreatedAt = DateTime.UtcNow,
                PayGradeComponents = [.. payGradeDto.PayComponentIds
                    .Select(id => new PayGradeComponent
                    {
                        PayComponentId = id
                    })]
            };
        }

        public static PayGrade ToPayGradeFromUpdateDto(this UpdatePayGradeDto payGradeDto, PayGrade existingPayGrade)
        {
            existingPayGrade.Name = payGradeDto.Name ?? existingPayGrade.Name;
            existingPayGrade.Code = payGradeDto.Code ?? existingPayGrade.Code;
            existingPayGrade.Description = payGradeDto.Description ?? existingPayGrade.Description;
            existingPayGrade.MinimumSalary = payGradeDto.MinimumSalary ?? existingPayGrade.MinimumSalary;
            existingPayGrade.MaximumSalary = payGradeDto.MaximumSalary ?? existingPayGrade.MaximumSalary;
            existingPayGrade.MidPointSalary = payGradeDto.MidPointSalary ?? existingPayGrade.MidPointSalary;
            existingPayGrade.IsExempt = payGradeDto.IsExempt ?? existingPayGrade.IsExempt;
            existingPayGrade.UpdatedAt = DateTime.UtcNow;

            // Update PayGradeComponents if PayComponentIds are provided
            if (payGradeDto.PayComponentIds != null)
            {
                existingPayGrade.PayGradeComponents = [.. payGradeDto.PayComponentIds
                    .Select(id => new PayGradeComponent
                    {
                        PayComponentId = id,
                        PayGradeId = existingPayGrade.Id
                    })];
            }

            return existingPayGrade;
        }
    }
}
