using System.ComponentModel.DataAnnotations;

namespace qwikhr.Dtos.Position
{
    public class UpdatePositionDto
    {
        [Required]
        public required string Name { get; set; }
    }
}