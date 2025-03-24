using System.ComponentModel.DataAnnotations;

namespace qwikhr.Dtos.Employee
{
    public class CreateEmployeeDto
    {
        // Personal Information
        [Required]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        public string LastName { get; set; } = string.Empty;
        public string? MiddleName { get; set; } // Optional
        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required, Phone]
        public string PhoneNumber { get; set; } = string.Empty;
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public string Gender { get; set; } = string.Empty; // Male, Female, Other
        [Required]
        public string MaritalStatus { get; set; } = string.Empty; // Single, Married, etc.

        // Employment Details
        [Required]
        public DateTime EmploymentDate { get; set; } // When the employee was hired
        [Required]
        public string EmploymentType { get; set; } = "Full-Time"; // Full-Time, Contract, Intern
        [Required]
        public Guid PositionId { get; set; }
        [Required]
        public Guid DepartmentId { get; set; }

        // Financial & Statutory Information
        public string BankName { get; set; } = string.Empty;
        public string AccountNumber { get; set; } = string.Empty;
        public string BVN { get; set; } = string.Empty; // Bank Verification Number
        public string PensionFundAdministrator { get; set; } = string.Empty;
        public string PensionNumber { get; set; } = string.Empty;
        public string TaxIdentificationNumber { get; set; } = string.Empty; // TIN

        // Next of Kin (Emergency Contact)
        public string NextOfKinName { get; set; } = string.Empty;
        public string NextOfKinPhone { get; set; } = string.Empty;
        public string NextOfKinRelationship { get; set; } = string.Empty;

        // Payroll & Salary Details
        public Guid PayGradeId { get; set; } // Salary is derived from PayGrade
    }

}