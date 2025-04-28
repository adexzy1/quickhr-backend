using System.Data;
using Microsoft.EntityFrameworkCore;
using qwikhr.Data;
using qwikhr.Models.Payroll;

namespace qwikhr.Services
{
    public class PayrollService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<PayrollService> _logger;
        private readonly StatutoryCalculatorService _statutoryCalculator;

        public PayrollService(ApplicationDbContext context, ILogger<PayrollService> logger, StatutoryCalculatorService statutoryCalculator)
        {
            _logger = logger;
            _context = context;
            _statutoryCalculator = statutoryCalculator;
        }

        // 1. Payroll Initialization
        public async Task<PayrollRun> RunPayrollWorkflowAsync(Guid payrollRunId)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync(
                IsolationLevel.Serializable);

            try
            {
                var payrollRun = await _context.PayrollRuns
                    .Include(pr => pr.Employees)
                        .ThenInclude(e => e.PayComponents)
                        .ThenInclude(pc => pc.PayComponent)
                    .Include(pr => pr.PayrollEntries)
                    .Include(pr => pr.Company)
                    .AsTracking()
                    .FirstOrDefaultAsync(pr => pr.Id == payrollRunId)
                    ?? throw new Exception("PayrollRun not found.");

                if (payrollRun.Status != PayrollRunStatus.Draft)
                    throw new Exception("PayrollRun has already been processed.");

                // Initialize entries for new employees
                var existingEmployeeIds = payrollRun.PayrollEntries?
                    .Select(pe => pe.EmployeeId)
                    .ToHashSet() ?? [];

                foreach (var employee in payrollRun.Employees?
                    .Where(e => !existingEmployeeIds.Contains(e.Id))
                    ?? [])
                {
                    if (employee?.PayComponents == null)
                        throw new InvalidOperationException($"Employee {employee?.Id} has no pay components");

                    var payrollEntry = new PayrollEntry
                    {
                        PayrollRunId = payrollRun.Id,
                        EmployeeId = employee.Id,
                        GrossPay = 0,
                        TotalDeductions = 0,
                        NetPay = 0,
                        BankAccountNumber = employee.AccountNumber,
                        Details = []
                    };

                    _context.PayrollEntries.Add(payrollEntry);
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return payrollRun;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Payroll initialization failed for run {PayrollRunId}", payrollRunId);
                throw;
            }
        }

        // 2. Payroll Calculation
        public async Task<PayrollRun> ProcessPayrollEntriesAsync(Guid payrollRunId)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync(
                IsolationLevel.Serializable);

