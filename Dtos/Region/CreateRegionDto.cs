using System.ComponentModel.DataAnnotations;

namespace qwikhr.Dtos.Region
{
    public class CreateRegionDto
    {
        [Required]
        public required string Name { get; set; }
    }
}