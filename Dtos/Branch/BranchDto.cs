namespace qwikhr.Dtos.Branch
{
    public class BranchDto
    {
        public int Id { get; set; }
        public Guid? Slug { get; set; }
        public string? Name { get; set; }
        public int CompanyId { get; set; }
    }
}