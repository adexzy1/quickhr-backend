using qwikhr.Mappers;
using Microsoft.AspNetCore.Mvc;
using qwikhr.Dtos.Region;
using qwikhr.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace qwikhr.Controllers
{
    [Route("api/regions")]
    [ApiController]
    public class RegionController(IRegionRepository regionRepository, ILogger<RegionController> logger) : ControllerBase
    {
        private readonly IRegionRepository _regionRepo = regionRepository;
        private readonly ILogger<RegionController> _logger = logger;

        [HttpGet]
        public async Task<IActionResult> GetRegions()
        {
            try
            {
                var regions = await _regionRepo.GetAllAsync();
                var regionsDto = regions.Select(r => r.ToRegionDto());
                return Ok(regionsDto);
            }
            catch (Exception e)
            {
                if (e is DbUpdateException dbEx && dbEx.InnerException != null)
                {
                    _logger.LogError(dbEx.InnerException, "Database update failed: {Message}", dbEx.InnerException.Message);
                }
                else
                {
                    _logger.LogError(e, "An error occurred: {Message}", e.Message);
                }
                return StatusCode(500, new { message = "An error occurred while processing your request. Please try again later." });

            }

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var region = await _regionRepo.GetByIdAsync(id);

                if (region == null)
                {
                    return NotFound();
                }

                return Ok(region.ToRegionDto());
            }
            catch (Exception e)
            {

                if (e is DbUpdateException dbEx && dbEx.InnerException != null)
                {
                    _logger.LogError(dbEx.InnerException, "Database update failed: {Message}", dbEx.InnerException.Message);
                }
                else
                {
                    _logger.LogError(e, "An error occurred: {Message}", e.Message);
                }
                return StatusCode(500, new { message = "An error occurred while processing your request. Please try again later." });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionDto regionDto)
        {
            try
            {
                var regionModel = await _regionRepo.UpdateAsync(id, regionDto);
                if (regionModel == null)
                {
                    return NotFound(new { message = "Region does not exist" });
                }
                return Ok(regionModel.ToRegionDto());
            }
            catch (Exception e)
            {

                if (e is DbUpdateException dbEx && dbEx.InnerException != null)
                {
                    _logger.LogError(dbEx.InnerException, "Database update failed: {Message}", dbEx.InnerException.Message);
                }
                else
                {
                    _logger.LogError(e, "An error occurred: {Message}", e.Message);
                }
                return StatusCode(500, new { message = "An error occurred while processing your request. Please try again later." });
            }

        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateRegionDto regionDto)
        {
            try
            {
                var regionModel = regionDto.ToRegionFromCreateDto();
                await _regionRepo.CreateAsync(regionModel);
                return CreatedAtAction(nameof(GetById), new { Id = regionModel.Id }, regionModel.ToRegionDto());
            }
            catch (Exception e)
            {

                if (e is DbUpdateException dbEx && dbEx.InnerException != null)
                {
                    _logger.LogError(dbEx.InnerException, "Database update failed: {Message}", dbEx.InnerException.Message);
                }
                else
                {
                    _logger.LogError(e, "An error occurred: {Message}", e.Message);
                }
                return StatusCode(500, new { message = "An error occurred while processing your request. Please try again later." });
            }

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            try
            {
                var regionModel = await _regionRepo.DeleteAsync(id);
                if (regionModel == null)
                {
                    return NotFound(new { message = "Region does not exist" });
                }

                return NoContent();
            }
            catch (Exception e)
            {
                if (e is DbUpdateException dbEx && dbEx.InnerException != null)
                {
                    _logger.LogError(dbEx.InnerException, "Database update failed: {Message}", dbEx.InnerException.Message);
                }
                else
                {
                    _logger.LogError(e, "An error occurred: {Message}", e.Message);
                }
                return StatusCode(500, new { message = "An error occurred while processing your request. Please try again later." });
            }


        }

    }
}