            try
            {
                var payrollRun = await _context.PayrollRuns
                    .Include(pr => pr.PayrollEntries)
                        .ThenInclude(pe => pe.Employee)
                        .ThenInclude(e => e.PayComponents)
                        .ThenInclude(epc => epc.PayComponent)
                    .Include(pr => pr.Company)
                    .AsTracking()
                    .FirstOrDefaultAsync(pr => pr.Id == payrollRunId)
                    ?? throw new Exception("PayrollRun not found.");

                if (payrollRun.PayrollEntries == null)
                    throw new Exception("PayrollEntries not loaded.");

                // First, process all entries without updating details
                foreach (var entry in payrollRun.PayrollEntries)
                {
                    if (entry.Employee?.PayComponents == null)
                        throw new InvalidOperationException($"Employee data missing for entry {entry.Id}");

                    // Reset and calculate
                    entry.GrossPay = 0;
                    entry.TotalDeductions = 0;

                    var basicSalary = GetBaseSalary(entry.Employee.PayComponents);

                    // First pass - regular components
                    foreach (var pc in entry.Employee.PayComponents
                        .Where(pc => pc.PayComponent?.CalculationType != CalculationType.PercentageOfEarnings))
                    {
                        ProcessComponent(entry, pc, basicSalary);
                    }

                    // Second pass - percentage-of-earnings
                    foreach (var pc in entry.Employee.PayComponents
                        .Where(pc => pc.PayComponent?.CalculationType == CalculationType.PercentageOfEarnings))
                    {
                        ProcessComponent(entry, pc, entry.GrossPay);
                    }

                    // Calculate statutory deductions
                    if (payrollRun.Company != null)
                    {
                        var bht = CalculateBht(entry.Employee.PayComponents);
                        var statutoryResult = await _statutoryCalculator.CalculateDeductions(basicSalary, entry.GrossPay, bht);
                        AddStatutoryDeductions(entry, statutoryResult);
                    }

                    entry.NetPay = entry.GrossPay - entry.TotalDeductions;
                }

                // Now update all details in a separate step
                await UpdateAllEntryDetailsAsync(payrollRun.PayrollEntries);

                // Update payroll totals
                payrollRun.TotalGrossPay = payrollRun.PayrollEntries.Sum(e => e.GrossPay);
                payrollRun.TotalDeductions = payrollRun.PayrollEntries.Sum(e => e.TotalDeductions);
                payrollRun.TotalNetPay = payrollRun.PayrollEntries.Sum(e => e.NetPay);
                payrollRun.Status = PayrollRunStatus.Calculated;

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return payrollRun;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Payroll processing failed for run {PayrollRunId}", payrollRunId);
                throw;
            }
        }

        private async Task UpdateAllEntryDetailsAsync(ICollection<PayrollEntry> entries)
        {
            // Load all existing details in one query
            var entryIds = entries.Select(e => e.Id).ToList();
            var allExistingDetails = await _context.PayrollEntryDetails
                .Where(d => entryIds.Contains(d.PayrollEntryId))
                .ToListAsync();

            var detailsByEntryId = allExistingDetails
                .GroupBy(d => d.PayrollEntryId)
                .ToDictionary(g => g.Key, g => g.ToDictionary(d => d.PayComponentId));

            foreach (var entry in entries)
            {
                entry.Details ??= new List<PayrollEntryDetail>();

                var existingDetails = detailsByEntryId.TryGetValue(entry.Id, out var details)
                    ? details
                    : new Dictionary<Guid, PayrollEntryDetail>();

                foreach (var empPc in entry.Employee.PayComponents)
                {
                    if (empPc?.PayComponent == null) continue;

                    var amount = GetComponentAmount(empPc, entry.GrossPay);
                    var componentId = empPc.PayComponent.Id;

                    if (existingDetails.TryGetValue(componentId, out var detail))
                    {
                        // Update existing detail
                        detail.Amount = amount;
                        detail.Description = empPc.PayComponent.Code ?? empPc.PayComponent.Name;
                        detail.Category = empPc.PayComponent.Category.ToString();
                        detail.IsTaxable = empPc.PayComponent.IsTaxable;
                    }
                    else
                    {
                        // Add new detail
                        var newDetail = new PayrollEntryDetail
                        {
                            Id = Guid.NewGuid(),
                            PayrollEntryId = entry.Id,
                            PayComponentId = componentId,
                            Amount = amount,
                            Description = empPc.PayComponent.Code ?? empPc.PayComponent.Name,
                            Category = empPc.PayComponent.Category.ToString(),
                            IsTaxable = empPc.PayComponent.IsTaxable,
                        };
                        _context.PayrollEntryDetails.Add(newDetail);
                        entry.Details.Add(newDetail);
                    }
                }

                // Remove obsolete details
                var currentComponentIds = entry.Employee.PayComponents
                    .Where(pc => pc?.PayComponent != null)
                    .Select(pc => pc.PayComponent.Id)
                    .ToHashSet();

                var obsoleteDetails = existingDetails.Values
                    .Where(d => !currentComponentIds.Contains(d.PayComponentId))
                    .ToList();

                foreach (var detail in obsoleteDetails)
                {
                    _context.PayrollEntryDetails.Remove(detail);
                    entry.Details.Remove(detail);
                }
            }
        }


        private static void AddStatutoryDeductions(PayrollEntry entry, StatutoryCalculatorService.StatutoryCalculationResult result)
        {
            // Add statutory deductions to the entry
            entry.TotalDeductions += result.PensionEmployee;
            entry.TotalDeductions += result.NHF;
            entry.TotalDeductions += result.PAYE;
            entry.NHF = result.NHF;
            entry.PensionEmployee = result.PensionEmployee;
            entry.PensionEmployer = result.PensionEmployer;
            entry.PAYETax = result.PAYE;
            entry.PensionEmployee = result.PensionEmployee;
            entry.PensionEmployer = result.PensionEmployer;
            entry.NHF = result.NHF;


            // Add employer contributions (these don't affect employee's net pay)
            // You might want to track these separately for reporting purposes
        }

        // 3. Helper Methods
        private static void ProcessComponent(PayrollEntry entry, EmployeePayComponent empPayComponent, decimal baseAmount)
        {
            var component = empPayComponent.PayComponent
                ?? throw new InvalidOperationException("PayComponent is null");

            decimal amount = component.CalculationType switch
            {
                CalculationType.FixedAmount => empPayComponent.Amount,
                CalculationType.PercentageOfBase => baseAmount * (empPayComponent.Amount / 100),
                CalculationType.PercentageOfEarnings => baseAmount * (empPayComponent.Amount / 100),
                _ => empPayComponent.Amount
            };

            switch (component.Category)
            {
                case PayComponentCategory.Earnings:
                    entry.GrossPay += amount;
                    break;
                case PayComponentCategory.Deduction:
                case PayComponentCategory.Tax:
                    entry.TotalDeductions += amount;
                    break;
            }
        }

        private static decimal GetBaseSalary(ICollection<EmployeePayComponent> payComponents)
        {
            return payComponents?
                .Where(pc => pc?.PayComponent != null &&
                      pc.PayComponent.Code?.Equals("Basic_Salary", StringComparison.OrdinalIgnoreCase) == true)
                .Sum(pc => pc.Amount)
                ?? throw new InvalidOperationException("No valid base salary found");
        }

        private static decimal GetComponentAmount(EmployeePayComponent empPc, decimal grossPay)
        {
            if (empPc?.PayComponent == null)
                throw new InvalidOperationException("Invalid pay component");

            if (empPc.PayComponent.Code?.Equals("Basic_Salary", StringComparison.OrdinalIgnoreCase) == true)
                return empPc.Amount;

            return empPc.PayComponent.CalculationType switch
            {
                CalculationType.FixedAmount => empPc.Amount,
                CalculationType.PercentageOfBase => GetBaseSalary(empPc.Employee.PayComponents) * (empPc.Amount / 100),
                CalculationType.PercentageOfEarnings => grossPay * (empPc.Amount / 100),
                _ => empPc.Amount
            };
        }

        private static decimal CalculateBht(ICollection<EmployeePayComponent> payComponents)
        {
            var basicSalary = payComponents?
                   .Where(pc => pc?.PayComponent != null &&
                         pc.PayComponent.Code?.Equals("Basic_Salary", StringComparison.OrdinalIgnoreCase) == true)
                   .Sum(pc => pc.Amount);

            var housingAllowance = payComponents?.FirstOrDefault(pc => pc?.PayComponent != null &&
                  pc.PayComponent.Code?.Equals("housing_allowance", StringComparison.OrdinalIgnoreCase) == true)?.Amount
            ?? throw new InvalidOperationException("No valid housing allowance found");

            var transportAllowance = payComponents?.FirstOrDefault(pc => pc?.PayComponent != null &&
                  pc.PayComponent.Code?.Equals("transport_allowance", StringComparison.OrdinalIgnoreCase) == true)?.Amount
            ?? throw new InvalidOperationException("No valid transport allowance found");

            return basicSalary + housingAllowance + transportAllowance ?? 0;

        }

        // 4. Finalization
        public async Task<bool> FinalizePayrollRunAsync(Guid payrollRunId)
        {
            var payrollRun = await _context.PayrollRuns
                .FirstOrDefaultAsync(pr => pr.Id == payrollRunId)
                ?? throw new Exception("PayrollRun not found");

            if (payrollRun.Status != PayrollRunStatus.Approved)
                throw new Exception("PayrollRun must be approved before finalization");

            payrollRun.Status = PayrollRunStatus.Finalized;
            payrollRun.FinalizedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}