using System.ComponentModel.DataAnnotations.Schema;
using qwikhr.Common;

namespace qwikhr.Models
{
    public class LocationPayRule : CompanyEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid LocationId { get; set; }
        public required string RuleType { get; set; } // "MinimumWage", "TaxRule", etc.
        public string? Description { get; set; }
        public decimal Amount { get; set; }
        public DateTime EffectiveDate { get; set; }
        public DateTime? EndDate { get; set; }

        [ForeignKey("LocationId")]
        public virtual Location? Location { get; set; }
    }

}