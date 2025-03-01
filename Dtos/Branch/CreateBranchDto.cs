namespace qwikhr.Dtos.Branch
{
    public class CreateBranchDto
    {
        public required string Name { get; set; }
        public int CompanyId { get; set; }
    }
}