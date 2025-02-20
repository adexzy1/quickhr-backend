namespace qwikhr.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
[Table("employees")]

public class Employee
{
    [Key]
    public int Id { get; set; }
    
    public Guid? Slug { get; set; }

    [Required]
    [MaxLength(50)]
    public string? FirstName { get; set; }

    [Required]
    [MaxLength(50)]
    public string? LastName { get; set; }

    [Required]
    [EnumDataType(typeof(EmployeeStatus))]
    public EmployeeStatus Status { get; set; }
    
    [ForeignKey("FK_CompanyId")]
    public int CompanyId { get; set; }
    public Company? Company { get; set; }
}

public enum EmployeeStatus
{
    Onboarding,
    Offboarding,
    OnLeave,
    Immigration,
    Active,
    Suspended,
    Exited
}