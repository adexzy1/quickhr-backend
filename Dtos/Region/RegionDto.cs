using qwikhr.Dtos.Branch;

namespace qwikhr.Dtos.Region;

public class RegionDto
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}