using System.ComponentModel.DataAnnotations.Schema;
using qwikhr.Models;

namespace qwikhr.Common
{

    public abstract class CompanyEntity
    {
        public Guid CompanyId { get; set; }

        [ForeignKey("CompanyId")]
        public Company? Company { get; set; }
    }

}