namespace qwikhr.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class State
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    
    [Required]
    [MaxLength(50)]
    public required string Code { get; set; }
    
    [Required]
    [MaxLength(50)]
    public required string Name { get; set; }

    [Required]
    [MaxLength(50)]
    public required string Url { get; set; }
    
    [Required]
    [MaxLength(50)]
    public required string Latitude { get; set; }
    
    [Required]
    [MaxLength(50)]
    public required string Longitude { get; set; }
    
    [Required]
    [MaxLength(50)]
    public required string Cities { get; set; }
    
    public Guid CountryId { get; set; }

    [ForeignKey("CountryId")]
    public Country? County { get; set; }
}