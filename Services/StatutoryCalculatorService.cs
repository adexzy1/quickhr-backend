using Microsoft.EntityFrameworkCore;
using qwikhr.Data;
using qwikhr.Models.Payroll;

namespace qwikhr.Services
{
    public class StatutoryCalculatorService(ApplicationDbContext context)
    {
        private readonly ApplicationDbContext _context = context;

        public async Task<StatutoryCalculationResult> CalculateDeductions(
            decimal basicSalary,
            decimal grossPay, decimal bht)
        {
            var rates = await _context.StatutoryDeductions
                .Where(r => r.IsActive)
                .ToListAsync();

            var pension = await CalculatePension(bht, rates);
            var nhf = await CalculateNHF(basicSalary, rates);
            var adjustedGross = grossPay - (pension.EmployeeAmount + nhf);
            var cra = await CalculateCRA(grossPay, adjustedGross);
            var taxableIncome = adjustedGross - cra - pension.EmployeeAmount - nhf;
            if (taxableIncome < 0)
            {
                taxableIncome = 0;
            }
            var paye = await CalculatePAYE(taxableIncome);
            var nsitf = await CalculateNSITF(grossPay, rates);

            return new StatutoryCalculationResult
            {
                PensionEmployee = pension.EmployeeAmount / 12,
                PensionEmployer = pension.EmployerAmount / 12,
                NHF = nhf / 12,
                PAYE = paye,
                NSITF = nsitf / 12
            };
        }

        private static async Task<(decimal EmployeeAmount, decimal EmployerAmount)> CalculatePension(
            decimal basicSalary,
            List<StatutoryDeduction> rates)
        {
            var pensionRate = rates.FirstOrDefault(r => r.Code == "PEN")
                ?? throw new Exception("Pension rate not configured");
            Console.WriteLine((basicSalary * pensionRate.EmployeeRate,
                   basicSalary * (pensionRate.EmployerRate ?? 0)));
            return await Task.FromResult((basicSalary * pensionRate.EmployeeRate,
                   basicSalary * (pensionRate.EmployerRate ?? 0)));
        }

        private static async Task<decimal> CalculateNHF(decimal basicSalary, List<StatutoryDeduction> rates)
        {
            var nhfRate = rates.FirstOrDefault(r => r.Code == "NHF")
                ?? throw new Exception("NHF rate not configured");

            return await Task.FromResult(basicSalary * nhfRate.EmployeeRate);
        }

        private async Task<decimal> CalculatePAYE(decimal taxableIncome)
        {
            var bands = await _context.PayeTaxBands
                .OrderBy(b => b.LowerBound)
                .ToListAsync();

            decimal annualSalary = taxableIncome;
            decimal annualTax = 0;

            foreach (var band in bands)
            {
                if (annualSalary > band.LowerBound)
                {
                    decimal taxableInBand = Math.Min(
                        band.UpperBound ?? decimal.MaxValue,
                        annualSalary) - band.LowerBound;

                    annualTax += taxableInBand * band.Rate;
                }
            }

            return annualTax / 12;
        }

        private static async Task<decimal> CalculateNSITF(decimal grossPay, List<StatutoryDeduction> rates)
        {
            var nsitfRate = rates.FirstOrDefault(r => r.Code == "NSITF")
                ?? throw new Exception("NSITF rate not configured");

            return await Task.FromResult(grossPay * (nsitfRate.EmployerRate ?? 0));
        }

        private static async Task<decimal> CalculateCRA(decimal grossIncome, decimal adjustedGross)
        {
            // Step 1: Base CRA (greater of ₦200,000 or 1% of gross income)
            decimal baseCRA = Math.Max(200000, grossIncome * 0.01m);

            // Step 2: Additional CRA (20% of gross income after pension deduction)
            // decimal adjustedGross = grossIncome - pensionContribution;
            decimal additionalCRA = adjustedGross * 0.20m;

            // Step 3: Total CRA
            decimal totalCRA = baseCRA + additionalCRA;


            return await Task.FromResult(totalCRA); // Ensure non-negative taxable income
        }



        public record StatutoryCalculationResult
        {
            public decimal PensionEmployee { get; init; }
            public decimal PensionEmployer { get; init; }
            public decimal NHF { get; init; }
            public decimal PAYE { get; init; }
            public decimal NSITF { get; init; }
        }
    }
}
