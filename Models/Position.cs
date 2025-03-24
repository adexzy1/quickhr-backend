namespace qwikhr.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using qwikhr.Common;

[Table("positions")]

public class Position : CompanyEntity
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    [Required]
    [MaxLength(50)]
    public required string Name { get; set; }
    public ICollection<Employee> Employees { get; set; } = [];
}