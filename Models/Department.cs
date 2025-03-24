namespace qwikhr.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using qwikhr.Common;

[Table("departments")]

public class Department : CompanyEntity
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    [MaxLength(50)]
    public string? Name { get; set; }

    public Guid? ManagerId { get; set; }
    [ForeignKey("ManagerId")]
    public Employee? Manager { get; set; }
    public ICollection<Employee> Employees { get; set; } = [];
}