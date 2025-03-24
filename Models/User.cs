namespace qwikhr.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

[Table("users")]

public class User : IdentityUser<int>
{
    public Guid? Slug { get; set; } = Guid.NewGuid();

    [MaxLength(50)]
    public string? FirstName { get; set; }

    [MaxLength(50)]
    public string? LastName { get; set; }
    [MaxLength(50)]
    public string? MiddleName { get; set; }

    public bool Status { get; set; } = true;

    [ForeignKey("EmployeeId")]
    public Employee? Employee { get; set; }
    public Guid CompanyId { get; set; }
    [ForeignKey("CompanyId")]
    public Company? Company { get; set; }
}



