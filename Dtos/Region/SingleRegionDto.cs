using qwikhr.Dtos.Branch;

namespace qwikhr.Dtos.Region
{
    public class SingleRegionDto : RegionDto
    {
        public List<BranchDto> Branches { get; set; } = [];
    }
}