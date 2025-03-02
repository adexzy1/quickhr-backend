namespace qwikhr.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
[Table("branches")]

public class Branch
{
    [Key]
    public int Id { get; set; }

    public Guid? Slug { get; set; } = Guid.NewGuid();

    [Required]
    [MaxLength(50)]
    public string? Name { get; set; }

    [ForeignKey("FK_CompanyId")]
    public int CompanyId { get; set; }
    public Company? Company { get; set; }
}