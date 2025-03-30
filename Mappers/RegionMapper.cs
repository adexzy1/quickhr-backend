using qwikhr.Dtos.Region;
using qwikhr.Models;


namespace qwikhr.Mappers
{

    public static class RegionMapper
    {
        public static RegionDto ToRegionDto(this Region regionModel)
        {
            return new RegionDto
            {
                Id = regionModel.Id,
                Name = regionModel.Name,
                CreatedAt = regionModel.CreatedAt,
            };
        }

        public static Region ToRegionFromCreateDto(this CreateRegionDto createDto)
        {
            return new Region
            {
                Name = createDto.Name,
            };
        }

        public static SingleRegionDto ToSingleRegionDto(this Region regionModel)
        {
            return new SingleRegionDto
            {
                Id = regionModel.Id,
                Name = regionModel.Name,
                Branches = [.. regionModel.Branches.Select(b => b.ToBranchDto())]
            };
        }
    }
}