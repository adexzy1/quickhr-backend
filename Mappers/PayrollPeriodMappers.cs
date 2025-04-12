using qwikhr.Dtos.Payroll;
using qwikhr.Models.Payroll;

namespace qwikhr.Mappers
{
    public static class PayrollPeriodMapper
    {
        // Map PayrollPeriod to PayrollPeriodDto
        public static PayrollPeriodDto ToPayrollPeriodDto(this PayrollPeriod payrollPeriod)
        {
            return new PayrollPeriodDto
            {
                Id = payrollPeriod.Id,
                Name = payrollPeriod.Name,
                StartDate = payrollPeriod.StartDate,
                EndDate = payrollPeriod.EndDate,
                PayDate = payrollPeriod.PayDate,
                Status = payrollPeriod.Status.ToString(),
                IsLocked = payrollPeriod.IsLocked
            };
        }

        // Map CreatePayrollPeriodDto to PayrollPeriod
        public static PayrollPeriod ToPayrollPeriodFromCreateDto(this CreatePayrollPeriodDto dto)
        {
            return new PayrollPeriod
            {
                Name = dto.Name,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                PayDate = dto.PayDate,
            };
        }

        // Map UpdatePayrollPeriodDto to PayrollPeriod
        public static void UpdatePayrollPeriod(PayrollPeriod payrollPeriod, UpdatePayrollPeriodDto dto)
        {
            payrollPeriod.Name = dto.Name;
            payrollPeriod.StartDate = dto.StartDate;
            payrollPeriod.EndDate = dto.EndDate;
            payrollPeriod.PayDate = dto.PayDate;
            payrollPeriod.IsLocked = dto.IsLocked;
        }
    }
}