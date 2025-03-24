using System.ComponentModel.DataAnnotations;

namespace qwikhr.Dtos.Employee
{
    public class UpdateEmployeeDto
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public decimal? Salary { get; set; }
        public Guid? PositionId { get; set; }
        public Guid? DepartmentId { get; set; }
        public Guid? PayCategoryId { get; set; }

        // ✅ Employment Details
        public DateTime? HireDate { get; set; }
        public DateTime? TerminationDate { get; set; }
        public string? EmploymentType { get; set; }

        // ✅ Bank Information
        public string? BankName { get; set; }
        public string? AccountNumber { get; set; }

        // ✅ Tax & Pension Information
        public string? TaxIdentificationNumber { get; set; }
        public string? PensionFundAdministrator { get; set; }
        public string? PensionAccountNumber { get; set; }

        // ✅ Next of Kin Details
        public string? NextOfKinName { get; set; }
        public string? NextOfKinPhone { get; set; }
        public string? NextOfKinRelationship { get; set; }

        public List<EmployeeAllowanceDto>? Allowances { get; set; }
        public List<EmployeeDeductionDto>? Deductions { get; set; }
    }

}