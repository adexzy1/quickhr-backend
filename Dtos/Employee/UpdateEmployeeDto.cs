using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using qwikhr.Models;

namespace qwikhr.Dtos.Employee
{
    public class UpdateEmployeeDto
    {
        // ✅ Personal Information
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? MiddleName { get; set; } // Optional
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Gender { get; set; } // Male, Female, Other
        public string? MaritalStatus { get; set; } // Single, Married, etc.

        // ✅ Employment Details
        public DateTime? EmploymentDate { get; set; } // When the employee was hired
        public DateTime? TerminationDate { get; set; } // When the employee was terminated
        public EmploymentType? EmploymentType { get; set; } // Full-Time, Contract, Intern
        public Guid? PositionId { get; set; }
        public Guid? DepartmentId { get; set; }
        public Guid? PayGradeId { get; set; }

        // ✅ Bank Information
        public string? BankName { get; set; }
        public string? AccountNumber { get; set; }
        public string? BVN { get; set; } // Bank Verification Number

        // ✅ Tax & Pension Information
        public string? TaxIdentificationNumber { get; set; } // TIN
        public string? PensionFundAdministrator { get; set; }
        public string? PensionAccountNumber { get; set; }

        // ✅ Next of Kin Details
        public string? NextOfKinName { get; set; }
        public string? NextOfKinPhone { get; set; }
        public string? NextOfKinRelationship { get; set; }

        // ✅ Additional Information
        public List<EmployeeAllowanceDto>? Allowances { get; set; }
        public List<EmployeeDeductionDto>? Deductions { get; set; }
        public string? PensionNumber { get; internal set; }
    }
}