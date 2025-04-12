using Microsoft.EntityFrameworkCore;
using qwikhr.Data;
using qwikhr.Models.Payroll;

namespace qwikhr.Services
{
    public class PayrollService
    {
        private readonly ApplicationDbContext _context;

        public PayrollService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Consolidated Workflow: Process Payroll
        public async Task<PayrollRun> RunPayrollWorkflowAsync(Guid payrollRunId)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync(
                System.Data.IsolationLevel.Serializable);

            try
            {
                // Fetch with all needed includes
                var payrollRun = await _context.PayrollRuns
                    .Include(pr => pr.Employees)
                        .ThenInclude(e => e.PayComponents)
                    .Include(pr => pr.PayrollEntries)
                        .ThenInclude(pe => pe.Details)
                    .AsTracking()
                    .FirstOrDefaultAsync(pr => pr.Id == payrollRunId);

                if (payrollRun == null)
                {
                    throw new Exception("PayrollRun not found.");
                }

                if (payrollRun.Status != PayrollRunStatus.Draft)
                {
                    throw new Exception("PayrollRun has already been processed.");
                }

                // Initialize entries
                var existingEmployeeIds = payrollRun.PayrollEntries.Select(pe => pe.EmployeeId).ToHashSet();
                var newEmployees = payrollRun.Employees.Where(e => !existingEmployeeIds.Contains(e.Id)).ToList();

                foreach (var employee in newEmployees)
                {
                    var payrollEntry = new PayrollEntry
                    {
                        PayrollRunId = payrollRun.Id,
                        EmployeeId = employee.Id,
                        GrossPay = 0,
                        TotalDeductions = 0,
                        NetPay = 0,
                        Details = []
                    };

                    // First add the entry to get an ID
                    _context.PayrollEntries.Add(payrollEntry);

                    // Now create details with the proper relationship
                    foreach (var payComponent in employee.PayComponents)
                    {
                        payrollEntry.Details.Add(new PayrollEntryDetail
                        {
                            PayrollEntry = payrollEntry, // Set navigation property instead of ID
                            PayComponentId = payComponent.PayComponentId,
                            Amount = 0,
                            Units = 0,
                            Description = payComponent.PayComponent?.Code,
                            Category = "Allowance",
                        });
                    }
                }

                // Save after all entries and details are created
                await _context.SaveChangesAsync();

                // Update status
                payrollRun.Status = PayrollRunStatus.Calculated;

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return payrollRun;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                await transaction.RollbackAsync();
                throw new Exception("Payroll data was modified by another process. Please retry.", ex);
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
        // Process Payroll Entries with Nigerian Payroll Rules
    
        public async Task<PayrollRun> ProcessPayrollEntriesAsync(Guid payrollRunId)
{
    // Use a transaction with retry for deadlocks
    var executionStrategy = _context.Database.CreateExecutionStrategy();
    
    return await executionStrategy.ExecuteAsync(async () =>
    {
        using var transaction = await _context.Database.BeginTransactionAsync();
        
        try
        {
            // Load payroll run WITH LOCK to prevent concurrent modifications
            var payrollRun = await _context.PayrollRuns
                .Include(pr => pr.PayrollEntries)
                    .ThenInclude(pe => pe.Details)
                .FirstOrDefaultAsync(pr => pr.Id == payrollRunId);

            if (payrollRun?.PayrollEntries == null)
                throw new Exception("PayrollRun or PayrollEntries not found.");

            // Process calculations
            foreach (var entry in payrollRun.PayrollEntries)
            {
                // Fetch payroll details
                var basicSalary = entry.Details?.FirstOrDefault(d => d.Description == "Basic Salary")?.Amount ?? 0;
                var housingAllowance = entry.Details?.FirstOrDefault(d => d.Description == "Housing Allowance")?.Amount ?? 0;
                var transportAllowance = entry.Details?.FirstOrDefault(d => d.Description == "Transport Allowance")?.Amount ?? 0;
                var otherAllowances = entry.Details?.Where(d => d.Category == "Allowance" && d.Description != "Basic Salary" && d.Description != "Housing Allowance" && d.Description != "Transport Allowance")
                    .Sum(d => d.Amount) ?? 0;

                // Calculate gross pay
                entry.GrossPay = basicSalary + housingAllowance + transportAllowance + otherAllowances;

                // Calculate statutory deductions
                var pensionEmployee = CalculatePensionContribution(basicSalary, 0.08m); // 8% of basic salary
                var pensionEmployer = CalculatePensionContribution(basicSalary, 0.10m); // 10% of basic salary
                var nhf = CalculateNHF(basicSalary); // 2.5% of basic salary
                var nsitf = CalculateNSITF(entry.GrossPay); // NSITF based on gross pay
                var paye = CalculatePAYE(entry.GrossPay); // PAYE tax based on gross pay

                // Total deductions
                entry.TotalDeductions = pensionEmployee + nhf + nsitf + paye;

                // Net pay
                entry.NetPay = entry.GrossPay - entry.TotalDeductions;

                // Update payroll entry details for deductions
                UpdatePayrollEntryDetail(entry, "Pension (Employee)", pensionEmployee, "Deduction");
                UpdatePayrollEntryDetail(entry, "Pension (Employer)", pensionEmployer, "Deduction");
                UpdatePayrollEntryDetail(entry, "NHF", nhf, "Deduction");
                UpdatePayrollEntryDetail(entry, "NSITF", nsitf, "Deduction");
                UpdatePayrollEntryDetail(entry, "PAYE", paye, "Deduction");
            }

            // Update totals
            payrollRun.TotalGrossPay = payrollRun.PayrollEntries.Sum(e => e.GrossPay);
            payrollRun.TotalDeductions = payrollRun.PayrollEntries.Sum(e => e.TotalDeductions);
            payrollRun.TotalNetPay = payrollRun.PayrollEntries.Sum(e => e.NetPay);
            payrollRun.Status = PayrollRunStatus.Calculated;

            // Save changes and commit
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();

            return payrollRun;
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            throw new Exception($"Payroll processing failed: {ex.Message}");
        }
    });
}

        // Helper: Calculate Pension Contribution
        private static decimal CalculatePensionContribution(decimal basicSalary, decimal rate)
        {
            return basicSalary * rate;
        }

        // Helper: Calculate NHF (National Housing Fund)
        private static decimal CalculateNHF(decimal basicSalary)
        {
            return basicSalary * 0.025m; // 2.5% of basic salary
        }

        // Helper: Calculate NSITF (Nigeria Social Insurance Trust Fund)
        private static decimal CalculateNSITF(decimal grossPay)
        {
            return grossPay * 0.01m; // 1% of gross pay
        }

        // Helper: Calculate PAYE (Pay-As-You-Earn Tax)
        private static decimal CalculatePAYE(decimal grossPay)
        {
            if (grossPay <= 30000)
                return grossPay * 0.07m; // 7% for income <= 30,000
            else if (grossPay <= 60000)
                return grossPay * 0.11m; // 11% for income <= 60,000
            else if (grossPay <= 110000)
                return grossPay * 0.15m; // 15% for income <= 110,000
            else if (grossPay <= 160000)
                return grossPay * 0.19m; // 19% for income <= 160,000
            else if (grossPay <= 320000)
                return grossPay * 0.21m; // 21% for income <= 320,000
            else
                return grossPay * 0.24m; // 24% for income > 320,000
        }

        // Helper: Update Payroll Entry Details
        private static void UpdatePayrollEntryDetail(PayrollEntry entry, string description, decimal amount, string category)
        {
            var detail = entry.Details?.FirstOrDefault(d => d.Description == description);
            if (detail != null)
            {
                detail.Amount = amount;
            }
            else
            {
                entry.Details?.Add(new PayrollEntryDetail
                {
                    Description = description,
                    Amount = amount,
                    Category = category
                });
            }
        }



        // Finalize PayrollRun
        public async Task<bool> FinalizePayrollRunAsync(Guid payrollRunId)
        {
            // Fetch the payroll run
            var payrollRun = await _context.PayrollRuns
                .Include(pr => pr.PayrollEntries)
                .FirstOrDefaultAsync(pr => pr.Id == payrollRunId);

            if (payrollRun == null)
                throw new Exception("PayrollRun not found.");

            // Ensure the payroll run is fully approved
            if (payrollRun.Status != PayrollRunStatus.Approved)
                throw new Exception("PayrollRun must be fully approved before finalization.");

            // Mark the payroll run as finalized
            payrollRun.Status = PayrollRunStatus.Finalized;
            payrollRun.FinalizedAt = DateTime.UtcNow;

            // Save changes
            await _context.SaveChangesAsync();

            return true;
        }
    }
}