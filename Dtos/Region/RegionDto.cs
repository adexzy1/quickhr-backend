namespace qwikhr.Dtos.Region;

public class RegionDto
{
    public int Id { get; set; }
    public Guid Slug { get; set; }
    public string? Name { get; set; }
    public int CompanyId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}