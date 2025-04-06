using qwikhr.Dtos.Payroll;
using qwikhr.Models.Payroll;

namespace qwikhr.Mappers
{
    public static class EmployeePayComponentMapper
    {

        public static EmployeePayComponentDto ToEmployeePayComponentDto(this EmployeePayComponent employeePayComponent)
        {
            return new EmployeePayComponentDto
            {
                Id = employeePayComponent.Id,
                PayComponentName = employeePayComponent.PayComponent?.Name ?? string.Empty, // Handle null PayComponent
                Amount = employeePayComponent.Amount,
                Frequency = employeePayComponent.Frequency,
                EffectiveDate = employeePayComponent.EffectiveDate,
                EndDate = employeePayComponent.EndDate,
                IsActive = employeePayComponent.IsActive
            };
        }

        public static void UpdateFromEmployeePayComponentDto(this EmployeePayComponent employeePayComponent, UpdateEmployeePayComponentDto dto)
        {
            employeePayComponent.Amount = dto.Amount;
            employeePayComponent.Frequency = dto.Frequency;
            employeePayComponent.EffectiveDate = dto.EffectiveDate ?? employeePayComponent.EffectiveDate;
            employeePayComponent.EndDate = dto.EndDate;
        }
    }

}

