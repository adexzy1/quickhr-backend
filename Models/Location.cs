using qwikhr.Common;

namespace qwikhr.Models
{
    public class Location : CompanyEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;
        public bool IsActive { get; set; }


        // Navigation properties
        public ICollection<Employee> Employees { get; set; } = [];
        public virtual ICollection<LocationPayRule>? PayRules { get; set; } = [];
    }
}