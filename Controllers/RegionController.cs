using qwikhr.Mappers;
using Microsoft.AspNetCore.Mvc;
using qwikhr.Dtos.Region;
using qwikhr.Interfaces;

namespace qwikhr.Controllers
{
    [Route("api/regions")]
    [ApiController]
    public class RegionController(IRegionRepository regionRepository) : ControllerBase
    {
        private readonly IRegionRepository _regionRepo = regionRepository;

        [HttpGet]
        public async Task<IActionResult> GetRegions()
        {
            var regions = await _regionRepo.GetAllAsync();
            var regionsDto = regions.Select(r => r.ToRegionDto());
            return Ok(regionsDto);
        }

        [HttpGet("{slug}")]
        public async Task<IActionResult> GetAll(Guid slug)
        {
            var region = await _regionRepo.GetBySlugAsync(slug);

            if (region == null)
            {
                return NotFound();
            }

            return Ok(region.ToRegionDto());
        }

        [HttpPut("{slug}")]
        public async Task<IActionResult> GetBySlug([FromRoute] Guid slug, [FromBody] UpdateRegionDto regionDto)
        {

            var regionModel = await _regionRepo.UpdateAsync(slug, regionDto);

            if (regionModel == null)
            {
                return NotFound();
            }
            return Ok(regionModel.ToRegionDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateRegionDto regionDto)
        {
            var regionModel = regionDto.ToRegionFromCreateDto();
            await _regionRepo.CreateAsync(regionModel);
            return CreatedAtAction(nameof(GetBySlug), new { slug = regionModel.Slug }, regionModel.ToRegionDto());
        }

        [HttpDelete("{slug}")]
        public async Task<IActionResult> Delete([FromRoute] Guid slug)
        {
            var regionModel = await _regionRepo.DeleteAsync(slug);
            if (regionModel == null)
            {
                return NotFound();
            }

            return NoContent();
        }

    }
}