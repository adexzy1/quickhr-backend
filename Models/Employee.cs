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
        public string? MiddleName { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required, Phone]
        public string PhoneNumber { get; set; } = string.Empty;
        public required DateTime DateOfBirth { get; set; }
        public string Gender { get; set; } = string.Empty;
        public string MaritalStatus { get; set; } = string.Empty;

        // Employment Information
        [Required]
        public DateTime EmploymentDate { get; set; }
        public DateTime? TerminationDate { get; set; }
        public Guid EmploymentTypeId { get; set; }
        public Guid PositionId { get; set; }
        public Guid DepartmentId { get; set; }
        public Guid PayGradeId { get; set; }
        public int UserId { get; set; } // Foreign key to the Identity User
        public EmployeeStatus Status { get; set; } = EmployeeStatus.Active; // Enum-based status

        // Financial & Statutory Information
        public string BankName { get; set; } = string.Empty;
        public string AccountNumber { get; set; } = string.Empty;
        public string BVN { get; set; } = string.Empty;
        public string PensionFundAdministrator { get; set; } = string.Empty;
        public string PensionNumber { get; set; } = string.Empty;
        public string TaxIdentificationNumber { get; set; } = string.Empty;

        // Next of Kin (Emergency Contact)
        public string NextOfKinName { get; set; } = string.Empty;
        public string NextOfKinPhone { get; set; } = string.Empty;
        public string NextOfKinRelationship { get; set; } = string.Empty;

        // Navigation Properties
        [ForeignKey("PayGradeId")]
        public PayGrade? PayGrade { get; set; }
        [ForeignKey("PositionId")]
        public Position? Position { get; set; }
        [ForeignKey("DepartmentId")]
        public Department? Department { get; set; }
        [ForeignKey("UserId")]
        public User? User { get; set; }
        [ForeignKey("EmploymentTypeId")]
        public virtual EmploymentType? EmploymentType { get; set; }
        public virtual ICollection<EmployeePayComponent> PayComponents { get; set; } = [];
        public virtual ICollection<EmployeeLeaveBalance> LeaveBalances { get; set; } = [];
        public virtual ICollection<LeaveRequest> LeaveRequests { get; set; } = [];
    }
}
