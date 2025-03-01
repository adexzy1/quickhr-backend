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
                Slug = regionModel.Slug,
                Name = regionModel.Name,
                CreatedAt = regionModel.CreatedAt,
                CompanyId = regionModel.CompanyId
            };
        }

        public static Region ToRegionFromCreateDto(this CreateRegionDto createDto)
        {
            return new Region
            {
                Name = createDto.Name,
                CompanyId = createDto.CompanyId,
                Slug = Guid.NewGuid()
            };
        }
    }
}