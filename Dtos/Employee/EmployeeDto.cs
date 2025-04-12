using System;
using System.ComponentModel.DataAnnotations;
using qwikhr.Dtos.Generic;
using qwikhr.Dtos.Payroll;
using qwikhr.Models;

namespace qwikhr.Dtos.Employee
{
    public class EmployeeDto
    {
        public Guid Id { get; set; }
        // Personal Information
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; } = string.Empty; // Male, Female, Other
        public string MaritalStatus { get; set; } = string.Empty; // Single, Married, etc.
        public DateTime EmploymentDate { get; set; } // When the employee was hired
        public string BankName { get; set; } = string.Empty;
        public string AccountNumber { get; set; } = string.Empty;
        public string BVN { get; set; } = string.Empty;
        public string PensionFundAdministrator { get; set; } = string.Empty;
        public string PensionNumber { get; set; } = string.Empty;
        public string TaxIdentificationNumber { get; set; } = string.Empty;
        public string NextOfKinName { get; set; } = string.Empty;
        public string NextOfKinPhone { get; set; } = string.Empty;
        public string NextOfKinRelationship { get; set; } = string.Empty;
        public DateTime? TerminationDate { get; set; }
        public EntityDto? EmploymentType { get; set; } = new();
        public EntityDto? PayGrade { get; set; } = new();
        public EntityDto? Department { get; set; } = new();
        public EntityDto? Position { get; set; } = new();
    }

    public class SingleEmployeeDto : EmployeeDto
    {
        public List<EmployeePayComponentDto> EmployeePayComponents { get; set; } = [];
    }
}