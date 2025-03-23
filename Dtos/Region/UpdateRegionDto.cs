using System.ComponentModel.DataAnnotations;

namespace qwikhr.Dtos.Region
{
    public class UpdateRegionDto
    {
        [Required]
        public string? Name { get; set; }
    }
}