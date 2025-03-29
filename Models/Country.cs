namespace qwikhr.Models;

using System.ComponentModel.DataAnnotations;

public class Country
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
    public required string States { get; set; }
}