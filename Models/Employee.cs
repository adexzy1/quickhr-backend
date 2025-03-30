using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using qwikhr.Common;
using qwikhr.Models.Payroll;

namespace qwikhr.Models
{
    public class Employee : CompanyEntity
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

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

        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; } = string.Empty; // Male, Female, Other
        public string MaritalStatus { get; set; } = string.Empty; // Single, Married, etc.

        [Required]
        public DateTime EmploymentDate { get; set; } // When the employee was hired
        public DateTime? TerminationDate { get; set; }

        public string EmploymentType { get; set; } = "Full-Time"; // Full-Time, Contract, Intern

        public Guid PositionId { get; set; }
        [ForeignKey("PositionId")]
        public Position? Position { get; set; }

        public Guid DepartmentId { get; set; }
        [ForeignKey("DepartmentId")]
        public Department? Department { get; set; }

        // Financial & Statutory Information (Nigeria-Specific)
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

        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User? User { get; set; }

        // salary
        public Guid PayGradeId { get; set; }  // Reference to PayGrade
        [ForeignKey("PayGradeId")]
        public PayGrade? PayGrade { get; set; }

        public ICollection<EmployeePayAdjustment> PayAdjustments { get; set; } = [];

        // Leave & Shift Management
        public ICollection<EmployeeLeaveBalance> LeaveBalances { get; set; } = [];
        public ICollection<LeaveRequest> LeaveRequests { get; set; } = [];
    }
}