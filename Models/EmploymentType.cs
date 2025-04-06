using qwikhr.Common;

namespace qwikhr.Models
{
    public class EmploymentType : CompanyEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public required string Name { get; set; } // Full-time, Part-time, Contractor, etc.
        public string? Code { get; set; }
        public bool IsEligibleForBenefits { get; set; } = true;
    }
}