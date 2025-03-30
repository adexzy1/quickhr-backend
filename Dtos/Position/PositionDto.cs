using System.ComponentModel.DataAnnotations;

namespace qwikhr.Dtos.Position
{
    public class PositionDto
    {
        public Guid Id { get; set; }
        [Required]
        public required string Name { get; set; }
    }

}