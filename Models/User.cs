namespace qwikhr.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
[Table("users")]

public class User
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
    [MaxLength(50)]
    public string? Username { get; set; }

    [Required]
    [EnumDataType(typeof(UserRole))]
    public UserRole Role { get; set; }

    [Required]
    [EnumDataType(typeof(UserStatus))]
    public UserStatus Status { get; set; }

    [Required]
    [EmailAddress]
    public string? Email { get; set; }

    [Required]
    [MinLength(6)]
    public string? Password { get; set; }

    [ForeignKey("Employee")]
    public int EmployeeId { get; set; }
    
    [ForeignKey("FK_CompanyId")]
    public int CompanyId { get; set; }
    public Company? Company { get; set; }
}

public enum UserRole
{
    Admin,
    Hr,
    Employee,
    Manager
}

public enum UserStatus
{
    Active,
    Inactive
}