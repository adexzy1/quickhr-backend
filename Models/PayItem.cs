namespace qwikhr.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
[Table("pay_items")]

public class PayItem
{
    [Key]
    public int Id { get; set; }
    
    public Guid? Slug { get; set; }
    
    [ForeignKey("FK_CompanyId")]
    public int CompanyId { get; set; }
    public Company? Company { get; set; }
}