namespace qwikhr.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
[Table("positions")]

public class Position
{
    [Key]
    public int Id { get; set; }
    
    public Guid? Slug { get; set; }

    [Required]
    [MaxLength(50)]
    public string? FirstName { get; set; }
    
    [ForeignKey("FK_CompanyId")]
    public int CompanyId { get; set; }
    public Company? Company { get; set; }
}