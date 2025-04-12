using qwikhr.Dtos.Payroll;
using qwikhr.Models.Payroll;

namespace qwikhr.Mappers
{
    public static class PayrollRunMapper
    {
        // Map PayrollRun to PayrollRunDto
        public static PayrollRunDto ToPayrollRunDto(this PayrollRun payrollRun)
        {
            return new PayrollRunDto
            {
                Id = payrollRun.Id,
                PayrollPeriodId = payrollRun.PayrollPeriodId,
                RunDate = payrollRun.RunDate,
                RunById = payrollRun.RunById,
                Status = payrollRun.Status.ToString(),
                Notes = payrollRun.Notes,
                TotalGrossPay = payrollRun.TotalGrossPay,
                TotalDeductions = payrollRun.TotalDeductions,
                TotalNetPay = payrollRun.TotalNetPay,
                FinalizedAt = payrollRun.FinalizedAt
            };
        }

        // Map CreatePayrollRunDto to PayrollRun
        public static PayrollRun ToPayrollRunFromCreateDto(this CreatePayrollRunDto dto)
        {
            return new PayrollRun
            {
                PayrollPeriodId = dto.PayrollPeriodId,
                RunDate = dto.RunDate,
                RunById = dto.RunById,
                Notes = dto.Notes,
                Status = PayrollRunStatus.Draft // Default status
            };
        }

        // Map UpdatePayrollRunDto to PayrollRun
        public static void UpdatePayrollRunFromDto(this PayrollRun payrollRun, UpdatePayrollRunDto dto)
        {
            payrollRun.PayrollPeriodId = dto.PayrollPeriodId;
            payrollRun.RunDate = dto.RunDate;
            payrollRun.RunById = dto.RunById;
            payrollRun.Notes = dto.Notes;
            payrollRun.Status = Enum.TryParse(dto.Status, out PayrollRunStatus status) ? status : payrollRun.Status;
        }
    }
}