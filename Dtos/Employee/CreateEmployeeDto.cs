using System.ComponentModel.DataAnnotations;

namespace qwikhr.Dtos.Employee
{
    public class CreateEmployeeDto
    {
        // ✅ Personal Information
        [Required]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        public string LastName { get; set; } = string.Empty;
        public string? MiddleName { get; set; } // Optional
        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty; // Used for Identity User creation
        [Required, Phone]
        public string PhoneNumber { get; set; } = string.Empty; // Used for Identity User creation
        [Required]
        public required DateTime DateOfBirth { get; set; }
        [Required]
        public required string Gender { get; set; } // Male, Female, Other
        [Required]
        public required string MaritalStatus { get; set; } // Single, Married, etc.

        // ✅ Employment Details
        [Required]
        public DateTime EmploymentDate { get; set; } // When the employee was hired
        public Guid EmploymentTypeId { get; set; } // Full-Time, Contract, Intern
        public Guid PositionId { get; set; }
        public Guid DepartmentId { get; set; }
        public Guid PayGradeId { get; set; }

        // ✅ Bank Information
        public string? BankName { get; set; }
        public string? AccountNumber { get; set; }
        public string? BVN { get; set; } // Bank Verification Number

        // ✅ Tax & Pension Information
        public string? TaxIdentificationNumber { get; set; } // TIN
        public string? PensionFundAdministrator { get; set; }
        public string? PensionNumber { get; set; }

        // ✅ Next of Kin Details
        public string? NextOfKinName { get; set; }
        public string? NextOfKinPhone { get; set; }
        public string? NextOfKinRelationship { get; set; }
    }
}