namespace qwikhr.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
[Table("companies")]

public class Company
{
    [Key]
    public int Id { get; set; }
    
    public Guid? Slug { get; set; }
}