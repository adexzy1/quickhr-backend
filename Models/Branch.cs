namespace qwikhr.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using qwikhr.Common;

[Table("branches")]

public class Branch : CompanyEntity
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    [Required]
    [MaxLength(50)]
    public string? Name { get; set; }
    public Guid RegionId { get; set; }
    [ForeignKey("RegionId")]
    public Region? Region { get; set; }
}