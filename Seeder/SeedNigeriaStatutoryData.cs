using Microsoft.EntityFrameworkCore;
using qwikhr.Models.Payroll;

namespace qwikhr.Seeder;

public static class SeedNigeriaStatutoryData
{
    public static void Seed(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<StatutoryDeduction>().HasData(
            // PENSION (RSA)
            new StatutoryDeduction
            {
                Id = Guid.Parse("18e12e5d-7a5e-4920-9b85-411b5b1e0c21"),
                Name = "Pension Contribution",
                Code = "PEN",
                EmployeeRate = 0.08m,  // 8%
                EmployerRate = 0.10m,  // 10%
                LegalReference = "Pension Reform Act 2014 Section 4(1)",
                ApplyToAllCompanies = true,
                EffectiveDate = new DateTime(2014, 7, 1, 0, 0, 0, DateTimeKind.Utc), // Ensure UTC
                IsActive = true
            },

            // NHF
            new StatutoryDeduction
            {
                Id = Guid.Parse("4a7b1e9f-2c63-4e9a-b7a6-8d3f1e5d2c8a"),
                Name = "National Housing Fund",
                Code = "NHF",
                EmployeeRate = 0.025m,  // 2.5%
                LegalReference = "NHF Act 1992 Section 6",
                ApplyToAllCompanies = true,
                EffectiveDate = new DateTime(1992, 1, 1, 0, 0, 0, DateTimeKind.Utc), // Ensure UTC
                IsActive = true
            },

            // PAYE
            new StatutoryDeduction
            {
                Id = Guid.Parse("7f3e8d2a-1b5c-4e9f-a8d7-6c3b9f1e5d2a"),
                Name = "PAYE Tax",
                Code = "PAYE",
                EmployeeRate = 0m, // Progressive rates handled separately
                LegalReference = "PITA 2011 as amended",
                ApplyToAllCompanies = true,
                EffectiveDate = new DateTime(2020, 1, 1, 0, 0, 0, DateTimeKind.Utc), // Ensure UTC
                IsActive = true
            },

            // NSITF
            new StatutoryDeduction
            {
                Id = Guid.Parse("2b8e1d5a-9c3f-4e7d-b6a1-8d5f3e2c1a7b"),
                Name = "NSITF Contribution",
                Code = "NSITF",
                EmployerRate = 0.01m,  // 1% (employer-paid)
                LegalReference = "Employee Compensation Act 2010",
                ApplyToAllCompanies = true,
                EffectiveDate = new DateTime(2011, 1, 1, 0, 0, 0, DateTimeKind.Utc), // Ensure UTC
                IsActive = true
            },

            // ITF
            new StatutoryDeduction
            {
                Id = Guid.Parse("5d3e8f2a-1b7c-4e9a-a8d6-7c3b8f1e5d2b"),
                Name = "ITF Levy",
                Code = "ITF",
                EmployerRate = 0.01m,  // 1% (employer-paid)
                LegalReference = "ITF Act 2011 Section 6(1)",
                ApplyToAllCompanies = true,
                EffectiveDate = new DateTime(2011, 1, 1, 0, 0, 0, DateTimeKind.Utc), // Ensure UTC
                IsActive = true
            },

            // NHIA (Health Insurance)
            new StatutoryDeduction
            {
                Id = Guid.Parse("9a8b7c6d-5e4f-3a2b-1c0d-9e8f7a6b5c4d"),
                Name = "NHIA Contribution",
                Code = "NHIA",
                EmployeeRate = 0.015m,  // 1.5%
                EmployerRate = 0.035m,  // 3.5%
                LegalReference = "NHIA Act 2022 Section 25",
                ApplyToAllCompanies = true,
                EffectiveDate = new DateTime(2022, 5, 1, 0, 0, 0, DateTimeKind.Utc), // Ensure UTC
                IsActive = true
            }
        );

        modelBuilder.Entity<PayeTaxBand>().HasData(
            new PayeTaxBand { Id = 1, LowerBound = 0, UpperBound = 300000, Rate = 0.07m, AnnualCumulative = 0 },
            new PayeTaxBand { Id = 2, LowerBound = 300001, UpperBound = 600000, Rate = 0.11m, AnnualCumulative = 21000 },
            new PayeTaxBand { Id = 3, LowerBound = 600001, UpperBound = 1100000, Rate = 0.15m, AnnualCumulative = 54000 },
            new PayeTaxBand { Id = 4, LowerBound = 1100001, UpperBound = 1600000, Rate = 0.19m, AnnualCumulative = 129000 },
            new PayeTaxBand { Id = 5, LowerBound = 1600001, UpperBound = 3200000, Rate = 0.21m, AnnualCumulative = 224000 },
            new PayeTaxBand { Id = 6, LowerBound = 3200001, UpperBound = null, Rate = 0.24m, AnnualCumulative = 560000 }
        );
    }
}