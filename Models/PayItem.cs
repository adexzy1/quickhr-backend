namespace qwikhr.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using qwikhr.Common;

[Table("pay_items")]

public class PayItem : CompanyEntity
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
}