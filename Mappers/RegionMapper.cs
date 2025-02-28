using qwikhr.Dtos.Region;
using qwikhr.Models;
using Riok.Mapperly.Abstractions;

namespace qwikhr.Mappers;

[Mapper]
public partial class RegionMapper
{
    public partial RegionDto RegionToDto(Region region);
    public partial Region DtoToRegion(RegionDto regionDto);
    public partial IReadOnlyList<RegionDto> RegionListToDtoList(IReadOnlyList<Region> regions);
}