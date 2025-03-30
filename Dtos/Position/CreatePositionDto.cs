using System.ComponentModel.DataAnnotations;

namespace qwikhr.Dtos.Position
{
    public class CreatePositionDto
    {
        [Required]
        public required string Name { get; set; }
    }
}