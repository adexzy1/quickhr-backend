using System.Runtime.CompilerServices;

namespace qwikhr.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
[Table("companies")]

public class Company
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    
    [MaxLength(50)]
    public required string Name { get; set; }
    
    [MaxLength(255)]
    public string? Email { get; set; }
    
    [MaxLength(50)]
    public string? Phone { get; set; }
    
    [MaxLength(50)]
    public string? AltPhone { get; set; }
    
    [MaxLength(255)]
    public string? AddressLine1 { get; set; }
    
    [MaxLength(50)]
    public string? AddressLine2 { get; set; }
    
    [MaxLength(50)]
    public string? City { get; set; }
    
    [MaxLength(50)]
    public string? State { get; set; }
    
    [MaxLength(50)]
    public string? ZipCode { get; set; }
    
    [MaxLength(50)]
    public string? Country { get; set; }
    
    [MaxLength(255)]
    public string? Website { get; set; }
    
    [MaxLength(50)]
    public string? Status { get; set; }
}